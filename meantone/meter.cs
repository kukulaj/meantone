using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace meantone
{
    public enum Boundary {BigFraction, AlignedFraction, HeatCapacity };
    public class Meter
    {
        Work work;
        Boundary bound;
        bool up;
        int calls;
         
        int capi;
        int capacity_decrease;
        double[] old_temp;
        double[] old_cost;
        double[] old_capacity;
        int blen;
        StreamWriter logfile;
        double target;

        public Meter(Work w)
        {
            work = w;
            logfile = new StreamWriter(work.file_prefix + "log.txt");
            blen = 27;
             
            old_cost = new double[blen];
            old_temp = new double[blen];
            old_capacity = new double[blen];

          
            capi = 0;
            
            bound = Boundary.HeatCapacity;
            capacity_decrease = 0;
            calls = 0;
        }

        public void Close()
        {
            logfile.Close();
        }


        public void Set_Up(bool pup)
        {
            up = pup;
             
            capacity_decrease = 0;
        }
        public void Set_Target(double t)
        {
            target = t;
        }

        public void Adjust_Target()
        {
            double bfrac = work.bfrac();
            double afrac = work.align_count();

            switch (bound)
            {
                case Boundary.BigFraction:
                    target = up ? (target = 0.03 + bfrac * 0.97) : (bfrac * 0.95);
                    Console.WriteLine(string.Format("new target: {0}", target));
                    break;
                case Boundary.AlignedFraction:
                    target = up ? (target = 0.03 + afrac * 0.97) : (afrac * 0.95);
                    Console.WriteLine(string.Format("new target: {0}", target));
                    break;
                case Boundary.HeatCapacity:           
                    break;
            }
        }

        public bool Step(double temp)
        {
            bool result = false;
            bool cresult = false;
            double cost = work.rcost;

            old_temp[capi] = temp;
            old_cost[capi] = cost;
            double slope = 0.0;

            int prev_capi = (capi + old_temp.Length - 1) % old_temp.Length;
            if (calls > 1)
            {
                old_capacity[capi] = (old_cost[capi] - old_cost[prev_capi])
                    /(old_temp[capi] - old_temp[prev_capi]);
            }


            int next_capi = (capi + 1) % old_temp.Length;
            if (calls > old_temp.Length + 3)
            {
                double st2 = 0.0;
                double st3 = 0.0;
                double st4 = 0.0;
                double sc = 0.0;
                double sct = 0.0;
                double sct2 = 0.0;
                double st = 0.0;

                for(int i = 0; i < old_temp.Length; i++)            
                {
                    st += old_temp[i];
                    st2 += old_temp[i] * old_temp[i];
                    st3 += old_temp[i] * old_temp[i] * old_temp[i];
                    st4 += old_temp[i] * old_temp[i] * old_temp[i] * old_temp[i];
                   
                    sc += old_cost[i];
                    sct += old_cost[i] * old_temp[i];
                    sct2 += old_cost[i] * old_temp[i] * old_temp[i];
                }
                double n = (double)(old_temp.Length);

                double cl = n * sct - sc * st;
                double cm = st * st - n * st2; 
                double cn = st2*st - n * st3;
                double cp = n * sct2 - sc * st2;
                //double cq = st * st2 - n * st3;
                double cr = st2 * st2 - n * st4;

                double cc =  (cm * cp - cn * cl)/ (cn * cn - cm * cr);
                double cb = (-cl - cn * cc) / cm;
                double ca = (sc - cb * st - cc * st2) / n;

                double check1 = sc - n * ca - cb * st - cc * st2;
                double check2 = sct -  ca * st  - cb * st2 - cc * st3;
                double check3 = sct2 - ca * st2 - cb * st3 - cc * st4;

                /*
                if (check1 * check1 + check2 * check2 + check3 * check3 > 20.0)
                {
                    double check4 = cl + cb * cm + cc * cn;
                    double check5 = cp + cb * cn + cc * cr;
                    Console.WriteLine("oops");
                }
                */

                slope = cc;
                double model = cc * temp * temp + cb * temp + ca;
                 
                Console.WriteLine(string.Format("capacity = {0}; model = {1}; slope = {2}",
                    old_capacity[capi], model, slope));

                if ((up && slope < -25.0) || (!up && slope > 25.0))
                {
                    capacity_decrease++;
                    if (capacity_decrease > 3)
                    {
                        cresult = true;
                    }
                }
                else 
                {
                    capacity_decrease = 0;
                }
            }
            capi = next_capi;
            calls++;

            double bfrac = work.bfrac();
            double afrac = work.align_count();

            logfile.WriteLine(string.Format("{0} {1} {2}", temp, work.rcost, slope));

            switch (bound)
            {
                case Boundary.BigFraction:
                    result = up ? (bfrac < target) : (bfrac > target);
                    break;
                case Boundary.AlignedFraction:
                    result = up ? (afrac < target) : (afrac > target);
                    break;
                case Boundary.HeatCapacity:
                    result = cresult; 
                    break;
            }         

            return result;
        }
    }
}
