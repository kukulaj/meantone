using System;
using System.Collections.Generic;
using System.Text;

namespace meantone
{
    class PumpStructureSum : PumpStructure
    {
        PumpStructure a;
        PumpStructure b;
        public PumpStructureSum(FactoryEDO5 pf, PumpStructure pa, PumpStructure pb)
        {
            factory = pf;
            a = pa;
            b = pb;
        }
        public override int pattern(int loc)
        {
            int pitch = 0;
            pitch = a.pattern(loc) + b.pattern((loc+1) % factory.map.size);
            pitch = pitch % factory.edo;

            return pitch;
        }
    
    }
}
