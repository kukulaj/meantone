using System;
using System.Collections.Generic;
using System.Text;

namespace meantone
{
    public class PumpStructureOffOnOff : PumpStructure
    {
        int pumpi;
        public PumpStructureOffOnOff(FactoryEDO5 pf, int ppi)
        {
            factory = pf;
            pumpi = ppi;
        }
        public override int pattern(int loc)
        {
            int pitch = 0;
            int rsize = factory.map.row_size;
            int chunk = factory.map.size / rsize;

            int big = loc / chunk;
            int small = loc % rsize;

            if (big > rsize / 4
                && big < (3 * rsize) / 4)
            {
                Pump pump = factory.pumps[pumpi];
                pitch = pump.sequence[(small * pump.sequence.Length) / rsize];
            }

            return pitch;
        }
    }
}
