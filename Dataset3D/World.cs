using OpenTK;
using OpenTK.Extra;
using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using static Dataset3D.Form1;

namespace Dataset3D
{
    //A single item of a transformed 3d-model
    public class ObjItem
    {
        public Matrix4 ShiftTransform;
        public Matrix4 RotTransform = Matrix4.Identity;
        public Matrix4 RotTransform2 = Matrix4.Identity; //modified rotation during dataset generation
        public float InPlaceScale=1;
        public ObjMesh mesh;

#warning group into class MaterialInfo
        public Color photo_color;
        public Color segm_color;

        //temporary params of the object related to its position in a frame
        public ObjItemFrameInfo frame_info=new ObjItemFrameInfo();

        public int type { get; set; } //e.g. 111 or 112
        public string filename{ get; set; } //e.g. box_a.obj or box_b.obj
        public string name { get { return mesh.Name; } }//e.g. box_a or box_b
        public string label { get { return mesh.Info; } }//e.g. box in both cases
        public int label_id { get; set; } //e.g. 1 in both cases
        public bool no_frame { get; set; } //doesn't require classification
        public bool draw_always { get; set; } //draw even if center goes out of camera FOV
        public float kframe { get; set; } //frame size multiplier
        public float krot { get; set; } //maximum rotation axis deviation from vertical
        public float k_sz_xy { get; set; } //ratio of xy size to maximum object size (for trees obstacle calculation)

        public ObjItem() {
        }
        public ObjItem(string line, FileManager fileManager, World world)
        {
            var arr = line.Split(' ');
            var name = "scenes" + Path.DirectorySeparatorChar + arr.Last();

            var lst = new List<string>(arr);
            lst.RemoveRange(lst.Count - 2, 2);
            var P = lst.ConvertAll(x => float.Parse(x, CultureInfo.InvariantCulture));

            var IC = CultureInfo.InvariantCulture;
            var kpi = (float)(Math.PI / 180);

            RotTransform = //Matrix4.CreateScale(P[6])*
                Matrix4.CreateRotationX(P[3] * kpi)
                * Matrix4.CreateRotationY(P[4] * kpi)
                * Matrix4.CreateRotationZ(P[5] * kpi);

            InPlaceScale = P[6];

#warning An alterative is to use GL.Scale() during drawing but it leads to incorrect lighting

            ShiftTransform = Matrix4.CreateTranslation(
                P[0] * world.P.GlobalScale, P[1] * world.P.GlobalScale, P[2] * world.P.GlobalScale);

            mesh = fileManager.objects[arr[arr.Length-2]];
            photo_color = ColorTranslator.FromHtml(arr[arr.Length - 1]);

#warning copy all other transform.json fields from mesh into here as in ObjCreator line 230
            var OL = (ObjLine)mesh.UserObject;
            no_frame = OL.no_frame;
            draw_always = OL.draw_always;
            kframe = OL.kframe;
            krot = OL.krot;
            k_sz_xy = OL.k_sz_xy;
        }

        public Matrix4 GetTransform(WorldParams P)
        {
            Matrix4 res;
            if (P != null)
                res = P.GlobalShiftTransform;
            else res = Matrix4.Identity;

            res = ShiftTransform * res;
            res = RotTransform2 * RotTransform * res;

            if (P != null)
                res = P.GlobalRotTransform * res;

            return res;
        }
        public void Draw(DrawMode dm, WorldParams P)
        {
            GL.PushMatrix();

            var T = GetTransform(P);
            GL.MultMatrix(ref T);

            //if (P!=null) GL.MultMatrix(ref P.GlobalShiftTransform);
            //GL.MultMatrix(ref ShiftTransform);
            //GL.MultMatrix(ref RotTransform);
            //if (P != null) GL.MultMatrix(ref P.GlobalRotTransform);

            var color = photo_color;
            if (dm == DrawMode.Segmentation) color = segm_color;

            SelectMaterial(mesh.IsTextured, color, dm);

            var AllScale = InPlaceScale * P.GlobalScale;

            bool needscale = AllScale != mesh.Scale[0];
            if (needscale)
            {
#warning not thread safe, frequent data manipulation

                mesh.Scale =new Vector3(AllScale, AllScale, AllScale);
                mesh.RecalculateGeomParams();
            }

            mesh.Draw(disable_tex: dm != DrawMode.Normal);
            

            if (!frame_info.no_frame)
            { 
                //frame_info = new ObjItemFrameInfo();

                var p = mesh.Center;

                frame_info.center = OpenTK.Extra.Helper.from3Dto2D(Matrix4.Zero, Matrix4.Zero,
                    null, p, out frame_info.k_3d_to_px);

                frame_info.k_3d_to_px *= kframe;
                frame_info.region = GetObjectRegion(P, frame_info);
            }

            GL.PopMatrix();
        }

