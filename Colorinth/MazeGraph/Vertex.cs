using System;
using System.Collections.Generic;

namespace Colorinth.Graph
{
    public class Vertex
    {
        public List<Vertex> edges;
        public Vertex previous;
        public int key;

        public Vertex()
        {
            edges = new List<Vertex>();
            previous = null;
            key = Int32.MaxValue;
            
        }
    }
}