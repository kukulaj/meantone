using System;
using System.Collections.Generic;
using System.Text;

namespace meantone
{
    public class PatternRepeat : Pattern
    {
        public PatternRepeat(Type_Map pmap, Work w, int pvi, Rhythm[] roots) : base(pmap, w, pvi)
        {
            for(int i = 0; i<measure_count; i++)
            {
                sequence[i] = roots[i];
            }
        }
    }
}
