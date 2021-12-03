using System;
using System.Collections.Generic;
using System.Text;

namespace meantone
{
    public class FactoryEDO342 : FactoryEDO5
    {
        private bool[] dichotomy;
        public FactoryEDO342(Type_Map map, bool[] pinc) : base(342, map, pinc)
        {
            build_dichotomy(11);

            commas = new Comma[3];
            pumps = new Pump[3];
            commas[0] = new Comma(this, new int[] { 2, 2, -7, 4 });
            pumps[0] = new Pump(commas[0]);

            commas[1] = new Comma(this, new int[] { 8, 1 });
            pumps[1] = new Pump(commas[1], new int[]
            {0, 142, 284, 84, 226, 26, 168, 310, 110 });
            commas[2] = new Comma(this, new int[] { -4, -3, -2, 2});
            pumps[2] = new Pump(commas[2]);
           //{0, 71, 142, 213, 284, 13 });

            PumpStructure a342 = new PumpStructureSimple(this, 1);
            PumpStructure b342 = new PumpStructureBig(this, 2);
            pumpStructure = new PumpStructureSimple(this, 2);

            
            for (int i = 0; i < edo; i++)
            {
                scale[i] = false;
            }

            scale[0] = true;
            scale[11] = true;
            scale[32] = true;
            scale[52] = true;
            scale[55] = true;
            scale[66] = true;
            scale[87] = true;
            scale[98] = true;
            scale[110] = true;
            scale[122] = true;
            scale[142] = true;
            scale[153] = true;
            scale[156] = true;
            scale[176] = true;
            scale[197] = true;
            scale[208] = true;
            scale[229] = true;
            scale[240] = true;
            scale[252] = true;
            scale[263] = true;
            scale[284] = true;
            scale[287] = true;
            scale[298] = true;
            scale[310] = true;
            scale[321] = true;

            /*
            for (int i = 0; i < 24; i++)
            {
                scale[(71 * i) % edo] = true;
            }
            */
        }

        private void build_dichotomy(int odd_limit)
        {
            dichotomy = new bool[edo];

            dichotomy[0] = true;

            for(int odd = 3; odd <= odd_limit; odd += 2)
            {
                for (int odd2 = 1; odd2 < odd; odd2 += 2)
                {
                   double c =   ((double)edo)* Math.Log( ((double)odd) / ((double)odd2))/ Math.Log(2);
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
            if (dp < 0)
            {
                dp = -dp;
            }

            if (dp % edo == 0)
            {
                return 85.0;
            }

            if (dichotomy[dp % edo])
                return 0.0;
            return 5000.0;
        }
    }
}
