using System;
using System.Collections.Generic;
using System.Text;

namespace meantone
{
    public class Factory118 : VectorFactory
    {
        public double[] intervals;
        public double[][] why;
        public Factory118()
        {
            intervals = new double[60];
            why = new double[60][];
            for (int i = 0; i < 60; i++)
            {
                intervals[i] = 100;

            }

            for (int minors = 0; minors < 1; minors++)
            {
                for (int fifths = -8; fifths < 8; fifths++)
                {
                    for (int thirds = -8; thirds < 8; thirds++)
                    {
                        int i = 49 * fifths + 38 * thirds + 31 * minors;
                        if (i < 0)
                        {
                            i = -i;
                        }

                        i = i % 118;
                        if (i > 59)
                        {
                            i = 118 - i;
                        }
                        int c = thirds * thirds + fifths * fifths + minors * minors;
                        if (c < intervals[i])
                        {
                            intervals[i] = c;
                            why[i] = new double[3] { fifths, thirds, minors };
                        }
                    }
                }
            }

            for (int i = 0; i < 60; i++)
            {
                System.Console.WriteLine(string.Format("i = {0}; c(i) = {1}; why = {2} {3} {4};",
                    i, intervals[i], why[i][0], why[i][1], why[i][2]));
            }
        }

        public override Vector randomVector(Random rand, int root)
        {
            return new Vector118(this, rand, root);
        }

        public override Vector initialVector(Random rand, int root, int loc)
        {
            return new Vector118(this, rand, root);
        }
    }
}
