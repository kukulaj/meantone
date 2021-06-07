﻿using System;
using System.Collections.Generic;
using System.Text;

namespace meantone
{
    public class FactoryEDO31 : FactoryEDO5
    {
        private bool[] dichotomy;

        public FactoryEDO31(Type_Map map, bool[] pinc) : base(31, map, pinc)
        {
            commas = new Comma[10];
            pumps = new Pump[10];


            commas[0] = new Comma(this, new int[] { 0, 5, -2 });
            pumps[0] = new Pump(commas[0]);
            commas[1] = new Comma(this, new int[] { 4, -1, 0 });
            pumps[1] = new Pump(commas[1]);
            commas[2] = new Comma(this, new int[] { 3, 7, 0 });
            pumps[2] = new Pump(commas[2]);
            commas[3] = new Comma(this, new int[] { 1, 0, 3 });
            pumps[3] = new Pump(commas[3]);
            commas[4] = new Comma(this, new int[] { 2, 2, -1 });
            pumps[4] = new Pump(commas[4]);
            commas[5] = new Comma(this, new int[] { 3, 1, -2, -1 });
            pumps[5] = new Pump(commas[5]);
            commas[6] = new Comma(this, new int[] { 4, -3, -1, 1 });
            pumps[6] = new Pump(commas[6]);
            commas[7] = new Comma(this, new int[] { 2, -3, 1 });
            pumps[7] = new Pump(commas[7], new int[] {0, 8, 16, 24 });
            commas[8] = new Comma(this, new int[] { 3, 4, -5 });
            pumps[8] = new Pump(commas[8]);
            commas[9] = new Comma(this, new int[] { 1, -8, 0 });
            pumps[9] = new Pump(commas[9], new int[] 
            {0, 18, 8, 29, 19, 6, 27, 17, 30, 20, 10 });
            PumpStructure a = new PumpStructureSimple(this, 7);
            PumpStructure b = new PumpStructureBig(this, 7);
            pumpStructure = new PumpStructureSimple(this, 9);

           
            for (int i = 0; i < edo; i++)
            {
                scale[i] = false;
            }

            dichotomy = new bool[edo];
            dichotomy[0] = true;
            dichotomy[18] = true;
            dichotomy[15] = true;

            dichotomy[10] = true;
            dichotomy[6] = true;
            dichotomy[17] = true;

            dichotomy[13] = true;
            dichotomy[21] = true;
            dichotomy[30] = true;

            /*
            dichotomy[8] = true;
            
            dichotomy[15] = true;
            
            dichotomy[27] = true;
            dichotomy[29] = true;
            dichotomy[20] = true;
            dichotomy[25] = true;
            dichotomy[19] = true;
            */
            
            
            
            for (int i = 0; i < 16; i++)
            {
                scale[(10 * i) % edo] = true;
            }
            

            /*
            scale = new bool[4];
            for(int i=0; i<2; i++)
            {
                scale[(1*i) % scale.Length] = true;
            }
            */

            /*
            scale = new bool[18];
            scale[0] = true;
            scale[5] = true;
            scale[10] = true;
            scale[13] = true;
            */
        }

        public override double vertical_interval_cost(int dp)
        {
            if (dp < 0)
            {
                dp = -dp;
            }
            if (dichotomy[dp % edo])
                return 0.0;
            return 3000.0;
        }
    }
}
