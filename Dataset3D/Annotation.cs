using OpenTK;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;

namespace Dataset3D
{
    public class ObjectRegion
    {
        public string name;
        public int label_id;             //i
        public float xmin;
        public float ymin;
        public float xmax;
        public float ymax;

        public float GetW() { return xmax - xmin; }
        public float GetH() { return ymax - ymin; }

        public Vector2 GetCenter() { return new Vector2((xmin + xmax) / 2, (ymin + ymax) / 2); }

        public string GetYOLORegion(float W, float H, Dictionary<int, int> mapping)
        {
            var cx = (xmin + xmax) / 2;
            var dx = (xmax - xmin);
            var cy = (ymin + ymax) / 2;
            var dy = (ymax - ymin);

            cx /= W;
            cy /= H;
            dx /= W;
            dy /= H;

            var k = 0;// 0.1f;
            if (cx < -k || cy < -k || cx > 1 + k || cy > 1 + k)
                return null;

            var label_id2 = label_id;
            if(mapping!=null) if (mapping.ContainsKey(label_id)) label_id2 = mapping[label_id];

            return string.Format(CultureInfo.InvariantCulture, "{0} {1:F4} {2:F4} {3:F4} {4:F4}",
               label_id2, cx, cy, dx, dy);
        }
    }
    public class Annotation
    {
        public string folder;
        public string filename;

        public int img_width;
        public int img_height;
        public int img_depth;

        public List<ObjectRegion> objects;

        public string ToXml()
        {
            var sb = new StringBuilder();
            using (StringWriter sw = new StringWriter(sb))
            {
                var xml = new XmlTextWriter(sw);
                xml.Formatting = Formatting.Indented;

                xml.WriteStartElement("annotation");

                el(xml, "folder", folder);

                el(xml, "filename", filename);

                el(xml, "segmented", 0);

                xml.WriteStartElement("size");
                el(xml, "width", img_width);
                el(xml, "height", img_height);
                el(xml, "depth", img_depth);
                xml.WriteEndElement();

                for (int i = 0; i < objects.Count; i++)
                {
                    xml.WriteStartElement("object");

                    el(xml, "name", objects[i].name);
                    el(xml, "pose", "Unspecified");
                    el(xml, "truncated", 0);
                    el(xml, "difficult", 0);

                    xml.WriteStartElement("bndbox");

                    el(xml, "xmin", (int)objects[i].xmin);
                    el(xml, "ymin", (int)objects[i].ymin);
                    el(xml, "xmax", (int)objects[i].xmax);
                    el(xml, "ymax", (int)objects[i].ymax);

                    xml.WriteEndElement(); //bndbox
                    xml.WriteEndElement(); //object
                }
                xml.WriteEndElement(); //annotation

                xml.Close();
            }
            return sb.ToString();
        }

        void el(XmlTextWriter xml, string name, object val)
        {
            xml.WriteStartElement(name);
            xml.WriteValue(val);
            xml.WriteEndElement();
        }
    }
}
