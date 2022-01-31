using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace meantone
{
    public class VectorEDO5 : VectorEDO
    {
        FactoryEDO5 factory;

        public VectorEDO5(FactoryEDO5 f, int p)
        {
            factory = f;
            edo = f.edo;
            pitch = p;
        }

        public VectorEDO5(FactoryEDO5 f, Random rand, int root)
        {
            factory = f;
            edo = f.edo;
            pitch = rand.Next(edo);
            pitch += root * edo;
        }

        public VectorEDO5(FactoryEDO5 f, Random rand, int root, int loc)
        {
            factory = f;
            edo = f.edo;
            pitch = factory.pumpStructure.pattern(loc);

            int incr = ((edo + factory.scale.Length/2) / factory.scale.Length) * factory.scale.Length;

            while (pitch < root * edo)
            {
                pitch += incr; // factory.scale.Length;
            }
        }

        public Vector delta(int p)
        {
            return new VectorEDO5(factory, pitch + p);
        }

         

        public override Vector[] jostle()
        {

            int ostep = 5;
            int ocnt = (3 * edo) / ostep; ;

            int move = 0;
            Vector[] options = new Vector[1 + 2 * ocnt];
            options[0] = delta(0);
            for (int oi = 0; oi < ocnt; oi++)
            {
                int step = 1 + factory.map.rand.Next(ostep);
                move += step;
                options[1 + 2 * oi] = delta(move);
                options[2 + 2 * oi] = delta(-move);
            }

            /*
            const int r3 = 3;
            const int r5 = 2;
            const int r7 = 1;
            Vector[] options = new Vector[4 + (2*r3+1)*(2*r5+1)*(2*r7+1)];
            options[0] = delta(factory.edo);
            options[1] = delta(-factory.edo);
            options[2] = delta(2 * factory.edo);
            options[3] = delta(-2 * factory.edo);
            int oi = 4;
            for (int i3 = -r3; i3 <= r3; i3++)
            {
                for (int i5 = -r5; i5 <= r5; i5++)
                {
                    for (int i7 = -r7; i7 <= r7; i7++)
                    {
                        options[oi] = make_option(i3, i5, i7);
                        oi++;
                    }
                }
            }
            */
            return options;
        }

        public override double vertical_concordance(Vector av)
        {
            VectorEDO5 v = (VectorEDO5)av;

            int dp = pitch - v.pitch;
            return factory.vertical_interval_cost(dp);

        }

        public override double vertical_concordance(Vector av, int phase)
        {
            VectorEDO5 v = (VectorEDO5)av;

            int dp = pitch - v.pitch;
            return factory.vertical_interval_cost(dp, phase);

        }

        public override double concordance(Vector av)
        {
            VectorEDO5 v = (VectorEDO5)av;

            int dp = pitch - v.pitch;
            return factory.interval_cost(dp);

        }

        public override Interval interval(Vector other)
        {
            IntervalEDO5 i = new IntervalEDO5();
            i.dist = Math.Abs(pitch - ((VectorEDO5)other).pitch);
            return i;
        }

        public override bool inScale(int loc)
        {
            return factory.inScale(pitch, loc);
        }

    }
}
