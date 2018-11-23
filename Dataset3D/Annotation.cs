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
        public float xmin;
        public float ymin;
        public float xmax;
        public float ymax;

        public string GetYOLORegion(float W, float H)
        {
            var cx = (xmin + xmax) / 2;
            var dx = (xmax - xmin) / 2;
            var cy = (ymin + ymax) / 2;
            var dy = (ymax - ymin) / 2;

            return string.Format(CultureInfo.InvariantCulture, "{0} {1:F4} {2:F4} {3:F4} {4:F4}",
               name, cx / W, cy / H, dx / W, dy / H);
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
                xml.WriteStartElement("annotation");

                el(xml, "folder", folder);

                el(xml, "filename", filename);

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
