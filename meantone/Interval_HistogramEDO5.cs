using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace meantone
{
    public class Interval_HistogramEDO5 : Interval_Histogram
    {
        int edo;
        int[] table;
        int overflow;

        public Interval_HistogramEDO5(int pedo)
        {
            edo = pedo;
            table = new int[5 * edo];
        }

        public override void Tally(Interval i)
        {
            int d = ((IntervalEDO5)i).dist;
            if (d < table.Length)
            {
                table[d]++;
            }
            else 
            {
                overflow++;
            }
        }

        public override void Write(StreamWriter file)
        {
            for(int i = 0; i< table.Length; i++)
            {
                file.WriteLine(string.Format("{0} {1}", i, table[i]));
            }
            file.WriteLine(string.Format("overflow {0}", overflow));
        }

    }
}
