using System;
using System.Collections.Generic;
using System.Text;

namespace meantone
{
    public class FactoryEDO171 : FactoryEDO5
    {
        private bool[,] dichotomy;
        public FactoryEDO171(Type_Map map, bool[] pinc) : base(171, map, pinc)
        {
            commas = new Comma[10];
            pumps = new Pump[10];

            //comma3[0] = 19;
            //comma5[0] = -19;
            //comma7[0] = 0;
            commas[0] = new Comma(this, new int[] { 1, 2, -4 });
            pumps[0] = new Pump(commas[0]);
            commas[1] = new Comma(this, new int[] { 0, 3, 5 });
            pumps[1] = new Pump(commas[1], new int[]
            {44, 66, 88, 110, 132, 154, 5, 27, 49, 71, 93, 115 });
            commas[2] = new Comma(this, new int[] { 7, -4, -1 });
            pumps[2] = new Pump(commas[2], new int[]
            {104, 33, 59, 85, 111, 137 });
            commas[3] = new Comma(this, new int[] { 9, 0, -9 });
            pumps[3] = new Pump(commas[3]);
            commas[4] = new Comma(this, new int[] { -8, -1, 0 });
            pumps[4] = new Pump(commas[4]);
            commas[5] = new Comma(this, new int[] { -2, -7, 3 });
            pumps[5] = new Pump(commas[5], new int[]
                {0, 55, 88, 143, 27, 127, 11, 44, 99, 154, 83, 116});

            commas[6] = new Comma(this, new int[] { 1, 5, 1 });
            pumps[6] = new Pump(commas[6], new int[]
                {110, 165, 49, 104, 159, 43});

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

            //  PumpStructure a171 = new PumpStructureBig(this, 4);
            // PumpStructure b171 = new PumpStructureSimple(this, 1);
            // pumpStructure = new PumpStructureSum(this, a171, b171);
            //pumpStructure = new PumpStructureSimple(this, 2);

            /*
            scale = new bool[19];
            for (int i = 0; i < 3; i++)
            {
                scale[(7 * i) % scale.Length] = true;
            }
            */

            
            for (int i = 0; i < edo; i++)
            {
                scale[i] = false;
            }
            
            
            for (int i = 0; i < 33; i++)
            {
                scale[(26 * i) % edo] = true;
            }


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
            dichotomy = new bool[1, edo];
            dichotomy[0, 0] = true;
            dichotomy[0, 55] = true;
            dichotomy[0, 116] = true;
            dichotomy[0, 100] = true;
            dichotomy[0, 71] = true;
            dichotomy[0, 45] = true;
            dichotomy[0, 126] = true;
            dichotomy[0, 38] = true;
            dichotomy[0, 133] = true;
            dichotomy[0, 83] = true;
            dichotomy[0, 88] = true;
            dichotomy[0, 138] = true;
            dichotomy[0, 33] = true;            
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
