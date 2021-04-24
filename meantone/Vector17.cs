using System;
using System.Collections.Generic;
using System.Text;

namespace meantone
{
    public class Vector17 : VectorEDO
    {
        Factory17 factory;
        readonly int[] starting_pitch =
        { 0, 25, 50, 75, 100, 19 };

        public Vector17(Factory17 f, int p)
        {
            edo = 17;
            factory = f;
            pitch = p;
        }

        public Vector17(Factory17 f, Random rand, int root)
        {
            edo = 17;
            factory = f;
            pitch = rand.Next(340);
            pitch += root * edo;
        }

        public Vector17(Factory17 f, Random rand, int root, int loc)
        {
            edo = 17;
            factory = f;
            pitch = rand.Next(210);
            //pitch = starting_pitch[(loc/2) % 6];
            pitch += root * edo;
        }

        public Vector delta(int p)
        {
            return new Vector17(factory, pitch + p);
        }

        public override Vector[] jostle()
        {
            Vector[] options = new Vector[13];
            options[0] = delta(0);
            options[1] = delta(7);
            options[2] = delta(-7);
            options[3] = delta(4);
            options[4] = delta(-4);
            options[5] = delta(3);
            options[6] = delta(-3);
            options[7] = delta(10);
            options[8] = delta(-10);
            options[9] = delta(1);
            options[10] = delta(-1);
            options[11] = delta(17);
            options[12] = delta(-17);

            return options;
        }

        public override double concordance(Vector av)
        {

            Vector17 v = (Vector17)av;

            int dp = pitch - v.pitch;
            if (dp < 0)
            {
                dp = -dp;
            }
            dp = dp % edo;
            if (dp > edo / 2)
            {
                dp = edo - dp;
            }

            double result = 2.0 * factory.intervals[dp];
            return result;
        }
    }
}
