using System;
using System.Collections.Generic;
using System.Text;

namespace meantone
{
    public class VectorJust : Vector
    {
        int[] powers;
        FactoryJust factory;

        readonly double[] primes = { 2.0, 3.0, 5.0, 7.0, 11.0, 13.0 };
        readonly int[] octaves = { 1, 1, 2, 2, 3, 3 };

        public VectorJust(FactoryJust fj, int[] v)
        {
            factory = fj;
            powers = v;
        }

        public VectorJust(FactoryJust fj, int lim, Random rand, int root)
        {
            factory = fj;
            powers = new int[lim];
            powers[0] = root;
            for (int d = 1; d < lim; d++)
            {
                powers[d] = rand.Next(3);
                powers[0] -= octaves[d] * powers[d];
            }
        }

        public VectorJust(FactoryJust fj, int lim, Random rand, int root, int location)
        {
            factory = fj;
            powers = new int[lim];
            powers[0] = root;
            for (int d = 1; d < lim; d++)
            {
                powers[d] = 0;
                /*
                int loc = location % 12;
                switch (d)
                {
                    case 1:
                        switch ((location % 12) / 3)
                        {
                            case 0:
                                powers[d] = loc; 
                                break;
                            case 1:
                                powers[d] = 3;
                                break;
                            case 2:
                                powers[d] = 9 - loc;
                                break;
                            case 3:
                                powers[d] = 0;
                                break;
                        }
                        break;
                    case 2:
                        switch ((location % 12) / 3)
                        {
                            case 0:
                                powers[d] = 0;
                                break;
                            case 1:
                                powers[d] = loc - 3;
                                break;
                            case 2:
                                powers[d] = 3;
                                break;
                            case 3:
                                powers[d] = 12 - loc;
                                break;
                        }
                        break;
                    default:
                        powers[d] = rand.Next(3);
                        break;
                }
                powers[0] -= octaves[d] * powers[d];
                */
            }
        }

        public override double log_frequency()
        {
            double lf = 0.0;
            for (int d = 0; d < powers.Length; d++)
            {
                lf += Math.Log(primes[d]) * (double)powers[d];
            }

            return lf;
        }

        public override bool same_interval(Vector v, Vector w1, Vector w2)
        {
            VectorJust vc = (VectorJust)v;
            VectorJust w1c = (VectorJust)w1;
            VectorJust w2c = (VectorJust)w2;

            for (int d = 0; d < powers.Length; d++)
            {
                if (primes[d] - vc.primes[d] != w1c.primes[d] - w2c.primes[d])
                {
                    return false;
                }
            }
            return true;
        }

        public override double histd()
        {
            double lf = 0.0;
            for (int d = 1; d < powers.Length; d++)
            {
                lf += Math.Log(primes[d]) * (double)powers[d];
            }

            return lf;
        }

        public override Vector[] jostle()
        {
            int option_count = 1;
            for (int d = 0; d < powers.Length; d++)
            {
                option_count += 2 * factory.deltas[d].Length;
            }


            Vector[] options = new Vector[option_count];
            options[0] = new VectorJust(factory, powers);

            int oi = 1;
            for (int d = 0; d < powers.Length; d++)
            {
                int[][] ddels = factory.deltas[d];
                for (int dd = 0; dd < ddels.Length; dd++)
                {
                    for (int sign = -1; sign < 2; sign += 2)
                    {
                        int[] op = new int[powers.Length];
                        for (int pi = 0; pi < powers.Length; pi++)
                        {
                            op[pi] = powers[pi];
                        }
                        for (int pi = 0; pi < ddels[dd].Length; pi++)
                        {
                            op[pi] += sign * ddels[dd][pi];
                        }

                        options[oi] = new VectorJust(factory, op);
                        oi++;
                    }
                }
            }


            return options;
        }

        public override bool octave_equivalent(Vector av)
        {
            VectorJust v = (VectorJust)av;
            for (int pi = 1; pi < powers.Length; pi++)
            {
                if (powers[pi] != v.powers[pi])
                {
                    return false;
                }
            }
            return true;
        }

        public override double concordance(Vector av)
        {
            VectorJust v = (VectorJust)av;

            double dist = 0.0;
            double weight = 1.0;
            for (int pi = 1; pi < powers.Length; pi++)
            {
                double di = (double)(powers[pi] - v.powers[pi]);
                dist += weight * di * di;
                weight *= 1.3;
            }

            double result = 0.8 * (double)dist;
            return result;
        }

        public override bool same(Vector av)
        {
            VectorJust v = (VectorJust)av;
            for (int pi = 1; pi < powers.Length; pi++)
            {
                if (powers[pi] != v.powers[pi])
                {
                    return false;
                }
            }
            return true;
        }
    }
}
