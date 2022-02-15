using System;
using System.Collections.Generic;
using System.Text;

namespace meantone
{
   
    public class FactoryEDO12 : FactoryEDO5
    {
        private bool[,] dichotomy;
        bool[] augmented;
        public FactoryEDO12(Type_Map map, bool[] pinc) : base(12, map, pinc)
        {
            commas = new Comma[2];
            pumps = new Pump[2];
            commas[0] = new Comma(this, new int[] { 4, -1 });
            pumps[0] = new Pump(commas[0], new int[] { 0, 7, 2, 9, 4 });
            commas[1] = new Comma(this, new int[] { 0, 3 });
            pumps[1] = new Pump(commas[1]);

            pumpStructure = new PumpStructureRandom(this);

            /*
            for (int i = 0; i < edo; i++)
            {
                scale[i] = false;
            }

            for (int i = 0; i < 7; i++)
            {
                scale[(5 + 7 * i) % edo] = true;
            }
            */

            augmented = new bool[4];
            augmented[0] = true;
            augmented[1] = true;

            dichotomy = new bool[1, edo];
            dichotomy[0, 0] = true;
            dichotomy[0, 7] = true;
            dichotomy[0, 5] = true;
            dichotomy[0, 4] = true;
            dichotomy[0, 8] = true;
            dichotomy[0, 3] = true;
            dichotomy[0, 9] = true;
            dichotomy[0, 2] = true;
            dichotomy[0, 10] = true;

        }

        public override double vertical_interval_cost(int dp, int loc)
        {
            return interval_cost(dp);
            if (dp < 0)
            {
                dp = -dp;
            }

            int phase = 0;
            /*
            if ((loc+1) % map.row_size == 0)
            {
                phase = 1;
            }
            */
            if (dp % edo == 0)
            {
                return 100.0;
            }

            if (dichotomy[phase, dp % edo])
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
            int col = (loc / 9) % 9;

            switch ((loc % 8) / 2)
            {
                case 0:
                    
                    result = inAScale(augmented, pitch);
                    
                    break;
                case 1:
                    result = inAScale(augmented, pitch - 7);
                    break;
                case 2:
                    result = inAScale(augmented, pitch - 2);
                    
                    break;

                case 3:
                    result = inAScale(augmented, pitch - 9);
                   
                    break;
                   
            }
            return result;
        }
    }
}
