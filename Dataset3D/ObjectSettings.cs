﻿using System;
using System.Collections.Generic;

namespace Dataset3D
{
    public class ObjLine
    {
        public string name;
        public float[] rot;
        public float scale;
        public float[] pos;
        public float kframe;
        public ObjLine()
        {
            rot = new float[] { 0, 0, 0 };
            scale = 1;
            pos = new float[] { 0, 0, 0 };
            kframe = 1;
        }
    }
    public class ObjectSettings
    {
        public ObjLine[] lines;

        public ObjectSettings()
        {
            lines = new ObjLine[1];
            lines[0] = new ObjLine() { name = "default" };
        }

        public ObjLine GetDefaultLine()
        {
            for (int i = 0; i < lines.Length; i++)
            {
                if (lines[i].name == "default") return lines[i];
            }
            return null;
        }
    }
}
