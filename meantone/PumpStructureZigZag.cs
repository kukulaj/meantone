﻿using System;
using System.Collections.Generic;
using System.Text;

namespace meantone
{
    public class PumpStructureZigZag : PumpStructure
    {
        public int pi1;
        public int pi2;
        public PumpStructureZigZag(FactoryEDO5 pf, int ppi1, int ppi2)
        {
            factory = pf;
            pi1 = ppi1;
            pi2 = ppi2;
        }

        public override int pattern(int loc)
        {
            int pitch = 0;
            int rsize = factory.map.row_size;
            int chunk = factory.map.size / rsize;

            int big = loc / chunk;
            int small = loc % rsize;

            if (big < rsize / 4)
            {
                Pump pump = factory.pumps[pi1];
                pitch = pump.sequence[(small * pump.sequence.Length) / rsize];
            }
            else if (big < (rsize) / 2)
            {
                Pump pump = factory.pumps[pi2];
                pitch = pump.sequence[(small * pump.sequence.Length) / rsize];
            }
            else if (big < (3 * rsize) / 4)
            {
                Pump pump = factory.pumps[pi1];
                int len = pump.sequence.Length;
                pitch = pump.sequence[len - 1 - (small * len) / rsize];
            }
            else
            {
                Pump pump = factory.pumps[pi2];
                int len = pump.sequence.Length;
                pitch = pump.sequence[len - 1 - (small * len) / rsize];
            }
            return pitch;
        }
    }
}
