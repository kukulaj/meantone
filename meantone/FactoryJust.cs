using System;
using System.Collections.Generic;
using System.Text;

namespace meantone
{
    public class FactoryJust : VectorFactory
    {
        int limit;
        public int[][][] deltas;

        public FactoryJust(int lim)
        {
            limit = lim;

            int[] d_2 = { 1 };
            int[][] sd_2 = { d_2 };
            int[] d_3_2 = { -1, 1 };
            int[] d_4_3 = { 2, -1 };
            int[] d_9_8 = { -3, 2 };
            int[][] sd_3 = { d_3_2, d_4_3, d_9_8 };
            int[] d_5_4 = { -2, 0, 1 };
            int[] d_6_5 = { 1, 1, -1 };
            int[][] sd_5 = { d_5_4, d_6_5 };
            int[] d_7_4 = { -2, 0, 0, 1 };
            int[] d_7_5 = { 0, 0, -1, 1 };
            int[] d_7_6 = { -1, -1, 0, 1 };
            int[][] sd_7 = { d_7_4, d_7_5, d_7_6 };
            int[] d_11_8 = { -3, 0, 0, 0, 1 };
            int[] d_11_7 = { 0, 0, 0, -1, 1 };
            int[] d_11_6 = { -1, -1, 0, 0, 1 };
            int[] d_11_10 = { -1, 0, -1, 0, 1 };
            int[][] sd_11 = { d_11_8, d_11_7, d_11_6, d_11_10 };
            int[][][] sda = { sd_2, sd_3, sd_5, sd_7, sd_11 };
            deltas = sda;
        }

        public override Vector randomVector(Random rand, int root)
        {
            return new VectorJust(this, limit, rand, root);
        }
        public override Vector initialVector(Random rand, int root, int location)
        {
            return new VectorJust(this, limit, rand, root, location);
        }
    }
}
