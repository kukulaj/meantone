using System;
using System.Collections.Generic;
using System.Text;

namespace meantone
{
    public class FactoryEDO171 : FactoryEDO5
    {
        private bool[] dichotomy;
        public FactoryEDO171(Type_Map map, bool[] pinc) : base(171, map, pinc)
        {
            build_dichotomy2(9);

            commas = new Comma[10];
            pumps = new Pump[10];

            //comma3[0] = 19;
            //comma5[0] = -19;
            //comma7[0] = 0;
            commas[0] = new Comma(this, new int[] { 1, 2, -4 });
            pumps[0] = new Pump(commas[0]
            //  , new int[]
            //{0, 33, 66, 99, 154, 38, 71 }
            );
            commas[1] = new Comma(this, new int[] { 0, 3, 5 });
            pumps[1] = new Pump(commas[1], new int[]
                {0, 33, 66, 99, 44, 77, 22, 55 });
            //{44, 66, 88, 110, 132, 154, 5, 27, 49, 71, 93, 115 });
            commas[2] = new Comma(this, new int[] { 7, -4, -1 });

            int ofs = 26 * 36;
            pumps[2] = new Pump(commas[2], new int[]
                {66, 111, 40, 85, 130, 59, 104, 33 });
                //{33, 133, 78, 7, 123, 52, 168, 97, 42, 142, 71, 0 }); 
                //{ 129, 74, 3, 103, 48, 148, 77, 22, 122, 67, 167, 96});


            /*
                {
                    (0+ofs)%edo,
                    (126+ofs)%edo,
                    (26+ofs)%edo,
                    (152+ofs)%edo,
                    (52+ofs)%edo,
                    (7+ofs)%edo,
                    (78+ofs)%edo,
                    (33+ofs)%edo,
                }); 
            */
            commas[3] = new Comma(this, new int[] { 9, 0, -9 });
            pumps[3] = new Pump(commas[3]);
            commas[4] = new Comma(this, new int[] { -8, -1, 0 });
            pumps[4] = new Pump(commas[4], new int[]
                {90, 19, 119, 48, 148, 77, 6, 106, 35 });


            commas[5] = new Comma(this, new int[] { -2, -7, 3 });
            pumps[5] = new Pump(commas[5], new int[]
                {0, 55, 88, 143, 27, 127, 11, 44, 99, 154, 83, 116});

            commas[6] = new Comma(this, new int[] { 1, 5, 1 });
            pumps[6] = new Pump(commas[6]
                //,  new int[]
                //{110, 165, 49, 104, 159, 43}
                // { 0, 116, 61, 132, 77, 22, 138}
                //{129, 74, 19, 135, 35, 151, 96 }
                );

            commas[7] = new Comma(this, new int[] { 36, -12, 1 });
            pumps[7] = new Pump(commas[7], new int[]
                {74, 148, 51, 125, 28, 102, 5, 79, 153, 56, 130, 33, 107});

            /*
            commas[8] = new Comma(this, new int[] { 0, 6, 10 });
            pumps[8] = new Pump(commas[8], new int[]
                {110, 121, 132, 143, 154, 165});
            */

            commas[9] = new Comma(this, new int[] { -1, 1, 9 });
            pumps[9] = new Pump(commas[9], new int[]
                {66, 99, 132, 165, 27, 60, 93, 126, 159, 21});

            PumpStructure a171 = new PumpStructureBig(this, 0);
            PumpStructure b171 = new PumpStructureSimple(this, 6);
            pumpStructure = new PumpStructureSimple(this, 2);
            //pumpStructure = new PumpStructureTwo(this, 0, 6);


            scale = new bool[19];
            for (int i = 0; i < 8; i++)
            {
                scale[(7 * i) % scale.Length] = true;
            }


            /*
            for (int i = 0; i < edo; i++)
            {
                scale[i] = false;
            }
            for (int i = 0; i < 47; i++)
            {
                scale[(11 * i) % scale.Length] = true;
            }
            */
            /*
            scale[0] = true;
            scale[6] = true;
            scale[16] = true;
            scale[22] = true;
            scale[32] = true;
            scale[38] = true;
            scale[55] = true;
            scale[61] = true;
            scale[71] = true;
            scale[77] = true;
            scale[87] = true;
            scale[93] = true;
            scale[99] = true;
            scale[116] = true;
            scale[122] = true;
            scale[132] = true;
            scale[138] = true;
            scale[148] = true;
            scale[154] = true;
            */
            /*
            for (int i = 0; i < 53; i++)
            {
                scale[(100 * i) % edo] = true;
            }
            */

            /*
            scale[0] = true;
            scale[11] = true;
            scale[22] = true;
            scale[27] = true;
            scale[44] = true;
            scale[55] = true;
            scale[66] = true;
            scale[83] = true;
            scale[88] = true;
            scale[99] = true;
            scale[116] = true;
            scale[127] = true;
            scale[138] = true;
            scale[143] = true;
            scale[154] = true;
     */

        }
        private void build_dichotomy2(int odd_limit)
        {
            dichotomy = new bool[edo];

            dichotomy[0] = true;

            for (int odd = 3; odd <= odd_limit; odd += 2)
            {
                for (int odd2 = 1; odd2 < odd; odd2 += 2)
                {
                    double c = ((double)edo) * Math.Log(((double)odd) / ((double)odd2)) / Math.Log(2);
                    int k = (int)(0.5 + c);
                    k = k % edo;
                    Console.WriteLine(string.Format("{0} consonant", k));
                    dichotomy[k] = true;
                    dichotomy[edo - k] = true;
                }

            }
        }


        public override double vertical_interval_cost(int dp, int loc)
        {
            return interval_cost(dp);
            if (dp < 0)
            {
                dp = -dp;
            }


            if (dp % edo == 0)
            {
                return 100.0;
            }

            if (dichotomy[dp % edo])
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


            result = inAScale(scale, pitch);

            return result;
        }
    }
}
