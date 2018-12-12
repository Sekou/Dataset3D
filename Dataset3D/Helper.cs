using OpenTK;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace Dataset3D
{
    public class Helper
    {
        public static double[] convertFloatsToDoubles(float[] input)
        {
            if (input == null)
            {
                return null; // Or throw an exception - your choice
            }

            double[] output = new double[input.Length];
            for (int i = 0; i < input.Length; i++)
            {
                output[i] = input[i];
            }
            return output;
        }

        public static Random rnd = new Random();
        public static float r() { return (float)rnd.NextDouble(); }
        public static float r2() { return 2*(float)rnd.NextDouble()-1; }
        public static void CheckCreateDir(string path)
        {
            bool Exists = Directory.Exists(path);
            if (!Exists)
                System.IO.Directory.CreateDirectory(path);
        }

        public static string CorrectPath(string path)
        {
            var x = path.Replace("\\\\", "/").Replace("\\", "/");
            x = x.Replace("//", "/");
            x = x.Replace("/", "\\");
            return x;
        }


        //image from array of brightnesses
        public static Bitmap BitmapFromFloatsGrayscale(float[] img, int w, int h)
        {
            var res = new Bitmap(w, h, PixelFormat.Format24bppRgb);
            var imgData = res.LockBits(new Rectangle(0, 0, w, h), ImageLockMode.ReadOnly, PixelFormat.Format24bppRgb);
            int i = 0;
            unsafe
            {
                byte* imgPtr = (byte*)imgData.Scan0;
                for (var y = 0; y < h; y++)
                {
                    for (var x = 0; x < w; x++, i++)
                    {
                        var b = (i < img.Length) ? (byte)(255f * img[i]) : (byte)0;
                        imgPtr[0] = b;
                        imgPtr[1] = b;
                        imgPtr[2] = b;

                        imgPtr += 3;
                    }
                    imgPtr += imgData.Stride - (imgData.Width * 3);
                }
            }
            res.UnlockBits(imgData);
            return res;
        }

        public static List<float[]> GetRandomPoints(int N,
            float[] min, float[] max, bool[] active, float k)
        {
            var points = new List<float[]>();
            for (int i = 0; i < N; i++)
                points.Add(getrnd(min, max));

            for (int i = 0; i < points.Count; i++)
            {
                for (int j = i+1; j < points.Count; j++)
                {
                    float[] a = points[i], b = points[j];
                    for (int m = 0; m < active.Length; m++)
                    {
                        if (active[m])
                        {
                            var d = Math.Max(1, a[m] - b[m]);
                            var A = (max[m] - min[m]);
                            var f = k * A/ d / points.Count;
                            a[m] += f;
                            b[m] -= f;
                        }
                    }
                }
            }

            return points;
        }

        static float getrnd(float min, float max)
        {
            return min+(float)rnd.NextDouble() * (max - min);
        }
        static float[] getrnd(float[] min, float[] max)
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
                points[i]=T(points[i]);
            }
        }

    }
}
