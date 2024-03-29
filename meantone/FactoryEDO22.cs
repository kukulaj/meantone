﻿using System;
using System.Collections.Generic;
using System.Text;

namespace meantone
{
    public class FactoryEDO22 : FactoryEDO5
    {
        private bool[,] dichotomy;
        public FactoryEDO22(Type_Map map, bool[] pinc) : base(22, map, pinc)
        {
            commas = new Comma[12];
            pumps = new Pump[12];
            commas[0] = new Comma(this, new int[] { 3, 7, 0 });
            int[] path = new int[] {0, 13, 20, 11, 8, 3, 10, 1, 8, 15 };
            

            pumps[0] = new Pump(commas[0], path);

            commas[1] = new Comma(this, new int[] { 3, 7, 0 });
            int[] path1 = new int[] { 7, 20, 11, 18, 3, 16};


            pumps[1] = new Pump(commas[1], path1);

            commas[2] = new Comma(this, new int[] {4, 2 });
            pumps[2] = new Pump(commas[2]);

            commas[3] = new Comma(this, new int[] { 1, -5 });
            pumps[3] = new Pump(commas[3]);

            commas[4] = new Comma(this, new int[] { -4, -2 });
            pumps[4] = new Pump(commas[4]);

            commas[5] = new Comma(this, new int[] {-1, 5 });
            pumps[5] = new Pump(commas[5]);

            pumpStructure = new PumpStructureSimple(this, 2); 
                //new PumpStructureStripe(this, new int[] {2, 3, 4, 5 }, 0, 1);
                //new PumpStructureRandom(this);
            //PumpStructure b53 = new PumpStructureBig(this, 2);
            //pumpStructure = new PumpStructureSum(this, a53, b53);

            /*
            scale = new bool[11];
            scale[0] = true;
            scale[3] = true;
            scale[5] = true;
            scale[7] = true;
            scale[9] = true;
            */
            /*
            for (int i = 0; i < edo; i++)
            {
                scale[i] = false;
            }

            for (int i = 0; i < 14; i++)
            {
                scale[(5 * i) % edo] = true;
            }
            */

            dichotomy = new bool[3, edo];
            dichotomy[0, 0] = true;
            dichotomy[0, 13] = true; // 3:2
            dichotomy[0, 9] = true;  // 4:3
            dichotomy[0, 15] = true; // 8:5
            dichotomy[0, 7] = true;  // 5:4
            dichotomy[0, 6] = true;  // 6:5
            dichotomy[0, 16] = true; // 5:3
            dichotomy[0, 4] = true;  // 9:8 = 8:7
            dichotomy[0, 18] = true; // 16:9 = 7:4
            dichotomy[0, 11] = true; // 7:5 = 10:7
            dichotomy[0, 5] = true; // 7:6
            dichotomy[0, 17] = true; // 12:7

            //dichotomy[0, 48] = true;
            //dichotomy[0, 5] = true;
            //dichotomy[0, 44] = true;
            //dichotomy[0, 9] = true;



        }

        public override double vertical_interval_cost(int dp, int loc)
        {
            return interval_cost(dp);
            if (dp < 0)
            {
                dp = -dp;
            }

            int phase = 0;// loc % 2;
            /*
           if (loc % 12 == 11)
           {
               phase = 2;
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
        private bool inAScale(bool[] aScale, int pitch)
        {
            int period = aScale.Length;
            int rem = pitch % period;
            if (rem < 0)
            {
                rem = rem + period;
            }
            return aScale[rem];
        }

        public override bool inScale(int pitch, int loc)
        {

            bool result = true;


            //result = inAScale(scale, pitch
            //    - 6 * (loc % 5));


            return result;
        }
    }
}
