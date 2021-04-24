using System;
using System.Collections.Generic;
using System.Text;

namespace meantone
{
    public class Vector10 : VectorEDO
    {
        Factory10 factory;

        public Vector10(Factory10 f, int p)
        {
            edo = 10;
            factory = f;
            pitch = p;
        }

        public Vector10(Factory10 f, Random rand, int root)
        {
            edo = 10;
            factory = f;
            pitch = rand.Next(15);
            pitch += root * edo;
        }

        public Vector10(Factory10 f, Random rand, int root, int loc)
        {
            edo = 10;
            factory = f;
            pitch = rand.Next(15);
            //pitch = starting_pitch[(loc) % 14];


            pitch += root * edo;
        }


        public Vector delta(int p)
        {
            return new Vector10(factory, pitch + p);
        }

        public override Vector[] jostle()
        {
            Vector[] options = new Vector[5];
            options[0] = delta(0);
            options[1] = delta(3);
            options[2] = delta(-3);
            options[3] = delta(7);
            options[4] = delta(-7);

            return options;
        }

        public override double concordance(Vector av)
        {

            Vector10 v = (Vector10)av;

            int dp = pitch - v.pitch;
            if (dp < 0)
            {
                dp = -dp;
            }
            dp = dp % edo;
            if (dp > (edo + 1) / 2)
            {
                dp = edo - dp;
            }

            double result = 2.0 * factory.intervals[dp];
            return result;
        }
    }
}
