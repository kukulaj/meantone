using System;
using System.Collections.Generic;
using System.Text;

namespace meantone
{
    public class MeanToneVector : Vector
    {
        public int octave;
        public int fifth;

        public MeanToneVector(int no, int nf)
        {
            octave = no;
            fifth = nf;
        }

        public MeanToneVector(Random rand, int root)
        {
            octave = rand.Next(3);
            fifth = rand.Next(4) - octave;
            octave += root;
        }

        public MeanToneVector(Random rand, int root, int loc)
        {
            octave = rand.Next(3);
            fifth = ((loc % 12) / 3) - octave;
            octave += root;
        }

        public override double log_frequency()
        {
            return 0.25 * Math.Log(5.0) * (double)fifth + Math.Log(2.0) * (double)octave;
        }

        public override bool same_interval(Vector v, Vector w1, Vector w2)
        {
            MeanToneVector vc = (MeanToneVector)v;
            MeanToneVector w1c = (MeanToneVector)w1;
            MeanToneVector w2c = (MeanToneVector)w2;

            if (octave - vc.octave != w1c.octave - w2c.octave)
            {
                return false;
            }

            if (fifth - vc.fifth != w1c.fifth - w2c.fifth)
            {
                return false;
            }
            return true;
        }

        public override double histd()
        {
            return (double)fifth;
        }

        public Vector delta(int dt, int df)
        {
            return new MeanToneVector(octave + dt, fifth + df);
        }

        public override Vector[] jostle()
        {
            Vector[] options = new Vector[11];
            options[0] = delta(0, 0);
            options[1] = delta(-1, 2);
            options[2] = delta(1, -2);
            options[3] = delta(-2, 4);
            options[4] = delta(2, -4);
            options[5] = delta(2, -3);
            options[6] = delta(-2, 3);
            options[7] = delta(1, -1);
            options[8] = delta(-1, 1);
            options[9] = delta(0, 1);
            options[10] = delta(0, -1);

            return options;
        }

        public override bool octave_equivalent(Vector av)
        {
            MeanToneVector v = (MeanToneVector)av;

            return (fifth == v.fifth);
        }

        public override double concordance(Vector av)
        {
            MeanToneVector v = (MeanToneVector)av;

            int df = fifth - v.fifth;
            if (df < 0)
            {
                df = -df;
            }

            int c1t = (4 * df) / 17;
            int c2t = c1t + 1;

            int c1f = df - 4 * c1t;
            int c2f = df - 4 * c2t;

            int s1 = c1t * c1t + c1f * c1f;
            int s2 = c2t * c2t + c2f * c2f;

            if (s1 < s2)
            {
                return 3.0 * (double)s1;
            }

            return 3.0 * (double)s2;
        }

        public override bool same(Vector av)
        {
            MeanToneVector v = (MeanToneVector)av;
            if (octave == v.octave && fifth == v.fifth)
            {
                return true;
            }
            return false;
        }
    }
}
