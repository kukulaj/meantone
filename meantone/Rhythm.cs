using System;
using System.Collections.Generic;
using System.Text;

namespace meantone
{
    public class Rhythm
    {
        public List<Rhythm_Element> path;
        public double duration;
        Random rand;
        Work work;

        public Rhythm(Work w, double d)
        {
            duration = d;
            work = w;
            rand = work.rand;
            path = new List<Rhythm_Element>();
        }

        public Rhythm(Work w, double d, int parts)
        {
            work = w;
            rand = work.rand;
            duration = d;
            path = new List<Rhythm_Element>();
            double pduration = duration / (double)parts;
            double start = 0.0;

            for (int part = 0; part < parts; part++)
            {
                walk_part(start, pduration, 1, parts, false, 0.2);
                start += pduration;
            }
        }

        public Rhythm(Rhythm from, double min_dur)
        {
            work = from.work;
            rand = from.rand;
            duration = from.duration;
            path = new List<Rhythm_Element>();

            foreach(Rhythm_Element re in from.path)
            {
                walk_part(re.offset, re.duration, re.numer, re.denom, re.silent, min_dur);
            }
        }

        public Rhythm(Work w, Random r, double d, double min_dur, int max_div, double mphase, double dphase, int vi, double tdur)
        {
            work = w;
            rand = r;
            duration = d;

            work.trace.Write(string.Format("[{0}; {1}]", min_dur, max_div));

            path = new List<Rhythm_Element>();
            Rhythm_Element e = new Rhythm_Element(0.0, d, 1, 1);
             
            divide(e, min_dur, max_div, d, mphase, dphase, vi, tdur);
        }

        public void walk_part(double start, double pduration, int snumer, int sdenom, bool osilent, double min_dur)
        {
            const int prange = 4;
            int[] powers = new int[prange];
            double[] factors = new double[prange];
            int[] numers = new int[prange];
            int[] denoms = new int[prange];
            double[] probs = new double[prange];

            for (int i = 0; i < prange; i++)
            {
                powers[i] = rand.NextDouble() > 0.5 ? 0 : 1;
            }

            numers[0] = 2;
            numers[1] = 3;
            numers[2] = 4;
            numers[3] = 5;
            denoms[0] = 3;
            denoms[1] = 4;
            denoms[2] = 5;
            denoms[3] = 6;

            double max_factor = 1.0;
           
                for (int fi = 0; fi < prange; fi++)
            {
                factors[fi] = ((double)(numers[fi])) / (double)(denoms[fi]);
                max_factor *= factors[fi];
            }

            int div = (int)Math.Sqrt(pduration * max_factor / min_dur);

            probs[0] = 0.07;
            probs[1] = 0.07;
            probs[2] = 0.07;
            probs[3] = 0.07;
            //    probs[3] = 0.13;
            //    probs[4] = 0.1;

            double t = start;
            double te = start + pduration;
            bool done = false;

            //Rhythm_Element remainder = new Rhythm_Element(start, pduration, snumer, sdenom);

            while (!done)
            {
                for (int f = 0; f < prange; f++)
                {
                    if (rand.NextDouble() < probs[f])
                    {
                        powers[f] = 1 - powers[f];
                    }
                }

                double delta = pduration / (double)div;

                int numer = snumer;
                int denom = sdenom * div;
                for (int f = 0; f < prange; f++)
                {
                    double m = factors[f];
                    int top = numers[f];
                    int bottom = denoms[f];
                    int p = powers[f];
                    if (p < 0)
                    {
                        p = -p;
                        m = 1.0 / m;
                        int swap = top;
                        top = bottom;
                        bottom = swap;
                    }

                    for (int j = 0; j < p; j++)
                    {
                        delta *= m;
                        numer *= top;
                        denom *= bottom;
                    }
                }

               

                if (t + delta + 0.001 > te)
                {
                    done = true;
              //      path.Add(remainder);    
              //      remainder.silent = true;
                }
                else
                {
                    Rhythm_Element re = new Rhythm_Element(t, delta, numer, denom);
                    path.Add(re);
                    if (osilent)
                    {
                        if (rand.NextDouble() < 0.5) // some rests, too!
                        {
                            re.silent = true;
                        }
                    }
                    else
                    {
                        if (rand.NextDouble() < 0.11) // some rests, too!
                        {
                            re.silent = true;
                        }
                    }
                    t += delta;

                //    int rdenom = denom * remainder.denom;
                //    int rnumer = remainder.numer * denom - numer * remainder.denom;
                //    if (rnumer <= 0)
                //    {
                //        done = true;
                 //   }
                 //   else 
                 //   {
                 //       while (rdenom > 10000)
                 //       {
                     //       rnumer = (rnumer-1) / 5;
                   //         rdenom = rdenom / 5;
                     //   }

                   //     remainder = new Rhythm_Element(t, te - t, rnumer, rdenom);
                   // }
                }
            }
        }


