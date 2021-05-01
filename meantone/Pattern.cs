using System;
using System.Collections.Generic;
using System.Text;

namespace meantone
{
    public class Pattern
    {
        public Rhythm[] sequence;
        int measure_count;
        int face;
        int vi;
        double min_dur;
        Type_Map map;

        public Pattern(Type_Map pmap, Work w, int pvi, Rhythm[] root)
        {
            map = pmap;
            vi = pvi;
            measure_count = w.measure_count;
            sequence = new Rhythm[measure_count];

            min_dur = 1.5 / (4.5 + (double)vi);

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

        public Pattern(Type_Map pmap, Work w, int pvi)
        {
            map = pmap;
            measure_count = w.measure_count;
            sequence = new Rhythm[measure_count];
            vi = pvi;

            Rhythm origin = new Rhythm(w, map.duration);
            origin.singleton();

            min_dur = 0.9 / (2.25 + (double)vi);
            Rhythm osplit = origin.splinter(min_dur, 3 + vi);
            Rhythm ostick = osplit.stick(0.2);
            Rhythm root = ostick.silence(min_dur, 0.0, 0.0, vi, min_dur);
            assign(root, 0, measure_count);
        }


        private void divar(Rhythm root, int from, int to)
        {
            sequence[from] = root;
            Rhythm rsplit = root.splinter(min_dur, 4 + 2 * vi);
            Rhythm rstick = rsplit.stick(0.2);
            Rhythm nroot = rstick.silence(min_dur, 0.0, 0.0, vi, min_dur);
            assign(nroot, from + 1, to);
        }

        private void dup(Rhythm root, int i)
        {      
            for (int r = 0; r < face; r += map.row_size)
            {
                sequence[i + r ] = root;
            }
        }
        private void assign(Rhythm root, int from, int to)
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
