using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace meantone
{
    public class Interval_Histogram
    {
        public virtual void Tally(Interval i)
        { }
        public virtual void Write(StreamWriter file)
        { }
    }
}
