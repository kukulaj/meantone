using System;
using System.Collections.Generic;
using System.Text;

namespace meantone
{
    public class FactoryEDO27 : FactoryEDO5
    {
        public FactoryEDO27(Type_Map map, bool[] pinc) : base(27, map, pinc)
        {
            commas = new Comma[6];
            pumps = new Pump[6];
            commas[0] = new Comma(this, new int[] { 2, -3, 1 });
            pumps[0] = new Pump(commas[0], new int[]
            {14, 21, 1 });
            commas[1] = new Comma(this, new int[] { 5, -1, -2 });
            pumps[1] = new Pump(commas[1]);
            commas[2] = new Comma(this, new int[] { 0, -3, 0 });
            pumps[2] = new Pump(commas[2]);
            commas[3] = new Comma(this, new int[] { 9, -1 });
            pumps[3] = new Pump(commas[3], new int[]
            {9, 20, 4, 15, 26, 10, 21, 5, 16, 0 });
            commas[4] = new Comma(this, new int[] { -1, 5, -5 });
            pumps[4] = new Pump(commas[4], new int[]
            {0, 13, 26, 12, 25, 11});
            commas[5] = new Comma(this, new int[] { -2, 0, -1 });
            pumps[5] = new Pump(commas[5], new int[]
           {0, 11, 22});

            //pumpStructure = new PumpStructureSimple(tpumps[3] = new Pump(commas[3], new int[]
            PumpStructure a27 = new PumpStructureSimple(this, 4);
            PumpStructure b27 = new PumpStructureBig(this, 5);

            pumpStructure = new PumpStructureSum(this, a27, b27);

            /*
            for (int i = 0; i < edo; i++)
            {
                scale[i] = false;
            }

            for (int i = 0; i < 17; i++)
            {
                scale[(13 * i) % edo] = true;
            }
            */
        }
    }
}