        public static void SelectMaterial(bool IsTextured, Color c, DrawMode dm)
        {
            GL.Color3(Color.White);

            float[] mat_specular = new float[] { 0, 0, 0, 0 };
            float[] mat_shininess = new float[] { 0 };
            float[] mat_diffuse = new float[] { 1, 1, 1, 1 };

            if (IsTextured == false || dm == DrawMode.Segmentation)
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

        public ObjectRegion GetObjectRegion(WorldParams P, ObjItemFrameInfo frame_info)
        {
            int[] viewport = new int[4];
            GL.GetInteger(GetPName.Viewport, viewport);

            var or = new ObjectRegion();

            //name = Regex.Replace(name, @"^[\d]+[_\-]", "");
            or.name = "" + label;

            float width3d = mesh.Radius * 2;

            var kx = width3d * frame_info.k_3d_to_px;
            var ky = width3d * frame_info.k_3d_to_px;

            or.label_id = label_id;          //i
            or.xmin = (frame_info.  center.X - kx / 2);
            or.ymin = (frame_info.center.Y - ky / 2);
            or.xmax = (frame_info.center.X + kx / 2);
            or.ymax = (frame_info.center.Y + ky / 2);

            return or;
        }
    }

    //Class to keep info about object screen position
    public class ObjItemFrameInfo
    {
        public Vector2 center; //object center on a screen in pixels
        public float k_3d_to_px; //3d world units to pixels ratio
        public ObjectRegion region; //rectangle region on screen
        public float dx, dy, distance;//distance from object to camera
        public bool no_frame;//hides frame
    }

        //Global params for all objects in a file
    public class WorldParams
    {
        public float GlobalScale = 1;
        public Matrix4 GlobalShiftTransform = Matrix4.Identity;
        public Matrix4 GlobalRotTransform = Matrix4.Identity;
        public float[] light_pos;
        public Color background_color;

        public WorldParams() { }
        public WorldParams(List<string> lines)
        {
            var dict = new Dictionary<string, string>();
            for (int i = 0; i < lines.Count; i++)
            {
                var arr = lines[i].Split(':');
                dict[arr[0].Trim()] = arr[1].Trim();
            }
            var IC = CultureInfo.InvariantCulture;

            float k = 1;
            if (dict["units"] == "m") k = 1000;
            GlobalScale = float.Parse(dict["scale_default"], IC)*k;

            var R = Array.ConvertAll(dict["rotate_default"].Split(' '),
                x=>float.Parse(x, IC));
            var T = Array.ConvertAll(dict["shift_default"].Split(' '),
                x => float.Parse(x, IC));

            var kpi = (float)(Math.PI / 180);
            GlobalShiftTransform = Matrix4.CreateTranslation(T[0] * GlobalScale, T[1] * GlobalScale, T[2] * GlobalScale) ;
            GlobalRotTransform =
                 Matrix4.CreateRotationX(R[0] * kpi)
                * Matrix4.CreateRotationY(R[1] * kpi)
                * Matrix4.CreateRotationZ(R[2] * kpi);

            light_pos = Array.ConvertAll(dict["light_pos"].Split(' '),
                x => float.Parse(x, IC)*k);

            background_color = ColorTranslator.FromHtml(dict["background_color"]);
        }
    }

    //2019-01-10
    //A model of a 3d-scene generated based on a simple .txt file
    public class World
    {
        public WorldParams P;

        public Color backgroung_color;

#warning make also as ObjectItem of Plane subtype
        public Plane plane;
        public int iteration;

        public List<ObjItem> obj_items = new List<ObjItem>();

