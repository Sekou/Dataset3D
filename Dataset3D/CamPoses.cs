using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dataset3D
{
    public class CamPose
    {
        public Vector3 p;
        public float ang;

        public bool Similar(CamPose other)
        {
#warning magic numbers

            var da = ang - other.ang;
            Helper.NormalizeAng(ref da);
            if (Math.Abs(da) > 0.1f) return false;

            var dp = (p - other.p).Length;
            if (dp > 1000) return false;

            return true;
            
        }
    }

    public class CamPoses
    {
        public List<CamPose> poses = new List<CamPose>();

        public bool Contains(CamPose cp)
        {
            for (int i = 0; i < poses.Count; i++)
            {
                if (poses[i].Similar(cp)) return true;
            }
            return false;
        }
    }
}
