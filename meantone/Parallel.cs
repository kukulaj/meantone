using System;
using System.Collections.Generic;
using System.Text;

namespace meantone
{
    // a sequence of notes at one time and a sequence of notes at a different time
    // we'd like them to move in parallel
    // to move by the same interval, or at least in the same direction
    public class Parallel
    {
        Vertex before;
        Vertex after;
        HashSet<Parallel> across;

        public Parallel(Vertex b, Vertex a)
        {
            before = b;
            after = a;
            across = new HashSet<Parallel>(); 
        }
    }
}
