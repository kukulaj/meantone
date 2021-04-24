using System;
using System.Collections.Generic;
using System.Text;

namespace meantone
{
    public class Vector12 : VectorEDO
    {
        Factory12 factory;
        readonly int[] starting_pitch =
        { 0, 25, 50, 75, 100, 19 };

        readonly int[] syntonic = { 0, 7, 2, 10, 5 };

        readonly int[] diesis = { 0, 4, 8 };


        public Vector12(Factory12 f, int p)
        {
            edo = 12;
            factory = f;
            pitch = p;
        }

        public Vector12(Factory12 f, Random rand, int root)
        {
            edo = 12;
            factory = f;
            pitch = rand.Next(34);
            pitch += root * edo;
        }

        public Vector12(Factory12 f, Random rand, int root, int loc)
        {
            edo = 12;
            factory = f;
            //pitch = rand.Next(21);
            //pitch = starting_pitch[(loc/2) % 6];

            int x = loc % 40;
            int y = loc / 40;

            int xi = (x * (syntonic.Length - 1)) / 40;
            int yi = (y * (diesis.Length - 1)) / 12;

            int xp = syntonic[xi];
            int yp = diesis[yi];
            pitch = (xp + yp) % edo;

            pitch += root * edo;
        }

        public Vector delta(int p)
        {
            return new Vector12(factory, pitch + p);
        }

        public override Vector[] jostle()
        {
            Vector[] options = new Vector[13];
            options[0] = delta(0);
            options[1] = delta(5);
            options[2] = delta(-5);
            options[3] = delta(4);
            options[4] = delta(-4);
            options[5] = delta(3);
            options[6] = delta(-3);
            options[7] = delta(7);
            options[8] = delta(-7);
            options[9] = delta(2);
            options[10] = delta(-2);
            options[11] = delta(12);
            options[12] = delta(-12);

            return options;
        }

        public override double concordance(Vector av)
        {

            Vector12 v = (Vector12)av;

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
