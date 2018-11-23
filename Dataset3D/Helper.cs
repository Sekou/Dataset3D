using System;
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

    }
}
