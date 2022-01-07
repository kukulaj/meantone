using System;
using System.Collections.Generic;
using System.Text;

namespace meantone
{
    public class FactoryEDO270 : FactoryEDO5
    {

        public FactoryEDO270(Type_Map map, bool[] pinc) : base(270, map, pinc)
        {
            build_dichotomy(11);
            pumpStructure = new PumpStructureNull(this);
        }
    }
}
