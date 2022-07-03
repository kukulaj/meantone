using System;
using System.Collections.Generic;
using System.Text;

namespace meantone
{
    public class FactoryEDO270 : FactoryEDO5
    {
        bool[] synthetic;
        public FactoryEDO270(Type_Map map, bool[] pinc) : base(270, map, pinc)
        {
            synthetic = new bool[27];
            for(int i = 0; i < 11; i++)
            {
                synthetic[(i * 10) % synthetic.Length] = true;
            }


            build_dichotomy(11);
            commas = new Comma[4];
            pumps = new Pump[4];
            commas[0] = new Comma(this, new int[] { 2, 2, 1, 3 });
            pumps[0] = new Pump(commas[0]);
            commas[1] = new Comma(this, new int[] { -1, -2, -4, 1 });
            pumps[1] = new Pump(commas[1]);
            commas[2] = new Comma(this, new int[] { 5, 0, 2, 1});
            pumps[2] = new Pump(commas[2]);
            commas[3] = new Comma(this, new int[] { 3, -2, 1, -2 });
            pumps[3] = new Pump(commas[3]);

            PumpStructure ps1 = new PumpStructureTwoAcross(this, 1, 3);
            PumpStructure ps2 = new PumpStructureTwo(this, 0, 2);

            pumpStructure = new PumpStructureSum(this, ps1, ps2);
        }

        /*
        public override double vertical_interval_cost(int dp, int loc)
        {
            return interval_cost(dp);
        }
        */

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
            return true;
           // return inAScale(synthetic, pitch);
        }

        }



    }
