using System;
using System.Collections.Generic;
using System.Text;

namespace meantone
{
    public class FactoryEDO118 : FactoryEDO5
    {
        private bool[,] dichotomy;
        public FactoryEDO118(Type_Map map, bool[] pinc) : base(118, map, pinc)
        {
            commas = new Comma[4];
            pumps = new Pump[4];
            commas[0] = new Comma(this, new int[] { 8, 1, 0 });
            pumps[0] = new Pump(commas[0]
            , new int[]
            {89, 40, 109, 60, 11, 80, 31, 100, 51 }
            );
            commas[1] = new Comma(this, new int[] { 2, 15, 0 });
            pumps[1] = new Pump(commas[1]);
            commas[2] = new Comma(this, new int[] { 6, -14, 0 });
            pumps[2] = new Pump(commas[2], new int[]
            {0, 80, 42, 73, 35, 66, 28, 59, 21, 101, 14, 94, 7, 87 });
            commas[3] = new Comma(this, new int[] { 14, -13, 0 });

            int[] pkp = new int[14];
            for (int i = 0; i < 14; i++)
            {
                pkp[i] = ((14 + i) * 31) % 118;
            }

            pumps[3] = new Pump(commas[3], pkp);
                //new int[]
                //{0, 31, 62, 93, 6, 37, 68, 99, 12, 43, 74, 105, 18, 49 });

            //PumpStructure a118 = new PumpStructureSimple(this, 1);
            //PumpStructure b118 = new PumpStructureBig(this, 2);
            //pumpStructure = new PumpStructureSum(this, a118, b118);
            pumpStructure = new PumpStructureSimple(this, 0);

            
            for (int i = 0; i < edo; i++)
            {
                scale[i] = false;
            }
            for (int i = 0; i < 17; i++)
            {
                scale[(69 * i) % edo] = true;
            }
            
            /*
            for (int i = 0; i < 42; i++)
            {
                scale[(31 * i) % edo] = true;
            }
            */

            /*
            scale = new bool[59];
            for (int i = 0; i < 17; i++)
            {
                scale[(7 * i) % 59] = true;
            }
            */
            /*
            scale[0] = true;
            scale[6] = true;
            scale[12] = true;
            scale[18] = true;
            scale[25] = true;
            */

            dichotomy = new bool[2, edo];
            dichotomy[0, 0] = true;
            dichotomy[0, 69] = true;
            dichotomy[0, 49] = true;
            dichotomy[0, 38] = true;
            dichotomy[0, 80] = true;
            dichotomy[0, 31] = true;
            dichotomy[0, 87] = true;
            dichotomy[0, 18] = true;
            dichotomy[0, 100] = true;
            dichotomy[0, 20] = true;
            dichotomy[0, 98] = true;

            dichotomy[1, 0] = true;
            dichotomy[1, 69] = true;
            dichotomy[1, 49] = true;
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
            if (dichotomy[phase, dp % edo])
                return 0.0;
            return 5000.0;
        }
    }
}
