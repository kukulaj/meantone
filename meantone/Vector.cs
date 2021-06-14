using System;
using System.Collections.Generic;
using System.Text;

namespace meantone
{
    public abstract class Vector
    {
        public abstract bool same(Vector v);

        public abstract Vector[] jostle();

        public abstract double log_frequency();

        public abstract double concordance(Vector av);

        public virtual double vertical_concordance(Vector av)
        {
            return concordance(av);
        }

        public virtual double vertical_concordance(Vector av, int phase)
        {
            return concordance(av);
        }
        public virtual double horizontal_concordance(Vector av)
        {
            return concordance(av);
        }


        public abstract bool octave_equivalent(Vector av);

        public abstract double histd();

        public double adjacence(Vector v)
        {
            double conc = concordance(v);

            double dist = log_frequency() - v.log_frequency();

            return conc + 300.0 * dist * dist;
        }

        public double sameness(Vector v)
        {
            if (same(v))
            {
                return 0.0;
            }

            return 5.0 + concordance(v);
        }

        public abstract bool same_interval(Vector v, Vector w1, Vector w2);

        public double frequency()
        {
            return Math.Exp(log_frequency());
        }

        public virtual Interval interval(Vector other)
        {
            return new Interval();
        }

        public virtual bool inScale()
        {
            return true;
        }
    }

}
