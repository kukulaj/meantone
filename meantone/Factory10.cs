using System;
using System.Collections.Generic;
using System.Text;

namespace meantone
{
    public class Factory10 : VectorFactory
    {
        public double[] intervals;
        public double[][] why;
        public Factory10()
        {
            intervals = new double[6];

            for (int i = 0; i < 6; i++)
            {
                intervals[i] = 100;

            }

            for (int thirds = -5; thirds < 5; thirds++)
            {
                int i = 3 * thirds;
                if (i < 0)
                {
                    i = -i;
                }

                i = i % 10;
                if (i > 5)
                {
                    i = 10 - i;
                }
                int c = thirds * thirds;
                if (c < intervals[i])
                {
                    intervals[i] = c;
                }
            }

            for (int i = 0; i < 6; i++)
            {
                System.Console.WriteLine(string.Format("i = {0}; c(i) = {1};",
                    i, intervals[i]));
            }
        }

        public override Vector randomVector(Random rand, int root)
        {
            return new Vector10(this, rand, root);
        }

        public override Vector initialVector(Random rand, int root, int loc)
        {
            return new Vector10(this, rand, root, loc);
        }
    }
}
