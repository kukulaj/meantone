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
            commas = new Comma[2];
            pumps = new Pump[2];
            commas[0] = new Comma(this, new int[] { 4, -1, 0 });
            pumps[0] = new Pump(commas[0]);
            commas[1] = new Comma(this, new int[] { 1, -5, 0 });
            pumps[1] = new Pump(commas[1], new int[] 
            {5, 18, 10, 4, 17, 11 });
            pumpStructure = new PumpStructureSimple(this, 1);

            /*
            for (int i = 0; i < edo; i++)
            {
                scale[i] = false;
            }

            for (int i = 0; i < 13; i++)
            {
                scale[(6 * i) % edo] = true;
            }
            */

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
