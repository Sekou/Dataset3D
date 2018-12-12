using OpenTK;
using OpenTK.Extra;
using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
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
            loaded = true;

            cube = new Cube(500, 100, 100);
            axes = new AxesGraphics();
            //obj = ObjMesh.LoadFromFile("meshes/teapot.obj");
            b = new Bitmap(control3D1.Width, control3D1.Height);

            oc = new ObjectCreator("meshes/", b.Width, b.Height);
            oc.Randomize(null);

            InitCamera();
            GL.Enable(EnableCap.Blend);


            var fov2 = fovRadians / 2;
            // L/2 / far = tn(fov2)
            // L=2*far*tn(fov2)
            wPlane = 0.98f*(float)(2 * farPlane * Math.Tan(fov2)*aspect);
            hPlane = wPlane / aspect;

            texs = new Textures("background");
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

        int t=-1;
        //ОТРИСОВКА ПО ТАЙМЕРУ
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (!loaded) return;
            if (cb_pause.Checked) return;
            if (t >= nud_max_images.Value) return;

            GL.Clear(ClearBufferMask.ColorBufferBit 
                | ClearBufferMask.DepthBufferBit);

            GL.ClearColor(oc.bg);

            GL.PushMatrix();

            float sz_real = 400;

            var infos = new List<ObjectRegion>();

            float A = 600;

            var lst = Helper.GetRandomPoints((int)nud_nobj.Value,
                new float[] { -A, -A, -0.8f*A },
                new float[] { 0, A, 0.8f*A }, 
                new[] { false, true, true }, 1);

            Helper.TransformPoints(lst, pt =>
            {
                var k = Math.Abs(pt[0] / A);
                var k2 = 0.35f;
                var k3 = (1 - k2 + 2 * k2 * k);
                return new float[] { pt[0], pt[1] * k3, pt[2] * k3 };
            });

            for (int i = 0; i < lst.Count; i++)
            {
                //отрисовка объектов
                var obj = DrawObj(sz_real, lst, i);
                infos.Add(obj);
            }

            string filename = t + ".jpg";
            string ext = "";
            if(rb_xml.Checked)
            {
                var annot = new Annotation()
                {
                    filename = filename,
                    folder = rtb_folder.Text,
                    img_depth = 3,
                    img_width = control3D1.ImageWidth,
                    img_height = control3D1.ImageHeight,
                    objects = infos
                };
                rtb_regions.Text = annot.ToXml();
                ext = ".xml";
            }
            else
            {
                var s = string.Join("\r\n",
                    infos.ConvertAll(x => 
                    x.GetYOLORegion(Width, Height)));

                rtb_regions.Text = s;
                ext = ".txt";
            }

            if (cb_draw_axes.Checked)
            {
                GL.Disable(EnableCap.Lighting);
                axes.Draw(true, true, false);
                GL.Enable(EnableCap.Lighting);
            }

            if (Helper.rnd.NextDouble() < (double)nud_ptex.Value)
            {
                DrawBackground();
            }

            GL.PopMatrix();

            if (cb_save.Checked && t >= 0)
            {
                b = control3D1.ToBitmap();

                Helper.CheckCreateDir(rtb_folder.Text);
                b.Save(rtb_folder.Text + "\\" + filename, System.Drawing.Imaging.ImageFormat.Jpeg);

                if (cb_depth.Checked)
                {
                    var b2 = DrawDepth();
                    var folder2 = rtb_folder.Text + "\\depth";
                    Helper.CheckCreateDir(folder2);
                    b2.Save(folder2 + "\\" + filename, System.Drawing.Imaging.ImageFormat.Jpeg);
                }

                File.WriteAllText(rtb_folder.Text + "\\" + t + ext, rtb_regions.Text);
            }

            control3D1.SwapBuffers();

            label1.Text = "" + t;

            t++;
        }

        private ObjectRegion DrawObj(float sz_real, List<float[]> points, int ind)
        {
            var p=oc.Randomize(points[ind]);

            oc.Draw();

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

        public float FixDepth(float depthSample, float min_dist, float max_dist)
        {
            depthSample = 2.0f * depthSample - 1.0f;
            var D1 = max_dist + min_dist;
            var D2 = max_dist - min_dist;
            var zLinear = 2.0f * min_dist * max_dist / (D1 - depthSample * D2);
            return zLinear;
        }
    }


  
}
