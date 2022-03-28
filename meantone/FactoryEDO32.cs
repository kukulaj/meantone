using System;
using System.Collections.Generic;
using System.Text;

namespace meantone
{
    public class FactoryEDO32 : FactoryEDO5
    {
        public FactoryEDO32(Type_Map map, bool[] pinc) : base(32, map, pinc)
        {
            commas = new Comma[1];
            pumps = new Pump[1];
            commas[0] = new Comma(this, new int[] { 6, -5, 0 });
            pumps[0] = new Pump(commas[0],
              new int[] { 22, 31, 8, 17, 26, 3});
            pumpStructure = new PumpStructureSimple(this, 0);

            
           for (int i = 0; i < edo; i++)
           {
               scale[i] = false;
           }
          
           for(int i = 0; i < 18; i++)
            {
                scale[(i * 9) % 32] = true;
            }
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
            return inAScale(scale, pitch);
          
        }
    }
    
}
