using System;
using System.Collections.Generic;
using System.Text;

namespace meantone
{
    public class FactoryEDO59 : FactoryEDO5
    {
        private bool[,] dichotomy;
        public FactoryEDO59(Type_Map map, bool[] pinc) : base(59, map, pinc)
        {
            commas = new Comma[1];
            pumps = new Pump[1];
            commas[0] = new Comma(this, new int[] { 5, -3, 0 });
            pumps[0] = new Pump(commas[0], 
                new int[] {21, 56, 37, 13, 48, 24, 5, 40 }); 
             
             
            pumpStructure = new PumpStructureSimple(this, 0);


            for (int i = 0; i < edo; i++)
            {
                scale[i] = false;
            }
            for (int i = 0; i < 15; i++)
            {
                scale[(8 * i) % edo] = true;
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

            dichotomy = new bool[1, edo];
            dichotomy[0, 0] = true;
            dichotomy[0, 36] = true;
            dichotomy[0, 24] = true;
            dichotomy[0, 19] = true;
            dichotomy[0, 40] = true;
            dichotomy[0, 16] = true;
            dichotomy[0, 43] = true;

            
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
