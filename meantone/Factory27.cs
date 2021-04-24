using System;
using System.Collections.Generic;
using System.Text;

namespace meantone
{
    public class Factory27 : VectorFactory
    {
        public double[] intervals;

        const int edo = 90;
        public Factory27()
        {
            intervals = new double[1 + edo / 2];

            for (int i = 0; i < 1 + edo / 2; i++)
            {
                intervals[i] = 100;
            }
            for (int sevenths = -5; sevenths < 5; sevenths++)
            {
                for (int fifths = -5; fifths < 5; fifths++)
                {
                    for (int thirds = -5; thirds < 5; thirds++)
                    {
                        int i = 6 * sevenths + 20 * fifths + 49 * thirds;
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

            for (int i = 0; i < 1 + edo / 2; i++)
            {
                System.Console.WriteLine(string.Format("i = {0}; c(i) = {1};",
                    i, intervals[i]));
            }
        }

        public override Vector randomVector(Random rand, int root)
        {
            return new Vector27(this, rand, root);
        }

        public override Vector initialVector(Random rand, int root, int loc)
        {
            return new Vector27(this, rand, root, loc);
        }
    }
}
