using OpenTK;
using OpenTK.Extra;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;

namespace Dataset3D
{
    //A single item of a transformed 3d-model
    public class ObjItem
    {
        public Matrix4 ObjItemTransform;
        public ObjMesh mesh;
        public ObjItem(string line)
        {
            var arr = line.Split(' ');

            var name = "scenes" + Path.DirectorySeparatorChar + arr.Last();
            mesh = ObjMesh.LoadFromFile(name);
        }
        public void Draw()
        {
            mesh.Draw();
        }
    }

    //Global params for all objects in a file
    public class WorldParams
    {
        public WorldParams(List<string> lines)
        {

        }
    }

    //2019-01-10
    //A model of a 3d-scene generated based on a simple .txt file
    public class World
    {
        WorldParams P;
        List<ObjItem> obj_items = new List<ObjItem>();

        public World(string file)
        {
            var lines = File.ReadAllLines(file);

            var param_lines = new List<string>();
            for (int i = 0; i < lines.Length; i++)
            {
                if (lines[i].Contains(':'))
                    param_lines.Add(lines[i]);
                else
                {
                    if (P == null) P = new WorldParams(param_lines);
                    var obj_item = new ObjItem(lines[i]);
                    obj_items.Add(obj_item);
                }
            }

        }

        public void Draw()
        {
            Control3D.ClearColor(Color.Violet);
            for (int i = 0; i < obj_items.Count; i++)
            {
                obj_items[i].Draw();

            }

        }
    }
}
