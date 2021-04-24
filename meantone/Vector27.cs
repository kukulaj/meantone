using System;
using System.Collections.Generic;
using System.Text;

namespace meantone
{
    public class Vector27 : VectorEDO
    {
        Factory27 factory;


        public Vector27(Factory27 f, int p)
        {
            edo = 90;
            factory = f;
            pitch = p;
        }

        public Vector27(Factory27 f, Random rand, int root)
        {
            edo = 90;
            factory = f;
            pitch = rand.Next(60);
            pitch += root * 27;
        }

        public Vector27(Factory27 f, Random rand, int root, int loc)
        {
            edo = 90;
            factory = f;
            pitch = (((loc / 2) % 9) * 20);
            //pitch = rand.Next(60);
            //pitch = starting_pitch[(loc/2) % 6];
            int vi = root / 2;

            double rootlf = -0.4 + 0.8 * (double)vi;

            while (log_frequency() > rootlf + 0.3)
            {
                pitch -= 27;
            }
            while (log_frequency() < rootlf - 0.3)
            {
                pitch += 27;
            }
        }

        public override double log_frequency()
        {
            return Math.Log(10.0) * (1.0 / 90.0) * (double)pitch;
        }

        public Vector delta(int p)
        {
            return new Vector27(factory, pitch + p);
        }

        public override Vector[] jostle()
        {
            Vector[] options = new Vector[13];
            options[0] = delta(0);
            options[1] = delta(6);
            options[2] = delta(-6);
            options[3] = delta(20);
            options[4] = delta(-20);
            options[5] = delta(22);
            options[6] = delta(-22);
            options[7] = delta(1);
            options[8] = delta(-1);
            options[9] = delta(5);
            options[10] = delta(-5);
            options[11] = delta(7);
            options[12] = delta(-7);

            return options;
        }

        public override double concordance(Vector av)
        {

            Vector27 v = (Vector27)av;

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
