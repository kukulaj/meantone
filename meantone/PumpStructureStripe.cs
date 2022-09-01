using System;
using System.Collections.Generic;
using System.Text;

namespace meantone
{
    public class PumpStructureStripe : PumpStructure
    {
        int flow;
        int cut;
        Pump[] pumps;
        public PumpStructureStripe(FactoryEDO5 pf, int[] ppumps, int pflow, int pcut)
        {
            factory = pf;
            flow = pflow;
            cut = pcut;

            pumps = new Pump[ppumps.Length];

            for(int i = 0; i < ppumps.Length; i++)
            {
                pumps[i] = factory.pumps[ppumps[i]];
            }
        }

        public override int pattern(int loc)
        {
            int d = factory.map.dimension;
            int side = factory.map.row_size;

            int[] coord = new int[d];

            int cloc = loc;
            for(int i = 0; i < d; i++)
            {
                coord[i] = cloc % side;
                cloc = cloc / side;
            }
             
            int width = side / pumps.Length;
            int p = ((coord[cut] + width/2)  / width) % pumps.Length;

            if(pumps[p].sequence.Length == 0)
            {
                return 0;
            }

            int f = coord[flow] * pumps[p].sequence.Length / side;

            return pumps[p].sequence[f];
        }
    }
}
