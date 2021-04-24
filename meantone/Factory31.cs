using System;
using System.Collections.Generic;
using System.Text;

namespace meantone
{
    public class Factory31 : VectorFactory
    {
        public double[] intervals;

        const int edo = 31;
        const int irange = 1 + edo / 2;
        public Factory31()
        {
            intervals = new double[irange];

            for (int i = 0; i < irange; i++)
            {
                intervals[i] = 100;
            }

            for (int fifths = -5; fifths < 5; fifths++)
            {
                for (int thirds = -5; thirds < 5; thirds++)
                {
                    for (int sevenths = 0; sevenths < 1; sevenths++)
                    {
                        int i = 18 * fifths + 10 * thirds + 25 * sevenths;
                        if (i < 0)
                        {
                            i = -i;
                        }

                        i = i % edo;
                        if (i > edo / 2)
                        {
                            i = edo - i;
                        }
                        int c = thirds * thirds + fifths * fifths + sevenths * sevenths;
                        if (c < intervals[i])
                        {
                            intervals[i] = c;
                        }
                    }
                }
            }

            for (int i = 0; i < irange; i++)
            {
                System.Console.WriteLine(string.Format("i = {0}; c(i) = {1};",
                    i, intervals[i]));
            }
        }

        public override Vector randomVector(Random rand, int root)
        {
            return new Vector31(this, rand, root);
        }

        public override Vector initialVector(Random rand, int root, int loc)
        {
            return new Vector31(this, rand, root, loc);
        }
    }
}
