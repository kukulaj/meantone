using System;
using System.Collections.Generic;
using System.Text;

namespace meantone
{
    public class FactoryEDO19 : FactoryEDO5
    {
        private bool[,] dichotomy;
        bool[] diatonic;
        bool[] chromatic;
        bool[] negri;
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
            pumpStructure = new PumpStructureRandom(this);

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

            /*
            for (int i = 0; i < edo; i++)
            {
                scale[i] = false;
            }

            for (int i = 0; i < 10; i++)
            {
                scale[(2 * i) % edo] = true;
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


            diatonic = new bool[19];
            for(int i = 0; i < 7; i++)
            {
                diatonic[(11 * i)%19] = true;
            }


            chromatic = new bool[19];
            for (int i = 0; i < 12; i++)
            {
                chromatic[(11 * i)%19] = true;
            }


            negri = new bool[19];
            for (int i = 0; i < 10; i++)
            {
                negri[(2 * i)%19] = true;
            }


        }

        public override double vertical_interval_cost(int dp, int loc)
        {
            return interval_cost(dp);;
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

        private bool inAScale(bool[] aScale, int pitch)
        {
            int period = aScale.Length;
            int rem = pitch % period;
            if (rem < 0)
            {
                rem = rem + period;
            }
            return aScale[rem];
        }

        public override bool inScale(int pitch, int loc)
        {

            bool result = true;
            int col = (loc / 9) % 9;

            switch ((loc / 81) % 9)
            {
                case 0:
                case 8:
                    result = inAScale(chromatic, pitch);
                    /*
                    switch(col)
                    {
                        case 2:
                        case 4:
                        case 7:
                            result = inAScale(diatonic, pitch-11);
                            break;
                        case 5:
                        case 6:
                            result = inAScale(diatonic, pitch - 3);
                            break;
                        default:
                            result = inAScale(diatonic, pitch);
                            break;
                    }
                    */
                    break;
                case 2:
                    result = inAScale(chromatic, pitch - 6);
                    break;
                case 4:
                    result = inAScale(chromatic, pitch - 4);
                    /*
                    switch (col)
                    {
                        case 2:
                        case 4:
                        case 7:
                            result = inAScale(chromatic, pitch - 11);
                            break;
                        case 5:
                        case 6:
                            result = inAScale(chromatic, pitch - 3);
                            break;
                        default:
                            result = inAScale(chromatic, pitch);
                            break;
                    }
                    */
                    break;

                case 6:
                    result = inAScale(chromatic, pitch - 2);
                    /*
                    switch (col)
                    {
                        case 1:
                        case 7:
                            result = inAScale(diatonic, pitch - 12);
                            break;
                        case 3:
                        case 5:
                            result = inAScale(diatonic, pitch - 15);
                            break;
                        case 4:
                            result = inAScale(diatonic, pitch - 7);
                            break;
                        default:
                            result = inAScale(diatonic, pitch-4);
                            break;
                    }
                    */
                    break;
                default:
                    result = inAScale(negri, pitch-17);
                    break;
            }
            return result;
        }

    }
}
