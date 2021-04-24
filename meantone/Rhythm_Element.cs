using System;
using System.Collections.Generic;
using System.Text;

namespace meantone
{
    public class Rhythm_Element
    {
        public double offset;
        public double duration;
        public int numer;
        public int denom;
        public bool silent;


        public Rhythm_Element(double o, double d)
        {
            offset = o;
            duration = d;
            silent = false;
        }
        public Rhythm_Element(double o, double d, int rn, int rd )
        {
            offset = o;
            duration = d;
            silent = false;
            numer = rn;
            denom = rd;
            if (rn <= 0)
            {
                rn = 1;
            }
            reduce();
        }

        public void reduce()
        {
            if (numer <= 0)
            {
                numer = 1;
            }

            int small = numer;
            int big = denom;
            while (small != big)
            {
                int next = big % small;

                big = small;
                if (next != 0)
                {
                    small = next;
                }
            }
            numer = numer / small;
            denom = denom / small;
            if (numer <= 0)
            {
                numer = 1;
            }
        }

    }
}
