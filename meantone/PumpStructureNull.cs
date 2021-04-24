using System;
using System.Collections.Generic;
using System.Text;

namespace meantone
{
    public class PumpStructureNull : PumpStructure
    {
        public PumpStructureNull(FactoryEDO5 pf)
        {
            factory = pf;
        }
        public override int pattern(int loc)
        {
            return 0;
        }
    }
}
