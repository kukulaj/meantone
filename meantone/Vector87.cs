using System;
using System.Collections.Generic;
using System.Text;

namespace meantone
{
    public class Vector87 : VectorEDO
    {
        Factory87 factory;

        public Vector87(Factory87 f, int p)
        {
            factory = f;
            edo = 87;
            pitch = p;
        }

        public Vector87(Factory87 f, Random rand, int root)
        {
            factory = f;
            edo = 87;
            pitch = rand.Next(170);
            pitch += root * 87;
        }

        public Vector87(Factory87 f, Random rand, int root, int loc)
        {
            factory = f;
            edo = 87;

            int small = loc % 18;
            int big = loc / 18;

            pitch = (28 * ((-6 * small + 9) / 18 + (3 * big + 9) / 18) + 51 * ((5 * small + 9) / 18 + (12 * big + 9) / 18)) % 87;

            //pitch = rand.Next(170);
            pitch += root * 87;
        }

        public Vector delta(int p)
        {
            return new Vector87(factory, pitch + p);
        }

        public override Vector[] jostle()
        {
            Vector[] options = new Vector[13];
            options[0] = delta(0);
            options[1] = delta(36);
            options[2] = delta(-36);
            options[3] = delta(28);
            options[4] = delta(-28);
            options[5] = delta(8);
            options[6] = delta(-8);
            options[7] = delta(51);
            options[8] = delta(-51);
            options[9] = delta(23);
            options[10] = delta(-23);
            options[11] = delta(15);
            options[12] = delta(-15);
            return options;
        }

        public override double concordance(Vector av)
        {
            Vector87 v = (Vector87)av;

            int dp = pitch - v.pitch;
            if (dp < 0)
            {
                dp = -dp;
            }

            if (dp < Factory87.icnt)
            {
                double result = 2.0 * factory.intervals[dp];
                return result;
            }
            return 1000.0;

        }

    }
}
