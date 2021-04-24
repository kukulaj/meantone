using System;
using System.Collections.Generic;
using System.Text;

namespace meantone
{
    public class Vector106 : VectorEDO
    {
        Factory106 factory;
        readonly int[] starting_pitch =
        { 0, 25, 50, 75, 100, 19 };

        public Vector106(Factory106 f, int p)
        {
            edo = 106;
            factory = f;
            pitch = p;
        }

        public Vector106(Factory106 f, Random rand, int root)
        {
            edo = 106;
            factory = f;
            pitch = rand.Next(340);
            pitch += root * edo;
        }

        public Vector106(Factory106 f, Random rand, int root, int loc)
        {
            edo = 106;
            factory = f;
            pitch = rand.Next(210);
            //pitch = starting_pitch[(loc/2) % 6];
            pitch += root * edo;
        }

        public Vector delta(int p)
        {
            return new Vector106(factory, pitch + p);
        }

        public override Vector[] jostle()
        {
            Vector[] options = new Vector[13];
            options[0] = delta(0);
            options[1] = delta(44);
            options[2] = delta(-44);
            options[3] = delta(25);
            options[4] = delta(-25);
            options[5] = delta(19);
            options[6] = delta(-19);
            options[7] = delta(37);
            options[8] = delta(-37);
            options[9] = delta(18);
            options[10] = delta(-18);
            options[11] = delta(7);
            options[12] = delta(-7);

            return options;
        }

        public override double concordance(Vector av)
        {

            Vector106 v = (Vector106)av;

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
