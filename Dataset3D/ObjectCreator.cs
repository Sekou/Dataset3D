using Newtonsoft.Json;
using OpenTK;
using OpenTK.Extra;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using static Dataset3D.Form1;

namespace Dataset3D
{
    public class FileManager
    {
        public Dictionary<string, ObjMesh> objects = new Dictionary<string, ObjMesh>();
        public List<string> obj_filenames; //filenames (keys) are unique
        public List<string> obj_labels; //some labels may be the same for different objects
        public Dictionary<string, int> label_ids = new Dictionary<string, int>(); //e.g. "cube1"=0, "cube2"=0, "sphere_big"=1, "sphere_small"=1, ...
        public ObjectSettings os;

        public FileManager(string folder)
        {
            var dir = new DirectoryInfo(folder);
            var files = dir.GetFiles("*", SearchOption.TopDirectoryOnly);

            string ext = ".obj";
            foreach (var f in files)
            {
                if (f.FullName.EndsWith(ext))
                {
                    var obj = ObjMesh.LoadFromFile(f.FullName);
                    obj.Name = f.Name.Replace(".obj", "");

                    var arr = new[] { ".png", ".jpg" };
                    foreach (var ext2 in arr)
                    {
                        var texpath = Helper.CorrectPath(
                        f.Directory.FullName + "/textures/" + obj.Name + ext2);
                        if (File.Exists(texpath))
                        {
                            obj.TextureID = OpenTK.Extra.Helper.LoadTexture(texpath);
                            obj.IsTextured = true;
                            break;
                        }
                    }
                    objects[f.Name] = obj;
                }
            }

            var fi = new FileInfo(folder + "/transform.json");

            if (!fi.Exists)
            {
                os = new ObjectSettings();
                var s = JsonConvert.SerializeObject(os);
                File.WriteAllText(fi.FullName, s);
            }
            else
            {
                var s = File.ReadAllText(fi.FullName);
                os = JsonConvert.DeserializeObject<ObjectSettings>(s);
            }

            var defaultline = os.GetDefaultLine();
            foreach (var o in objects.Values)
            {
                ApplyLineToObj(defaultline, o);
            }
            foreach (var line in os.lines)
            {
                var name = line.name;
                if (!name.EndsWith(ext) && name != "default") name += ext;
                if (objects.ContainsKey(name))
                {
                    ApplyLineToObj(line, objects[name]);
                }
            }

            obj_filenames = objects.Keys.ToList();
            obj_labels = new List<string>();

            int label_id = 0;

            foreach (var filename in obj_filenames)
            {
                var o = objects[filename];
                if (!obj_labels.Contains(o.Info))
                {
                    obj_labels.Add(o.Info);
                    if (!label_ids.ContainsKey(o.Info))
                        label_ids[o.Info] = label_id++;
                }
            }
        }

        public void ApplyLineToObj(ObjLine l, ObjMesh m)
        {
            if (!string.IsNullOrWhiteSpace(l.label))
                m.Info = l.label;
            else m.Info = m.Name;

            m.UserObject = l;
            m.Scale = new Vector3(l.scale, l.scale, l.scale);
            var kpi = (float)Math.PI / 180;
            m.Rotation = new Vector3(l.rot[0] * kpi, l.rot[1] * kpi, l.rot[2] * kpi);
            m.Position = new Vector3(l.pos[0], l.pos[1], l.pos[2]);
        }
    }


    public class ObjectCreator
    {
        public FileManager fileManager;
        public Textures backgrounds;
        int W, H;

        public ObjectCreator(string folder, string backgr_folder, int W, int H)
        {
            this.W = W;
            this.H = H;

            fileManager = new FileManager(folder);

            backgrounds = new Textures(backgr_folder);

        }
       

        public Random rnd = new Random(0);
        public int RNDC{get{return 50 + rnd.Next(206);}}

        public float r() { return (float)rnd.NextDouble(); }
        public float r2() { return 2 * (float)rnd.NextDouble() - 1; }
        public void ResetRandom(int seed)
        {
            rnd = new Random(seed);
        }
        
        #region Point Generation
        public List<float[]> GetObjectsPositions(int num_objs)
        {
            float A = 600;

            var lst = GetRandomPoints(num_objs,
                new float[] { -2*A, -A, -0.8f * A },
                new float[] { 0, A, 0.8f * A },
                new[] { false, true, true }, 1);

            TransformPoints(lst, pt =>
            {
                var k = Math.Abs(pt[0] / A);
                var k2 = 0.35f;
                var k3 = (1 - k2 + 2 * k2 * k);
                return new float[] { pt[0], pt[1] * k3, pt[2] * k3 };
            });
            return lst;
        }

        public List<float[]> GetRandomPoints(int N,
          float[] min, float[] max, bool[] active, float k)
        {
            var points = new List<float[]>();
            for (int i = 0; i < N; i++)
                points.Add(getrnd(min, max));

            for (int i = 0; i < points.Count; i++)
            {
                for (int j = i + 1; j < points.Count; j++)
                {
                    float[] a = points[i], b = points[j];
                    for (int m = 0; m < active.Length; m++)
                    {
                        if (active[m])
                        {
                            var d = Math.Max(1, a[m] - b[m]);
                            var A = (max[m] - min[m]);
                            var f = k * A / d / points.Count;
                            a[m] += f;
                            b[m] -= f;
                        }
                    }
                }
            }

            return points;
        }

        float getrnd(float min, float max)
        {
            return min + (float)rnd.NextDouble() * (max - min);
        }
        float[] getrnd(float[] min, float[] max)
        {
            var res = new float[min.Length];
            for (int i = 0; i < res.Length; i++)
                res[i] = getrnd(min[i], max[i]);
            return res;
        }

        public static void TransformPoints(List<float[]> points, Func<float[], float[]> T)
        {
            for (int i = 0; i < points.Count; i++)
            {
                points[i] = T(points[i]);
            }
        } 
        #endregion

        public World GetWorld(int iteration, int num_objs, ViewportParams vp)
        {
            ResetRandom(iteration);

            var world = new World();
            var lst = GetObjectsPositions(num_objs);

            world.P.light_pos = new[] { r() * 500, r2() * 500, r2() * 500 };
            world.plane = new Plane(backgrounds.GetRandomTexture(rnd), vp.wFarPlane, vp.hFarPlane);
            world.iteration = iteration;

            for (int i = 0; i < lst.Count; i++)
            {
                var P = lst[i];

                //ROTATION AND TRANSLATION
                var axis = new Vector3(r2(), r2(), r2());
                var q = Quaternion.FromAxisAngle(axis, (float)Math.PI * r2());
                q.Normalize();

                var oi = new ObjItem();
                oi.RotTransform = Matrix4.CreateFromQuaternion(q);
                oi.ShiftTransform = Matrix4.CreateTranslation(P[0], P[1], P[2]);

                //MESH AND LABELS
                oi.type = rnd.Next(fileManager.objects.Count);
                oi.filename = fileManager.obj_filenames[oi.type];

                var o = fileManager.objects[oi.filename];
                oi.mesh = o;

                oi.label_id = fileManager.label_ids[oi.label];

                int color_type = fileManager.obj_labels.IndexOf(o.Info);

                oi.segm_color = Helper.GetColorById(color_type);
                oi.photo_color = Color.FromArgb(255, RNDC, RNDC, RNDC);

                world.obj_items.Add(oi);
            }

            return world;

        }


        


    }
}
