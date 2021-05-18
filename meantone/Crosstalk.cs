using System;
using System.Collections.Generic;
using System.Text;

namespace meantone
{
    public class Crosstalk : Pattern
    {
        public Crosstalk(Type_Map pmap, Work w, int pvi) : base(pmap, w, pvi)
        {
            Rhythm origin = new Rhythm(w, map.duration);
            origin.singleton();

            min_dur = 0.9 / (2.25 + (double)vi);
            Rhythm osplit = origin.splinter(min_dur, 2 + vi);
            Rhythm ostick = osplit.stick(0.2);
            Rhythm root = ostick.silence(min_dur, 0.0, 0.0, vi, min_dur);
            assign(root, 0, measure_count);
        }
    }
}
