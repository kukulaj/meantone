using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace meantone
{
    public class FactoryEDO5 : VectorFactory
    {
        public int edo;
        int pcnt;
        bool[] included;
        int[] primes;
        int[] range;
        int[] coord;
        double[] cost;
        public int[] pstep;


        public double[] intervals;
        public double[] best;
        public bool[] scale;
        
        public int icnt;
        
        public int[] comma3;
        public int[] comma5;
        public int[] comma7;
        public Type_Map map;
        public Comma[] commas;
        public Pump[] pumps;
        public PumpStructure pumpStructure;

        public FactoryEDO5(int e, Type_Map pmap)
        {
            edo = e;
            map = pmap;
           
            primes = new int[] { 2, 3, 5, 7, 11, 13, 17, 19, 23, 29, 31, 37, 41, 43, 47, 53, 59};
            range = new int[] { 10, 6, 4, 3, 2, 2, 2, 2, 2, 2, 2, 1, 1, 1, 1, 1, 1};
            pcnt = primes.Length;
            coord = new int[pcnt];
            cost = new double[pcnt];
            double bc = 0.2;
            for(int i = 0; i < pcnt; i++)
            {
                cost[i] = bc;
                bc = 1.2 * (bc + 0.4);
            }

            pstep = new int[pcnt];
            for (int i = 0; i < pcnt; i++)
            {
                pstep[i] = (int)(0.5 + (((float)edo) * Math.Log((double)(primes[i])) / Math.Log(2.0)));
            }

            scale = new bool[edo];
            for (int i = 0; i < edo; i++)
            {
                scale[i] = true;
            }
        }

        public FactoryEDO5(int e, Type_Map pmap, bool[] pinc): this(e, pmap)
        {
            pumpStructure = new PumpStructureNull(this);
            included = pinc;

            for(int i = 0; i < pcnt; i++)
            {
                if(i < included.Length && included[i])
                {
                    coord[i] = range[i];
                }
            }

            icnt = 5 * edo;

            intervals = new double[icnt];
            string[] why = new string[icnt];
            double[] best = new double[icnt];

            for (int i = 0; i < icnt; i++)
            {
                intervals[i] = 0.00001;
            }

            bool done = false;
            while (!done)
            {
                int interval = 0;
                double ji = 0.0;
                for(int i = 0; i < pcnt; i++)
                {
                    interval += pstep[i] * coord[i];
                    ji += coord[i] * Math.Log((double)(primes[i]));
                }

                 
                if (interval < 0)
                {
                    interval = -interval;
                    ji = -ji;
                }

                if (interval < icnt)
                {
                    double scale = ((double)edo) * ji / Math.Log(2);
                    double tempered = (double)interval;

                    double err = Math.Abs(scale - tempered);

                    double c = err * err * 60.0;
                    for (int i = 0; i < pcnt; i++)
                    {
                        c += cost[i] * coord[i] * coord[i];
                    }
                    intervals[interval] += 1.0 / (c * c);

                    if (best[interval] == 0.0 || c < best[interval])
                    {
                        best[interval] = c;
                        why[interval] = "[";
                        for (int i = 0; i < included.Length; i++)
                        {
                            why[interval] += string.Format("{0} ", coord[i]);
                        }

                        why[interval] += ">";
                    }
                }
                
                    bool inc = false;
                    for (int i = 0; i < pcnt && !inc; i++)
                    {
                        if (i < included.Length && included[i])
                        {
                            coord[i]--;
                            if (coord[i] < -range[i])
                            {
                                coord[i] = range[i];
                            }
                            else
                            {
                                inc = true;
                            }
                        }
                    }
                    
                    done = !inc;

                }
                string cfilename = map.file_prefix + "intervals.txt";

                StreamWriter cfile = new System.IO.StreamWriter(cfilename);
                intervals[0] = 0.0;
                for (int i = 1; i < icnt; i++)
                {  
                   intervals[i] = 1.0 / intervals[i]; // multiple hits lowers cost
                  
                    cfile.WriteLine(string.Format("{0}: {1}; {2} ", i, intervals[i], why[i]));
                }
                cfile.Close();
            
        }

        public  void olFactoryEDO5(int e, Type_Map tmap)  
        {
            map = tmap;
            
             

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
                
                case 34:
                    comma3[0] = 4;
                    comma5[0] = 2;
                    comma3[1] = 1;
                    comma5[1] = -8;
                    commas = new Comma[2];
                    pumps = new Pump[3];
                    commas[0] = new Comma(this, new int[] { 4, 2, 0 });
                    pumps[0] = new Pump(commas[0], new int[] {0, 23, 3, 17, 6, 20 });
                    commas[1] = new Comma(this, new int[] { 5, -6, 0 });
                    pumps[1] = new Pump(commas[1]);
                    pumps[2] = new Pump(commas[0], new int[] {0, 20, 6, 17, 3, 23});
                    pumpStructure = new PumpStructureSimple(this, 0);

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
                    
                   
                    break;

                case 65:
                    commas = new Comma[4];
                    pumps = new Pump[4];
                    commas[0] = new Comma(this, new int[] { 15, 10});
                    pumps[0] = new Pump(commas[0], new int[] 
                    {0, 38, 11, 32, 5, 26, 64, 37, 58, 31, 52, 25, 63, 19, 57, 13, 51, 
                    24, 45, 18, 39, 12, 50, 6, 44});

                    pumpStructure = new PumpStructureSimple(this, 0);

                    scale = new bool[13];

                    for (int i = 0; i < 5; i++)
                    {
                        scale[(6 * i) % 13] = true;
                    }
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
               
                default:
                   
                    break;
            }

            

            icnt = 5 * edo;

            

            
             
           
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

            

            pitch = pumps[0].sequence[(small * pumps[0].sequence.Length) / map.row_size];

            return pitch;
        }

        public override void show_pattern()
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
