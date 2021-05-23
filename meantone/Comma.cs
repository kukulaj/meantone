using System;
using System.Collections.Generic;
using System.Text;

namespace meantone
{
    public class Comma
    {
        public FactoryEDO5 factory;
        public int[] factors;

        public Comma(FactoryEDO5 pfactory, int[] pfactors)
        {
            factory = pfactory;
            factors = pfactors;

            int vec = 0;
            for (int i = 0; i < factors.Length; i++)
            {
                vec += factors[i] * factory.pstep[i+1];
            }
            if (vec % factory.edo != 0)
            {
                Console.WriteLine("comma error");
            }
        }
    }
}
