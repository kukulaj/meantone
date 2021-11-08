using System;
using System.Collections.Generic;
using System.Text;

namespace meantone
{
    public class FactoryEDO22 : FactoryEDO5
    {
        private bool[,] dichotomy;
        public FactoryEDO22(Type_Map map, bool[] pinc) : base(22, map, pinc)
        {
            commas = new Comma[2];
            pumps = new Pump[2];
            commas[0] = new Comma(this, new int[] { 3, 7, 0 });
            int[] path = new int[] {0, 13, 20, 11, 8, 3, 10, 1, 8, 15 };
            

            pumps[0] = new Pump(commas[0], path);

            commas[1] = new Comma(this, new int[] { 3, 7, 0 });
            int[] path1 = new int[] { 7, 20, 11, 18, 3, 16};


            pumps[1] = new Pump(commas[1], path1);


            pumpStructure = new PumpStructureSimple(this, 1);
            //PumpStructure b53 = new PumpStructureBig(this, 2);
            //pumpStructure = new PumpStructureSum(this, a53, b53);

            
            scale = new bool[11];
            scale[0] = true;
            scale[3] = true;
            scale[5] = true;
            scale[7] = true;
            scale[9] = true;

            /*
            for (int i = 0; i < edo; i++)
            {
                scale[i] = false;
            }

            for (int i = 0; i < 13; i++)
            {
                scale[(5 * i) % edo] = true;
            }
            */
            dichotomy = new bool[3, edo];
            dichotomy[0, 0] = true;
            dichotomy[0, 13] = true;
            dichotomy[0, 9] = true;
            dichotomy[0, 15] = true;
            dichotomy[0, 7] = true;
            dichotomy[0, 6] = true;
            dichotomy[0, 16] = true;
            //dichotomy[0, 48] = true;
            //dichotomy[0, 5] = true;
            //dichotomy[0, 44] = true;
            //dichotomy[0, 9] = true;

            

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
            if (dp % edo == 0)
            {
                return 100.0;
            }

            if (dichotomy[phase, dp % edo])
                return 0.0;
            return 5000.0;
        }
    }
}
