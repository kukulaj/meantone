using System;
using System.Collections.Generic;
using System.Text;

namespace meantone
{
    public abstract class Edge_Factory
    {
        public abstract void make_edge(Vertex v1, Vertex v2);
    }
}
