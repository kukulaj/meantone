using System;
using System.Collections.Generic;
using System.Text;

namespace meantone
{
    public class Matrix : Pattern
    {
        public Matrix(Type_Map pmap, Work w, int pvi, Rhythm[] roots ) : base(pmap, w, pvi)
        {
             
            min_dur = 1.8 / (2.0 + (double)vi);
            Rhythm root = roots[0].vary(min_dur, vi);

            Rhythm[] frow = new Rhythm[map.row_size];
            Rhythm[] brow = new Rhythm[map.row_size];
           

            for(int i = 0; i < map.row_size; i++)
            {
                frow[i] = root.vary(min_dur, vi);
                brow[i] = frow[i];
            }

            int fi = 0;
            int bi = measure_count - 1;
            while(fi <= bi)
            {
                if(map.rand.NextDouble() < 0.5)
                {
                    int i = fi % map.row_size;
                    sequence[fi] = frow[i];
                    fi++;
                    if (map.rand.NextDouble() < 0.05)
                    {
                        frow[i] = frow[i].vary(min_dur, vi);
                    }
                }
                else 
                {
                    int i = bi % map.row_size;
                    sequence[bi] = brow[i];
                    bi--;
                    if (map.rand.NextDouble() < 0.05)
                    {
                        brow[i] = brow[i].vary(min_dur, vi);
                    }
                }
                
            }
        }
    }
}
