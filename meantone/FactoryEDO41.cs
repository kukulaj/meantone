using System;
using System.Collections.Generic;
using System.Text;

namespace meantone
{
    public class FactoryEDO41 : FactoryEDO5
    {
        private bool[,] dichotomy;
        public FactoryEDO41(Type_Map map, bool[] pinc) : base(41, map, pinc)
        {
            commas = new Comma[2];
            pumps = new Pump[2];
            commas[0] = new Comma(this, new int[] { 2, 2, -1 });
            pumps[0] = new Pump(commas[0], new int[] {0, 24, 37, 20, 33 });
            commas[1] = new Comma(this, new int[] { 1, -5 });
            pumps[1] = new Pump(commas[0], new int[] { 0, 24, 11, 39, 26, 13 });

            pumpStructure = new PumpStructureTwo(this, 1, 0);

            for (int i = 0; i < edo; i++)
            {
                scale[i] = false;
            }

            for (int i = 0; i < 19; i++)
            {
                scale[(15 + 13*i) %edo] = true;
            }
            dichotomy = new bool[1, edo];
            dichotomy[0, 0] = true;
            dichotomy[0, 13] = true;
            dichotomy[0, 28] = true;
            dichotomy[0, 24] = true;
            dichotomy[0, 17] = true;
            dichotomy[0, 11] = true;
            dichotomy[0, 30] = true;
            dichotomy[0, 33] = true;
            dichotomy[0, 8] = true;
            dichotomy[0, 20] = true;
            dichotomy[0, 21] = true;
            dichotomy[0, 9] = true;
            dichotomy[0, 32] = true;

        }

        public override double vertical_interval_cost(int dp, int loc)
        {
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
    }
}
