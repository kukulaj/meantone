using System;
using System.Collections.Generic;
using System.Text;

namespace meantone
{
    public class Factory12 : VectorFactory
    {
        public double[] intervals;

        const int edo = 12;
        const int irange = 1 + edo / 2;
        public Factory12()
        {
            const double scale = 2.5;
            intervals = new double[]
            {
                scale * Math.Log(1),
                scale * Math.Log(15),
                scale * Math.Log(9),
                scale * Math.Log(5),
                scale * Math.Log(5),
                scale * Math.Log(3),
                scale * Math.Log(45)
            };
            /*
            for (int i = 0; i < irange; i++)
            {
                intervals[i] = 100;
            }

            for (int fifths = -5; fifths < 5; fifths++)
            {
                for (int thirds = -5; thirds < 5; thirds++)
                {
                    int i = 7 * fifths + 4 * thirds;
                    if (i < 0)
                    {
                        i = -i;
                    }

                    i = i % edo;
                    if (i > edo / 2)
                    {
                        i = edo - i;
                    }
                    double c = 2.1 * (double)(thirds * thirds) + (double)(fifths * fifths);
                    if (c < intervals[i])
                    {
                        intervals[i] = c;

                    }
                }
            }
            */

            for (int i = 0; i < irange; i++)
            {
                System.Console.WriteLine(string.Format("i = {0}; c(i) = {1};",
                    i, intervals[i]));
            }
        }

        public override Vector randomVector(Random rand, int root)
        {
            return new Vector12(this, rand, root);
        }

        public override Vector initialVector(Random rand, int root, int loc)
        {
            return new Vector12(this, rand, root);
        }
    }
}
