using System;
using System.Collections.Generic;
using System.Text;

namespace meantone
{
    class PumpStructureRandom : PumpStructure
    {
        public PumpStructureRandom(FactoryEDO5 pf)
        {
            factory = pf;
        }
        public override int pattern(int loc)
        {
            return factory.map.rand.Next(factory.edo);
        }
    
    }
}
