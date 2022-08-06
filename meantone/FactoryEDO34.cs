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
            
            commas = new Comma[3];
            pumps = new Pump[5];
            commas[0] = new Comma(this, new int[] { 4, 2, 0 });
            pumps[0] = new Pump(commas[0]
               // , new int[] { 23, 9, 20, 6, 26, 3 }
                );
            commas[1] = new Comma(this, new int[] { 5, -6, 0 });
            pumps[1] = new Pump(commas[1]);
            pumps[2] = new Pump(commas[0], new int[] { 0, 20, 6, 17, 3, 23 });

            int[] path = new int[9];
            for (int i = 0; i < 9; i++)
            {
                path[i] = (10 + i * 11) % 34;
            }
           
            pumps[3] = new Pump(commas[0], path );
            commas[2] = new Comma(this, new int[] { 1, -8, 0 });
            pumps[4] = new Pump(commas[2]);
            pumpStructure =
                //new PumpStructureRandom(this);
                new PumpStructureTwo(this, 0, 4);

            build_dichotomy(new int[] {1, 3, 5, 9 });
            /*
            for (int i = 0; i < edo; i++)
            {
                scale[i] = false;
            }

            for(int i = 0; i<19; i++)
            {
                scale[(i * 11) % edo] = true;
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

            /*
            scale = new bool[17];
            for (int i = 0; i < 6; i++)
            {
                scale[(20 * i) % 17] = true;
            }
            */
            /*
            dichotomy = new bool[1, edo];
            dichotomy[0, 0] = true;
            dichotomy[0, 9] = true;
            dichotomy[0, 25] = true;
            dichotomy[0, 11] = true;
            dichotomy[0, 23] = true;
            dichotomy[0, 14] = true;
            dichotomy[0, 20] = true;
           // dichotomy[0, 31] = true;
            // dichotomy[0, 3] = true;
            dichotomy[0, 28] = true;
            dichotomy[0, 6] = true;
            dichotomy[0, 29] = true;
            dichotomy[0, 5] = true;
            */
        }

        /*
        public override double vertical_interval_cost(int dp, int loc)
        {
            //return interval_cost(dp);
            if (dp < 0)
            {
                dp = -dp;
            }

            int phase = 0;// loc % 2;
            
           if (loc % 12 == 11)
           {
               phase = 2;
           }
            
            if (dp % edo == 0)
            {
                return 85.0;
            }

            if (dichotomy[phase, dp % edo])
                return 0.0;
            return 5000.0;
        }
    */
    }
}
