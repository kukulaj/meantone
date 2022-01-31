using System;
using System.Collections.Generic;
using System.Text;

namespace meantone
{
    class FactoryEDO53 : FactoryEDO5
    {
        private bool[] schismatic;
        private bool[] hanson;
        private bool[] diatonic;
        private bool[,] dichotomy;
        public FactoryEDO53(Type_Map map, bool[] pinc) : base(53, map, pinc)
        {
            commas = new Comma[5];
            pumps = new Pump[6];
            commas[0] = new Comma(this, new int[] { 5, -6, 0 });
            int[] path = new int[6];
            for(int i=0; i<6; i++)
            {
                path[i] = (i * 14 + 56) % edo;
            }
            
            pumps[0] = new Pump(commas[0]);


            commas[1] = new Comma(this, new int[] { 8, 1, 0 });
            pumps[1] = new Pump(commas[1],
               new int[] { 9, 40, 18, 49, 27, 5, 36, 14, 45 });
            commas[2] = new Comma(this, new int[] { 3, 7, 0 });
            pumps[2] = new Pump(commas[2]);

            commas[3] = new Comma(this, new int[] {-2, -2, 1 });
            pumps[3] = new Pump(commas[3],
               new int[] { 0, 31, 48, 26, 43 });
            pumps[4] = new Pump(commas[3],
               new int[] {36, 14,  24, 2, 19 });

            commas[4] = new Comma(this, new int[] { 1, 1, 0, 0, 3 });
            pumps[5] = new Pump(commas[4]);
               

            pumpStructure = new PumpStructureRandom(this);
            //PumpStructure b53 = new PumpStructureBig(this, 2);
            //pumpStructure = new PumpStructureSum(this, a53, b53);

            hanson= new bool[53];
            diatonic = new bool[53];

            diatonic[0] = true;
            diatonic[9] = true;
            diatonic[17] = true;
            diatonic[22] = true;
            diatonic[31] = true;
            diatonic[40] = true;
            diatonic[48] = true;

            for (int i = 0; i < 19; i++)
            { 
                hanson[(i*14)%53] = true;
            }


            /*
            scale = new bool[14];
            scale[0] = true;
            scale[3] = true;
            scale[11] = true;
            */

            /*
            for (int i = 0; i < edo; i++)
            {
                scale[i] = false;
            }
            */

            schismatic = new bool[53];
            for (int i = 0; i < 17; i++)
            {
                schismatic[(31 * i) % edo] = true;
            }
            


            dichotomy = new bool[3,edo];
            dichotomy[0,0] = true;
            dichotomy[0,31] = true;
            dichotomy[0,22] = true;
            dichotomy[0,14] = true;
            dichotomy[0,39] = true;
            dichotomy[0,17] = true;
            dichotomy[0,36] = true;
            dichotomy[0, 9] = true;
            dichotomy[0, 44] = true;

            /*
            dichotomy[0, 10] = true;
            dichotomy[0, 43] = true;
            dichotomy[0, 26] = true;
            dichotomy[0, 27] = true;
            dichotomy[0, 12] = true;
            dichotomy[0, 41] = true;
            */

            //dichotomy[0, 48] = true;
            //dichotomy[0, 5] = true;
            //dichotomy[0, 44] = true;
            //dichotomy[0, 9] = true;

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
             if(dp%edo == 0)
            {
                return 100.0;
            }

            if (dichotomy[phase, dp % edo])
                return 0.0;
            return 5000.0;
        }

        private  bool inAScale(bool[] aScale, int pitch)
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
            return inAScale(schismatic, pitch);

            bool result = true;
            switch ((loc / 27) % 27)
            {
                case 0:
                    result = inAScale(diatonic, pitch);
                    break;
                case 7:
                case 8:
                    result = inAScale(diatonic, pitch - 28);
                    break;
                case 15:
                case 16:
                    result = inAScale(diatonic, pitch-3);
                    break;
                case 23:
                case 24:
                case 25:
                case 26:
                    result = inAScale(diatonic, pitch);
                    break;
                
                case 1:
                case 6:
                case 9:
                case 14:
                case 17:
                case 22:
                    result = true;
                    break;
                default:
                    result = inAScale(hanson, pitch);
                    break;

            }
            return result;
        }
            
    
    }
}
