using System;
using System.Collections.Generic;

namespace Colorinth.Graph
{
    public class Vertex
    {
        public bool visited;
        public byte coordinateX;
        public byte coordinateY;
        public int index;
        public List<Vertex> edges;
        public Vertex previous;
        public int key;

        public Vertex()
        {
            visited = false;
            edges = new List<Vertex>();
            previous = null;
            key = Int32.MaxValue;
            
        }
    }
}