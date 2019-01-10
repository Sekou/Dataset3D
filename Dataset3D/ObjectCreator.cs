using Newtonsoft.Json;
using OpenTK;
using OpenTK.Extra;
using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;

namespace Dataset3D
{
    public class ObjectCreator
    {
        public Dictionary<string, ObjMesh> objects = new Dictionary<string, ObjMesh>();
        List<string> obj_keys; //keys are unique
        List<string> obj_labels; //some labels may be the same for different objects
        Dictionary<string, int> label_ids=new Dictionary<string, int>(); //e.g. "cube1"=0, "cube2"=0, "sphere_big"=1, "sphere_small"=1, ...
        public ObjectSettings os;

        int W, H;

        public ObjectCreator(string folder, int W, int H)
        {
            this.W = W;
            this.H = H;

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

            var fi = new FileInfo(folder + "transform.json");

            if (!fi.Exists)
            {
                os = new ObjectSettings();
                var s = JsonConvert.SerializeObject(os);
                File.WriteAllText(fi.FullName, s);
            }
            else {
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
                if (!name.EndsWith(ext) && name!="default") name += ext;
                if (objects.ContainsKey(name))
                {
                    ApplyLineToObj(line, objects[name]);
                }
            }

            obj_keys = objects.Keys.ToList();
            obj_labels = new List<string>();

            int label_id = 0;

            foreach (var key in obj_keys)
            {
                var o = objects[key];
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

        public Color color_bg;
        public Color color_fg;
        public Vector3 pos;
        public Vector3 pos_light;
        public Quaternion q;
        public int type;

        public Random rnd = new Random(0);
        int RNDC{get{return 50 + rnd.Next(206);}}

        public float r() { return (float)rnd.NextDouble(); }
        public float r2() { return 2 * (float)rnd.NextDouble() - 1; }
        public void ResetRandom(int seed)
        {
            rnd = new Random(seed);
        }

        public Vector3 RandomizePose(float[] P)
        {
            type = rnd.Next(objects.Count);

            if (P != null) pos = new Vector3(P[0], P[1], P[2]);
            else
            {
                var xrnd = -r() * 2000;
                var A = Math.Abs(xrnd * 1.1f);
                pos = new Vector3(xrnd, r2() * A, r2() * A);
            }

            pos_light = new Vector3(r(), r2(), r2()) * 500;

            var axis = new Vector3(r2(), r2(), r2());
            q = Quaternion.FromAxisAngle(axis, (float)Math.PI * r2());
            q.Normalize();

            return pos;
        }

        public void RandomizeBackgroundColor()
        {
            color_bg = Color.FromArgb(255, RNDC, RNDC, RNDC);
        }
        public void RandomizeForegroundColor()
        {
            color_fg = Color.FromArgb(255, RNDC, RNDC, RNDC);
        }

        private static void InitLight(Vector3 pos)
        {
            float[] light_position = new float[] { pos[0], pos[1], pos[2], 0 };
            float[] light_ambient = { 0.2f, 0.2f, 0.2f, 1.0f };
            float[] light_diffuse = { 0.8f, 0.8f, 0.8f, 1.0f };
            float[] light_specular = { 1.0f, 1.0f, 1.0f, 1.0f };

            GL.Light(LightName.Light0, LightParameter.Position, light_position);
            GL.Light(LightName.Light0, LightParameter.Ambient, light_ambient);
            GL.Light(LightName.Light0, LightParameter.Diffuse, light_diffuse);
            GL.Light(LightName.Light0, LightParameter.Specular, light_specular);

            GL.Enable(EnableCap.Lighting);
            GL.Enable(EnableCap.Light0);
            GL.Enable(EnableCap.DepthTest);
            GL.Enable(EnableCap.CullFace);
        }
        private void SelectMaterial(ObjMesh obj, Color c, DrawMode dm)
        {
            float[] mat_specular = new float[] { 0, 0, 0, 0 };
            float[] mat_shininess = new float[] { 0 };
            float[] mat_diffuse = new float[] { 1, 1, 1, 1 };

            if (obj.IsTextured == false || dm==DrawMode.Segmentation)
                mat_diffuse = new float[] { c.R / 255f, c.G / 255f, c.B / 255f, 1 };


            float[] mat_ambient = new float[]
                { mat_diffuse[0]/2, mat_diffuse[1] / 2, mat_diffuse[2] / 2, 1 };

            //float[] mat_ambient = new float[]
            //   {0, 0, 0, 0 };

            GL.ShadeModel(ShadingModel.Smooth);

            GL.Material(MaterialFace.Front, MaterialParameter.Specular, mat_specular);
            GL.Material(MaterialFace.Front, MaterialParameter.Shininess, mat_shininess);
            GL.Material(MaterialFace.Front, MaterialParameter.Ambient, mat_ambient);
            if (dm == DrawMode.Normal)
            {
                GL.Material(MaterialFace.Front, MaterialParameter.Diffuse, mat_diffuse);
                GL.Material(MaterialFace.Front, MaterialParameter.Emission, new float[] { 0, 0, 0, 0 });
            }
            if (dm == DrawMode.Segmentation)
                GL.Material(MaterialFace.Front, MaterialParameter.Emission, mat_diffuse);
        }

        public Vector2 center; //object center on a screen in pixels
        public float k_3d_to_px; //3d world units to pixels ratio

        public void Draw(DrawMode dm)
        {
            if (dm == DrawMode.Normal)
                InitLight(pos_light);
            else
            {
#warning other lights?
                GL.Disable(EnableCap.Light0);
                GL.Disable(EnableCap.Light1);
                GL.Disable(EnableCap.Light2);

                GL.Enable(EnableCap.Lighting);
            }

            GL.PushMatrix();

            GL.Translate(pos.X, pos.Y, pos.Z);

            var v4 = q.ToAxisAngle();
            GL.Rotate(v4.W*(180/(float)Math.PI), v4.Xyz);

            var key = obj_keys[type];
            var o = objects[key];

            int color_type = obj_labels.IndexOf(o.Info);

            var c = dm == DrawMode.Segmentation ? Helper.GetColorById(color_type) : color_fg;
            SelectMaterial(o, c, dm);

            bool disable_tex = dm == DrawMode.Segmentation;
            o.Draw(disable_tex);

            center = OpenTK.Extra.Helper.from3Dto2D(Matrix4.Zero, Matrix4.Zero, null, new Vector3(), out k_3d_to_px);
            k_3d_to_px *= ((ObjLine)o.UserObject).kframe;

            GL.PopMatrix();
        }



        public ObjectRegion GetObjectRegion(float width3d)
        {
            int[] viewport = new int[4];
            GL.GetInteger(GetPName.Viewport, viewport);

            var or = new ObjectRegion();

            var key = obj_keys[type];
            var obj = objects[key];
            var label_id = label_ids[obj.Info];

            //name = Regex.Replace(name, @"^[\d]+[_\-]", "");
            or.name = ""+obj.Info;

            var kx = width3d * k_3d_to_px;
            var ky = width3d * k_3d_to_px;

            or.label_id = label_id;          //i
            or.xmin = (center.X - kx / 2);
            or.ymin = (center.Y - ky / 2);
            or.xmax = (center.X + kx / 2);
            or.ymax = (center.Y + ky / 2);

            return or;
        }
    }
}
