using System;
using System.Collections.Generic;
using System.Text;

namespace meantone
{
    public abstract class VectorFactory
    {
        public abstract Vector randomVector(Random rand, int root);

        public virtual Vector initialVector(Random rand, int root, int location)
        {
            return randomVector(rand, root);
        }

        public virtual Interval_Histogram getHistogram()
        {
            return new Interval_Histogram();
        }

        public virtual void show_pattern()
        {
        }
        public virtual void scaleSearch()
        {
        }
    }
}
