using System;
using System.Collections.Generic;
using System.Text;

namespace meantone
{
    class MatrixRepeat : Pattern
    {
        public MatrixRepeat(Type_Map pmap, Work w, int pvi, Rhythm[] roots) : base(pmap, w, pvi)
        {

            min_dur = 1.2 / (3.0 + (double)vi);
            Rhythm root = roots[0].vary(min_dur, vi);

            Rhythm[] path = new Rhythm[measure_count / map.row_size];
            path[0] = root;
            for (int i = 1; i < path.Length; i++)
            { 
                path[i] = path[i-1].vary(min_dur, vi);
            }

            int[] frow = new int[map.row_size];
            int[] brow = new int[map.row_size];


            for (int i = 0; i < map.row_size; i++)
            {
                frow[i] = 0;
                brow[i] = 0;
            }

            int fi = 0;
            int bi = measure_count - 1;
            while (fi <= bi)
            {
                if (map.rand.NextDouble() < 0.5)
                {
                    int i = fi % map.row_size;
                    sequence[fi] = path[frow[i]];
                    fi++;
                    if (map.rand.NextDouble() < 0.3)
                    {
                        frow[i]++;
                    }
                }
                else
                {
                    int i = bi % map.row_size;
                    sequence[bi] = path[brow[i]];
                    bi--;
                    if (map.rand.NextDouble() < 0.3)
                    {
                        brow[i]++;
                    }
                }

            }
        }
    }
}
