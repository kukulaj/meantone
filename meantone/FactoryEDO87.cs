using System;
using System.Collections.Generic;
using System.Text;

namespace meantone
{
    class FactoryEDO87 : FactoryEDO5
    {
        public FactoryEDO87(Type_Map map, bool[] pinc) : base(87, map, pinc)
        {
            commas = new Comma[6];
            pumps = new Pump[6];
            commas[0] = new Comma(this, new int[] { 5, -6, 0 });
            pumps[0] = new Pump(commas[0]);
            commas[1] = new Comma(this, new int[] { 2, 15, 0 });
            pumps[1] = new Pump(commas[1]);
            commas[2] = new Comma(this, new int[] { -2, 1, -2, 1 });
            pumps[2] = new Pump(commas[2]);
            commas[3] = new Comma(this, new int[] { 1, -1, -1, -1 });
            pumps[3] = new Pump(commas[3]);
            commas[4] = new Comma(this, new int[] { -2, -4, 0, 1 });
            pumps[4] = new Pump(commas[4]);
            commas[5] = new Comma(this, new int[] { -3, 1, -1, -1, 1 });
            pumps[5] = new Pump(commas[5],
               // new int[] {0, 28, 45, 19, 66, 15, 51 });
               new int[] { 0, 28, 45, 19, 66, 15, 51 });
            pumpStructure = new PumpStructureSimple(this, 5);
            //PumpStructure b87 = new PumpStructureBig(this, 3);
            //pumpStructure = new PumpStructureSum(this, a87, b87);

            /*
            scale = new bool[29];
            for(int i = 0; i<5; i++)
            {
                scale[(7 * i) % 29] = true;
            }
            */

            scale = new bool[3];
            scale[0] = true;
            scale[1] = true;
        }
    }
}
