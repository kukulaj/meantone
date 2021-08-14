using System;
using System.Collections.Generic;
using System.Text;

namespace meantone
{
    class FactoryEDO53 : FactoryEDO5
    {
        private bool[,] dichotomy;
        public FactoryEDO53(Type_Map map, bool[] pinc) : base(53, map, pinc)
        {
            commas = new Comma[3];
            pumps = new Pump[3];
            commas[0] = new Comma(this, new int[] { 5, -6, 0 });
            int[] path = new int[6];
            for(int i=0; i<6; i++)
            {
                path[i] = (i * 14 + 56) % edo;
            }
            
            pumps[0] = new Pump(commas[0], path);


            commas[1] = new Comma(this, new int[] { 8, 1, 0 });
            pumps[1] = new Pump(commas[1],
                new int[] { 48, 17, 39, 22, 44, 13, 35, 4, 26 });
            commas[2] = new Comma(this, new int[] { 3, 7, 0 });
            pumps[2] = new Pump(commas[2]);
            pumpStructure = new PumpStructureSimple(this, 0);
            //PumpStructure b53 = new PumpStructureBig(this, 2);
            //pumpStructure = new PumpStructureSum(this, a53, b53);

            /*
            scale = new bool[14];
            scale[0] = true;
            scale[3] = true;
            scale[11] = true;
            */

            
            for (int i = 0; i < edo; i++)
            {
                scale[i] = false;
            }

            for (int i = 0; i < 15; i++)
            {
                scale[(14 * i) % edo] = true;
            }
            
            dichotomy = new bool[3,edo];
            dichotomy[0,0] = true;
            dichotomy[0,31] = true;
            dichotomy[0,22] = true;
            dichotomy[0,14] = true;
            dichotomy[0,17] = true;
            dichotomy[0,39] = true;
            dichotomy[0,36] = true;
            dichotomy[0, 48] = true;
            dichotomy[0, 5] = true;
            dichotomy[0, 44] = true;
            dichotomy[0, 9] = true;

            dichotomy[1, 0] = true;           
            dichotomy[1, 14] = true;
            dichotomy[1, 17] = true;
            dichotomy[1, 39] = true;
            dichotomy[1, 36] = true;
            dichotomy[1, 48] = true;
            dichotomy[1, 5] = true;

            dichotomy[2, 0] = true;
            dichotomy[2, 22] = true;
            dichotomy[2,31] = true;

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
             if(dp%edo == 0)
            {
                return 100.0;
            }

            if (dichotomy[phase, dp % edo])
                return 0.0;
            return 5000.0;
        }
    }
}
