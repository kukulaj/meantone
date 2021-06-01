using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace meantone
{
    public class Vertex
    {
        HashSet<Vertex> vertical;
        HashSet<Vertex> acrossi;
        HashSet<Vertex> acrossp;
        public Dictionary<Vertex, Parallel> before;
        public Dictionary<Vertex, Parallel> after;
        public Vector vector;
        public Measure measure;
        public double duration;
        public double start;
        public double costf;
        public double amplitude;

        public Vertex(Measure m, double d, double s)
        {
            measure = m;
            duration = d;
            start = s;
            measure.voice.work.add_vertex(this);

            vertical = new HashSet<Vertex>();
            acrossi = new HashSet<Vertex>();
            acrossp = new HashSet<Vertex>();
            before = new Dictionary<Vertex, Parallel>();
            after = new Dictionary<Vertex, Parallel>();

            vector = measure.voice.work.vectorFactory.initialVector(m.voice.work.rand,
                measure.voice.root,
                measure.location);
        }

        public double end()
        {
            return start + duration;
        }

        public void link_acrossi(Vertex v)
        {
            if (v != this)
            {
                acrossi.Add(v);
            }
        }
        public void link_acrossp(Vertex v)
        {
            if (v != this)
            {
                acrossp.Add(v);
            }
        }

        public void link_vertical(Vertex v)
        {
            if (v != this)
            {
                vertical.Add(v);
            }
        }

        public bool check()
        {
            bool ok = true;

            foreach (KeyValuePair<Vertex, Parallel> kp in after)
            {
                if (!kp.Key.before.ContainsKey(this))
                {
                    ok = false;
                }
            }

            foreach (KeyValuePair<Vertex, Parallel> kp in before)
            {
                if (!kp.Key.after.ContainsKey(this))
                {
                    ok = false;
                }
            }

            foreach (Vertex v in vertical)
            {
                if (!v.vertical.Contains(this))
                {
                    ok = false;
                }
            }

            foreach (Vertex v in acrossi)
            {
                if (!v.acrossi.Contains(this))
                {
                    ok = false;
                }
            }

            foreach (Vertex v in acrossp)
            {
                if (!v.acrossp.Contains(this))
                {
                    ok = false;
                }
            }

            return ok;
        }

        public bool overlap(Vertex v)
        {
            if (end() + 0.001 < v.start)
            {
                return false;
            }

            if (v.end() + 0.001 < start)
            {
                return false;
            }

            return true;
        }

        public double cost()
        {
            costf = cost(vector);
            return costf;
        }

        public double scost(Vector v)
        {
            double df = v.log_frequency() - measure.voice.rootlf;
            double result = 2000.0 * df * df * df * df;

            if(!v.inScale())
            {
                result += 8000;
            }

            return result;
        }

        public double ocost(Vector v, Vertex other)
        {
            double upper = v.log_frequency();
            double lower = other.vector.log_frequency();
            if (measure.voice.vi < other.measure.voice.vi)
            {
                double swap = lower;
                lower = upper;
                upper = swap;
            }

            double result = 5.0 * Math.Exp(lower - upper);
            return result;
        }

        double weighted_adjacence(Vector v, Vertex there)
        {
            int d = measure.duri;
            if (d < there.measure.duri)
            {
                d = there.measure.duri;
            }

            double weight = 1.0 / (1.0 + ((double)d));

            return weight * v.concordance(there.vector)
                + (1.0 - weight) * v.adjacence(there.vector);
        }


        public double rcost(Vector v)
        {
            double total = 0.0;

            foreach (KeyValuePair<Vertex, Parallel> there in before)
            {

                total += v.adjacence(there.Key.vector);
            }

            total = correction(total);

            foreach (KeyValuePair<Vertex, Parallel>  there in after)
            {
                total += v.adjacence(there.Key.vector);
            }

            total = correction(total);

            foreach (Vertex there in vertical)
            {
                total += ocost(v, there);
                total = correction(total);
                total += v.concordance(there.vector);
                total = correction(total);
                if (v.octave_equivalent(there.vector))
                {
                    total += 7.5;
                }
            }
            total = correction(total);

            return total;
        }

        public double pitch_cost(Vector v, bool individual)
        {
            double total = 0.0;
            foreach (Vertex there in acrossp)
            {
                total += v.sameness(there.vector);
            }
            return (individual ? 2.0 : 1.0) * total;
        }

        public void align_count(int[] tallies)
        {
           

            foreach (KeyValuePair<Vertex, Parallel> kvp in before)
            {
               
                foreach (Parallel there in kvp.Value.across)
                {
                    tallies[0]++;
                    if (vector.same_interval(kvp.Key.vector, there.after.vector, there.before.vector))
                    {
                        tallies[1]++;
                    }
                }
            }

            foreach (KeyValuePair<Vertex, Parallel> kvp in after)
            {
               
                foreach (Parallel there in kvp.Value.across)
                {
                    tallies[0]++;
                    if (vector.same_interval(kvp.Key.vector, there.before.vector, there.after.vector))
                    {
                        tallies[1]++; 
                    }
                }
            }

        }

        public double interval_cost(Vector v, bool individual)
        {
            double total = 0.0;

            foreach(KeyValuePair<Vertex, Parallel> kvp in before)
            {
                foreach(Parallel there in kvp.Value.across)
                if (!v.same_interval(kvp.Key.vector, there.after.vector, there.before.vector))
                {
                    total += 1.0;
                }
            }

            foreach (KeyValuePair<Vertex, Parallel> kvp in after)
            {
                foreach (Parallel there in kvp.Value.across)
                    if (!v.same_interval(kvp.Key.vector, there.before.vector, there.after.vector))
                    {
                        total += 1.0;
                    }
            }
           
            double result =  320.0 * total;
            if(individual)
            {
                result *= 4.0;
            }
            return result;
        }

        public double cost(Vector v)
        {
            return scost(v) + rcost(v) + interval_cost(v, false) + pitch_cost(v, false);
        }

        double correction(double c)
        {
            if (Double.IsInfinity(c) || Double.IsNaN(c))
                return 1000.0;
            else
                return c;
        }

        public double dcost(Vector v)
        {
            double sc = scost(v);
            sc = correction(sc);
            double rc = rcost(v);
            rc = correction(rc);
            double ic = interval_cost(v, true);
            ic = correction(ic);
            double pc = pitch_cost(v, true);
            pc = correction(pc);

            return sc + 2.0 * rc + ic + pc;
        }

        public Vector jostle(double temperature)
        {
            Vector[] options = vector.jostle();
            int ocnt = options.GetLength(0);
            double[] costs = new double[ocnt];

            double icost = dcost(vector);
            for (int i = 0; i < ocnt; i++)
            {
                costs[i] = dcost(options[i]);
            }


            double ptot = 0;
            double[] probs = new double[ocnt];
            for (int i = 0; i < ocnt; i++)
            {
                double p = Math.Exp(-costs[i] / temperature);
                ptot += p;
                probs[i] = p;
            }

            double pick = measure.voice.work.rand.NextDouble() * ptot;
            int pi = 0;
            for (int i = 0; i < ocnt; i++)
            {
                if (pick < probs[i])
                {
                    pi = i;
                    break;
                }
                pick -= probs[i];
            }

            Vector choice = options[pi];
            // vector = choice;
            double delta = (costs[pi] - icost);

            return choice;
        }

        public double play(StreamWriter file, double s)
        {
            int vi = measure.voice.vi;
            int instrument;
            switch (vi)
            {
                case 1:
                    instrument = 4;
                    break;
                case 2:
                    instrument = /*measure.itype / 4 */ (measure.location % 12) / 6 > 0 ? 5 : 6;

                    break;
                case 3:
                    instrument = /*measure.itype*/ measure.location % 2 > 0 ? 2 : 3;
                    break;
                default:
                    instrument = 1;
                    break;
            }

            double d = duration * measure.shrink();
            double cstarttime = s + start * measure.shrink();
            double starttime = cstarttime + measure.voice.work.tweak_time(cstarttime);
            double endtime = cstarttime + d + measure.voice.work.tweak_time(cstarttime + d);

            string score = string.Format("i{0} {1} {2} {3} {4}",
               vi + 1 /*instrument*/,
               starttime,
                0.95 * (endtime - starttime),
                vector.frequency(),
                amplitude / 5.0);
            file.WriteLine(score);
            return s + d;
        }

        public void intervalTally(Interval_Histogram afterIH, 
            Interval_Histogram acrossIH, 
            Interval_Histogram verticalIH)
        {
            foreach(Vertex v in vertical)
            {
                Interval i = vector.interval(v.vector);
                verticalIH.Tally(i);
            }

            foreach (Vertex v in acrossi)
            {
                Interval i = vector.interval(v.vector);
                acrossIH.Tally(i);
            }
            foreach (Vertex v in acrossp)
            {
                Interval i = vector.interval(v.vector);
                acrossIH.Tally(i);
            }
            foreach (KeyValuePair<Vertex, Parallel> v in after)
            {
                Interval i = vector.interval(v.Key.vector);
                afterIH.Tally(i);
            }

        }

    }
}
