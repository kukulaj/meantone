using System;
using System.Collections.Generic;
using System.Text;

namespace meantone
{
    public class Above_Factory : Edge_Factory
    {        public override void make_edge(Vertex v1, Vertex v2)
        {
            v1.link_vertical(v2);
            v2.link_vertical(v1);
        }
    }
}
