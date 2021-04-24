using System;
using System.Collections.Generic;
using System.Text;

namespace meantone
{
    public class Vector84 : VectorEDO
    {
        Factory84 factory;

        public Vector84(Factory84 f, int p)
        {
            factory = f;
            edo = 84;
            pitch = p;
        }

        public Vector84(Factory84 f, Random rand, int root)
        {
            factory = f;
            edo = 84;
            pitch = rand.Next(170);
            pitch += root * 84;
        }

        public Vector84(Factory84 f, Random rand, int root, int loc)
        {
            factory = f;
            edo = 84;
            pitch = rand.Next(170);
            pitch += root * 84;
        }

        public Vector delta(int p)
        {
            return new Vector84(factory, pitch + p);
        }

        public override Vector[] jostle()
        {
            Vector[] options = new Vector[13];
            options[0] = delta(0);
            options[1] = delta(35);
            options[2] = delta(-35);
            options[3] = delta(27);
            options[4] = delta(-27);
            options[5] = delta(8);
            options[6] = delta(-8);
            options[7] = delta(49);
            options[8] = delta(-49);
            options[9] = delta(22);
            options[10] = delta(-22);
            options[11] = delta(14);
            options[12] = delta(-14);
            return options;
        }

        public override double concordance(Vector av)
        {
            Vector84 v = (Vector84)av;

            int dp = pitch - v.pitch;
            if (dp < 0)
            {
                dp = -dp;
            }

            if (dp < Factory84.icnt)
            {
                double result = 2.0 * factory.intervals[dp];
                return result;
            }
            return 1000.0;

        }

    }
}
