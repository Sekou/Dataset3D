﻿using OpenTK;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;

namespace Dataset3D
{
    public enum DrawMode { Normal, Segmentation };

    public static class Helper
    {

        public static Color[] colors;
        static Helper()
        {
            colors = new Color[1000];
            for (int i = 0; i < colors.Length; i++)
            {
                var c = Color.FromArgb(rnd.Next(20,235), rnd.Next(20, 235), rnd.Next(20, 235));
                colors[i] = c;
            }
        }

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

        public static Random rnd = new Random(1);
       
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

      

        public static Color GetColorById(int obj_type)
        {
            return colors[obj_type];
        }

        public static void NormalizeAng(ref float x)
        {
            var pi = (float)Math.PI;
            var pi2 = (float)Math.PI * 2;
            while (x > pi) x -= pi2;
            while (x < -pi) x += pi2;
        }

        static ImageCodecInfo GetEncoder(ImageFormat format)
        {
            ImageCodecInfo[] codecs = ImageCodecInfo.GetImageDecoders();
            foreach (ImageCodecInfo codec in codecs)
            {
                if (codec.FormatID == format.Guid)
                {
                    return codec;
                }
            }
            return null;
        }
        public static ImageCodecInfo jgpEncoder = GetEncoder(ImageFormat.Jpeg);

        public static EncoderParameters GetJpgQualityParams(int percents)
        {
            var myEncoderParameters = new EncoderParameters(1);

            var myEncoderParameter = new EncoderParameter(Encoder.Quality,
                (long)percents);
            myEncoderParameters.Param[0] = myEncoderParameter;

            return myEncoderParameters;
        }

        public static void SaveJPG(Bitmap b, string path)
        {
            b.Save(path, jgpEncoder, GetJpgQualityParams(90));
        }

        public static void SavePNG(Bitmap b, string path)
        {
            b.Save(path, ImageFormat.Png);
        }

        public static Vector4 MakeVec4(Vector3 v)
        {
            return new Vector4(v[0], v[1], v[2], 1);
        }

        public static Vector3 MakeVec3(Vector4 v)
        {
            return new Vector3(v[0], v[1], v[2]);
        }
    }
}
