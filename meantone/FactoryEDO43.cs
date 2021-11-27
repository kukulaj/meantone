using System;
using System.Collections.Generic;
using System.Text;

namespace meantone
{
    public class FactoryEDO43 : FactoryEDO5
    {
        private bool[,] dichotomy;
        public FactoryEDO43(Type_Map map, bool[] pinc) : base(43, map, pinc)
        {
            commas = new Comma[2];
            pumps = new Pump[2];
            
            pumpStructure = new PumpStructureNull(this);

             
            dichotomy = new bool[1, edo];
            dichotomy[0, 0] = true;
            dichotomy[0, 18] = true;
            dichotomy[0, 25] = true;
            dichotomy[0, 14] = true;
            dichotomy[0, 29] = true;
            dichotomy[0, 11] = true;
            dichotomy[0, 32] = true;
            dichotomy[0, 7] = true;
            dichotomy[0, 36] = true;
            

        }

        public override double vertical_interval_cost(int dp, int loc)
        {
            if (dp < 0)
            {
                dp = -dp;
            }

            int phase = 0;
            /*
            if ((loc+1) % map.row_size == 0)
            {
                phase = 1;
            }
            */
            if (dp % edo == 0)
            {
                return 100.0;
            }

            if (dichotomy[phase, dp % edo])
                return 0.0;
            return 5000.0;
        }
    }
    }
