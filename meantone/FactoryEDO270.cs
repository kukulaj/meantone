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
            commas = new Comma[1];
            pumps = new Pump[1];
            commas[0] = new Comma(this, new int[] { 2, 2, 1, 3 });
            pumps[0] = new Pump(commas[0]);

            pumpStructure = new PumpStructureSimple(this, 0);
        }
    }
}
