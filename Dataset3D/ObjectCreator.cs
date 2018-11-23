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

namespace Dataset3D
{
    public class ObjectCreator
    {
        public Dictionary<string, ObjMesh> objects = new Dictionary<string, ObjMesh>();
        List<string> obj_keys;
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
                    var arr = new[] { ".png", ".jpg" };
                    foreach(var ext2 in arr)
                    {
                        var texpath = Helper.CorrectPath(
                        f.Directory.FullName + "/textures/" +
                        f.Name.Replace(".obj", ext2));
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
        }
        public void ApplyLineToObj(ObjLine l, ObjMesh m)
        {
            m.UserObject = l;
            m.Scale = new Vector3(l.scale, l.scale, l.scale);
            var kpi = (float)Math.PI / 180;
            m.Rotation = new Vector3(l.rot[0] * kpi, l.rot[1] * kpi, l.rot[2] * kpi);
            m.Position = new Vector3(l.pos[0], l.pos[1], l.pos[2]);
        }

        public Color bg;
        public Color fg;
        public Vector3 pos;
        public Vector3 pos_light;
        public Quaternion q;
        public int type;

        int RNDC{get{return 50 + Helper.rnd.Next(206);}}

        public void Randomize()
        {
            type = Helper.rnd.Next(objects.Count);

            bg = Color.FromArgb(255, RNDC, RNDC, RNDC);
            fg = Color.FromArgb(255, RNDC, RNDC, RNDC);

            var xrnd = -Helper.r() * 2000;
            var A = Math.Abs(xrnd * 1.1f);
            pos = new Vector3(xrnd, Helper.r2() * A, Helper.r2() * A);
            pos_light = new Vector3(Helper.r(), Helper.r2(), Helper.r2()) * 500;

            var axis = new Vector3(Helper.r2(), Helper.r2(), Helper.r2());
            q = Quaternion.FromAxisAngle(axis, (float)Math.PI*Helper.r2());
            q.Normalize();
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
        private void SelectMaterial(ObjMesh obj, Color c)
        {
            float[] mat_specular = new float[] { 0, 0, 0, 0 };
            float[] mat_shininess = new float[] { 0 };
            float[] mat_diffuse = new float[] { 1, 1, 1, 1 };
            if(obj.IsTextured==false)
                mat_diffuse = new float[] { c.R / 255f, c.G / 255f, c.B / 255f, 1 };

            float[] mat_ambient = new float[] 
                { mat_diffuse[0]/2, mat_diffuse[1] / 2, mat_diffuse[2] / 2, 1 };

            //float[] mat_ambient = new float[]
            //   {0, 0, 0, 0 };

            GL.ShadeModel(ShadingModel.Smooth);

            GL.Material(MaterialFace.Front, MaterialParameter.Specular, mat_specular);
            GL.Material(MaterialFace.Front, MaterialParameter.Shininess, mat_shininess);
            GL.Material(MaterialFace.Front, MaterialParameter.Ambient, mat_ambient);
            GL.Material(MaterialFace.Front, MaterialParameter.Diffuse, mat_diffuse);
        }

        public Vector2 center; //object center on a screen in pixels
        public float k_3d_to_px; //3d world units to pixels ratio

        public void Draw()
        {
            InitLight(pos_light);
            GL.PushMatrix();

            GL.Translate(pos.X, pos.Y, pos.Z);

            var v4 = q.ToAxisAngle();
            GL.Rotate(v4.W*(180/(float)Math.PI), v4.Xyz);

            var key = obj_keys[type];
            var o = objects[key];

            SelectMaterial(o, fg);  

            o.Draw();

            center = OpenTK.Extra.Helper.from3Dto2D(Matrix4.Zero, Matrix4.Zero, null, new Vector3(), out k_3d_to_px);
            k_3d_to_px *= ((ObjLine)o.UserObject).kframe;

            GL.PopMatrix();
        }

        public ObjectRegion GetObjectRegion(float width3d)
        {
            int[] viewport = new int[4];
            GL.GetInteger(GetPName.Viewport, viewport);

            var or = new ObjectRegion();
            or.name = ""+type;

            var kx = width3d * k_3d_to_px;
            var ky = width3d * k_3d_to_px;

            or.xmin = (center.X - kx / 2);
            or.ymin = (center.Y - ky / 2);
            or.xmax = (center.X + kx / 2);
            or.ymax = (center.Y + ky / 2);

            return or;
        }
    }
}