        public void divide(Rhythm_Element e,
            double min_dur,
            int max_div,
            double d,
            double mphase,
            double vphase,
            int vi,
            double tdur)
        {
            e.reduce();

            // if the note is too short to split, just play it straight
            // or if the note is longer, roll the dice and maybe play it straight
            // (1.0 - 2.0 * min_dur) / (11.0 * e.duration + 1.0 - 24.0 * min_dur)

            double local_min = min_dur;
            if (e.duration < 2 * local_min || rand.NextDouble() < Math.Exp(0.18 * (2.0 - e.duration / local_min)))
            {
                double zerop = tdur
                               * (2.0
                               + Math.Cos(2.0 * Math.PI * (0.5 * mphase + e.offset / duration))
                               * Math.Cos(2.0 * Math.PI * (vphase + ((double)vi) / 4.0)));
                double restp = Math.Exp(-(e.duration - min_dur) / zerop);
                if (rand.NextDouble() < restp)
                {
                    work.trace.Write(string.Format("R{0}/{1}", e.numer, e.denom));
                    return;
                }

                e.duration *= 0.92;
                path.Add(e);
                work.trace.Write(string.Format("{0}/{1}", e.numer, e.denom));
                return;
            }

            // divide the note into a random number of parts, but not too many
            int mdiv = (int)(e.duration / min_dur);
            if (mdiv > max_div)
            {
                mdiv = max_div;
            }
            int div = 2 + rand.Next(mdiv - 1);

            double time = e.offset;
            double pulse = e.duration / (double)div;
            int denom = div * e.denom;

            int parts = 2 + rand.Next(div - 1);
            Rhythm_Element[] part = new Rhythm_Element[parts];
            for (int i = 0; i < parts; i++)
            {
                Rhythm_Element ne = new Rhythm_Element(time, pulse, e.numer, denom);
                part[i] = ne;
            }

            for (int i = 0; i < div - parts; i++)
            {
                int pick = rand.Next(parts);
                part[pick].duration += pulse;
                part[pick].numer += e.numer;
            }

            for (int i = 0; i < parts; i++)
            {
                part[i].offset = time;
                time += part[i].duration;
            }

            work.trace.Write("(");
            bool first = true;
            for (int i = 0; i < parts; i++)
            {
                if (!first)
                {
                    work.trace.Write(" ");
                }
                first = false;

                divide(part[i], min_dur, max_div, d, mphase, vphase, vi, tdur);
            }
            work.trace.Write(")");
        }

        public void singleton()
        {
            path.Clear();
            Rhythm_Element ne = new Rhythm_Element(0.0, duration, 1, 1);
             
            path.Add(ne);
        }

        public Rhythm vary(double min_dur, int vi)
        {
            Rhythm rsplit = splinter(min_dur, 4 + 2 * vi);
            Rhythm rstick = rsplit.stick(0.2);
            Rhythm nroot = rstick.silence(min_dur, 0.0, 0.0, vi, min_dur/ 2.0);
            return nroot;
        }

        public Rhythm vary2(double min_dur, int vi)
        {
            Rhythm nroot = new Rhythm(this, min_dur);
            nroot = nroot.stick(0.2);
            //nroot = rstick.silence(min_dur, 0.0, 0.0, vi, min_dur);
            return nroot;
        }

        public Rhythm silence(double min_dur, double mphase, double vphase, int vi, double tdur)
        {
            Rhythm silenced = new Rhythm(work, duration);

            foreach (Rhythm_Element re in path)
            {
                Rhythm_Element ne = new Rhythm_Element(re.offset, re.duration, re.numer, re.denom);
                 
                double zerop = tdur
                               * (2.0
                               + Math.Cos(2.0 * Math.PI * (0.5 * mphase + re.offset / duration))
                               * Math.Cos(2.0 * Math.PI * (vphase + ((double)vi) / 4.0)));
                double restp = Math.Exp(-(re.duration - min_dur) / zerop);
                if (rand.NextDouble() < restp)
                {
                    ne.silent = !re.silent;
                }
                silenced.path.Add(ne);
            }
            return silenced;
        }

        public Rhythm stick(double sprob)
        {
            Rhythm stuck = new Rhythm(work, duration);

            Rhythm_Element last = null;

            foreach (Rhythm_Element re in path)
            {
                if (last == null || last.denom != re.denom || rand.NextDouble() > sprob)
                {
                    if (last != null)
                    {
                        last.reduce();
                        stuck.path.Add(last);
                    }

                    last = new Rhythm_Element(re.offset, re.duration, re.numer, re.denom);
                     
                    last.silent = re.silent;
                }
                else
                {
                    last.numer += re.numer;
                    last.duration += re.duration;
                    last.silent &= re.silent;
                }
            }
            if (last != null)
            {
                last.reduce();
                stuck.path.Add(last);
            }

            return stuck;
        }

        public Rhythm splinter(double min_dur,
            int max_div)
        {
            Rhythm splintered = new Rhythm(work, duration);

            foreach (Rhythm_Element re in path)
            {
                int div = 1;
                double prob = Math.Exp(0.18 * (2.0 - re.duration / min_dur));
                if (re.duration > 2 * min_dur
                    && rand.NextDouble() > prob)
                {
                    // divide the note into a random number of parts, but not too many
                    int mdiv = (int)(re.duration / min_dur);
                    if (mdiv > max_div)
                    {
                        mdiv = max_div;
                    }
                    div = 2 + rand.Next(mdiv - 1);
                }

                double time = re.offset;
                double pulse = re.duration / (double)div;
                int denom = div * re.denom;
                for (int i = 0; i < div; i++)
                {
                    Rhythm_Element ne = new Rhythm_Element(time, pulse, re.numer, denom); 
                    ne.silent = re.silent;
                    splintered.path.Add(ne);
                    time = time + pulse;
                }
            }
            return splintered;
        }
    }
}
