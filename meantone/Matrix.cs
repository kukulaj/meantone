using System;
using System.Collections.Generic;
using System.Text;

namespace meantone
{
    public class Matrix : Pattern
    {
        public Matrix(Type_Map pmap, Work w, int pvi, Rhythm[] roots ) : base(pmap, w, pvi)
        {
             
            min_dur = 0.95 / (2.0 + (double)vi);
            Rhythm root = roots[0].vary(min_dur, vi);

            Rhythm[] row = new Rhythm[map.row_size];
            row[0] = root;

            for(int i = 1; i < map.row_size; i++)
            {
                row[i] = root.vary(min_dur, vi);
            }

            for (int i = 0; i < measure_count; i++)
            {
                sequence[i] = row[i % map.row_size];
            }
        }
    }
}
