using System;
using System.Collections.Generic;
using System.Text;

namespace meantone
{
    class FactoryEDO46 : FactoryEDO5
    {
        public FactoryEDO46(Type_Map map, bool[] pinc) : base(46, map, pinc)
        {
            commas = new Comma[4];
            pumps = new Pump[4];
            commas[0] = new Comma(this, new int[] { 1, 1, 0, -2 });
            pumps[0] = new Pump(commas[0]);
            commas[1] = new Comma(this, new int[] { 1, 0, 3, 0 });
            pumps[1] = new Pump(commas[1]);
            commas[2] = new Comma(this, new int[] { 1, -1, -1, -1 });
            pumps[2] = new Pump(commas[1]);
            commas[3] = new Comma(this, new int[] { 0, -2, -1, 1 });
            pumps[3] = new Pump(commas[1]);
            PumpStructure p1 = new PumpStructureStripe(this, new int[] { 0, 1 }, 0, 2);
            PumpStructure p2 = new PumpStructureStripe(this, new int[] { 2, 3 }, 1, 3);
            pumpStructure = new PumpStructureSum(this, p1, p2);

            build_dichotomy(11);
        }
    }
}
