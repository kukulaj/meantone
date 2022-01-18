using System;
using System.Collections.Generic;
using System.Text;

namespace meantone
{
    public class FactoryEDO270 : FactoryEDO5
    {

        public FactoryEDO270(Type_Map map, bool[] pinc) : base(270, map, pinc)
        {
            build_dichotomy(13);
            commas = new Comma[4];
            pumps = new Pump[4];
            commas[0] = new Comma(this, new int[] { 2, 2, 1, 3 });
            pumps[0] = new Pump(commas[0]);
            commas[1] = new Comma(this, new int[] { 0, 1, 0, 3, -1 });
            pumps[1] = new Pump(commas[0]);
            commas[2] = new Comma(this, new int[] { 1, 2, -4});
            pumps[2] = new Pump(commas[0]);
            commas[3] = new Comma(this, new int[] { 2, 1, 1, 0, 1 });
            pumps[3] = new Pump(commas[0]);

            PumpStructure ps1 = new PumpStructureTwoAcross(this, 1, 3);
            PumpStructure ps2 = new PumpStructureSimple(this, 2);

            pumpStructure = new PumpStructureRandom(this);
        }
        public override double vertical_interval_cost(int dp, int loc)
        {
            return interval_cost(dp);
        }
    }
}
