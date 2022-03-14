using System;
using System.Collections.Generic;
using System.Text;

namespace meantone
{
    public class FactoryEDO441 : FactoryEDO5
    {
        public FactoryEDO441(Type_Map map, bool[] pinc) : base(441, map, pinc)
        {
            scale = new bool[63];
            for (int i = 0; i < 14; i++)
            {
                scale[(i * 22) % 63] = true;
            }
            pumpStructure = new PumpStructureRandom(this);
        }
        public override double vertical_interval_cost(int dp, int loc)
        {
            return interval_cost(dp);
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


            result = inAScale(scale, pitch
                - 57 * (loc % 10));
               
           
            return result;
        }
    }
}
