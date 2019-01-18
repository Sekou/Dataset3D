using OpenTK;
using OpenTK.Extra;
using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
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


        private void Form1_Load(object sender, EventArgs e)
        {
            nud_tint_ValueChanged(null, null);

            control3D1.OnDraw = () =>
            {
                if (cb_pause_draw.Checked) return;

                hpt.Start();
                if (cb_move.Checked)
                {
                    if (cb_file_world.Checked && file_world!=null)
                    {
                        //InitCamera2();
                        DrawAllObjects(-1, DrawMode.Normal, file_world);
                        if (traj != null && cb_draw_traj.Checked)
                            traj.Draw();
                    }
                    else
                    {
                        //InitCamera(vp);
                        DrawAllObjects(t, DrawMode.Normal, random_world);
                    }
                }
                var dt=hpt.Stop();
                smooth_dt += 0.1f * (dt - smooth_dt);
                lb_render_time.Text = string.Format("Frame Time: {0:F0} ms", smooth_dt * 1000);
            };

            nud_cam_speed_ValueChanged(null, null);
        }
        //Camera and viewport params
        public class ViewportParams
        {
            public float nearPlane = 2;
            public float farPlane = 5000;
            public float fovRadians;
            public float aspect;
            public float camDist = 700;
            public float wFarPlane;
            public float hFarPlane;

            public ViewportParams GetCopy()
            {
                return (ViewportParams)MemberwiseClone();
            }
        }
        private void bt_reset_Click(object sender, EventArgs e)
        {
            loaded = true;

            cube = new Cube(500, 100, 100);
            axes = new AxesGraphics();
            //obj = ObjMesh.LoadFromFile("meshes/teapot.obj");
            b = new Bitmap(control3D1.Width, control3D1.Height);

            oc = new ObjectCreator("meshes", "background", b.Width, b.Height);

            vp = new ViewportParams();
            vp.aspect = control3D1.Width / (float)control3D1.Height;
            vp.fovRadians = (float)(80 * Math.PI / 180);
            var fov2 = vp.fovRadians / 2;
            // L/2 / far = tn(fov2)
            // L=2*far*tn(fov2)
            vp.wFarPlane = 1.05f * (float)(2 * vp.farPlane * Math.Tan(fov2) * vp.aspect);
            vp.hFarPlane = vp.wFarPlane / vp.aspect;

            GL.Enable(EnableCap.Blend);

            t = 0;
            smooth_dt = 0;
            cb_pause.Checked = true;

            lb_frame.Text = "...";

            InitCamera(vp);
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

        ViewportParams vp;

        private void InitCamera(ViewportParams vp)
        {
            Matrix4 p = Matrix4.CreatePerspectiveFieldOfView(
                vp.fovRadians, vp.aspect, vp.nearPlane, vp.farPlane);
            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadMatrix(ref p);
            GL.PushMatrix();

            Matrix4 modelview = Matrix4.LookAt
                (vp.camDist, 0, 0, 0, 0, 0, 0, 0, 1);

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
            if (cb_pause_draw.Checked) return;

            Matrix4 mvm, projm;
            GL.GetFloat(GetPName.ModelviewMatrix, out mvm);
            GL.GetFloat(GetPName.ProjectionMatrix, out projm);
            mvm.Transpose();
            projm.Transpose();
            tb_mvm.Text = mvm.ToString();
            tb_projm.Text = projm.ToString();

            if (cb_move.Checked)
            {
                control3D1.DrawAll(false, false, !cb_move_traj.Checked, false);
                return;
            }

            //return;
            if (!loaded) return;
            if (cb_pause.Checked) return;
            if (t >= nud_max_images.Value) return;

            hpt.Start();

            lb_frame.Text = "" + t;

            DrawRandom();

            var dt = hpt.Stop();
            smooth_dt += 0.1f * (dt - smooth_dt);
            lb_render_time.Text = string.Format("Frame Time: {0:F0} ms", smooth_dt * 1000);
            t++;
        }

        //рисует несколько случайных объектов
        private void DrawRandom()
        {
            GL.PushMatrix();

            InitCamera(vp);
            random_world = DrawAllObjects(t, DrawMode.Normal);

            string filename, annot_ext;
            var annot = GetAnnotation(random_world, out filename, out annot_ext);

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
                    DrawAllObjects(t, DrawMode.Normal, random_world);
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

                    DrawAllObjects(t, DrawMode.Segmentation, random_world);
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
        }

        private string GetAnnotation(World w, out string filename, out string ext)
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
                    objects = w.obj_items.Select(oi=>oi.frame_info.region).ToList()
                };
                rtb_regions.Text = result = annot.ToXml();
                ext = ".xml";
            }
            else
            {
                var s = string.Join("\r\n",
                    w.obj_items.Select(oi => 
                    oi.frame_info.region.GetYOLORegion(
                        control3D1.ImageWidth, control3D1.ImageHeight)).ToList()
                  );

                rtb_regions.Text = result = s;
                ext = ".txt";
            }
            return result;
        }

        World DrawAllObjects(int iteration, DrawMode dm, World existing_world=null)
        {
            //синхронизация цветов фона
            oc.ResetRandom((existing_world != null) ? existing_world.iteration : iteration);

            var world = existing_world;
            
            if (existing_world == null)
            {
                world = oc.GetWorld(iteration, (int)nud_nobj.Value, vp);
                world.backgroung_color = Color.FromArgb(255, oc.RNDC, oc.RNDC, oc.RNDC);

                world.plane = null;
                if (oc.rnd.NextDouble() < (double)nud_ptex.Value)
                {
                    world.plane = new Plane(oc.backgrounds.GetRandomTexture(oc.rnd),
                        vp.wFarPlane, vp.hFarPlane);
                }
            }

            //GL.ClearColor(Color.Blue);

            if (dm == DrawMode.Normal)
            {
                Control3D.ClearColor(world.backgroung_color);

                if (world.plane!=null)
                {
                    //BACKGROUND
#warning move into PlaneObjItem:ObjectItem
                    DrawBackground(world, vp);
                }
            }
            else if (dm == DrawMode.Segmentation)
            {
                GL.Disable(EnableCap.Texture2D);

                Control3D.ClearColor(Color.Black);
            }

            if (cb_draw_axes.Checked)
            {
                axes.Draw(true, true, false);
            }
         
            world.Draw(dm, (o) =>
            {
                if (cb_draw_bb.Checked)
                {
                    var info = o.frame_info;
                    var szw = o.frame_info.region.GetW();
                    var szh = o.frame_info.region.GetH();
                    DrawRect(info.center.X, info.center.Y, szw, szh);
                }
            });

            //control3D1.SwapBuffers();

            return world;
        }

        public void DrawBackground(World world, ViewportParams vp)
        {
            GL.PushMatrix();
            //GL.LoadIdentity();
            GL.Translate(-vp.farPlane + vp.camDist + 100, 0, 0);
            //GL.Rotate(90, 0, 0, 1);
            GL.Rotate(90, 0, 1, 0);
            GL.Rotate(90, 0, 0, 1);
            Plane.DrawTexture(world.plane.tex, vp.wFarPlane, vp.wFarPlane);
            GL.PopMatrix();
        }

        void DrawRect(float xc, float yc, float w, float h)
        {
            GL.Disable(EnableCap.Lighting);
            GL.Disable(EnableCap.Texture2D);
            GL.Color3(Color.Red);
            GL.LineWidth(1);
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
        }

        private void nud_tint_ValueChanged(object sender, EventArgs e)
        {
            timer1.Interval = (int)nud_tint.Value;
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
                depth_arr[i] = FixDepth(depth_arr[i], vp.nearPlane, vp.farPlane)/ vp.farPlane;

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
                depth_arr[i] = FixDepth(depth_arr[i], vp.nearPlane, vp.farPlane) / vp.farPlane;

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
            Process.Start(@"explorer.exe", tb_folder.Text);
        }

        private void cb_hide_CheckedChanged(object sender, EventArgs e)
        {
            if(cb_hide.Checked) control3D1.Hide();
            else control3D1.Show();
        }

        World file_world;
        SimpleTrajectory traj;

        World random_world;

        private void bt_load_world_Click(object sender, EventArgs e)
        {
            var fm = new FileManager(Helper.CorrectPath(tb_scene.Text + "/" + "meshes"));
            file_world = new World(Helper.CorrectPath(tb_scene.Text + "/" + "scene.txt"), fm);

            traj = new SimpleTrajectory(Helper.CorrectPath(tb_scene.Text + "/" + "traj.txt"));
            nud_cam.Maximum = (decimal)traj.GetMaxTime();

            InitCamera2();
        }

        private void InitCamera2()
        {
            var vp2 = vp.GetCopy();
            vp2.farPlane = file_world.P.farPlane;
            vp2.nearPlane = file_world.P.nearPlane;
            InitCamera(vp2);
        }

        private void nud_cam_speed_ValueChanged(object sender, EventArgs e)
        {
            control3D1.MoveSpeed = (float)nud_cam_speed.Value;
        }

        private void nud_cam_ValueChanged(object sender, EventArgs e)
        {
            if (cb_pause.Checked) return;
            if(traj!=null && cb_move_traj.Checked)
            {
                var ind = (float)nud_cam.Value;
                if (ind < traj.points.Count)
                {
                    traj.SetCamAtTime((float)nud_cam.Value);
                }
            }
        }

        private void bt_save_traj_ds_Click(object sender, EventArgs e)
        {
            var b1 = cb_move.Checked; cb_move.Checked = true;
            var b2 = cb_draw_traj.Checked; cb_draw_traj.Checked = false;
            var b3 = cb_move_traj.Checked; cb_move_traj.Checked = true;
            var b4 = cb_pause.Checked; cb_pause.Checked = true;

            if (file_world == null || traj == null)
            {
                MessageBox.Show("Load world and trajectory first");
                return;
            }
            
            int ind = -1;
            for (float t = 0; t <= traj.GetMaxTime(); t+=(float)nud_tint2.Value, ind++)
            {
                traj.SetCamAtTime(t);
                DrawAllObjects(-1, DrawMode.Normal, file_world);
                control3D1.DrawAll(false, false, false, false);

                if (ind < 0) continue;
                var path = Helper.CorrectPath(tb_folder.Text + "/" + tb_scene.Text+"/");
                Helper.CheckCreateDir(path);
                control3D1.ToBitmap().Save(path + ind + ".jpg", System.Drawing.Imaging.ImageFormat.Jpeg);

                nud_cam.Value = (decimal) t;
                Application.DoEvents();

            }

            cb_move.Checked = b1;
            cb_draw_traj.Checked = b2;
            cb_move_traj.Checked = b3;
            cb_pause.Checked = b4;
        }
    }
}
