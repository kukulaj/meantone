using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace meantone
{
    public class Measure
    {
        public int location;
        int vertex_count;
        public Vertex[] vertices;
        public Voice voice;
        double duration;
        double stretch;
        public double temp_factor;
        public int duri;
        public int itype;

        Vertex make_vertex(double d, double s)
        {
            return new Vertex(this, d * stretch, s * stretch);
        }

        public Measure(Voice ve, Type_Map map, int loc)
        {
            voice = ve;
            location = loc;
            duration = map.duration;
            stretch = 1.0;
            int di = map.duri[loc];
            duri = di;
            itype = 0;
            int type = 30;

            if (ve.vi > -1)
            {
                int row = location / 16;
                int within = location % 12;
                int phase = within / 2;
                int pick = 0;
                switch (phase)
                {
                    case 0:
                    case 2:
                        pick = row;
                        break;
                    default:
                        pick = (row + 1) % 18;
                        break;
                }
                double min_dur = 0.23 / (1.5 + (double)ve.vi);
                double tphase = ((double)location) / ((double)map.size);
                walk(ve.pattern.sequence[location]);

                /*walk(new Rhythm(ve.work,
                    ve.work.rand, 
                    map.duration, 
                    min_dur, 
                    7 + 2 * ve.vi,
                    map.mphase[location],
                    map.vphase[location],
                    ve.vi,
                    min_dur 
                    * (1.3 +  0.0
                    * Math.Cos(2.0 * Math.PI * 
                    (5.0 * tphase * tphase - 3.0 * tphase * tphase * tphase )
                    ) 
                    )
                    ) 
                    );
                */
                //walk(map.rhythms[ve.vi, map.mtype[loc]]);
            }
            else if (ve.vi == 0)
            {
                walk(voice.rhythms[4 + location % 4]);
            }
            else
                switch (type)
                {
                    case 0:
                        vertex_count = 8;
                        vertices = new Vertex[vertex_count];
                        vertices[0] = make_vertex(0.5, 0.0);
                        vertices[1] = make_vertex(0.5, 0.5);
                        vertices[2] = make_vertex(0.5, 1.0);
                        vertices[3] = make_vertex(0.5, 1.5);
                        vertices[4] = make_vertex(0.5, 2.0);
                        vertices[5] = make_vertex(0.5, 2.5);
                        vertices[6] = make_vertex(0.5, 3.0);
                        vertices[7] = make_vertex(0.5, 3.5);
                        break;
                    case 1:
                        vertex_count = 10;
                        vertices = new Vertex[vertex_count];
                        vertices[0] = make_vertex(0.25, 0.0);
                        vertices[1] = make_vertex(0.25, 0.25);
                        vertices[2] = make_vertex(0.5, 0.5);
                        vertices[3] = make_vertex(0.25, 1.0);
                        vertices[4] = make_vertex(0.25, 1.25);
                        vertices[5] = make_vertex(0.5, 1.5);
                        vertices[6] = make_vertex(0.25, 2.0);
                        vertices[7] = make_vertex(0.25, 2.25);
                        vertices[8] = make_vertex(0.5, 2.5);
                        vertices[9] = make_vertex(1.0, 3.0);
                        break;
                    case 2:
                        vertex_count = 8;
                        vertices = new Vertex[vertex_count];
                        vertices[0] = make_vertex(0.5, 0.0);
                        vertices[1] = make_vertex(0.5, 0.5);
                        vertices[2] = make_vertex(0.5, 1.0);
                        vertices[3] = make_vertex(0.5, 1.5);
                        vertices[4] = make_vertex(0.5, 2.0);
                        vertices[5] = make_vertex(0.25, 2.5);
                        vertices[6] = make_vertex(0.25, 2.75);
                        vertices[7] = make_vertex(0.5, 3.0);
                        break;
                    case 3:
                        vertex_count = 4;
                        vertices = new Vertex[vertex_count];
                        vertices[0] = make_vertex(0.9375, 0.0);
                        vertices[1] = make_vertex(0.9375, 1.0);
                        vertices[2] = make_vertex(0.9375, 2.0);
                        vertices[3] = make_vertex(0.9375, 3.0);
                        break;
                    case 4:
                        vertex_count = 6;
                        vertices = new Vertex[vertex_count];
                        vertices[0] = make_vertex(0.5, 0.0);
                        vertices[1] = make_vertex(0.5, 0.5);
                        vertices[2] = make_vertex(1.0, 1.0);
                        vertices[3] = make_vertex(0.5, 2.0);
                        vertices[4] = make_vertex(0.5, 2.5);
                        vertices[5] = make_vertex(1.0, 3.0);
                        break;
                    case 5:
                        vertex_count = 5;
                        vertices = new Vertex[vertex_count];
                        vertices[0] = make_vertex(0.5, 0.0);
                        vertices[1] = make_vertex(0.5, 0.5);
                        vertices[2] = make_vertex(1.0, 1.0);
                        vertices[3] = make_vertex(1.0, 2.0);
                        vertices[4] = make_vertex(1.0, 3.0);
                        break;
                    case 6:
                        vertex_count = 5;
                        vertices = new Vertex[vertex_count];
                        vertices[0] = make_vertex(0.3333, 0.0);
                        vertices[1] = make_vertex(0.6666, 0.3333);
                        vertices[2] = make_vertex(1.0, 1.0);
                        vertices[3] = make_vertex(1.0, 2.0);
                        vertices[4] = make_vertex(1.0, 3.0);
                        break;
                    case 7:
                        vertex_count = 3;
                        vertices = new Vertex[vertex_count];
                        vertices[0] = make_vertex(1.0, 0.0);
                        vertices[1] = make_vertex(2.0, 1.0);
                        vertices[2] = make_vertex(1.0, 3.0);
                        break;
                    case 8:
                        vertex_count = 3;
                        vertices = new Vertex[vertex_count];
                        vertices[0] = make_vertex(1.5, 0.0);
                        vertices[1] = make_vertex(2.0, 1.5);
                        vertices[2] = make_vertex(0.5, 3.5);
                        break;
                    case 9:
                        vertex_count = 4;
                        vertices = new Vertex[vertex_count];
                        vertices[0] = make_vertex(0.5, 0.0);
                        vertices[1] = make_vertex(0.5, 0.5);
                        vertices[2] = make_vertex(2.0, 1.0);
                        vertices[3] = make_vertex(1.0, 3.0);
                        break;
                    case 14:
                        vertex_count = 5;
                        vertices = new Vertex[vertex_count];
                        vertices[0] = make_vertex(0.9, 0.0);
                        vertices[1] = make_vertex(0.9, 0.9);
                        vertices[2] = make_vertex(0.3, 1.8);
                        vertices[3] = make_vertex(0.3, 2.1);
                        vertices[4] = make_vertex(0.3, 2.4);
                        break;
                    case 15:
                        vertex_count = 8;
                        vertices = new Vertex[vertex_count];
                        vertices[0] = make_vertex(0.375, 0.0);
                        vertices[1] = make_vertex(0.125, 0.375);
                        vertices[2] = make_vertex(0.5, 0.5);
                        vertices[3] = make_vertex(0.375, 1.0);
                        vertices[4] = make_vertex(0.125, 1.375);
                        vertices[5] = make_vertex(0.5, 1.5);
                        vertices[6] = make_vertex(0.75, 2.0);
                        vertices[7] = make_vertex(0.75, 2.75);
                        break;
                    case 20:
                        vertex_count = 4;
                        vertices = new Vertex[vertex_count];
                        vertices[0] = make_vertex(0.9375, 0.0);
                        vertices[1] = make_vertex(0.9375, 1.0);
                        vertices[2] = make_vertex(0.9375, 2.0);
                        vertices[3] = make_vertex(0.9375, 3.0);
                        break;
                    case 21:
                        vertex_count = 4;
                        vertices = new Vertex[vertex_count];
                        vertices[0] = make_vertex(0.4375, 0.0);
                        vertices[1] = make_vertex(1.4375, 0.5);
                        vertices[2] = make_vertex(0.4375, 2.0);
                        vertices[3] = make_vertex(1.4375, 2.5);
                        break;
                    case 22:
                        vertex_count = 2;
                        vertices = new Vertex[vertex_count];

                        vertices[0] = make_vertex(0.9375, 0.0);
                        vertices[1] = make_vertex(2.9375, 1.0);

                        break;
                    case 23:
                        vertex_count = 3;
                        vertices = new Vertex[vertex_count];
                        vertices[0] = make_vertex(0.9375, 0.0);
                        vertices[1] = make_vertex(0.9375, 1.0);
                        vertices[2] = make_vertex(1.9375, 2.0);

                        break;
                    case 24:
                        vertex_count = 2;
                        vertices = new Vertex[vertex_count];

                        vertices[0] = make_vertex(1.9375, 0.0);
                        vertices[1] = make_vertex(1.9375, 2.0);

                        break;
                    case 25:
                        vertex_count = 2;
                        vertices = new Vertex[vertex_count];

                        vertices[0] = make_vertex(2.9375, 0.0);
                        vertices[1] = make_vertex(0.4375, 3.5);
                        break;
                    case 26:
                        vertex_count = 1;
                        vertices = new Vertex[vertex_count];

                        vertices[0] = make_vertex(3.9375, 0.0);

                        break;
                    case 27:
                        vertex_count = 1;
                        vertices = new Vertex[vertex_count];

                        vertices[0] = make_vertex(2.9375, 0.0);

                        break;
                    case 28:
                        vertex_count = 1;
                        vertices = new Vertex[vertex_count];

                        vertices[0] = make_vertex(1.9375, 0.0);

                        break;
                    case 30:
                        vertex_count = 6;
                        vertices = new Vertex[vertex_count];
                        for (int rep = 0; rep < 2; rep++)
                        {
                            vertices[3 * rep + 0] = make_vertex(1.4375, 4 * rep + 0.0);
                            vertices[3 * rep + 1] = make_vertex(1.4375, 4 * rep + 1.5);
                            vertices[3 * rep + 2] = make_vertex(0.9375, 4 * rep + 3.0);
                        }
                        break;
                    case 31:
                        vertex_count = 3;
                        vertices = new Vertex[vertex_count];
                        vertices[0] = make_vertex(4.0 / 3.0 - 0.0625, 0.0);
                        vertices[1] = make_vertex(4.0 / 3.0 - 0.0625, 4.0 / 3.0);
                        vertices[2] = make_vertex(4.0 / 3.0 - 0.0625, 8.0 / 3.0);
                        break;
                    case 32:
                        vertex_count = 2;
                        vertices = new Vertex[vertex_count];
                        vertices[0] = make_vertex(1.0 - 0.0625, 2.0);
                        vertices[1] = make_vertex(1.0 - 0.0625, 3.0);

                        break;
                    default:
                        throw new System.InvalidOperationException("bad measure type");
                }

            for (int i = 1; i < vertex_count; i++)
            {
                Parallel p = new Parallel(vertices[i-1], vertices[i]);
                vertices[i].before[vertices[i - 1]] = p;
                vertices[i - 1].after[vertices[i]] = p;
            }
        }

        public void walk(Rhythm rhythm)
        {
            vertex_count = 0;
            foreach (Rhythm_Element re in rhythm.path)
            {
                if (!re.silent)
                {
                    vertex_count++;
                }
            }

            vertices = new Vertex[vertex_count];
            int i = 0;
            foreach (Rhythm_Element re in rhythm.path)
            {
                if (!re.silent)
                {
                    vertices[i] = make_vertex(re.duration, re.offset);
                    i++;
                }
            }
        }

        public double shrink()
        {

            return Math.Exp(-Math.Log(2) * ((double)duri) / 5.0);// * (1.0 + 0.03 * Math.Cos(2.0 * Math.PI * location / voice.map.size));
        }

        public void after(Measure m)
        {
            if (vertex_count > 0 && m.vertex_count > 0)
            {
                Vertex here = vertices[0];
                Vertex there = m.vertices[m.vertex_count - 1];
                if (here != there)
                {
                    Parallel p = new Parallel(there, here);
                    there.after[here] = p;
                    here.before[there] = p;
                }
            }
        }

        public void acrossi(Measure m)
        {
            link_overlap(new Acrossi_Factory(), m);
        }

        public void acrossp(Measure m)
        {
            link_overlap(new Acrossp_Factory(), m);
        }

        public void above(Measure m)
        {
            link_overlap(new Above_Factory(), m);
        }

        public static void stack(Edge_Factory ef, Measure[] m)
        {
            int[] vi = new int[m.Length];
            Vertex[] v = new Vertex[m.Length];
            int left = 0;

            for (int i = 0; i < m.Length; i++)
            {
                if(m[i].vertices.Length>0)
                {
                    v[i] = m[i].vertices[0];
                    left++;
                }
            }
           
            while (left > 1)
            {
                double earliest = 1000000.0;
                int earliesti = -1;

                for (int i = 0; i < m.Length; i++)
                {
                    if (v[i] != null)
                    {
                        bool found = false;
                        for (int j = i + 1; j < m.Length && !found; j++)
                        {
                            if(v[j] != null && v[i].overlap(v[j]))
                            {
                                ef.make_edge(v[i], v[j]);
                                found = true;
                            }
                        }
                        if (v[i].end() < earliest)
                        {
                            earliest = v[i].end();
                            earliesti = i;
                        }
                    }
                }

                if (m[earliesti].vertices.Length > vi[earliesti] + 2)
                {
                    vi[earliesti]++;
                    v[earliesti] = m[earliesti].vertices[vi[earliesti]];
                }
                else 
                {
                    v[earliesti] = null;
                    left--;
                }
            }
        }

        public void link_overlap(Edge_Factory ef, Measure m)
        {
            int hi = 0;
            int ti = 0;

            while (hi < vertex_count && ti < m.vertex_count)
            {
                Vertex here = vertices[hi];
                Vertex there = m.vertices[ti];

                if (here.overlap(there))
                {
                    ef.make_edge(here, there);

                }

                if (here.end() + 0.001 < there.end())
                {
                    hi++;
                }
                else if (there.end() + 0.001 < here.end())
                {
                    ti++;
                }
                else
                {
                    ti++;
                    hi++;
                }
            }
        }

        public void amplitudes()
        {
            if (vertex_count == 1)
            {
                vertices[0].amplitude = 3500;
                return;
            }

            /*
            double low_cost = vertices[0].costf;
            double high_cost = low_cost;
            
            for (int i = 1; i < vertex_count; i++)
            {
                double c = vertices[i].costf;
                if (c < low_cost)
                {
                    low_cost = c;
                }
                if(c > high_cost)
                {
                    high_cost = c;
                }
            }

            for (int i = 0; i < vertex_count; i++)
            {
                if (high_cost == low_cost)
                {
                    vertices[i].amplitude = 3500;
                }
                else
                {
                    vertices[i].amplitude
                        = 4000 - 1000 * (vertices[i].costf - low_cost) / (high_cost - low_cost);
                }
            }
            */

            // vertices[0].amplitude = 5000 - 1000 * vertices[0].start / duration;
            for (int i = 0; i < vertex_count; i++)
            {
                vertices[i].amplitude
                    = voice.work.rand.NextDouble() * 500
                    + 4000
                    + 300 * duration / (0.5 + vertices[i].duration);
            }
            //vertices[0].amplitude += 300;
        }


        public double play(StreamWriter file, double start)
        {
            double s = start;

            for (int i = 0; i < vertex_count; i++)
            {
                vertices[i].play(file, s);
            }
            return s + duration * shrink();
        }
    }
}
