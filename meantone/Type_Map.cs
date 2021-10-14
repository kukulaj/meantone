using System;
using System.Collections.Generic;
using System.Text;

namespace meantone
{
    public class Type_Map
    {
        public double[] mphase;
        public double[] vphase;

        public double duration;

        public int[] duri;
        double[] tempf;
        public int size;
        public int row_size;
        public int dimension;
        public Random rand;
        int drange;
        public string file_prefix;

        public Type_Map(Random r)
        {
            file_prefix = @"C:\Users\James\Documents\tuning\meantone\";
            row_size = 6;
            dimension = 3;
            size = 1;
            for (int i = 0; i < dimension; i++)
            {
                size = size * row_size;
            }

            duration = 6.7;

            drange = 4;
            rand = r;
            mphase = new double[size];
            vphase = new double[size];

            duri = new int[size];
            tempf = new double[size];
            for (int i = 0; i < size; i++)
            {
                tempf[i] = 1.0; // - 0.2 * Math.Cos(6.0 * ((double)i) * Math.PI / ((double)size));
                duri[i] = 3; // rand.Next() % drange;
                mphase[i] = rand.NextDouble();
                vphase[i] = rand.NextDouble();
            }
            //anneal();
        }

        double dcostn(int d1, int d2)
        {
            double result = 0.0;
            double d = d1 - d2;
            if (d < 0)
            {
                d = -d;
            }

            result = ((double)d) / 2.0;
            switch (d % 5)
            {
                case 0:
                    break;
                case 1:
                    result += 5;
                    break;
                case 2:
                    result += 2;
                    break;
                case 3:
                    result += 2;
                    break;
                case 4:
                    result += 5;
                    break;
            }

            return result;
        }

        double dcost(int d, int i)
        {
            double result = 0.0;

            result += dcostn(d, duri[(i + 1) % size]);
            result += dcostn(duri[(i + size - 1) % size], d);
            result += dcostn(d, duri[(i + row_size) % size]);
            result += dcostn(duri[(i + size - row_size) % size], d);

            return result;
        }

        double dtcost()
        {
            double result = 0.0;

            for (int i = 0; i < size; i++)
            {
                result += dcost(duri[i], i);
            }

            return result;
        }

        double djostle(int i, double temp)
        {
            double[] costs = new double[drange];

            for (int t = 0; t < drange; t++)
            {
                costs[t] = dcost(t, i);
            }

            int start = duri[i];

            double ptot = 0;
            double[] probs = new double[drange];
            for (int t = 0; t < drange; t++)
            {
                double p = Math.Exp(-costs[t] / (temp * tempf[i]));
                ptot += p;
                probs[t] = p;
            }

            double pick = rand.NextDouble() * ptot;
            int pt = 0;
            for (int t = 0; t < drange; t++)
            {
                if (pick < probs[t])
                {
                    pt = t;
                    break;
                }
                pick -= probs[t];
            }
            duri[i] = pt;

            return 2.0 * (costs[pt] - costs[start]);
        }

        double dequilibrate(double temp)
        {
            double c = dtcost();
            double c_low = c;
            double c_high = c;

            int since = 0;
            while (since < 1000)
            {
                int i = rand.Next() % size;
                double delta = djostle(i, temp);

                /*
                double check = tcost();
                if( c + delta != check)
                {
                    System.Console.WriteLine("type map oops");
                }
                */
                since++;
                c += delta;
                if (c < c_low)
                {
                    c_low = c;
                    since = 0;
                }
                if (c > c_high)
                {
                    c_high = c;
                    since = 0;
                }
            }
            System.Console.WriteLine(string.Format("dcost {0} {1} {2} at temp {3}",
                c_low, c, c_high, temp));
            return c;
        }

        void danneal()
        {
            for (double temp = 25; temp > 5.0; temp *= 0.98)
            {
                dequilibrate(temp);
            }
        }

