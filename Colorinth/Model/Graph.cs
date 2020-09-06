using System.Collections.Generic;

namespace Colorinth.Model
{
    public class Graph
    {
        public byte sizeX;
        public byte sizeY;
        public int totalSize;
        public List<Vertex> vertices;
        public Graph(byte sizeX, byte sizeY) {
            totalSize = sizeX*sizeY;
            this.sizeX = sizeX;
            this.sizeY = sizeY;
            vertices = new List<Vertex>();
            for (byte i = 0; i < sizeY; i++)
            {
                for (byte j = 0; j < sizeX; j++)
                {
                    Vertex v = new Vertex();
                    v.coordinateX = i;
                    v.coordinateY = j;
                    v.index = i*j;
                    vertices.Add(v);
                }
            }
        }
    }
}