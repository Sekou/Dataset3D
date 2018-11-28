using System;
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

    }
}
