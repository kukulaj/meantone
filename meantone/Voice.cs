﻿using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace meantone
{
    public class Voice
    {
        public Work work;
        public Type_Map map;
        public Measure[] measures;
        Measure[] sequence;
        int measure_count;
        int sequence_count;
        public int vi;
        public int root;
        public double rootlf;
        public bool freeze;
        public bool mute;
        public Rhythm[] rhythms;
        public double temp_factor;
        public Pattern pattern;

        public Voice(Work w, int voice, Type_Map tm, Rhythm[] rootr)
        {
            work = w;
            map = tm;
            vi = voice;
            root = (3 * vi) / 4;
            rootlf = -0.1 + 0.5 * (double)vi;
            temp_factor = 1.0;
            measure_count = work.measure_count;
            measures = new Measure[measure_count];
            //pattern = new Pattern(tm, w, voice, rootr);
            pattern = new Patchwork(tm, w, voice, rootr);

            int row_size = map.row_size;
            for (int i = 0; i < measure_count; i++)
            {
                measures[i] = new Measure(this, map, i);
                measures[i].temp_factor = 1.0; // - 0.15 * Math.Cos(2.0 * Math.PI * ((double)(i)) / ((double)map.size));
            }

            int range = 1;
            for (int d = 0; d < map.dimension; d++)
            {
                for (int i = 0; i < measure_count; i++)
                {
                    measures[i].acrossp(measures[(i + range) % measure_count]);
                }
                range = range * row_size;
            }

            sequence_count = work.measure_count;
            sequence = new Measure[sequence_count];

            int row = measure_count / row_size;

            if (true)
            {
                for (int i = 0; i < sequence_count; i++)
                {
                    sequence[i] = measures[i];
                    /*
                    sequence[4 * i] = measures[i];
                    sequence[4 * i + 1] = measures[i];
                    sequence[(4 * i + 6) % sequence_count] = measures[i];
                    sequence[(4 * i + 7) % sequence_count ] = measures[i];
                    */
                }
                for (int i = 0; i < sequence_count; i++)
                {
                    sequence[(i + 1) % sequence_count].after(sequence[i]);
                }
            }
            else
            {
                for (int i = 0; i < sequence_count; i++)
                {

                    sequence[i] = measures[i % 16];

                }
                for (int i = 0; i < measure_count; i++)
                {
                    measures[(i + 1) % measure_count].after(measures[i]);
                }
            }

            //for (int i = 0; i < row; i++)
            //{
            //sequence[8 * i]     = measures[2 * i];
            //sequence[8 * i + 1] = measures[2 * i];
            //sequence[8 * i + 2] = measures[2 * i + 1];
            //sequence[8 * i + 3] = measures[2 * i + 1];
            //sequence[8 * i + 4] = measures[2 * i];
            //sequence[8 * i + 5] = measures[2 * i + 1];
            //sequence[8 * i + 6] = measures[2 * i + 1];
            //sequence[8 * i + 7] = measures[2 * i];

            //sequence[(i) % sequence_count] = measures[i];
            //sequence[(4 * i + 1) % sequence_count] = measures[i];
            //sequence[(4 * i + 6) % sequence_count] = measures[i];
            //sequence[(4 * i + 7) % sequence_count] = measures[i];
            /*
            for(int j=0; j<row_size; j++)
            {
                Measure m = measures[i * row_size + j];
                switch (j)
                {
                    case 0:
                        sequence[(2 * i * row_size) % sequence_count] = m;
                        sequence[(2 * i * row_size + 4) % sequence_count] = m;
                        break;
                    case 1:
                        sequence[(2 * i * row_size + 2) % sequence_count] = m;
                        sequence[(2 * i * row_size + 6) % sequence_count] = m;
                        break;
                    case 2:
                        sequence[(2 * i * row_size + 5) % sequence_count] = m;
                        sequence[(2 * i * row_size + 9) % sequence_count] = m;
                        break;
                    case 3:
                        sequence[(2 * i * row_size + 7) % sequence_count] = m;
                        sequence[(2 * i * row_size + 11) % sequence_count] = m;
                        break;
                }
                */
            // sequence[(i * row_size + j) % sequence_count] = measures[i * row_size + j];
            // sequence[((2 * i) * row_size + j) % sequence_count] = measures[i * row_size + j];
            // sequence[((2 * i + 1) * row_size + j) % sequence_count] = measures[i * row_size + j];
            //  sequence[((3 * i + vi) * row_size + j) % sequence_count] = measures[i * row_size + j];
            //  sequence[((3 * i + 1 + vi) * row_size + j) % sequence_count] = measures[i * row_size + j];
            //  sequence[((3 * i + 2 + vi) * row_size + j) % sequence_count] = measures[i * row_size + j];

            //}

            /*
            sequence[(2 * i + vi) % sequence_count] = measures[i];
            sequence[(2 * i + vi + 1) % sequence_count] = measures[i];
             */
            /*
            sequence[(2 * i + vi + 3) % sequence_count] = measures[i];
            sequence[(2 * i + vi + 6) % sequence_count] = measures[i];
            */
            // }


        }

        public void above(Voice v)
        {
            for (int i = 0; i < sequence_count; i++)
            {
                sequence[i].above(v.sequence[i]);
            }
        }

        public void amplitudes()
        {
            for (int i = 0; i < measure_count; i++)
            {
                measures[i].amplitudes();
            }
        }


        public double play(StreamWriter file, int reps)
        {
            double s = 0.0;
            for (int r = 0; r < reps; r++)
            {
                for (int i = 0; i < sequence_count; i++)
                {
                    s = sequence[i].play(file, s);
                }
            }
            return s;
        }
    }
}
