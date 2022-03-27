using System;
using System.Collections.Generic;
using System.Text;

namespace meantone
{
    public class FactoryEDO7 : FactoryEDO5
    {
        public FactoryEDO7(Type_Map map, bool[] pinc) : base(7, map, pinc)
        {
            pumpStructure = new PumpStructureRandom(this);
        }

        public override double vertical_interval_cost(int dp, int loc)
        {
            return interval_cost(dp);
        }
        public override bool inScale(int pitch, int loc)
        {
            return true;
        }
    }
}
