using System;
using System.Collections.Generic;
using System.Text;

namespace meantone
{
    public class FactoryEDO118 : FactoryEDO5
    {
        public FactoryEDO118(Type_Map map, bool[] pinc) : base(118, map, pinc)
        {
            commas = new Comma[4];
            pumps = new Pump[4];
            commas[0] = new Comma(this, new int[] { 8, 1, 0 });
            pumps[0] = new Pump(commas[0], new int[]
            {27, 7, 105, 85, 65 });
            commas[1] = new Comma(this, new int[] { 2, 15, 0 });
            pumps[1] = new Pump(commas[1]);
            commas[2] = new Comma(this, new int[] { 6, -14, 0 });
            pumps[2] = new Pump(commas[2], new int[]
            {0, 80, 42, 73, 35, 66, 28, 59, 21, 101, 14, 94, 7, 87 });
            commas[3] = new Comma(this, new int[] { 14, -13, 0 });
            pumps[3] = new Pump(commas[3], new int[]
                {0, 31, 62, 93, 6, 37, 68, 99, 12, 43, 74, 105, 18, 49 });

            //PumpStructure a118 = new PumpStructureSimple(this, 1);
            //PumpStructure b118 = new PumpStructureBig(this, 2);
            //pumpStructure = new PumpStructureSum(this, a118, b118);
            pumpStructure = new PumpStructureSimple(this, 2);

            /*
            for (int i = 0; i < edo; i++)
            {
                scale[i] = false;
            }
            for (int i = 0; i < 35; i++)
            {
                scale[(27 * i) % edo] = true;
            }
            */

            scale = new bool[59];
            for (int i = 0; i < 17; i++)
            {
                scale[(7 * i) % 59] = true;
            }
            /*
            scale[0] = true;
            scale[6] = true;
            scale[12] = true;
            scale[18] = true;
            scale[25] = true;
            */
        }
    }
}
