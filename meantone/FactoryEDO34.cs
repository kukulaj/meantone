using System;
using System.Collections.Generic;
using System.Text;

namespace meantone
{
    public class FactoryEDO34 : FactoryEDO5
    {
        private bool[,] dichotomy;

        public FactoryEDO34(Type_Map map, bool[] pinc) : base(34, map, pinc)
        {
            
            commas = new Comma[2];
            pumps = new Pump[3];
            commas[0] = new Comma(this, new int[] { 4, 2, 0 });
            pumps[0] = new Pump(commas[0], new int[] { 23, 9, 20, 6, 26, 3 });
            commas[1] = new Comma(this, new int[] { 5, -6, 0 });
            pumps[1] = new Pump(commas[1]);
            pumps[2] = new Pump(commas[0], new int[] { 0, 20, 6, 17, 3, 23 });
            pumpStructure = new PumpStructureSimple(this, 0);

            /*
            for (int i = 0; i < edo; i++)
            {
                scale[i] = false;
            }
            */
            /*
            scale[0] = true;
            scale[3] = true;
            scale[6] = true;
            scale[17] = true;
            scale[20] = true;
            scale[23] = true;
            */

            scale = new bool[17];
            for (int i = 0; i < 5; i++)
            {
                scale[(20 * i) % 17] = true;
            }

            dichotomy = new bool[1, edo];
            dichotomy[0, 0] = true;
            dichotomy[0, 9] = true;
            dichotomy[0, 25] = true;
            dichotomy[0, 11] = true;
            dichotomy[0, 23] = true;
            dichotomy[0, 14] = true;
            dichotomy[0, 20] = true;
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
                return 85.0;
            }

            if (dichotomy[phase, dp % edo])
                return 0.0;
            return 5000.0;
        }
    }
}
