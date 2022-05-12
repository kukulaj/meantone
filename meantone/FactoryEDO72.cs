using System;
using System.Collections.Generic;
using System.Text;

namespace meantone
{
    class FactoryEDO72 : FactoryEDO5
    {

        public FactoryEDO72(Type_Map map, bool[] pinc) : base(72, map, pinc)
        {

            
           for (int i = 0; i < edo; i++)
           {
               scale[i] = false;
           }
           
           for(int i = 0; i < 21; i++)
            {
                scale[(i * 7) % edo] = true;
            }

            pumpStructure = new PumpStructureRandom(this);
        }
        public override double vertical_interval_cost(int dp, int loc)
        {
            return interval_cost(dp);
        }
    }
    
}
