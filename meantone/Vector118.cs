using System;
using System.Collections.Generic;
using System.Text;

namespace meantone
{
    public class Vector118 : VectorEDO
    {
        Factory118 factory;

        readonly int[] vishnuzma =
            {
            0, 80, 111, 73, 104, 66, 28,
            59, 21, 52, 14,  45,  7, 87
            };

        readonly int[] luna = { 0, 69, 107, 27, 65, 103, 23, 61, 99, 50, 88, 8, 46, 84, 4, 42, 80 };

        readonly int[] schisma = { 0, 69, 20, 89, 40, 78, 29, 98, 49 };

        public Vector118(Factory118 f, int p)
        {
            edo = 118;
            factory = f;
            pitch = p;
            if (p > 2000)
            {
                p = p % 118;
            }

        }

        public Vector118(Factory118 f, Random rand, int root)
        {
            edo = 118;
            factory = f;
            pitch = rand.Next(140);
            pitch += root * 118;
        }

        public Vector118(Factory118 f, Random rand, int root, int loc)
        {
            edo = 118;
            factory = f;
            // pitch = rand.Next(340);
            // pitch = starting_pitch[(loc) % 14];

            /*
            int x = loc % 9;
            int y = loc / 9;

            int xi = (x * (schisma.Length - 1)) / 9;
            int yi = (y * (luna.Length - 1)) / 17;
            */
            //int xi = (loc % 144) / 16;
            //int xp = schisma[xi];
            // int yp = luna[yi];
            //pitch = xp % edo;

            int row_size = 24;
            int big = loc / row_size;
            int small = loc % row_size;

            pitch = (38 * ((1 * small + row_size / 5) / row_size + (15 * big + (3 * row_size) / 5) / row_size)
                + 69 * ((8 * small + (2 * row_size) / 5) / row_size + (2 * big + (4 * row_size) / 5) / row_size))
                % edo;

            pitch += root * edo;
        }


        public Vector delta(int p)
        {
            return new Vector118(factory, pitch + p);
        }

        public override Vector[] jostle()
        {
            Vector[] options = new Vector[13];
            options[0] = delta(0);
            options[1] = delta(49);
            options[2] = delta(-49);
            options[3] = delta(38);
            options[4] = delta(-38);
            options[5] = delta(31);
            options[6] = delta(-31);
            options[7] = delta(69);
            options[8] = delta(-69);
            options[9] = delta(20);
            options[10] = delta(-20);
            options[11] = delta(11);
            options[12] = delta(-11);

            return options;
        }

        public override double concordance(Vector av)
        {

            Vector118 v = (Vector118)av;

            int dp = pitch - v.pitch;
            if (dp < 0)
            {
                dp = -dp;
            }
            dp = dp % 118;
            if (dp > 59)
            {
                dp = 118 - dp;
            }

            double result = 2.0 * factory.intervals[dp];
            return result;
        }
    }
}
