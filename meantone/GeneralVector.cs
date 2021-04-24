using System;
using System.Collections.Generic;
using System.Text;

namespace meantone
{
    public class GeneralVector : Vector
    {
        GeneralFactory factory;
        int[] parts;

        public GeneralVector(GeneralFactory f)
        {
            parts = new int[f.generators.Length];
            factory = f;
        }

        public GeneralVector(GeneralFactory f, Random rand, int root)
        {
            parts = new int[f.generators.Length];
            factory = f;
        }

        public override double log_frequency()
        {
            double result = 0.0;

            for (int i = 0; i < parts.Length; i++)
            {
                result += factory.generators[i] * (double)parts[i];
            }

            return result;
        }

        public override double histd()
        {
            return log_frequency();
        }


        public override bool octave_equivalent(Vector av)
        {
            return false;
        }

        public override bool same(Vector v)
        {
            bool result = true;

            GeneralVector gv = (GeneralVector)v;
            for (int i = 0; i < parts.Length; i++)
            {
                if (parts[i] != gv.parts[i])
                    return false;
            }

            return result;
        }

        public override bool same_interval(Vector v, Vector w1, Vector w2)
        {
            bool result = true;

            GeneralVector gv = (GeneralVector)v;
            GeneralVector gw1 = (GeneralVector)w1;
            GeneralVector gw2 = (GeneralVector)w2;

            for (int i = 0; i < parts.Length; i++)
            {
                if (parts[i] - gv.parts[i] != gw1.parts[i] - gw2.parts[i])
                    return false;
            }

            return result;
        }

        public override Vector[] jostle()
        {
            GeneralVector[] result = new GeneralVector[1 + 2 * factory.intervals.GetLength(0)];

            result[0] = new GeneralVector(factory);
            for (int i = 0; i < parts.Length; i++)
            {
                result[0].parts[i] = parts[i];
            }

            for (int j = 0; j < factory.intervals.GetLength(0); j++)
            {
                result[2 * j + 1] = new GeneralVector(factory);
                result[2 * j + 2] = new GeneralVector(factory);

                for (int i = 0; i < parts.Length; i++)
                {
                    result[2 * j + 1].parts[i] = parts[i] + factory.intervals[j, i];
                    result[2 * j + 2].parts[i] = parts[i] - factory.intervals[j, i]; ;
                }
            }

            return result;
        }

        public override double concordance(Vector av)
        {
            double result = 100.0;
            GeneralVector gv = (GeneralVector)av;

            int[] target = new int[parts.Length];
            for (int i = 0; i < parts.Length; i++)
            {
                target[i] = parts[i] - gv.parts[i];
            }

            Dictionary<int[], double> table = new Dictionary<int[], double>(new ChordEqualityComparer());
            Stack<int[]> stack = new Stack<int[]>();
            int[] origin = new int[parts.Length];

            table[target] = -1.0;

            if (table.ContainsKey(origin))
                return 0.0;

            table[origin] = 0.0;

            stack.Push(origin);

            bool found = false;
            while (!found && table.Count < 100000)
            {
                int[] start = stack.Pop();
                double c1 = table[start];
                double cnext = c1 + 3.0;

                for (int j = 0; j < factory.intervals.GetLength(0); j++)
                {
                    for (int s = 0; s < 2; s++)
                    {
                        int[] step = new int[parts.Length];
                        for (int i = 0; i < parts.Length; i++)
                        {
                            step[i] = start[i] + (1 - 2 * s) * factory.intervals[j, i];
                        }
                        if (table.ContainsKey(step))
                        {
                            double c2 = table[step];
                            if (c2 < 0.0)
                            {
                                found = true;
                                result = cnext;
                            }
                            else if (c2 > cnext)
                            {
                                table[step] = cnext;
                                stack.Push(step);
                            }
                        }
                        else
                        {
                            table[step] = cnext;
                            stack.Push(step);
                        }
                    }
                }
            }

            return result;
        }
    }
}
