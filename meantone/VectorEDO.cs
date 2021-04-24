using System;
using System.Collections.Generic;
using System.Text;

namespace meantone
{
    public abstract class VectorEDO : Vector
    {
        public int edo;
        public int pitch;

        public override double log_frequency()
        {
            return Math.Log(2.0) * (1.0 / (double)edo) * (double)pitch;
        }

        public override double histd()
        {
            int rem = pitch % edo;
            if (rem < 0)
            {
                rem += edo;
            }
            return (double)rem;
        }

        public override bool octave_equivalent(Vector av)
        {
            VectorEDO v = (VectorEDO)av;

            bool result = ((pitch - v.pitch) % edo) == 0;
            return result;
        }

        public override bool same(Vector av)
        {
            VectorEDO v = (VectorEDO)av;
            if (pitch == v.pitch)
            {
                return true;
            }
            return false;
        }

        public override bool same_interval(Vector v, Vector w1, Vector w2)
        {
            VectorEDO vc = (VectorEDO)v;
            VectorEDO w1c = (VectorEDO)w1;
            VectorEDO w2c = (VectorEDO)w2;

            if (pitch - vc.pitch != w1c.pitch - w2c.pitch)
            {
                return false;
            }

            return true;
        }
    }

}
