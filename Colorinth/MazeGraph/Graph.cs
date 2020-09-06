using System.Collections.Generic;

namespace Colorinth.Graph
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
            for (int i = 0; i < totalSize; i++) {
                vertices.Add(new Vertex());
            }
        }
    }
}