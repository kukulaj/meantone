using System;
using System.Collections.Generic;
using System.Text;

namespace meantone
{
    class FactoryEDO99 : FactoryEDO5
    {
  
        public FactoryEDO99(Type_Map map, bool[] pinc) : base(99, map, pinc)
        {
            commas = new Comma[4];
            pumps = new Pump[4];
            commas[0] = new Comma(this, new int[] { 6, -1, 1 });
            pumps[0] = new Pump(commas[0]);
            commas[1] = new Comma(this, new int[] { 1, -3, -2});
            pumps[1] = new Pump(commas[1]);
            commas[2] = new Comma(this, new int[] { 0, -5, 2});
            pumps[2] = new Pump(commas[2]);
            commas[3] = new Comma(this, new int[] { 1, 2, -4});
            pumps[3] = new Pump(commas[3]);
            
            pumpStructure = 
                new PumpStructureStripe(this, new int[] { 0, 1, 2, 3 }, 0, 1);
            // new PumpStructureSum(this, p1, p2);

            build_dichotomy(9);
        }
    }
}
