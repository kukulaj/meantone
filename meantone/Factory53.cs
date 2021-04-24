using System;
using System.Collections.Generic;
using System.Text;

namespace meantone
{
    public class Factory53 : VectorFactory
    {
        public double[] intervals;
        public const int icnt = 500;
        public Factory53()
        {

            intervals = new double[icnt];
            double[][] why = new double[icnt][];
            for (int i = 0; i < icnt; i++)
            {
                intervals[i] = 500;
            }

            for (int octaves = -11; octaves < 12; octaves++)
            {
                for (int fifths = -7; fifths < 8; fifths++)
                {
                    for (int thirds = -5; thirds < 6; thirds++)
                    {
                        int i = 84 * fifths + 123 * thirds + 53 * octaves;
                        if (i < 0)
                        {
                            i = -i;
                        }

                        if (i < icnt)
                        {

                            double c = 1.3 * (double)(thirds * thirds)
                                + 1.0 * (double)(fifths * fifths)
                                + 0.2 * (double)(octaves * octaves);
                            if (c < intervals[i])
                            {
                                intervals[i] = c;
                                why[i] = new double[3] { octaves, fifths, thirds };
                            }
                        }
                    }
                }
            }

            for (int i = 0; i < icnt; i++)
            {
                if (why[i] != null)
                {
                    System.Console.WriteLine(string.Format("i = {0}; c(i) = {1}; why = {2} {3} {4};",
                        i, intervals[i], why[i][0], why[i][1], why[i][2]));
                }
            }
        }

        public override Vector randomVector(Random rand, int root)
        {
            return new Vector53(this, rand, root);
        }

        public override Vector initialVector(Random rand, int root, int loc)
        {
            return new Vector53(this, rand, root, loc);
        }
    }
}