        public World() { P = new WorldParams(); }
        public World(string file, FileManager fileManager)
        {
            var lines = File.ReadAllLines(file);

            var param_lines = new List<string>();
            for (int i = 0; i < lines.Length; i++)
            {
                if (lines[i].Contains(':'))
                    param_lines.Add(lines[i]);
                else
                {
                    if (P == null) P = new WorldParams(param_lines);
                    var obj_item = new ObjItem(lines[i], fileManager, this);
                    obj_items.Add(obj_item);
                    backgroung_color = P.background_color;
                }
            }
        }

#warning test
        public void Draw(DrawMode dm, CamParams cp, float maxDistFromCam, bool obstacles, Action<ObjItem> onObjDraw)
        {
            if (dm == DrawMode.Normal)
            {
                InitLight(P.light_pos);
            }
            else if (dm == DrawMode.Segmentation)
            {
                GL.Disable(EnableCap.Light0);
                GL.Enable(EnableCap.Lighting);
            }

            GL.PushMatrix();

            var obj_items2 = FilterObjectsInFOV(cp);

            for (int i = 0; i < obj_items2.Count; i++)
            {
                var oi = obj_items2[i];

                if (dm == DrawMode.Normal)
                {
                    GL.Enable(EnableCap.Light0);
                    GL.Enable(EnableCap.Lighting);
                }
                else if (dm == DrawMode.Segmentation)
                {
                    GL.Disable(EnableCap.Light0);
                    GL.Enable(EnableCap.Lighting);
                }

                oi.frame_info.no_frame = false;
                oi.frame_info.no_frame |= oi.no_frame;
                oi.frame_info.no_frame |= oi.frame_info.distance > maxDistFromCam;

                if (obstacles && !oi.frame_info.no_frame)
                    oi.frame_info.no_frame |= !FullyVisibleXY(oi, obj_items2);

                oi.Draw(dm, P);

                if (onObjDraw != null)
                {
                    onObjDraw(oi);
                }
            }

            GL.PopMatrix();
        }

        public bool FullyVisibleXY(ObjItem oi, List<ObjItem> others)
        {
            for (int i = 0; i < others.Count; i++)
            {
                var oi2 = others[i];
                if (oi2 == oi ||
#warning maybe use another flag to check for ground plane
                    oi2.draw_always) 
                    continue;
                if (oi.frame_info.distance < oi2.frame_info.distance)
                    continue;

                var AllScale = P.GlobalScale * oi.InPlaceScale;
                var sz = oi2.mesh.Radius * AllScale*oi2.k_sz_xy;

                var ang_w_obst = Math.Atan((sz / 2) / oi2.frame_info.distance);
                var ang_obst = Math.Atan(oi2.frame_info.dy / oi2.frame_info.dx);
                var ang_obj= Math.Atan(oi.frame_info.dy / oi.frame_info.dx);

                var da = ang_obst - ang_obj;

                if (Math.Abs(da) < ang_w_obst)
                    return false;
            }
            return true;
        }

        //returns a list of visible objects
        public List<ObjItem> FilterObjectsInFOV(CamParams cp)
        {
            var res = new List<ObjItem>();

            for (int i = 0; i < obj_items.Count; i++)
            {
                var oi = obj_items[i];

                var T = oi.GetTransform(P);

                float dx = 0, dy = 0, d = 0;
                if (!oi.draw_always)
                {
                    dx = T.M41 - cp.pCenter.X;
                    dy = T.M42 - cp.pCenter.Y;

                    d = (float)Math.Sqrt(dx * dx + dy * dy);

                    if (d > cp.farPlane)
                    {
                        oi.frame_info.no_frame = true;
                        continue;
                    }

                    float ang2 = (float)Math.Atan2(dy, dx);
                    var da = ang2 - cp.camYaw();
                    Helper.NormalizeAng(ref da);

                    if (Math.Abs(da) > cp.fovRadians())
                    {
                        oi.frame_info.no_frame = true;
                        continue;
                    }
                }

                oi.frame_info.dx = dx;
                oi.frame_info.dy = dy;
                oi.frame_info.distance = d;

                res.Add(oi);
            }
            return res;
        }

        private static void InitLight(float[] pos)
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
    }
}
