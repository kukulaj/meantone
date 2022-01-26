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
        double[] old_capacity;
        double avg_capacity;
        int capi;
        int capacity_decrease;
        double[] old_temp;
        double[] old_cost;
        int blen;
        StreamWriter logfile;
        double target;

        public Meter(Work w)
        {
            work = w;
            logfile = new StreamWriter(work.file_prefix + "log.txt");
            blen = 51;
            old_capacity = new double[blen];
            old_cost = new double[blen];
            old_temp = new double[blen];

            avg_capacity = 0.0;
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
            calls = 0;
            capi = 0;
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

            old_temp[capi] = temp;
            old_cost[capi] = work.rcost;

            int next_capi = (capi + 1) % old_capacity.Length;
            if (calls > old_capacity.Length + 5)
            {
                double capacity = 0.0;
                capacity = (old_cost[capi] - old_cost[next_capi]) 
                    / (old_temp[capi] - old_temp[next_capi]);
                Console.WriteLine(string.Format("heat capacity {0} -> {1}",
                    avg_capacity, capacity));

                if (capacity < 0.8 * old_capacity[next_capi])
                {
                    capacity_decrease++;
                    if (capacity_decrease > 8)
                    {
                        cresult = true;
                    }
                }
                else
                {
                    capacity_decrease = 0;
                }

                double weight = 0.9;
                avg_capacity = weight * avg_capacity + (1.0 - weight) * capacity;
                old_capacity[capi] = avg_capacity;
                capi = next_capi;
            }
            calls++;

            double bfrac = work.bfrac();
            double afrac = work.align_count();

            logfile.WriteLine(string.Format("{0} {1} {2}", temp, work.rcost, avg_capacity));

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
