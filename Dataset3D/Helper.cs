using OpenTK;
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
    }

    public class HiPerfTimer
    {
        [DllImport("Kernel32.dll")]
        private static extern bool QueryPerformanceCounter(out long lpPerformanceCount);

        [DllImport("Kernel32.dll")]
        private static extern bool QueryPerformanceFrequency(out long lpFrequency);

        private long startTime, stopTime;
        private long freq;

        // Constructor
        public HiPerfTimer()
        {
            startTime = 0; stopTime = 0;
            if (QueryPerformanceFrequency(out freq) == false)
            {
                // high-performance counter not supported
                throw new System.ComponentModel.Win32Exception();
            }
        }

        // Start the timer
        public void Start()
        {
            // lets do the waiting threads their work
            Thread.Sleep(0);
            QueryPerformanceCounter(out startTime);
        }

        // Stop the timer
        public float Stop()
        {
            QueryPerformanceCounter(out stopTime);
            return (float)Duration;
        }

        // Returns the duration of the timer (in seconds)
        public double Duration
        { get { return (double)(stopTime - startTime) / (double)freq; } }
    }

}
