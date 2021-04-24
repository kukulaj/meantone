using System;
using System.Collections.Generic;
using System.Text;

namespace meantone
{
    public class GeneralFactory : VectorFactory
    {
        public int interval_cnt;
        public int generator_cnt;

        public double[] generators;
        public int[,] intervals; // interval, generator

        public GeneralFactory(double[] g, int[,] i)
        {
            generators = g;
            for (int j = 0; j < g.Length; j++)
            {
                generators[j] = Math.Log(2) * generators[j] / 1200.0;
            }
            intervals = i;
        }

        public override Vector randomVector(Random rand, int root)
        {
            return new GeneralVector(this, rand, root);
        }

        public override Vector initialVector(Random rand, int root, int loc)
        {
            return new GeneralVector(this, rand, root);
        }
    }
}
