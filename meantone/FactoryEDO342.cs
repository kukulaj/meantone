using System;
using System.Collections.Generic;
using System.Text;

namespace meantone
{
    public class FactoryEDO342 : FactoryEDO5
    {
        public FactoryEDO342(Type_Map map, bool[] pinc) : base(342, map, pinc)
        {
            commas = new Comma[3];
            pumps = new Pump[3];
            commas[0] = new Comma(this, new int[] { 2, 2, -7, 4 });
            pumps[0] = new Pump(commas[0]);

            commas[1] = new Comma(this, new int[] { 8, 1 });
            pumps[1] = new Pump(commas[1], new int[]
            {0, 142, 284, 84, 226, 26, 168, 310, 110 });
            pumps[2] = new Pump(commas[1], new int[]
           {0, 71, 142, 213, 284, 13 });

            PumpStructure a342 = new PumpStructureSimple(this, 1);
            PumpStructure b342 = new PumpStructureBig(this, 2);
            pumpStructure = new PumpStructureSum(this, a342, b342);

            /*
            for (int i = 0; i < edo; i++)
            {
                scale[i] = false;
            }
            for (int i = 0; i < 24; i++)
            {
                scale[(71 * i) % edo] = true;
            }
            */
        }
    }
}
