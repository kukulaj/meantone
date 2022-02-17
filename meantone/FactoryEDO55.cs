using System;
using System.Collections.Generic;
using System.Text;

namespace meantone
{
    public class FactoryEDO55 : FactoryEDO5
    {
        private bool[] dichotomy;
        public FactoryEDO55(Type_Map map, bool[] pinc) : base(55, map, pinc)
        {
            scale = new bool[11];
            for (int i = 0; i<5; i++)
            { 
              scale[(i * 7) % 11] = true;
            }

            pumpStructure = new PumpStructureRandom(this);
            dichotomy = new bool[edo];
            dichotomy[0] = true;
            dichotomy[32] = true;
            dichotomy[23] = true;
            dichotomy[18] = true;
            dichotomy[37] = true;
            dichotomy[14] = true;
            dichotomy[41] = true;
            dichotomy[9] = true;
            dichotomy[46] = true;
        }

        public override double vertical_interval_cost(int dp, int loc)
        {
            return interval_cost(dp);
            if (dp < 0)
            {
                dp = -dp;
            }

            if (dp % edo == 0)
            {
                return 100.0;
            }
            if (dichotomy[dp % edo])
                return 0.0;
            return 5000.0;
        }
        private bool inAScale(bool[] aScale, int pitch)
        {
            int period = aScale.Length;
            int rem = pitch % period;
            if (rem < 0)
            {
                rem = rem + period;
            }
            return aScale[rem];
        }

        public override bool inScale(int pitch, int loc)
        {

            bool result = true;


            result = inAScale(scale, pitch - 32 * ((loc % 4)));

            return result;
        }
    }

}
