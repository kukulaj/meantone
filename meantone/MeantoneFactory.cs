using System;
using System.Collections.Generic;
using System.Text;

namespace meantone
{
    public class MeantoneFactory : VectorFactory
    {
        public override Vector randomVector(Random rand, int root)
        {
            return new MeanToneVector(rand, root);
        }

        public override Vector initialVector(Random rand, int root, int loc)
        {
            return new MeanToneVector(rand, root);
        }
    }
}