        double cost(double mp1, double vp1, double mp2, double vp2)
        {
            double result = 0.0;
            double mdiff = mp1 - mp2;

            if (mdiff < 0)
            {
                mdiff = -mdiff;
            }

            if (mdiff > 0.5)
            {
                mdiff = 1.0 - mdiff;
                vp1 = 1.0 - vp1;
            }
            result += mdiff * mdiff;

            double vdiff = vp2 - vp1;
            if (vdiff < 0.0)
            {
                vdiff = -vdiff;
            }
            if (vdiff > 0.5)
            {
                vdiff = 1.0 - vdiff;
            }

            result += vdiff * vdiff;

            return result;
        }



        double ocost(double mp, double vp, int i)
        {
            double result = 0.0;
            result += cost(mp, vp, mphase[(i + size - 1) % size], vphase[(i + size - 1) % size]);
            result += cost(mp, vp, mphase[(i + 1) % size], vphase[(i + 1) % size]);

            result += cost(mp, vp, mphase[(i + row_size) % size], vphase[(i + row_size) % size]);
            result += cost(mp, vp, mphase[(i + size - row_size) % size], vphase[(i + size - row_size) % size]);

            return result;
        }

        double tcost()
        {
            double result = 0.0;
            for (int i = 0; i < size; i++)
            {
                result += ocost(mphase[i], vphase[i], i);
            }
            return result;
        }

        double tweak(double p, double range)
        {
            double result = p;
            p += range * rand.NextDouble();
            if (p < 0.0)
            {
                p += 1.0;
            }
            if (p > 1.0)
            {
                p -= 1.0;
            }

            return p;
        }


        double jostle(int i, double temp)
        {
            int ocnt = 9;
            double range = 1.0;

            double[] costs = new double[ocnt];
            double[] omp = new double[ocnt];
            double[] ovp = new double[ocnt];

            omp[0] = mphase[i];
            ovp[0] = vphase[i];
            costs[0] = ocost(omp[0], ovp[0], i);

            for (int t = 1; t < ocnt; t++)
            {
                omp[t] = tweak(mphase[i], range);
                ovp[t] = tweak(vphase[i], range);
                double mdiff = omp[t] - mphase[i];
                if (mdiff < 0.0)
                {
                    mdiff = -mdiff;
                }
                if (mdiff > 0.5)
                {
                    ovp[t] = 1.0 - ovp[t];
                }

                costs[t] = ocost(omp[t], ovp[t], i);
                range *= 0.5;
            }

            double ptot = 0;
            double[] probs = new double[ocnt];
            for (int t = 0; t < ocnt; t++)
            {
                double p = Math.Exp(-costs[t] / temp);
                ptot += p;
                probs[t] = p;
            }

            double pick = rand.NextDouble() * ptot;
            int pt = 0;
            for (int t = 0; t < ocnt; t++)
            {
                if (pick < probs[t])
                {
                    pt = t;
                    break;
                }
                pick -= probs[t];
            }
            mphase[i] = omp[pt];
            vphase[i] = ovp[pt];

            return 2.0 * (costs[pt] - costs[0]);
        }

        double equilibrate(double temp)
        {
            double c = tcost();
            double c_low = c;
            double c_high = c;

            int since = 0;
            while (since < 10000)
            {
                int i = rand.Next() % size;
                double delta = jostle(i, temp);

                /*
                double check = tcost();
                if( c + delta != check)
                {
                    System.Console.WriteLine("type map oops");
                }
                */
                since++;
                c += delta;
                if (c < c_low)
                {
                    c_low = c;
                    since = 0;
                }
                if (c > c_high)
                {
                    c_high = c;
                    since = 0;
                }
            }
            System.Console.WriteLine(string.Format("cost {0} {1} {2} at temp {3}",
                c_low, c, c_high, temp));
            return c;
        }

        void anneal()
        {
            for (double temp = 10; temp > 0.11; temp *= 0.98)
            {
                equilibrate(temp);
            }

        }
    }
}
