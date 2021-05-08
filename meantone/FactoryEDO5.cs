using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace meantone
{
    public class FactoryEDO5 : VectorFactory
    {
        public double[] intervals;
        public double[] best;
        public bool[] scale;
        
        public int icnt;
        public int edo;
        public int ed3;
        public int ed5;
        public int ed7;
        public int ed11;
        public int[] harmonics;
        public int[] comma3;
        public int[] comma5;
        public int[] comma7;
        public Type_Map map;
        public Comma[] commas;
        public Pump[] pumps;
        public PumpStructure pumpStructure;

        public FactoryEDO5(int e, Type_Map tmap)
        {
            map = tmap;
            edo = e;
            ed3 = (int)(0.5 + (((float)edo) * Math.Log(3.0) / Math.Log(2.0)));
            ed5 = (int)(0.5 + (((float)edo) * Math.Log(5.0) / Math.Log(2.0)));
            ed7 = (int)(0.5 + (((float)edo) * Math.Log(7.0) / Math.Log(2.0)));
            ed11 = (int)(0.5 + (((float)edo) * Math.Log(11.0) / Math.Log(2.0)));
            harmonics = new int[4];
            harmonics[0] = ed3;
            harmonics[1] = ed5;
            harmonics[2] = ed7;
            harmonics[3] = ed11;

            int ccnt = 3;
            comma3 = new int[ccnt];
            comma5 = new int[ccnt];
            comma7 = new int[ccnt];

            scale = new bool[edo];
            for(int i = 0; i< edo; i++)
            {
                scale[i] = true;
            }

            pumpStructure = new PumpStructureNull(this);
            switch (edo)
            {
                case 10:
                    comma3[0] = 4;
                    comma5[0] = 2;
                    comma3[1] = 1;
                    comma5[1] = -2;
                    commas = new Comma[2];
                    pumps = new Pump[2];
                    commas[0] = new Comma(this, new int[] { 4, -2, -1 });
                    pumps[0] = new Pump(commas[0]);
                    commas[1] = new Comma(this, new int[] { 4, 2, 0 });
                    pumps[1] = new Pump(commas[1]);
                    pumpStructure = new PumpStructureSimple(this, 0);
                    break;
                case 12:
                    comma3[0] = 4;
                    comma5[0] = -1;
                    comma3[1] = 0;
                    comma5[1] = 3;
                    commas = new Comma[2];
                    pumps = new Pump[2];
                    commas[0] = new Comma(this, new int[] { 4, -1, 0 });
                    pumps[0] = new Pump(commas[0]);
                    commas[1] = new Comma(this, new int[] { 0, 3, 0 });
                    pumps[1] = new Pump(commas[1]);
                    PumpStructure  a = new PumpStructureSimple(this, 0);
                    PumpStructure b = new PumpStructureBig(this, 1);
                    pumpStructure = new PumpStructureSum(this, a, b);
                    break;

                case 15:
                    commas = new Comma[2];
                    pumps = new Pump[2];
                    commas[0] = new Comma(this, new int[] { 5, 0, 0 });
                    pumps[0] = new Pump(commas[0]);
                    commas[1] = new Comma(this, new int[] { 0, 3, 0 });
                    pumps[1] = new Pump(commas[1]);
                    pumpStructure = new PumpStructureSimple(this, 0);
                    break;
                case 19:
                    comma3[0] = 4;
                    comma5[0] = -1;
                    comma3[1] = 1;
                    comma5[1] = -5;

                    commas = new Comma[2];
                    pumps = new Pump[2];
                    commas[0] = new Comma(this, new int[] { 4, -1, 0 });
                    pumps[0] = new Pump(commas[0]);
                    commas[1] = new Comma(this, new int[] { 1, -5, 0 });
                    pumps[1] = new Pump(commas[1]);
                    pumpStructure = new PumpStructureSimple(this, 1);
                    break;
                case 22:
                    comma3[0] = 4;
                    comma5[0] = 2;
                    comma3[1] = 1;
                    comma5[1] = -5;
                    commas = new Comma[3];
                    pumps = new Pump[3];
                    commas[0] = new Comma(this, new int[] { 4, 2, 0 });
                    pumps[0] = new Pump(commas[0]);
                    commas[1] = new Comma(this, new int[] { 1, -5, 0 });
                    pumps[1] = new Pump(commas[1]);
                    commas[2] = new Comma(this, new int[] { 5, -3, 0 });
                    pumps[2] = new Pump(commas[2]);
                    pumpStructure = new PumpStructureSimple(this, 2);
                    break;
                case 27:
                    commas = new Comma[6];
                    pumps = new Pump[6];
                    commas[0] = new Comma(this, new int[] { 2, -3, 1 });
                    pumps[0] = new Pump(commas[0],  new int[] 
                    {14, 21, 1 });
                    commas[1] = new Comma(this, new int[] { 5, -1, -2 });
                    pumps[1] = new Pump(commas[1]);
                    commas[2] = new Comma(this, new int[] { 0, -3, 0 });
                    pumps[2] = new Pump(commas[2]);
                    commas[3] = new Comma(this, new int[] {9, -1 });
                    pumps[3] = new Pump(commas[3], new int[]
                    {9, 20, 4, 15, 26, 10, 21, 5, 16, 0 });
                    commas[4] = new Comma(this, new int[] { -1, 5, -5 });
                    pumps[4] = new Pump(commas[4], new int[]
                    {0, 13, 26, 12, 25, 11});
                    commas[5] = new Comma(this, new int[] { -2, 0, -1 });
                    pumps[5] = new Pump(commas[5], new int[]
                   {0, 11, 22});

                    //pumpStructure = new PumpStructureSimple(tpumps[3] = new Pump(commas[3], new int[]
                    PumpStructure a27 = new PumpStructureSimple(this, 4);
                    PumpStructure b27 = new PumpStructureBig(this, 5);

                    pumpStructure = new PumpStructureSum(this, a27, b27);


                    for (int i = 0; i < edo; i++)
                    {
                        scale[i] = false;
                    }

                    for (int i = 0; i < 17; i++)
                    {
                        scale[(13 * i) % edo] = true;
                    }


                    break;

                case 31:
                    comma3[1] = 0;
                    comma5[1] = 5;
                    comma7[1] = -2;
                    comma3[0] = 4;
                    comma5[0] = -1;
                    comma3[2] = 3;
                    comma5[2] = 7;

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
                    pumps[7] = new Pump(commas[7]);
                    commas[8] = new Comma(this, new int[] { 3, 4, -5 });
                    pumps[8] = new Pump(commas[8]);
                    commas[9] = new Comma(this, new int[] { 1, -8, 0 });
                    pumps[9] = new Pump(commas[9]);
                    PumpStructure a31 = new PumpStructureSimple(this, 9);
                    PumpStructure b31 = new PumpStructureBig(this, 1);
                    pumpStructure = new PumpStructureSum(this, a31, b31);

                    
                    for(int i = 0; i < edo; i++)
                    {
                        scale[i] = false;
                    }

                    for (int i = 0; i < 12; i++)
                    {
                        scale[(13 * i) % edo] = true;
                    }
                    
                    /*
                    scale = new bool[18];
                    scale[0] = true;
                    scale[5] = true;
                    scale[10] = true;
                    scale[13] = true;
                    */
                    break;
                case 34:
                    comma3[0] = 4;
                    comma5[0] = 2;
                    comma3[1] = 1;
                    comma5[1] = -8;
                    commas = new Comma[2];
                    pumps = new Pump[3];
                    commas[0] = new Comma(this, new int[] { 4, 2, 0 });
                    pumps[0] = new Pump(commas[0]);
                    commas[1] = new Comma(this, new int[] { 5, -6, 0 });
                    pumps[1] = new Pump(commas[1]);
                    pumps[2] = new Pump(commas[0], new int[] {0, 20, 6, 17, 3, 23});
                    pumpStructure = new PumpStructureSimple(this, 2);

                    /*
                    for (int i = 0; i < edo; i++)
                    {
                        scale[i] = false;
                    }
                    */
                    /*
                    scale[0] = true;
                    scale[3] = true;
                    scale[6] = true;
                    scale[17] = true;
                    scale[20] = true;
                    scale[23] = true;
                    */

                    scale = new bool[17];
                    for(int i = 0; i<4; i++)
                    {
                        scale[(3 * i)%17] = true;
                    }

                    break;
                case 41:
                    commas = new Comma[4];
                    pumps = new Pump[4];
                    commas[0] = new Comma(this, new int[] { -2, 2, 0, -1 });
                    pumps[0] = new Pump(commas[0]);
                    commas[1] = new Comma(this, new int[] {6, 1, -6 });
                    commas[2] = new Comma(this, new int[] { -5, 0, 0, 2});
                    commas[3] = new Comma(this, new int[] { 2, -5, 3});
                    //pumps[1] = new Pump(commas[1], new int[] { 27, 36, 4, 13, 22, 31, 40 });
                    pumps[1] = new Pump(commas[3], new int[] 
                    { 0, 28, 20, 7, 31, 18, 10, 38, 21, 8 });

                    pumpStructure = new PumpStructureSimple(this, 1);

                    for (int i = 0; i < edo; i++)
                    {
                        scale[i] = false;
                    }

                    /*
                    for (int i = 0; i < 14; i++)
                    {
                        scale[(9 * i) % edo] = true;
                    }
                     */

                    scale[0] = true;
                    scale[7] = true;
                    scale[8] = true;
                    scale[10] = true;
                    scale[17] = true;
                    scale[18] = true;
                    scale[20] = true;
                    scale[21] = true;
                    scale[28] = true;
                    scale[30] = true;
                    scale[31] = true;
                    scale[38] = true;
                    scale[39] = true;



                    /*
                    scale[0] = true;
                    scale[7] = true;
                    scale[12] = true;
                    scale[19] = true;
                    scale[24] = true;
                    scale[29] = true;
                    scale[36] = true;
                    */
                    /*
                    scale = new bool[24];
                    scale[0] = true;
                    scale[3] = true;
                    scale[6] = true;
                    scale[9] = true;
                    scale[13] = true;
                    scale[17] = true;
                    scale[20] = true;
                    */
                    break;
                case 50:
                    commas = new Comma[3];
                    pumps = new Pump[3];
                    commas[1] = new Comma(this, new int[] { 4, -1, 0 });
                    pumps[1] = new Pump(commas[1]);
                    pumpStructure = new PumpStructureSimple(this, 1);
                    break;
                case 53:
                    comma3[0] = 5;
                    comma5[0] = -6;
                    comma3[1] = 3;
                    comma5[1] = 7;
                    commas = new Comma[3];
                    pumps = new Pump[3];
                    commas[0] = new Comma(this, new int[] { 5, -6, 0 });
                    pumps[0] = new Pump(commas[0]);
                    commas[1] = new Comma(this, new int[] { 8, 1, 0 });
                    pumps[1] = new Pump(commas[1],
                        new int[] {48, 17, 39, 22, 44, 13, 35, 4, 26 });
                    commas[2] = new Comma(this, new int[] { 3, 7, 0 });
                    pumps[2] = new Pump(commas[2]);
                    pumpStructure  = new PumpStructureSimple(this, 1);
                    //PumpStructure b53 = new PumpStructureBig(this, 2);
                    //pumpStructure = new PumpStructureSum(this, a53, b53);

                    /*
                    scale = new bool[14];
                    scale[0] = true;
                    scale[3] = true;
                    scale[11] = true;
                    */

                    for (int i = 0; i < edo; i++)
                    {
                        scale[i] = false;
                    }

                    for (int i = 0; i < 12; i++)
                    {
                        scale[(22 * i) % edo] = true;
                    }
                    break;


                    break;

                case 72: 
                    commas = new Comma[4];
                    pumps = new Pump[4];
                    commas[0] = new Comma(this, new int[] { -5, 0, 0, 2 });
                    pumps[0] = new Pump(commas[0]);
                    commas[1] = new Comma(this, new int[] { 0, -3, 3, -1});
                    pumps[1] = new Pump(commas[1]);
                    commas[2] = new Comma(this, new int[] { -2, -2, 1, 0 });
                    pumps[2] = new Pump(commas[2]);
                    commas[3] = new Comma(this, new int[] { 5, -6});
                    pumps[3] = new Pump(commas[3], new int[] {0, 19, 38, 57, 4, 23, 42 });

                    //PumpStructure a12 = new PumpStructureBig(this, 0);
                    //PumpStructure b12 = new PumpStructureSimple(this, 2);
                    //pumpStructure = // a; 
                    //  new PumpStructureSum(this, a12, b12);
                    pumpStructure = new PumpStructureSimple(this, 3);

                    scale = new bool[19];
                    scale[0] = true;
                    scale[4] = true;
                    scale[8] = true;
                    scale[11] = true;
                    scale[15] = true;

                    break;

                case 84:

                    commas = new Comma[1];
                    pumps = new Pump[1];
                    commas[0] = new Comma(this, new int[] { 3, 7 });
                    pumps[0] = new Pump(commas[0], new int[] 
                    {0, 27, 54, 19, 46, 73, 38, 65, 8, 35 });

                    pumpStructure = new PumpStructureSimple(this, 0);
                    for (int i = 0; i < edo; i++)
                    {
                        scale[i] = false;
                    }

                    for (int i = 0; i < 13; i++)
                    {
                        scale[(35 + 19 * i) % edo] = true;
                    }
                    break;

                case 87:
                    comma3[0] = 5;
                    comma5[0] = -6;
                    comma3[1] = 2;
                    comma5[1] = 15;
                    commas = new Comma[5];
                    pumps = new Pump[5];
                    commas[0] = new Comma(this, new int[] { 5, -6, 0 });
                    pumps[0] = new Pump(commas[0]);
                    commas[1] = new Comma(this, new int[] { 2, 15, 0 });
                    pumps[1] = new Pump(commas[1]);
                    commas[2] = new Comma(this, new int[] { -2, 1, -2, 1 });
                    pumps[2] = new Pump(commas[2]);
                    commas[3] = new Comma(this, new int[] { 1, -1, -1, -1 });
                    pumps[3] = new Pump(commas[3]);
                    commas[4] = new Comma(this, new int[] { -2, -4, 0, 1 });
                    pumps[4] = new Pump(commas[4]);
                    pumpStructure  = new PumpStructureSimple(this, 4);
                    //PumpStructure b87 = new PumpStructureBig(this, 3);
                    //pumpStructure = new PumpStructureSum(this, a87, b87);
                    break;
                case 118:
                    comma3[1] = 8;
                    comma5[1] = 1;
                    comma3[0] = 2;
                    comma5[0] = 15;
                    commas = new Comma[4];
                    pumps = new Pump[4];
                    commas[0] = new Comma(this, new int[] { 8, 1, 0 });
                    pumps[0] = new Pump(commas[0], new int[] 
                    {27, 7, 105, 85, 65 });
                    commas[1] = new Comma(this, new int[] { 2, 15, 0 });
                    pumps[1] = new Pump(commas[1]);
                    commas[2] = new Comma(this, new int[] { 6, -14, 0 });
                    pumps[2] = new Pump(commas[2]);
                    commas[3] = new Comma(this, new int[] { 14, -13, 0 });
                    pumps[3] = new Pump(commas[3], new int[]
                        {0, 31, 62, 93, 6, 37, 68, 99, 12, 43, 74, 105, 18, 49 });

                    //PumpStructure a118 = new PumpStructureSimple(this, 1);
                    //PumpStructure b118 = new PumpStructureBig(this, 2);
                    //pumpStructure = new PumpStructureSum(this, a118, b118);
                    pumpStructure = new PumpStructureSimple(this, 3);

                    /*
                    for (int i = 0; i < edo; i++)
                    {
                        scale[i] = false;
                    }
                    for (int i = 0; i < 35; i++)
                    {
                        scale[(27 * i) % edo] = true;
                    }
                    */

                    scale = new bool[31];
                    scale[0] = true;
                    scale[6] = true;
                    scale[12] = true;
                    scale[18] = true;
                    scale[25] = true;

                    break;
                case 140:
                    comma3[0] = 0;
                    comma5[0] = 3;
                    comma7[0] = 5;
                    comma3[1] = 5;
                    comma5[1] = -6;

                    for (int i = 0; i < edo; i++)
                    {
                        scale[i] = false;
                    }
                    for (int i = 0; i < 32; i++)
                    {
                        scale[(13 * i) % edo] = true;
                    }

                    break;
                case 159:
                    commas = new Comma[3];
                    pumps = new Pump[3];

                     
                    commas[0] = new Comma(this, new int[] { 0, 0,  9, 6 });
                    pumps[0] = new Pump(commas[0]);
                     
                    pumpStructure = new PumpStructureSimple(this, 0);
                    break;
                case 171:
                    commas = new Comma[10];
                    pumps = new Pump[10];

                    //comma3[0] = 19;
                    //comma5[0] = -19;
                    //comma7[0] = 0;
                    commas[0] = new Comma(this, new int[] { 1, 2, -4 });
                    pumps[0] = new Pump(commas[0]);
                    commas[1] = new Comma(this, new int[] { 0, 3, 5 });
                    pumps[1] = new Pump(commas[1], new int[] 
                    {44, 66, 88, 110, 132, 154, 5, 27, 49, 71, 93, 115 });
                    commas[2] = new Comma(this, new int[] { 7, -4, -1 });
                    pumps[2] = new Pump(commas[2], new int[]
                    {104, 33, 59, 85, 111, 137 });
                    commas[3] = new Comma(this, new int[] { 9, 0, -9 });
                    pumps[3] = new Pump(commas[3]);
                    commas[4] = new Comma(this, new int[] { -8, -1, 0 });
                    pumps[4] = new Pump(commas[4]);
                    commas[5] = new Comma(this, new int[] { -2, -7, 3 });
                    pumps[5] = new Pump(commas[5], new int[]
                        {0, 55, 88, 143, 27, 127, 11, 44, 99, 154, 83, 116});

                    commas[6] = new Comma(this, new int[] { 1, 5, 1 });
                    pumps[6] = new Pump(commas[6], new int[]
                        {110, 165, 49, 104, 159, 43});

                    commas[7] = new Comma(this, new int[] { 36, -12, 1 });
                    pumps[7] = new Pump(commas[7], new int[]
                        {74, 148, 51, 125, 28, 102, 5, 79, 153, 56, 130, 33, 107});

                    /*
                    commas[8] = new Comma(this, new int[] { 0, 6, 10 });
                    pumps[8] = new Pump(commas[8], new int[]
                        {110, 121, 132, 143, 154, 165});
                    */

                    commas[9] = new Comma(this, new int[] { -1, 1, 9 });
                    pumps[9] = new Pump(commas[9], new int[]
                        {66, 99, 132, 165, 27, 60, 93, 126, 159, 21});

                    //  PumpStructure a171 = new PumpStructureBig(this, 4);
                    // PumpStructure b171 = new PumpStructureSimple(this, 1);
                    // pumpStructure = new PumpStructureSum(this, a171, b171);
                    pumpStructure = new PumpStructureSimple(this, 2);

                    comma3[0] = 1;
                    comma5[0] = 2;
                    comma7[0] = -4;
                    comma3[2] = 8;
                    comma5[2] = 1;

                    for (int i = 0; i < edo; i++)
                    {
                        scale[i] = false;
                    }
                    for (int i = 0; i < 33; i++)
                    {
                        scale[(26 * i) % edo] = true;
                    }
                    /*
                    scale[0] = true;
                    scale[11] = true;
                    scale[22] = true;
                    scale[27] = true;
                    scale[44] = true;
                    scale[55] = true;
                    scale[66] = true;
                    scale[83] = true;
                    scale[88] = true;
                    scale[99] = true;
                    scale[116] = true;
                    scale[127] = true;
                    scale[138] = true;
                    scale[143] = true;
                    scale[154] = true;
             */
                    break;
                case 270:
                    commas = new Comma[3];
                    pumps = new Pump[3];
                    commas[0] = new Comma(this, new int[] { 6, 4, -3, -3 });
                    pumps[0] = new Pump(commas[0]);
                    commas[1] = new Comma(this, new int[] { 9, -8, 4, -2 });
                    pumps[1] = new Pump(commas[1]);
                    commas[2] = new Comma(this, new int[] { 3, -2, 1, -2 });
                    pumps[2] = new Pump(commas[2]);
                    pumpStructure = new PumpStructureSimple(this, 2);

                    for (int i = 0; i < edo; i++)
                    {
                        scale[i] = false;
                    }
                    for (int i = 0; i < 44; i++)
                    {
                        scale[(43 * i) % edo] = true;
                    }


                    break;
                case 342:
                    commas = new Comma[3];
                    pumps = new Pump[3];
                    commas[0] = new Comma(this, new int[] { 2, 2, -7, 4 });
                    pumps[0] = new Pump(commas[0]);

                    commas[1] = new Comma(this, new int[] { 8, 1});
                    pumps[1] = new Pump(commas[1], new int[] 
                    {0, 142, 284, 84, 226, 26, 168, 310, 110 });
                    pumps[2] = new Pump(commas[1], new int[]
                   {0, 71, 142, 213, 284, 13 });

                    PumpStructure a342 = new PumpStructureSimple(this, 1);
                    PumpStructure b342 = new PumpStructureBig(this, 2);
                    pumpStructure = new PumpStructureSum(this, a342, b342);

                    for (int i = 0; i < edo; i++)
                    {
                        scale[i] = false;
                    }
                    for (int i = 0; i < 24; i++)
                    {
                        scale[(71 * i) % edo] = true;
                    }


                    break;
                default:
                    pumpStructure = new PumpStructureNull(this);
                    break;
            }

            for (int ci = 0; ci < ccnt; ci++)
            {
                if (0 != (comma3[ci] * ed3 + comma5[ci] * ed5 + comma7[ci] * ed7) % edo)
                {
                    Console.WriteLine("comma error");
                }
            }

            icnt = 5 * edo;

            intervals = new double[icnt];
            string[] why = new string[icnt];
            double[] best = new double[icnt];
            for (int i = 0; i < icnt; i++)
            {
                intervals[i] = 0.0;
            }

            for (int octaves = -20; octaves < 21; octaves++)
            {
                for (int fifths = -20; fifths < 21; fifths++)
                {
                    //int thirds = 0;
                    for (int thirds = -20; thirds < 21; thirds++)
                    {
                        int sevenths = 0;
                        //for (int sevenths = -20; sevenths < 21; sevenths++)
                        {
                            int elevens = 0;
                            //for (int elevens = -20; elevens < 21; elevens++)
                            {
                                int i = ed3 * fifths
                                    + ed5 * thirds
                                    + edo * octaves
                                    + ed7 * sevenths
                                    + ed11 * elevens;
                                if (i < 0)
                                {
                                    i = -i;
                                }
                                double ji =
                                       ((double)octaves) * Math.Log(2.0)
                                      + ((double)fifths) * Math.Log(3.0)
                                      + ((double)thirds) * Math.Log(5.0)
                                      + ((double)sevenths) * Math.Log(7.0)
                                       + ((double)elevens) * Math.Log(11.0);

                                double scale = ((double)edo) * ji / Math.Log(2);

                                if (i < icnt)
                                {
                                    scale = - scale;

                                    double tempered = (double)i;

                                    double err = Math.Abs(scale - tempered); 

                                    double c =
                                        err * err * 0.1
                                         + 1.3 * (double)(elevens * elevens)
                                        + 1.1 * (double)(sevenths * sevenths)
                                        + (double)(thirds * thirds)
                                        + 0.9 * (double)(fifths * fifths)
                                        + 0.15 * (double)(octaves * octaves);
                                    intervals[i] += 1.0 / (c * c );

                                    if (best[i] == 0.0 || c < best[i])
                                    {
                                        best[i] = c;
                                        why[i] = string.Format("[{0} {1} {2} {3} {4}>",
                                            octaves, fifths, thirds, sevenths, elevens);
                                    }
                                }
                            }
                        }
                    }
                }
            }

            string cfilename =  map.file_prefix + "intervals.txt";

            StreamWriter cfile = new System.IO.StreamWriter(cfilename);
            intervals[0] = 0.0;
            for (int i = 1; i < icnt; i++)
            {
                if (intervals[i] == 0.0)
                {
                    intervals[i] = 1000.0;
                }
                else
                {
                    intervals[i] = 1.0 / intervals[i]; // multiple hits lowers cost
                }
                cfile.WriteLine(string.Format("{0}: {1}; {2} ", i, intervals[i], why[i]));
            }
            cfile.Close();

            show_pattern();

            /*
            for (int i = 0; i < icnt; i++)
            {
                if (why[i] != null)
                {
                    System.Console.WriteLine(string.Format("i = {0}; c(i) = {1}; why = {2} {3} {4};",
                        i, intervals[i], why[i][0], why[i][1], why[i][2]));
                }
            }
            */
        }

        public override Vector randomVector(Random rand, int root)
        {
            return new VectorEDO5(this, rand, root);
        }

        public override Vector initialVector(Random rand, int root, int loc)
        {
            return new VectorEDO5(this, rand, root, loc);
        }

        private int cspace(int comma, int i, double jog)
        {
            if (comma == 0)
            {
                return 0;
            }
            int p = ((int)(jog + (double)(comma * i) / (double)map.row_size)) % comma;
            return p;
        }

        public int pattern(int loc)
        {
            int pitch = 0;

            int small = loc % map.row_size;
            int big = loc / map.row_size; ;

            int third = ed5 - 2 * edo;
            int fifth = ed3 - edo;
            int seventh = ed7 - 3 * edo;
            /*
            int c3_0 = 0, c5_0 = 0, c7_0 = 0, c3_1 = 0, c5_1 = 0, c7_1 = 0;

            c3_1 = comma3[2];
            c5_1 = comma5[2];
            c7_1 = comma7[2];

            switch ((8 * big) / map.row_size)
            {
                case 7:
                case 0:
                    c3_0 = comma3[0];
                    c5_0 = comma5[0];
                    c7_0 = comma7[0];
                    break;
                case 1:
                case 2:
                    c3_0 = comma3[1];
                    c5_0 = comma5[1];
                    c7_0 = comma7[1];
                    break;
                case 3:
                case 4:
                    c3_0 = -comma3[0];
                    c5_0 = -comma5[0];
                    c7_0 = -comma7[0];
                    break;
                case 5:
                case 6:
                    c3_0 = -comma3[1];
                    c5_0 = -comma5[1];
                    c7_0 = -comma7[1];
                    break;
            }

            pitch = (30 * edo 
                + third
                  * (cspace(c5_0, small, 0.3)
                     + cspace(c5_1, big, 0.3))
                + fifth
                  * (cspace(c3_0, small, 0.6)
                     + cspace(c3_1, big, 0.6))                         
                + seventh
                  * (cspace(c7_0, small, 0.9)
                     + cspace(c7_1, big, 0.9)))
                % edo;
            */

            pitch = pumps[0].sequence[(small * pumps[0].sequence.Length) / map.row_size];

            return pitch;
        }

        void show_pattern()
        {
            Console.WriteLine("initial pattern:");
            for (int r = 0; r < map.row_size; r++)
            {
                for (int c = 0; c < map.row_size; c++)
                {
                    int loc = c + r * map.row_size;
                    Console.Write(string.Format("{0} ", pumpStructure.pattern(loc)));
                }
                Console.WriteLine();
            }
        }

        public override Interval_Histogram getHistogram()
        {
            Interval_HistogramEDO5 ih = new Interval_HistogramEDO5(edo);
            return ih;
        }

        private void scaleCheck(int g, double avg, StreamWriter sfile)
        {
            bool[] tscale = new bool[edo];
            tscale[0] = true;
            tscale[g] = true;
            bool cycle = false;
            for(int i = 2; i<45 && ! cycle; i++)
            {
                int m = (i * g) % edo;
                if (m == 0)
                {
                    cycle = true;
                }
                else
                {
                    tscale[m] = true;

                    bool mos = true;
                    int last = 0;
                    int g1 = 0;
                    int g2 = 0;
                    for (int j = 1; j < edo && mos; j++)
                    {
                        if (tscale[j])
                        {
                            int gap = j - last;
                            if (g1 == 0)
                            {
                                g1 = gap;
                            }
                            else if (g1 != gap)
                            {
                                if (g2 == 0)
                                {
                                    g2 = gap;
                                }
                                else if (g2 != gap)
                                {
                                    mos = false;
                                }
                            }
                            last = j;
                        }

                    }
                    if (edo - last != g1 && edo - last != g2)
                    {
                        mos = false;
                    }
                    if (mos)
                    {
                        sfile.WriteLine(string.Format("{0} mos {1}: {2}, {3}",
                            g, i, g1, g2));
                    }


                    if (intervals[m] < avg)
                    {
                        sfile.WriteLine(string.Format("{0} {1} {2} {3}",
                            g, i, m, intervals[m]));
                    }
                }
            }
        }
        public override void scaleSearch()
        {
            string sfilename = map.file_prefix + "scales.txt";

            StreamWriter sfile = new System.IO.StreamWriter(sfilename);
            double tot = 0.0;
            for (int i = 1; i < edo; i++)
            {
                tot += intervals[i];
            }
            double avg = tot / (double)(edo - 1);

            for(int i = 1; i <= edo/2; i++ )
            {
                if(intervals[i] < 1.2 * avg)
                {
                    scaleCheck(i, avg, sfile);
                }
            }
            sfile.Close();
        }

    }
}
