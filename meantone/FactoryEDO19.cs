using System;
using System.Collections.Generic;
using System.Text;

namespace meantone
{
    public class FactoryEDO19 : FactoryEDO5
    {
        public FactoryEDO19(Type_Map map, bool[] pinc) : base(19, map, pinc)
        {
            commas = new Comma[2];
            pumps = new Pump[2];
            commas[0] = new Comma(this, new int[] { 4, -1, 0 });
            pumps[0] = new Pump(commas[0]);
            commas[1] = new Comma(this, new int[] { 1, -5, 0 });
            pumps[1] = new Pump(commas[1]);
            pumpStructure = new PumpStructureSimple(this, 1);
        }
    }
}
