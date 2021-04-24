using System;
using System.Collections.Generic;
using System.Text;

namespace meantone
{
    public class Vector53 : VectorEDO
    {
        Factory53 factory;
        /*
        readonly double[] intervals = 
            { 0, // 0
            17, // 1
            9, // 2
            5, // 3
            7, // 4
            4, // 5
            20, // 6
            17, // 7
            5, // 8
            6, // 9
            13, // 10
            8, // 11
            5, // 12
            9, // 13
            1, // 14
            16, // 15
            17, // 16
            2, // 17
            16, // 18
            4, // 19
            10, // 20
            20, // 21
            2, // 22
            10, // 23
            10, // 24
            4, // 25
            10, // 26
        };
        */
        public Vector53(Factory53 f, int p)
        {
            factory = f;
            edo = 53;
            pitch = p;
        }

        public Vector53(Factory53 f, Random rand, int root)
        {
            factory = f;
            edo = 53;
            pitch = rand.Next(170);
            pitch += root * 53;
        }

        public Vector53(Factory53 f, Random rand, int root, int loc)
        {
            int[] kleisma = new int[] { 0, 31, 14, 50, 28, 11, 42, 25, 3, 39, 17 };
            factory = f;
            edo = 53;

            int row = loc / 9;
            int col = loc % 9;
            //int krow = row / 4;
            //int scol = (3 *col) / 8;
            pitch = (col * 31 + row * 14) % 53;


            //pitch = kleisma[loc % 11] ;
            pitch += root * 53;
        }

        public Vector delta(int p)
        {
            return new Vector53(factory, pitch + p);
        }

        public override Vector[] jostle()
        {
            Vector[] options = new Vector[13];
            options[0] = delta(0);
            options[1] = delta(22);
            options[2] = delta(-22);
            options[3] = delta(17);
            options[4] = delta(-17);
            options[5] = delta(10);
            options[6] = delta(-10);
            options[7] = delta(31);
            options[8] = delta(-31);
            options[9] = delta(7);
            options[10] = delta(-7);
            options[11] = delta(12);
            options[12] = delta(-12);
            return options;
        }

        public override double concordance(Vector av)
        {
            Vector53 v = (Vector53)av;

            int dp = pitch - v.pitch;
            if (dp < 0)
            {
                dp = -dp;
            }
            /*
            dp = dp % 53;
            if (dp > 26)
            {
                dp = 53 - dp;
            }
            */
            if (dp < Factory53.icnt)
            {
                double result = 2.0 * factory.intervals[dp];
                return result;
            }
            return 1000.0;

        }

    }
}
