using System;
using System.Collections.Generic;
using System.Text;

namespace meantone
{
    public class FactoryEDO31 : FactoryEDO5
    {
        private bool[][] dichotomy;

        public FactoryEDO31(Type_Map map, bool[] pinc) : base(31, map, pinc)
        {
            commas = new Comma[11];
            pumps = new Pump[11];


            commas[0] = new Comma(this, new int[] { 0, 5, -2 });
            pumps[0] = new Pump(commas[0], new int[] 
            {9, 3, 24, 14, 4, 25, 19 });
            commas[1] = new Comma(this, new int[] { 4, -1, 0 });
            pumps[1] = new Pump(commas[1]);
            commas[2] = new Comma(this, new int[] { 3, 7, 0 });
            pumps[2] = new Pump(commas[2]);
            commas[3] = new Comma(this, new int[] { 1, 0, 3 });
            pumps[3] = new Pump(commas[3]);
            commas[4] = new Comma(this, new int[] { 2, 2, -1 });
            pumps[4] = new Pump(commas[4],new int[] {0, 18, 28, 15, 25 });
            commas[5] = new Comma(this, new int[] { 3, 1, -2, -1 });
            pumps[5] = new Pump(commas[5]);
            commas[6] = new Comma(this, new int[] { 4, -3, -1, 1 });
            pumps[6] = new Pump(commas[6]);
            commas[7] = new Comma(this, new int[] { 2, -3, 1 });
            pumps[7] = new Pump(commas[7], new int[] {0, 8, 16, 24 });
            commas[8] = new Comma(this, new int[] { 3, 4, -5 });
            pumps[8] = new Pump(commas[8]);
            commas[9] = new Comma(this, new int[] {-1, 8, 0 });
            pumps[9] = new Pump(commas[9]);
            // , new int[] {0, 18, 8, 29, 19, 6, 27, 17, 30, 20, 10 });

            commas[10] = new Comma(this, new int[] { 1,5, 1 });
            pumps[10] = new Pump(commas[9], new int[] {0, 21, 11, 1, 14, 4, 25 } );
            //PumpStructure a = new PumpStructureSimple(this, 1);
            //PumpStructure b = new PumpStructureBig(this, 9);
            pumpStructure = new PumpStructureRandom(this);

            /*
             for (int i = 0; i < edo; i++)
             {
                 scale[i] = false;
             }

             for (int i = 0; i < 22; i++)
             {
                 scale[(7 * i) % edo] = true;
             }
             */

            build_dichotomy(9);

            dichotomy = new bool[4][];
            for (int i = 0; i < 4; i++)
            {
                dichotomy[i] = new bool[edo];
            }
            dichotomy[0][0] = true;
            dichotomy[0][18] = true;
            dichotomy[0][13] = true;
            dichotomy[0][8] = true;
            dichotomy[0][23] = true;
            dichotomy[0][10] = true; 
            dichotomy[0][21] = true;
            dichotomy[0][5] = true;
            dichotomy[0][26] = true;
 
            
            dichotomy[0][15] = true;
            dichotomy[0][16] = true;
            dichotomy[0][25] = true;
            dichotomy[0][6] = true;
            dichotomy[0][24] = true;
            dichotomy[0][7] = true;
            

            dichotomy[1][0] = true;
            dichotomy[1][6] = true;
            dichotomy[1][25] = true;
            dichotomy[1][18] = true;
            dichotomy[1][12] = true;
            dichotomy[1][19] = true;

            dichotomy[2][0] = true;
            dichotomy[2][6] = true;
            dichotomy[2][25] = true;
            dichotomy[2][10] = true;
            dichotomy[2][4] = true;
            dichotomy[2][27] = true;

            dichotomy[3][0] = true;
            dichotomy[3][18] = true;
            dichotomy[3][13] = true;
            /*
            dichotomy[3][22] = true;
            dichotomy[3][7] = true;
            dichotomy[3][11] = true;
            dichotomy[3][27] = true;
            dichotomy[3][2] = true;
            dichotomy[3][6] = true;

            dichotomy[4][30] = true;
            dichotomy[4][1] = true;
            dichotomy[4][17] = true;
            dichotomy[4][19] = true;
            dichotomy[4][12] = true;
            dichotomy[4][28] = true;
            */

            /*
            dichotomy[8] = true;
            
            dichotomy[15] = true;
            
            dichotomy[27] = true;
            dichotomy[29] = true;
            dichotomy[20] = true;
            dichotomy[25] = true;
            dichotomy[19] = true;
            */


            /*
            for (int i = 0; i < 12; i++)
            {
                scale[(18 * i) % edo] = true;
            }
            */

            /*
            scale = new bool[4];
            for(int i=0; i<2; i++)
            {
                scale[(1*i) % scale.Length] = true;
            }
            */

            /*
            scale = new bool[18];
            scale[0] = true;
            scale[5] = true;
            scale[10] = true;
            scale[13] = true;
            */
        }
        public override double vertical_interval_cost(int dp, int loc)
        {
            return interval_cost(dp);
        }
            /*
            public override double vertical_interval_cost(int dp)
            {
                if (dp < 0)
                {
                    dp = -dp;
                }
                if (dp % edo == 0)
                    return 100.0;

                if (dichotomy[0][dp % edo])
                    return 0.0;
                return 5000.0;
            }
            */
            /*
            public override double vertical_interval_cost(int dp, int loc)
            {
                if (dp < 0)
                {
                    dp = -dp;
                }
                int phase = 3;
                switch(loc%6)
                {
                    case 1:
                    case 4:
                        phase = 0;
                        break;
                    case 2:
                        phase = 1;
                        break;
                    case 3:
                        phase = 2;
                        break;
                    default:
                        break;
                }
                if (dp % edo == 0)
                    return 100.0;

                if (dichotomy[0][dp % edo])
                    return 0.0;
                return 5000.0;
            }
            */
        }
}
