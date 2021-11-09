﻿using System.Collections.Generic;
using System.IO;
using System;

namespace meantone
{     
    public class Program
    {
        static void Main(string[] args)
        {
            Type_Map map = new Type_Map(new Random(5237));

            Work work;

            work = new Work(map);
            work.check();
      
            double cost = work.cost();
            Console.WriteLine(string.Format("initial cost {0}", cost));
            work.bfrac();
            work.histogram();
            int freeze = 0;

            bool fmode = false;
            for (int vi = 0; vi < work.voice_count; vi++)
            {
                work.voices[vi].freeze = fmode;
            }

            Vertex.parallelism = 0.0;
            double temp = 10.0;
            double target = 0.088;
            for (int iter = 0; iter < 1; iter++)
            {
                //work.voices[freeze].freeze = !fmode;

                for (int vi = 0; vi < work.voice_count; vi++)
                {
                    Console.WriteLine(string.Format("freeze[[0] = {1}",
                        vi, work.voices[vi].freeze));
                }

                //target = target * 0.97;
                temp = 1000000.0;
                work.jostle(temp, 1000);
                double bfrac = work.bfrac();
                double afrac = work.align_count();
                //temp = 140.0 - 5.0 * (double)iter;
                // temp = 3000.0;
                //work.bfrac();
                
                /*
                while (afrac > 0.36)
                {
                    temp = 1600.0;
                    work.jostle(temp, 1000);
                    afrac = work.align_count();
                    bfrac = work.bfrac();

                }
                */
                //temp = 300.0;
                //work.jostle(temp, 8000);
                //work.jostle(5000.0, 1500);

                bfrac = work.bfrac();
                afrac = work.align_count();
               
                //bfrac = 0.0;

                double move = 0.02;
                int effort = 400;
                //double target = 0.1;

                bool up = false;
                int bounce = 0;
                while (bounce < 1)
                {
                    if (up)
                    {
                        const double upper_lim = 10000.0;
                        while (temp < upper_lim && bfrac > target)
                        {
                            temp = temp / (1.0 - move);
                            work.jostle(temp, effort);
                            bfrac = work.bfrac();
                            afrac = work.align_count();
                        }
                        if(temp >= upper_lim)
                        {
                            target = 0.03 +bfrac * 0.97;
                            Console.WriteLine(string.Format("new target: {0}", target));
                        }
                    }
                    else
                    {
                        const double lower_lim = 300.0;
                        while (temp > lower_lim && bfrac < target)
                        {
                            temp *= (1.0 - move);
                            work.jostle(temp, effort);
                            bfrac = work.bfrac();
                            afrac = work.align_count();
                            //effort = (108 * effort) / 100;
                        }
                        if(temp <= lower_lim)
                        {
                            target = bfrac * 0.95;
                            Console.WriteLine(string.Format("new target: {0}", target));
                        }
                         
                    }
                        
                    up = !up;
                    effort = (2 * effort) ;
                    move *= 0.9;
                    bounce++;
                    Console.WriteLine(string.Format("bounce = {0};", bounce));
                }

               
                Vertex.parallelism = 1.0;
                double pf = work.align_count();
                while (pf < 0.4)
                {
                    work.jostle(temp, 1200);
                    work.bfrac();
                    pf = work.align_count();
                    Vertex.parallelism *= 1.5;
                }


                /*
                for(int vi=0; vi < work.voice_count; vi++)
                {
                    work.voices[vi].freeze = !work.voices[vi].freeze;
                }
                */
                work.voices[freeze].freeze = fmode;
                freeze = (freeze + 1) % work.voice_count;
            }

            //work.jostle(200.0, 3000);
            //work.bfrac();
            //work.align_count();

            //work.jostle(12.0, 100000);
            //work.bfrac();

            /*
            for (int iter = 0; iter < 6; iter++)
            {
                work.voices[freeze].freeze = true;
                
                work.jostle(30.0);
                work.bfrac();
                
                work.voices[freeze].freeze = false;
                freeze = (freeze + 1) % work.voice_count;
            }
            */
            double cost2 = work.cost();
            work.histogram();
            work.phase_histogram(map);

            /*
            StreamWriter cfile = new System.IO.StreamWriter(@"C:\Users\Jim\Documents\tuning\meantone\chordcount.txt");
            work.chords(cfile);
            cfile.Close();
            */

            work.amplitudes();

            StreamWriter file;

            file = new StreamWriter(work.file_prefix + "mtscore.txt"); 

            // work.voices[0].mute = true;
            
            file.WriteLine("f1 0 4096 10 1");
            file.WriteLine("f2 0 4096 10 1 0.5 0.3 0.25 0.2 0.167 0.14 0.125 .111");
            file.WriteLine("f3 0 4096 10 1 0.3 0.5 0.25 0.2 0.167 0.14 0.125 .111");
            file.WriteLine("f4 0 4096 10 1 0.3 0.4 0.4 ");
            file.WriteLine("f5 0 4096 10 1 0.5 0.3 ");
            file.WriteLine("f6 0 4096 10 1 0.3 0.3 0.3 0.3");
            file.WriteLine("f7 0 4096 10 1 0.6 0.2 0.1 0.3");

            work.play(file);

            file.Close();

            work.intevalHistograms();
        }
    }
}
