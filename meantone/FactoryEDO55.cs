using System;
using System.Collections.Generic;
using System.Text;

namespace meantone
{
    public class FactoryEDO55 : FactoryEDO5
    {
        private bool[] dichotomy;
        public FactoryEDO55(Type_Map map, bool[] pinc) : base(55, map, pinc)
        {
            pumpStructure = new PumpStructureNull(this);

            pumpStructure = new PumpStructureNull(this);
            dichotomy = new bool[edo];
            dichotomy[0] = true;
            dichotomy[32] = true;
            dichotomy[23] = true;
            dichotomy[18] = true;
            dichotomy[37] = true;
            dichotomy[14] = true;
            dichotomy[41] = true;
            dichotomy[9] = true;
            dichotomy[46] = true;
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
