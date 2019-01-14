using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Dataset3D
{
    public class Texture
    {
        public int texture_id;
        public string texturepath;

        public Texture(string path)
        {
            texturepath = path;
            texture_id = OpenTK.Extra.Helper.LoadTexture(texturepath);
        }
        public void Select()
        {
            GL.Enable(EnableCap.Texture2D);
            GL.BindTexture(TextureTarget.Texture2D, texture_id);
        }
    }
    public class Textures
    {
        public List<Texture> lst = new List<Texture>();
        public Textures(string folder)
        {
            var dir = new DirectoryInfo(folder);
            var files = dir.GetFiles("*.*", SearchOption.TopDirectoryOnly);
            foreach (var f in files)
            {
                bool ok = false;
                if (f.Name.EndsWith(".jpg")) ok=true;
                else if (!f.Name.EndsWith(".png")) ok=true;
                if (!ok) continue;

                var t = new Texture(f.FullName);
                lst.Add(t);
            }
        }

        public Texture GetRandomTexture(Random rnd)
        {
            var ind = rnd.Next(lst.Count);
            return lst[ind];
        }


    }
}
