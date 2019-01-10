using OpenTK;
using OpenTK.Extra;
using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace Dataset3D
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        bool loaded = false;

        Cube cube; //параллепипед
        AxesGraphics axes; //оси + сетка координат

        ObjectCreator oc;
        //ObjMesh obj;

        Bitmap b;

        Textures texs;

        //ИНИЦИАЛИЗАЦИЯ
        private void control3D1_Load(object sender, EventArgs e)
        {
            bt_reset_Click(null, null);
        }

        float nearPlane = 2;
        float farPlane = 5000;
        float fovRadians;
        float aspect;

        float camDist = 700;

        float wPlane;
        float hPlane;

        private void InitCamera()
        {
            Matrix4 p = Matrix4.CreatePerspectiveFieldOfView(
                fovRadians=(float)(80 * Math.PI / 180), 
                aspect=control3D1.Width/(float)control3D1.Height, 
                nearPlane, farPlane);
            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadMatrix(ref p);
            GL.PushMatrix();

            Matrix4 modelview = Matrix4.LookAt
                (camDist, 0, 0, 0, 0, 0, 0, 0, 1);

            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadMatrix(ref modelview);
            GL.PushMatrix();
        }

        int t=0;
        HiPerfTimer hpt = new HiPerfTimer();
        float smooth_dt = 0;
        //ОТРИСОВКА ПО ТАЙМЕРУ
        private void timer1_Tick(object sender, EventArgs e)
        {
            //return;
            if (!loaded) return;
            if (cb_pause.Checked) return;
            if (t >= nud_max_images.Value) return;

            hpt.Start();

            label1.Text = "" + t;

            GL.PushMatrix();

            float sz_real = 400;

            var infos = new List<ObjectRegion>();

            var lst = GetOpbjectsPositions();

            infos = DrawAllObjects(t, sz_real, lst, DrawMode.Normal);

            string filename, annot_ext;
            var annot=GetAnnotation(infos, out filename, out annot_ext);

            GL.PopMatrix();

            if (cb_save.Checked && t >= 0)
            {
                b = control3D1.ToBitmap();
            
                Helper.CheckCreateDir(tb_folder.Text);
                b.Save(tb_folder.Text + "\\" + filename, System.Drawing.Imaging.ImageFormat.Jpeg);

                if (cb_depth.Checked)
                {
                    //правая половинка стереоизображения
                    GL.PushMatrix();
                    GL.Translate(0, -60, 0);
                    infos = DrawAllObjects(t, sz_real, lst, DrawMode.Normal);
                    b = control3D1.ToBitmap();
                    
                    var b2 = DrawDepth();
                    var folder2 = tb_folder.Text + "\\depth";
                    Helper.CheckCreateDir(folder2);
                    b2.Save(folder2 + "\\" + filename, System.Drawing.Imaging.ImageFormat.Jpeg);

                    int ind = filename.LastIndexOf(".");
                    var ext = filename.Substring(ind);
                    var filename3 = filename.Substring(0, ind) + "-right" + ext;
                    var folder3 = tb_folder.Text + "\\stereo";
                    Helper.CheckCreateDir(folder3);
                    b.Save(folder3 + "\\" + filename3, System.Drawing.Imaging.ImageFormat.Jpeg);
                    GL.PopMatrix();
                }
                if (cb_segm.Checked)
                {
                    //control3D1.SwapBuffers();

                    DrawAllObjects(t, sz_real, lst, DrawMode.Segmentation);
                    //control3D1.SwapBuffers();

                    var b3 = control3D1.ToBitmap();
                    var folder3 = tb_folder.Text + "\\segm";
                    Helper.CheckCreateDir(folder3);
                    b3.Save(folder3 + "\\" + filename, System.Drawing.Imaging.ImageFormat.Jpeg);
                    //control3D1.SwapBuffers();
                    }

                File.WriteAllText(tb_folder.Text + "\\" + t + annot_ext, annot);
            }

            control3D1.SwapBuffers();
            var dt=hpt.Stop();
            smooth_dt += 0.1f * (dt - smooth_dt);
            lb_render_time.Text = string.Format("Frame Time: {0:F4} ms", smooth_dt*1000);
            t++;
        }

        private List<float[]> GetOpbjectsPositions()
        {
            float A = 600;

            var lst = Helper.GetRandomPoints((int)nud_nobj.Value,
                new float[] { -A, -A, -0.8f * A },
                new float[] { 0, A, 0.8f * A },
                new[] { false, true, true }, 1);

            Helper.TransformPoints(lst, pt =>
            {
                var k = Math.Abs(pt[0] / A);
                var k2 = 0.35f;
                var k3 = (1 - k2 + 2 * k2 * k);
                return new float[] { pt[0], pt[1] * k3, pt[2] * k3 };
            });
            return lst;
        }

        private string GetAnnotation(List<ObjectRegion> infos, out string filename, out string ext)
        {
            filename = t + ".jpg";
            ext = "";
            string result = "";
            if (rb_xml.Checked)
            {
                var annot = new Annotation()
                {
                    filename = filename,
                    folder = tb_folder.Text,
                    img_depth = 3,
                    img_width = control3D1.ImageWidth,
                    img_height = control3D1.ImageHeight,
                    objects = infos
                };
                rtb_regions.Text = result = annot.ToXml();
                ext = ".xml";
            }
            else
            {
                var s = string.Join("\r\n",
                    infos.ConvertAll(x =>
                    x.GetYOLORegion(Width, Height)));

                rtb_regions.Text = result = s;
                ext = ".txt";
            }
            return result;
        }

        List<ObjectRegion> DrawAllObjects(int iteration, float sz_real,
            List<float[]> lst, DrawMode dm)
        {
            var result = new List<ObjectRegion>();

            //синхронизация цветов фона
            oc.ResetRandom(iteration);
            oc.RandomizeBackgroundColor();


            //GL.ClearColor(Color.Blue);

            if (dm == DrawMode.Normal)
            {
                Control3D.ClearColor(oc.color_bg);

                if (oc.rnd.NextDouble() < (double)nud_ptex.Value)
                {
                    DrawBackground();
                }
            }
            else if (dm == DrawMode.Segmentation)
            {
                GL.Disable(EnableCap.Texture2D);

                Control3D.ClearColor(Color.Black);
            }

            if (cb_draw_axes.Checked)
            {
                GL.Disable(EnableCap.Lighting);
                axes.Draw(true, true, false);
            }

            //синхронизация типов объектов
            oc.ResetRandom(iteration);

            for (int i = 0; i < lst.Count; i++)
            {
                //отрисовка объектов
                oc.RandomizeForegroundColor();
                var obj = DrawObj(oc, sz_real, lst, i, dm);
                result.Add(obj);
            }
            //control3D1.SwapBuffers();
            return result;
        }

        private ObjectRegion DrawObj(ObjectCreator oc, float sz_real, List<float[]> points, int ind, 
            DrawMode dm)
        {
            var p=oc.RandomizePose(points[ind]);

            oc.Draw(dm);

            if (cb_draw_bb.Checked)
            {
                var sz_px = sz_real * oc.k_3d_to_px;// (float)Math.Pow(oc.k_3d_to_px, 1.1);
                DrawRect(oc.center.X, oc.center.Y, sz_px, sz_px);
            }

            return oc.GetObjectRegion(sz_real);
        }

        public void DrawBackground()
        {
            GL.PushMatrix();
            //GL.LoadIdentity();
            GL.Translate(-farPlane+camDist+100, 0, 0);
            //GL.Rotate(90, 0, 0, 1);
            GL.Rotate(90, 0, 1, 0);
            GL.Rotate(90, 0, 0, 1);
            Plane.DrawTexture(texs.GetRandomTexture(), wPlane, hPlane);
            GL.PopMatrix();
        }

        void DrawRect(float xc, float yc, float w, float h)
        {
            GL.Disable(EnableCap.Lighting);
            GL.Color3(Color.Red);
            int[] viewport = new int[4];
            GL.GetInteger(GetPName.Viewport, viewport);

            xc = 2 * xc / viewport[2] - 1;
            w = 2 * w / viewport[2];
            yc = 2 * yc/viewport[3] - 1;
            h = 2 * h/viewport[3];

            yc *= -1;

            GL.MatrixMode(MatrixMode.Modelview); GL.PushMatrix(); GL.LoadIdentity();
            GL.MatrixMode(MatrixMode.Projection); GL.PushMatrix(); GL.LoadIdentity();
            GL.Begin(PrimitiveType.LineLoop);

            GL.Vertex3(xc - w / 2, yc - h / 2, -1);
            GL.Vertex3(xc + w / 2, yc - h / 2, -1);
            GL.Vertex3(xc + w / 2, yc + h / 2, -1);
            GL.Vertex3(xc - w / 2, yc + h / 2, -1);

            GL.End();
            GL.PopMatrix();
            GL.MatrixMode(MatrixMode.Modelview); GL.PopMatrix();
            GL.Enable(EnableCap.Lighting);
        }

        private void nud_tint_ValueChanged(object sender, EventArgs e)
        {
            timer1.Interval = (int)nud_tint.Value;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            nud_tint_ValueChanged(null, null);
        }

        Bitmap lastDepthMap;
        public Bitmap DrawDepth()
        {
            int w = control3D1.ImageWidth, h = control3D1.ImageHeight;
            int L = w * h;
            var depth_arr = new float[L];
            //IntPtr unmanagedPointer = Marshal.AllocHGlobal(depth_arr.Length);

            GL.ReadPixels(0, 0, w, h, PixelFormat.DepthComponent, PixelType.Float, depth_arr);

            for (int i = 0; i < L; i++)
                depth_arr[i] = FixDepth(depth_arr[i], nearPlane, farPlane)/farPlane;

            lastDepthMap = Helper.BitmapFromFloatsGrayscale(depth_arr, w, h);
            lastDepthMap.RotateFlip(RotateFlipType.RotateNoneFlipY);

            return lastDepthMap;
        }

        public Bitmap DrawSegmentation()
        {
            int w = control3D1.ImageWidth, h = control3D1.ImageHeight;
            int L = w * h;
            var depth_arr = new float[L];

            GL.ReadPixels(0, 0, w, h, PixelFormat.DepthComponent, PixelType.Float, depth_arr);

            for (int i = 0; i < L; i++)
                depth_arr[i] = FixDepth(depth_arr[i], nearPlane, farPlane) / farPlane;

            lastDepthMap = Helper.BitmapFromFloatsGrayscale(depth_arr, w, h);
            lastDepthMap.RotateFlip(RotateFlipType.RotateNoneFlipY);

            return lastDepthMap;
        }

        public float FixDepth(float depthSample, float min_dist, float max_dist)
        {
            depthSample = 2.0f * depthSample - 1.0f;
            var D1 = max_dist + min_dist;
            var D2 = max_dist - min_dist;
            var zLinear = 2.0f * min_dist * max_dist / (D1 - depthSample * D2);
            return zLinear;
        }

        private void bt_open_folder_Click(object sender, EventArgs e)
        {
            Process.Start(@"c:\windows\explorer.exe", tb_folder.Text);
        }

        private void bt_reset_Click(object sender, EventArgs e)
        {
            loaded = true;

            cube = new Cube(500, 100, 100);
            axes = new AxesGraphics();
            //obj = ObjMesh.LoadFromFile("meshes/teapot.obj");
            b = new Bitmap(control3D1.Width, control3D1.Height);

            oc = new ObjectCreator("meshes/", b.Width, b.Height);
            oc.RandomizePose(null);

            InitCamera();
            GL.Enable(EnableCap.Blend);

            var fov2 = fovRadians / 2;
            // L/2 / far = tn(fov2)
            // L=2*far*tn(fov2)
            wPlane = 0.98f * (float)(2 * farPlane * Math.Tan(fov2) * aspect);
            hPlane = wPlane / aspect;

            texs = new Textures("background");

            t = 0;
            smooth_dt = 0;
            cb_pause.Checked = true;
        }

        private void cb_hide_CheckedChanged(object sender, EventArgs e)
        {
            if(cb_hide.Checked) control3D1.Hide();
            else control3D1.Show();
        }

        World world;
        private void bt_load_world_Click(object sender, EventArgs e)
        {
            var arr = tb_shift.Text.Split(' ');
            var arr2 = Array.ConvertAll(arr, a => float.Parse(a));

            world = new World("scenes/forest1.txt");

            InitCamera();

            GL.PushMatrix();
            GL.Translate(arr2[0], arr2[1], arr2[2]);
            GL.Scale(1000, 1000, 1000);
            world.Draw();
            control3D1.SwapBuffers();

            GL.PopMatrix();

        }
    }
}
