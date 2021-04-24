using System;
using System.Collections.Generic;
using System.Text;

namespace meantone
{
    public abstract class PumpStructure
    {
        public FactoryEDO5 factory;

        public abstract int pattern(int loc);
    }
}
