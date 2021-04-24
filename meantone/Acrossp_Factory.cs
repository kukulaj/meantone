using System;
using System.Collections.Generic;
using System.Text;

namespace meantone
{
    public class Acrossp_Factory : Edge_Factory
    {
        public override void make_edge(Vertex v1, Vertex v2)
        {
            v1.link_acrossp(v2);
            v2.link_acrossp(v1);
        }
    }
}
