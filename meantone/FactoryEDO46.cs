using System;
using System.Collections.Generic;
using System.Text;

namespace meantone
{
    class FactoryEDO46 : FactoryEDO5
    {
        public FactoryEDO46(Type_Map map, bool[] pinc) : base(46, map, pinc)
        {
            commas = new Comma[2];
            pumps = new Pump[2];
            commas[0] = new Comma(this, new int[] { 1, 1, 0, -2 });
            pumps[0] = new Pump(commas[0]);
            commas[1] = new Comma(this, new int[] { 1, 0, 3, 0 });
            pumps[1] = new Pump(commas[1]);
            pumpStructure = new PumpStructureTwo(this, 0, 1);

            build_dichotomy(11);
        }
    }
}
