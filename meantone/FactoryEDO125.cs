using System;
using System.Collections.Generic;
using System.Text;

namespace meantone
{
    class FactoryEDO125 : FactoryEDO5
    {
        public FactoryEDO125(Type_Map map, bool[] pinc) : base(125, map, pinc)
        {
            commas = new Comma[4];
            pumps = new Pump[4];
             
            pumpStructure = new PumpStructureRandom(this);

            scale = new bool[9];
            scale[0] = true;

            build_dichotomy(9);
        }

    }
}
