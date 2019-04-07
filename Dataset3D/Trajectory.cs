using OpenTK;
using OpenTK.Extra;
using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;

namespace Dataset3D
{

    public class Pt
    {
       public Vector3 p;
       public float time;
    }

    public class SimpleTrajectory
    {
        public List<Pt> points = new List<Pt>();

        public float GetMaxTime()
        {
            return points[points.Count - 1].time;
        }

        float Scale = 1;

        public SimpleTrajectory(string path)
        {
            var lines = File.ReadAllLines(path);

            for (int i = 0; i < lines.Length; i++)
            {
                if (lines[i].Contains(':'))
                {
                    var p=lines[i].Split(':');
                    if(p[0].Trim()=="units")
                    {
                        if(p[1].Trim() == "m")
                        {
                            Scale *= 1000;
                        }
                    }
                    continue;
                }

                var arr = lines[i].Split(' ');
                var arr2 = Array.ConvertAll(arr, 
                    x => float.Parse(x, CultureInfo.InvariantCulture));
                points.Add(new Pt { p = new Vector3(arr2[1], arr2[2], arr2[3]), time = arr2[0] });
            }
        }
        public void Draw()
        {
            GL.LineWidth(3);
            GL.Disable(EnableCap.Lighting);
            GL.Disable(EnableCap.Texture2D);

            GL.Enable(EnableCap.ProgramPointSize);
            GL.PointSize(10);
            GL.LineWidth(5);

            GL.Begin(PrimitiveType.Points);
            for (int i = 0; i < points.Count; i++)
            {
                var pt = points[i].p * Scale;
                GL.Color3(Color.Yellow);
                GL.Vertex3(pt[0], pt[1], pt[2]);
            }
            GL.End();

            GL.Begin(PrimitiveType.LineStrip);
            for (int i = 0; i < points.Count; i++)
            {
                var pt = points[i].p * Scale;
                GL.Color3(Color.Orange);
                GL.Vertex3(pt[0], pt[1], pt[2]);
            }
            GL.End();
        }

        public Matrix4 GetCamDir(Vector3 eye, Vector3 at)
        {
            /*
            var up = new Vector3(0, 0, 1);

            var Z = (at - eye).Normalized();
            var X = (Vector3.Cross(up, Z)).Normalized();

#warning to match opengl
            X *= -1;
            Z *= -1;

            var Y = Vector3.Cross(Z, X);

            var a = Vector3.Dot(X, -eye);
            var b = Vector3.Dot(Y, -eye);
            var c = Vector3.Dot(Z, -eye);

            var m = new Matrix4(
                new Vector4(X.X, Y.X, Z.X, 0),
                new Vector4(X.Y, Y.Y, Z.Y, 0),
                new Vector4(X.Z, Y.Z, Z.Z, 0),
                new Vector4(a, b, c, 1));

*/
            var m1 = Matrix4.LookAt(eye, at, new Vector3(0, 0, 1));

            return m1;
        }
        public Vector3[] InterpolateCamDir(int ind, float knext)
        {
            if (ind == points.Count - 1) ind--;
            if (ind == points.Count - 2) ind--;

            var p1 = points[ind].p;
            var p2 = points[ind + 1].p;
            var p3 = points[ind + 2].p;

            var eye = p1 + knext * (p2 - p1);
            var at = p2 + knext * (p3 - p2);

            //var m=GetCamDir(eye, at);

            return new Vector3[] { eye * Scale, at * Scale };
        }

        public void SetCamAtPoint(float indF, Control3D c3d, ref Form1.CamParams cp)
        {
            var ind = (int)indF;

            var knext = indF - ind;
            var pp = InterpolateCamDir(ind, knext);

            cp.SetCamPose(pp[0], pp[1], c3d);
        }
        public void SetCamAtTime(float time, Control3D c3d, ref Form1.CamParams cp)
        {
            int ind = 0;
            float t = float.MinValue;
            for (int i = 0; ; i++)
            {
                if (points[i].time >= time || i==points.Count)
                {
                    ind = Math.Max(0, i-1);
                    t = points[i].time;
                    break;
                }
            }

            float k = 0;
            var d1 = time - points[ind].time;
            var d2 = points[ind+1].time - time;
            k = d1 / (d1 + d2);

            SetCamAtPoint(ind + k, c3d, ref cp);
        }
    }
}
