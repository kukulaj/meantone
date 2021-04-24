using System;
using System.Collections.Generic;
using System.Text;

namespace meantone
{
    public class Pump
    {
        public Comma comma;
        public int[] sequence;

        public Pump(Comma pc, int[] ps)
        {
            comma = pc;
            sequence = new int[ps.Length];

            for(int i=0; i < ps.Length; i++)
            {
                sequence[i] = ps[i];
            }
        }

        public Pump(Comma pc)
        {
            comma = pc;

            int[] left = new int[comma.factors.Length];
            bool[] positive = new bool[comma.factors.Length];

            int slen = 0;
            for (int i = 0; i < comma.factors.Length; i++)
            {
                left[i] = Math.Abs(comma.factors[i]);
                positive[i] = comma.factors[i] > 0;
                slen += left[i];
            }

            sequence = new int[slen];
            int old = 0;
            for (int i = 0; i < slen; i++)
            {
                int bestj = -1;
                double bestf = 0.0;

                for (int j = 0; j < comma.factors.Length; j++)
                {
                    double f = ((double)left[j]) / (double)Math.Abs(comma.factors[j]);
                    if (f > bestf)
                    {
                        bestf = f;
                        bestj = j;
                    }
                }
                int move = comma.factory.harmonics[bestj];
                if (!positive[bestj])
                {
                    move = -move;
                }
                sequence[i] = (old + move) % comma.factory.edo;
                if (sequence[i] < 0)
                {
                    sequence[i] += comma.factory.edo;
                }
                left[bestj]--;
                old = sequence[i];
            }
        }
    }
}
