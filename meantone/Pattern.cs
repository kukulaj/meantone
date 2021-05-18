using System;
using System.Collections.Generic;
using System.Text;

namespace meantone
{
    public class Pattern
    {
        public Rhythm[] sequence;
        public int measure_count;
        public int face;
        public int vi;
        public double min_dur;
        public Type_Map map;

        protected Pattern(Type_Map pmap, Work w, int pvi)
        {
            map = pmap;
            vi = pvi;
            measure_count = w.measure_count;
            sequence = new Rhythm[measure_count];
        }

        public Pattern(Type_Map pmap, Work w, int pvi, Rhythm[] root)
        {
            map = pmap;
            vi = pvi;
            measure_count = w.measure_count;
            sequence = new Rhythm[measure_count];

            min_dur = 3.0 / (4.5 + (double)vi);

            face = map.size / map.row_size;
            Rhythm lroot = null;
            if (face == 1)
            {
                lroot = new Rhythm(w, vi);  
            }

            int fstart = 0;
            int fend = (measure_count / face) - 1;

            while (fstart <= fend)
            {
                if (face > 1)
                {
                    assign(root[fstart], 0 + fstart * face, map.row_size + fstart * face);
                    if (fend > fstart)
                    {
                        assign(root[fstart], 0 + fend * face, map.row_size + fend * face);
                    }

                }
                else
                {
                    lroot = lroot.vary(min_dur, vi);
                    min_dur *= 0.95;

                    sequence[fstart] = lroot;
                    if (fend > fstart)
                    {
                        sequence[fend] = sequence[fstart];
                    }
                }
                fstart++;
                fend--;
            }
            
        }

        protected void divar(Rhythm root, int from, int to)
        {
            sequence[from] = root;
            Rhythm rsplit = root.splinter(min_dur, 4 + 2 * vi);
            Rhythm rstick = rsplit.stick(0.2);
            Rhythm nroot = rstick.silence(min_dur, 0.0, 0.0, vi, min_dur);
            assign(nroot, from + 1, to);
        }

        protected void dup(Rhythm root, int i)
        {      
            for (int r = 0; r < face; r += map.row_size)
            {
                sequence[i + r ] = root;
            }
        }
        protected void assign(Rhythm root, int from, int to)
        {
            if (to - from < 4)
            {
                for (int i = from; i < to; i++)
                {
                    dup(root, i);
                }
                return;
            }

            int mid = (from + to) / 2;
            //dup(root, from);
            dup(root, mid);
            dup(root, to - 1);
            assign(/*new Rhythm(root, min_dur)*/ root.vary(min_dur, vi), from, mid);
            assign(/*new Rhythm(root, min_dur)*/ root.vary(min_dur, vi), mid + 1, to - 1);
        }

    }
}
