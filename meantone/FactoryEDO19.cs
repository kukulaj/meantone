using System;
using System.Collections.Generic;
using System.Text;

namespace meantone
{
    public class FactoryEDO19 : FactoryEDO5
    {
        private bool[,] dichotomy;
        public FactoryEDO19(Type_Map map, bool[] pinc) : base(19, map, pinc)
        {
            commas = new Comma[3];
            pumps = new Pump[3];
            commas[0] = new Comma(this, new int[] { 4, -1, 0 });
            pumps[0] = new Pump(commas[0]);
            commas[1] = new Comma(this, new int[] { 1, -5, 0 });
            pumps[1] = new Pump(commas[1], new int[] 
            {5, 18, 10, 4, 17, 11 });
            commas[2] = new Comma(this, new int[] { 3, 4 });
            pumps[2] = new Pump(commas[1], new int[]
            {0, 6, 12, 4, 10, 2, 8 });
            pumpStructure = new PumpStructureSimple(this, 2);

            /*
            pumpStructure = new PumpStructureArray(this, new int[,]
                {{12,  0, 10,  8,  6,  4,  2, 14, 16 },
                 {8,  16, 14,  2, 10, 12,  0,  6,  4 },
                 {4,   2,  6,  0, 14, 16, 10, 12,  8 },
                 {16, 14,  4, 12,  2,  0,  6,  8, 10 },
                 {2,   6,  8, 14,  4, 10, 12, 16,  0 },
                 {10, 12,  0,  6, 16,  8, 14,  4,  2 },
                 {14, 10, 12,  4,  8,  2, 16,  0,  6},
                 {6,   4,  2, 16,  0, 14,  8, 10, 12 },
                 {0,   8, 16, 10, 12,  6,  4,  2, 14 } });
            */

            for (int i = 0; i < edo; i++)
            {
                scale[i] = false;
            }

            for (int i = 0; i < 10; i++)
            {
                scale[(2 * i) % edo] = true;
            }
            

            dichotomy = new bool[1, edo];
            dichotomy[0, 0] = true;
            dichotomy[0, 8] = true;
            dichotomy[0, 11] = true;
            dichotomy[0, 6] = true;
            dichotomy[0, 13] = true;
            dichotomy[0, 5] = true;
            dichotomy[0, 14] = true;
            dichotomy[0, 3] = true;
            dichotomy[0, 16] = true;
        }

        public override double vertical_interval_cost(int dp, int loc)
        {
            if (dp < 0)
            {
                dp = -dp;
            }

            int phase = 0;// loc % 2;
            /*
           if (loc % 12 == 11)
           {
               phase = 2;
           }
            */
            if (dp % edo == 0)
            {
                return 70.0;
            }

            if (dichotomy[phase, dp % edo])
                return 0.0;
            return 5000.0;
        }
    }
}
