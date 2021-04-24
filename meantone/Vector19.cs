using System;
using System.Collections.Generic;
using System.Text;

namespace meantone
{
    public class Vector19 : VectorEDO
    {
        Factory19 factory;

        readonly double[] intervals =
            { 0, // 0
            5, // 1
            2, // 2
            4, // 3
            5, // 4
            1, // 5
            1, // 6
            4, // 7
            1, // 8
            4, // 9
        };

        public Vector19(Factory19 f, int p)
        {
            factory = f;
            edo = 19;
            pitch = p;
        }

        public Vector19(Factory19 f, Random rand, int root)
        {
            factory = f;
            edo = 19;
            pitch = rand.Next(60);
            pitch += root * 19;
        }

        public Vector19(Factory19 f, Random rand, int root, int loc)
        {
            factory = f;
            edo = 19;
            pitch = (((loc % 12) / 2) * 6) % 19;
            pitch += root * 19;
        }

        public Vector delta(int p)
        {
            return new Vector19(factory, pitch + p);
        }

        public override Vector[] jostle()
        {
            Vector[] options = new Vector[11];
            options[0] = delta(0);
            options[1] = delta(8);
            options[2] = delta(-8);
            options[3] = delta(6);
            options[4] = delta(-6);
            options[5] = delta(5);
            options[6] = delta(-5);
            options[7] = delta(11);
            options[8] = delta(-11);
            options[9] = delta(3);
            options[10] = delta(-3);

            return options;
        }

        public override double concordance(Vector av)
        {
            Vector19 v = (Vector19)av;

            int dp = pitch - v.pitch;
            if (dp < 0)
            {
                dp = -dp;
            }
            dp = dp % 19;
            if (dp > 9)
            {
                dp = 19 - dp;
            }

            double result = 2.0 * factory.intervals[dp];
            return result;
        }

    }
}
