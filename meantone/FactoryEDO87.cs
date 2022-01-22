using System;
using System.Collections.Generic;
using System.Text;

namespace meantone
{
    class FactoryEDO87 : FactoryEDO5
    {
        private bool[,] dichotomy;
        public FactoryEDO87(Type_Map map, bool[] pinc) : base(87, map, pinc)
        {
            commas = new Comma[6];
            pumps = new Pump[6];
            commas[0] = new Comma(this, new int[] { 5, -6, 0 });
            pumps[0] = new Pump(commas[0]);
            commas[1] = new Comma(this, new int[] { 2, 15, 0 });
            pumps[1] = new Pump(commas[1]);
            commas[2] = new Comma(this, new int[] { -2, 1, -2, 1 });
            pumps[2] = new Pump(commas[2]);
            commas[3] = new Comma(this, new int[] { 1, -1, -1, -1 });
            pumps[3] = new Pump(commas[3]);
            commas[4] = new Comma(this, new int[] { -2, -4, 0, 1 });
            pumps[4] = new Pump(commas[4]);
            commas[5] = new Comma(this, new int[] { -3, 1, -1, -1, 1 });
            pumps[5] = new Pump(commas[5],
               // new int[] {0, 28, 45, 19, 66, 15, 51 });
               new int[] { 0, 28, 45, 19, 66, 15, 51 });
            pumpStructure = new PumpStructureRandom(this);
            //PumpStructure b87 = new PumpStructureBig(this, 3);
            //pumpStructure = new PumpStructureSum(this, a87, b87);

            /*
            scale = new bool[29];
            for(int i = 0; i<5; i++)
            {
                scale[(7 * i) % 29] = true;
            }
            */
            /*
            scale = new bool[3];
            scale[0] = true;
            scale[1] = true;
            */

            dichotomy = new bool[3, edo];
            dichotomy[0, 0] = true;
            dichotomy[0, 51] = true;
            dichotomy[0, 36] = true;
            dichotomy[0, 28] = true;
            dichotomy[0, 59] = true;
            dichotomy[0, 23] = true;
            dichotomy[0, 64] = true;
            dichotomy[0, 15] = true;
            dichotomy[0, 72] = true;

        }
        public override double vertical_interval_cost(int dp, int loc)
        {
            return interval_cost(dp);

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
                return 100.0;
            }

            if (dichotomy[phase, dp % edo])
                return 0.0;
            return 5000.0;
        }
    }
}
