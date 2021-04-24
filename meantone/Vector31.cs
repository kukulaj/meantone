using System;
using System.Collections.Generic;
using System.Text;

namespace meantone
{
    public class Vector31 : VectorEDO
    {
        Factory31 factory;

        public Vector31(Factory31 f, int p)
        {
            factory = f;
            edo = 31;
            pitch = p;
        }

        public Vector31(Factory31 f, Random rand, int root)
        {
            factory = f;
            edo = 31;
            pitch = rand.Next(90);
            pitch += root * 31;
        }

        public Vector31(Factory31 f, Random rand, int root, int loc)
        {
            factory = f;
            edo = 31;
            pitch = 0;
            /*
            switch((loc % 10))
            {
                case 0:
                    pitch = 0;
                    break;
                
                case 1:
                    pitch = 18;
                    break;
                
                case 2:
                    pitch = 28;
                    break;
               
                case 3:
                    pitch = 7;
                    break;
                
                case 4:
                    pitch = 25;
                    break;
               
                case 5:
                    pitch = 4;
                    break;
                case 6:
                    pitch = 14;
                    break;
                case 7:
                    pitch = 24;
                    break;
               
                case 8:
                    pitch = 11;
                    break;
               
                case 9:
                    pitch = 21;
                    break;
               
                default:
                    pitch = 0;
                    break;
            }
            */

            pitch += root * 31;
        }

        public Vector delta(int p)
        {
            return new Vector31(factory, pitch + p);
        }

        public override Vector[] jostle()
        {
            Vector[] options = new Vector[19];
            options[0] = delta(0);
            options[1] = delta(10);
            options[2] = delta(-10);
            options[3] = delta(8);
            options[4] = delta(-8);
            options[5] = delta(13);
            options[6] = delta(-13);
            options[7] = delta(18);
            options[8] = delta(-18);
            options[9] = delta(3);
            options[10] = delta(-3);
            options[11] = delta(25);
            options[12] = delta(-25);
            options[13] = delta(6);
            options[14] = delta(-6);
            options[15] = delta(4);
            options[16] = delta(-4);
            options[17] = delta(7);
            options[18] = delta(-7);
            return options;
        }

        public override double concordance(Vector av)
        {

            Vector31 v = (Vector31)av;

            int dp = pitch - v.pitch;
            if (dp < 0)
            {
                dp = -dp;
            }
            dp = dp % 31;
            if (dp > 15)
            {
                dp = 31 - dp;
            }

            double result = 2.0 * factory.intervals[dp];
            return result;
        }
    }
}
