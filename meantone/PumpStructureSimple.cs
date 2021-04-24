using System;
using System.Collections.Generic;
using System.Text;

namespace meantone
{
    public class PumpStructureSimple : PumpStructure
    {
        int pumpi;
        public PumpStructureSimple(FactoryEDO5 pf, int ppi)
        {
            factory = pf;
            pumpi = ppi;
        }
        public override int pattern(int loc)
        {
            int pitch = 0;
            int rsize = factory.map.row_size;

            int small = loc % rsize;

            Pump pump = factory.pumps[pumpi];
            pitch = pump.sequence[(small * pump.sequence.Length) / rsize];

            return pitch;
        }
    }
}
