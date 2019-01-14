using OpenTK;
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
    public class SimpleTrajectory
    {
        public List<Vector3> points = new List<Vector3>();

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
                points.Add(new Vector3(arr2[0], arr2[1], arr2[2]));
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
                var pt = points[i] * Scale;
                GL.Color3(Color.Yellow);
                GL.Vertex3(pt[0], pt[1], pt[2]);
            }
            GL.End();

            GL.Begin(PrimitiveType.LineStrip);
            for (int i = 0; i < points.Count; i++)
            {
                var pt = points[i] * Scale;
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
        public Matrix4 GetCamDirAtPoint(int ind)
        {
            if (ind == points.Count - 1) ind--;

            var at = points[ind + 1];
            var eye = points[ind];

            var m=GetCamDir(eye, at);

            return m;
        }
        public Matrix4 InterpolateCamDir(int ind, float knext)
        {
            if (ind == points.Count - 1) ind--;
            if (ind == points.Count - 2) ind--;

            var p1 = points[ind];
            var p2 = points[ind + 1];
            var p3 = points[ind + 2];

            var eye = knext * p2 + (1 - knext) * p1;
            var at = knext * p3 + (1 - knext) * p2;

            var m=GetCamDir(eye, at);

            return m;
        }
        public Matrix4 InterpolateCamPos(int ind, float knext)
        {
            Vector3 v;
            if (ind >= points.Count - 1) v = points[points.Count - 1];
            else
            {
                var p1 = points[ind];
                var p2 = points[ind + 1];

                v = p2 * knext + p1 * (1 - knext);
            }

            return Matrix4.CreateTranslation(-v*Scale);
        }
        public void SetCamAtPoint(float indF)
        {
            var ind = (int)indF;

            var knext = indF - ind;
            var m = InterpolateCamDir(ind, knext);
            var m2 = InterpolateCamPos(ind, knext);

            var T = m2 * m;

            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadMatrix(ref T);
        }
    }
}
