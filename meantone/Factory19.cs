using System;
using System.Collections.Generic;
using System.Text;

namespace meantone
{
    public class Factory19 : VectorFactory
    {

        public double[] intervals;

        const int edo = 19;
        const int irange = 1 + edo / 2;

        public Factory19()
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
                    int i = 8 * fifths + 6 * thirds;
                    if (i < 0)
                    {
                        i = -i;
                    }

                    i = i % edo;
                    if (i > edo / 2)
                    {
                        i = edo - i;
                    }
                    int c = thirds * thirds + fifths * fifths;
                    if (c < intervals[i])
                    {
                        intervals[i] = c;
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
            return new Vector19(this, rand, root);
        }

        public override Vector initialVector(Random rand, int root, int loc)
        {
            return new Vector19(this, rand, root);
        }
    }
}