using System;
using System.Collections.Generic;
using System.Text;

namespace meantone
{
    class PumpStructureBig : PumpStructure
    {
        int pumpi;
        public PumpStructureBig(FactoryEDO5 pf, int ppi)
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

            Pump pump = factory.pumps[pumpi];
            pitch = pump.sequence[(big * pump.sequence.Length) / rsize];

            return pitch;
        }
    }
}
