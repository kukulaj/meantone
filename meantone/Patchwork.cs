using System;
using System.Collections.Generic;
using System.Text;

namespace meantone
{
    public class Patchwork : Pattern
    {
       
        public Patchwork(Type_Map pmap, Work w, int pvi) : base(pmap, w, pvi)
        {
            Rhythm origin = new Rhythm(w, map.duration);
            origin.singleton();

            min_dur = 0.9 / (2.25 + (double)vi);
            Rhythm osplit = origin.splinter(min_dur, 2 + vi);
            Rhythm ostick = osplit.stick(0.2);
            Rhythm root = ostick.silence(min_dur, 0.0, 0.0, vi, min_dur);
            root = root.vary(min_dur, vi);

            double vprob = 0.1 + 0.6 * (double)vi / (double)(vi + 2);
            for(int i = 0; i < measure_count; i++)
            {
                if (map.rand.NextDouble() < vprob)
                {
                    sequence[i] = root.vary(min_dur, vi);
                }
                else 
                {
                    sequence[i] = root;
                }
            }
           
        }

        public Patchwork(Type_Map pmap, Work w, int pvi, Rhythm[] roots) : base(pmap, w, pvi)
        {
            min_dur = 0.9 / (2.25 + (double)vi);
            Rhythm root = roots[0].vary(min_dur, vi);

            double vprob = 0.1 + 0.6 * (double)vi / (double)(vi + 2);
            for (int i = 0; i < measure_count; i++)
            {
                if (map.rand.NextDouble() < vprob)
                {
                    sequence[i] = root.vary(min_dur, vi);
                }
                else
                {
                    sequence[i] = root;
                }
            }

        }
    }
}
