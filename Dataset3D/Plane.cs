using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dataset3D
{
    public class Plane
    {
        Texture tex;
        public float w, h;

        public Plane(string texturepath, float w, float h)
        {
            tex = new Texture(texturepath);
            this.w = w;
            this.h = h;
        }

        public void Draw()
        {
            DrawTexture(tex, w, h);
        }
        public static void DrawTexture(Texture t, float w, float h)
        {
            SelectPlaneMaterial();

            t.Select();

            GL.Begin(PrimitiveType.Quads);

            float hw = w / 2, hh = h / 2;
            GL.TexCoord2(0, 0); GL.Vertex3(-hw, -hh, 0);
            GL.TexCoord2(1, 0); GL.Vertex3(hw, -hh, 0);
            GL.TexCoord2(1, 1); GL.Vertex3(hw, hh, 0);
            GL.TexCoord2(0, 1); GL.Vertex3(-hw, hh, 0);

            GL.End();
        }

        static void SelectPlaneMaterial()
        {
            float[] mat_specular = new float[] { 0, 0, 0, 0 };
            float[] mat_shininess = new float[] { 0 };
            float[] mat_diffuse = new float[] { 1, 1, 1, 1 };

            float k = 5;
            float[] mat_ambient = new float[] {k, k, k, k };

            GL.ShadeModel(ShadingModel.Smooth);

            GL.Material(MaterialFace.Front, MaterialParameter.Specular, mat_specular);
            GL.Material(MaterialFace.Front, MaterialParameter.Shininess, mat_shininess);
            GL.Material(MaterialFace.Front, MaterialParameter.Ambient, mat_ambient);
            GL.Material(MaterialFace.Front, MaterialParameter.Diffuse, mat_diffuse);
        }
    }
}
