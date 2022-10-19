using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace meantone
{
    public class Work
    {
        public VectorFactory vectorFactory;
        public Random rand;
        public int measure_count;
        int vertex_count;

        Vertex[] vertices;
        public int voice_count;
        public Voice[] voices;
        public StreamWriter trace;
        public Type_Map map;
        public string file_prefix;
        public double rcost;
       

        public Work(Type_Map m)
        {
            map = m;
            file_prefix = map.file_prefix;

            FileStream tfs = File.OpenWrite(file_prefix + "trace.txt");
            trace = new System.IO.StreamWriter(tfs);

            //vectorFactory = new GeneralFactory(generators, intervals);
            bool[] primes = new bool[6];
            primes[0] = true;
            primes[1] = true;
            primes[2] = true;
            //primes[3] = true;
            //primes[4] = true;
            //primes[5] = true;          

            vectorFactory = new FactoryEDO22( map, primes);
            vectorFactory.show_pattern();
            vectorFactory.scaleSearch();
            rand = map.rand;
            measure_count = map.size;
            voice_count = 4;

            vertex_count = 0;
            vertices = new Vertex[500 * voice_count * measure_count];

            
            Rhythm origin = new Rhythm(this, map.duration);
            origin.singleton();
            
            double min_dur = 2.0;
            Rhythm osplit = origin.splinter(min_dur, 9);
            
            Rhythm ostick = osplit.stick(0.3);
            Rhythm osplit2 = ostick.splinter(min_dur, 9);
            Rhythm ostick2 = osplit2.stick(0.2);
            Rhythm root = ostick2.silence(min_dur, 0.0, 0.0, 2, min_dur/1.5);
            root = root.vary(min_dur, 0);
            root = root.vary(min_dur, 0);
            root = root.vary(min_dur, 0);
            
            //Rhythm root = origin.split(map.row_size);

            Rhythm[] rows = new Rhythm[measure_count];
            rows[0] = root;
            for (int ri = 1; ri < rows.Length; ri++)
            {
                rows[ri] = rows[ri - 1].vary(min_dur, 0);
            }

            voices = new Voice[voice_count];
            for (int i = 0; i < voice_count; i++)
            {
                voices[i] = new Voice(this, i, map, rows);
            }

            /*
            for (int i = 0; i < voice_count-1; i++)
            {
                //for (int j = i + 1; j < voice_count; j++)

                {
                    voices[i].above(voices[i+1]);
                }
            }
            */
            Voice.stack(voices);

            tfs.Flush();
        }

        public void add_vertex(Vertex v)
        {
            vertices[vertex_count] = v;
            vertex_count++;
        }

        public double align_count()
        {
            int[] tallies = new int[2];
            for (int i = 0; i < vertex_count; i++)
            {
                vertices[i].align_count(tallies);
            }
            double result = ((double)tallies[1]) / (double)tallies[0];
            Console.WriteLine(string.Format("afrac = {0}", result ));
            return result;
               
        }

        public bool check()
        {
            bool ok = true;

            for (int i = 0; i < vertex_count; i++)
            {
                ok = ok && vertices[i].check();
            }
            return ok;
        }

        public double across_cost()
        {
            double tot = 0.0;
            for (int i = 0; i < vertex_count; i++)
            {
                double vcost = vertices[i].across_cost();
                tot += vcost;  
            }
            return tot;
        }

        public double vertical_cost()
        {
            double tot = 0.0;
            for (int i = 0; i < vertex_count; i++)
            {
                double vcost = vertices[i].vertical_cost();
                tot += vcost;
            }
            return tot;
        }
        
        public double horizontal_cost()
        {
            double tot = 0.0;
            for (int i = 0; i < vertex_count; i++)
            {
                double vcost = vertices[i].horizontal_cost();
                tot += vcost;
            }
            return tot;
        }

        public double parallel_cost()
        {
            double tot = 0.0;
            for (int i = 0; i < vertex_count; i++)
            {
                double vcost = vertices[i].parallel_cost();
                tot += vcost;
            }
            return tot;
        }

        public double self_cost()
        {
            double tot = 0.0;
            for (int i = 0; i < vertex_count; i++)
            {
                double vcost = vertices[i].self_cost();
                tot += vcost;
            }
            return tot;
        }


        public double cost()
        {
            double tot = 0.0;
            for (int i = 0; i < vertex_count; i++)
            {
                double vcost = vertices[i].cost();
                if (Double.IsNaN(vcost))
                {
                    vcost = 1000.0;
                }
                tot += vcost;
                if (Double.IsNaN(tot))
                {
                    tot = 1000000000.0;
                }
            }

            return tot;
        }

        public void amplitudes()
        {

            for (int i = 0; i < vertex_count; i++)
            {
                vertices[i].cost();
            }

            for (int i = 0; i < voice_count; i++)
            {

                voices[i].amplitudes();

            }
        }

        public double tweak_time(double t)
        {
            double result = 0;

            double w = 1.0;
            for (double p = 0.1; p < 30.0; p *= 1.302)
            {
                result += w *  Math.Sin(t * p);
                w *= 0.93;
            }

            return 0.01 * result;
        }

        public double equilibrate(double temp, int effort)
        {
            double result = 0.0;
            double ccost = cost();
            double lcost = ccost;
            double hcost = ccost;

            bool moved = true;
            while(moved)
            {
                moved = false;
                result += jostle(temp, effort);
                ccost = cost();
                if (ccost > hcost)
                {
                    moved = true;
                    hcost = ccost;
                }
                if (ccost < lcost)
                {
                    moved = true;
                    lcost = ccost;
                }
            }
            return result;
        }

        public double jostle(double temp, int effort)
        {
            double ccost = cost();
            double lcost = ccost;
            double hcost = ccost;
            int since = 0;
            double tdel = 0.0;

            while (since < effort)
            {
                int vi = rand.Next(vertex_count);
                Vertex v = vertices[vi];
                if (v.measure.voice.freeze)
                {
                    continue;
                }
                Vector choice = v.jostle(temp * v.measure.temp_factor * v.measure.voice.temp_factor);
                double delta = v.dcost(choice) - v.dcost(v.vector);
                if (Double.IsNaN(delta))
                {
                    delta = 1000.0;
                }

                //double newic = v.interval_cost(choice, true);
                //double newicg = v.interval_cost(choice, false);
                //double oldic = v.interval_cost(v.vector, true);
                //double oldicg = v.interval_cost(v.vector, false);


                v.vector = choice;
                /*
                  
                double actual = checkit - ccost;

                double err = actual - delta;
                double multiple = newic / newicg;
                double cmult = err / (oldicg - newicg);
                */

                ccost += delta;
                tdel += delta;

                /*
                 double checkit = cost();
                if (ccost > checkit + 1.0 || ccost < checkit - 1.0)
                {
                    System.Console.WriteLine(string.Format("barf"));
                    ccost = checkit;
                }
                */

                if (Double.IsNaN(ccost))
                {
                    ccost = cost();
                }

                if (ccost < lcost)
                {
                    since = 0;
                    lcost = ccost;
                    // System.Console.WriteLine(string.Format("temp {0}; cost {1}", temp, ccost));
                }
                else if (ccost > hcost)
                {
                    since = 0;
                    hcost = ccost;
                    // System.Console.WriteLine(string.Format("temp {0}; cost {1}", temp, ccost));
                }
                else
                {
                    since++;
                }
            }

            
            System.Console.Write(string.Format("temp {0}; cost {1}; ",
                temp, ccost));

            System.Console.Write(string.Format("v: {0}; ", vertical_cost()));
            System.Console.Write(string.Format("h: {0}; ", horizontal_cost()));
            System.Console.Write(string.Format("a: {0}; ", across_cost()));
            System.Console.Write(string.Format("p: {0}; ", parallel_cost()));
            System.Console.Write(string.Format("s: {0}; ", self_cost()));
            System.Console.WriteLine();

            double checkit2 = cost();
            if (ccost > 1.001 * checkit2 || ccost < 0.999 * checkit2)
            {
                System.Console.WriteLine(string.Format("{0} should be {1}", ccost, checkit2));
            }

            rcost = checkit2;
            return tdel;
        }

        public Dictionary<double, int> build_histogram()
        {
            Dictionary<double, int> pvals = new Dictionary<double, int>();

            for (int i = 0; i < vertex_count; i++)
            {
                double d = vertices[i].vector.histd();
                if (pvals.ContainsKey(d))
                {
                    pvals[d] = pvals[d] + 1;
                }
                else
                {
                    pvals[d] = 1;
                }

            }

            return pvals;
        }

        public void phase_histogram(Type_Map map)
        {
            if (map.dimension == 1)
                return;

            int row_size = map.row_size;
            int row_cnt = map.size / map.row_size;

            Dictionary<double, int>[] pvals = new Dictionary<double, int>[row_cnt];
            for (int i = 0; i < row_cnt; i++)
            {
                pvals[i] = new Dictionary<double, int>();
            }

            Dictionary<double, int> pval;
            for (int i = 0; i < vertex_count; i++)
            {
                int loc = vertices[i].measure.location;
                pval = pvals[loc / row_size];

                double d = vertices[i].vector.histd();
                if (pval.ContainsKey(d))
                {
                    pval[d] = pval[d] + 1;
                }
                else
                {
                    pval[d] = 1;
                }

            }
            StreamWriter file;

            FileStream pfs = File.OpenWrite(file_prefix + "phasegram.txt");
            // work.voices[0].mute = true;
            file = new System.IO.StreamWriter(pfs);
            for (int i = 0; i < row_size; i++)
            {
                file.WriteLine(string.Format("phase {0}", i));
                pval = pvals[i];
                foreach (KeyValuePair<double, int> kvp in pval)
                {
                    file.WriteLine(string.Format("{0} at {1}",
                           kvp.Value, kvp.Key));
                }
            }
            pfs.Flush();
        }


        // hard coded for specific case

        public void chords(StreamWriter f)
        {
            Dictionary<int[], int> table = new Dictionary<int[], int>(new ChordEqualityComparer());


            int cc = voices[0].measures.Length;

            for (int ci = 0; ci < cc; ci++)
            {
                int[] key = new int[voice_count - 1];
                double root = voices[0].measures[ci].vertices[0].vector.log_frequency();
                for (int vi = 1; vi < voice_count; vi++)
                {
                    key[vi - 1] = (int)(0.5 + 118.0 * (voices[vi].measures[ci].vertices[0].vector.log_frequency()
                        - root) / Math.Log(2.0));
                }
                if (table.ContainsKey(key))
                {
                    table[key] = table[key] + 1;
                }
                else
                {
                    table[key] = 1;
                }
            }

            foreach (KeyValuePair<int[], int> kvp in table)
            {
                f.WriteLine(string.Format("{0} at {1} {2}",
                       kvp.Value,
                       kvp.Key[0],
                       kvp.Key[1]));
            }
        }

        public void histogram()
        {
            Dictionary<double, int> pvals = build_histogram();

            foreach (KeyValuePair<double, int> kvp in pvals)
            {
                System.Console.WriteLine(string.Format("{0} at {1}",
                       kvp.Value, kvp.Key));
            }
        }

        public double bfrac()
        {
            Dictionary<double, int> pvals = build_histogram();

            double big = 0.0;
            foreach (KeyValuePair<double, int> kvp in pvals)
            {
                if (kvp.Value > big)
                {
                    big = kvp.Value;
                }
            }

            double result = big / (double)vertex_count;
            System.Console.WriteLine(string.Format("bfrac = {0}", result));
            return result;
        }

        public void spiny_seq(StreamWriter file)
        {
            for (int i = 0; i < voice_count; i++)
            {
                if (!voices[i].mute)
                {
                    voices[i].spiny_seq(file);
                }
            }
        }

        public double spiny_row(int j, double t, StreamWriter file)
        {
            double tmax = 0.0;
            for (int i = 0; i < voice_count; i++)
            {
                if (!voices[i].mute)
                {
                    double tv = voices[i].spiny_row(j, t, file);
                    if (tv > tmax) 
                    {
                        tmax = tv;
                    }
                }
            }
            return tmax;
        }

        public double braid_row(int j, double t, StreamWriter file)
        {
            double tmax = 0.0;
            for (int i = 0; i < voice_count; i++)
            {
                if (!voices[i].mute)
                {
                    double tv = voices[i].braid_row(j, t, file);
                    if (tv > tmax)
                    {
                        tmax = tv;
                    }
                }
            }
            return tmax;
        }
        public double chain_row(int j, double t, StreamWriter file)
        {
            double tmax = 0.0;
            for (int i = 0; i < voice_count; i++)
            {
                if (!voices[i].mute)
                {
                    double tv = voices[i].chain_row(j, t, file);
                    if (tv > tmax)
                    {
                        tmax = tv;
                    }
                }
            }
            return tmax;
        }
        public void aaba(int level, StreamWriter file)
        {
            for (int i = 0; i < voice_count; i++)
            {
                if (!voices[i].mute)
                {
                    voices[i].aaba(level, 0, file, 0.0);
                }
            }
        }
        public void play(StreamWriter file)
        {
            for (int i = 0; i < voice_count; i++)
            {
                if (!voices[i].mute)
                {
                    voices[i].play(file, 1);
                }
            }
        }

        public void intevalHistograms()
        {
            StreamWriter acrossf = new StreamWriter(file_prefix + "acrossIH.txt");
            StreamWriter afterf = new StreamWriter(file_prefix + "afterIH.txt");
            StreamWriter verticalf = new StreamWriter(file_prefix + "verticalIH.txt");

            Interval_Histogram acrossIH = vectorFactory.getHistogram();
            Interval_Histogram afterIH = vectorFactory.getHistogram();
            Interval_Histogram verticalIH = vectorFactory.getHistogram();

            for(int vi=0; vi<vertex_count; vi++)
            {
                vertices[vi].intervalTally(afterIH, acrossIH, verticalIH);
            }

            acrossIH.Write(acrossf);
            afterIH.Write(afterf);
            verticalIH.Write(verticalf);

            acrossf.Close();
            afterf.Close();
            verticalf.Close();
        }

    }
}
