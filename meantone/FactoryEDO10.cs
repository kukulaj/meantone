using System;
using System.Collections.Generic;
using System.Text;

namespace meantone
{
    public class FactoryEDO10 : FactoryEDO5
    {
        private bool[] dichotomy;
        public FactoryEDO10(Type_Map map, bool[] pinc) : base(10, map, pinc)
        {

            pumpStructure = new PumpStructureNull(this);
            dichotomy = new bool[edo];
            dichotomy[0] = true;
            dichotomy[2] = true;
            dichotomy[3] = true;
            dichotomy[4] = true;
            dichotomy[6] = true;
            dichotomy[7] = true;
            dichotomy[8] = true;
        }
        public override double vertical_interval_cost(int dp, int loc)
        {
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
    }
}
