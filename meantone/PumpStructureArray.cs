using System;
using System.Collections.Generic;
using System.Text;

namespace meantone
{
    public class PumpStructureArray : PumpStructure
    {
        int[,] plan;
        public PumpStructureArray(FactoryEDO5 pf, int[,] pplan)
        {
            factory = pf;
            plan = pplan;

        }
        public override int pattern(int loc)
        {
            int rs = factory.map.row_size;
            int small = loc % rs;
            int big = (loc / rs) % rs;
            return plan[(small * plan.GetLength(0))/rs, (big * plan.GetLength(1)) / rs];
        }
    }
}
