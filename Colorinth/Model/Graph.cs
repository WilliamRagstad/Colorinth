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
            for (byte i = 0; i < sizeX; i++)
            {
                for (byte j = 0; j < sizeY; j++)
                {
                    vertices.Add(new Vertex());
                    vertices[i*j].coordinateX = i;
                    vertices[i*j].coordinateY = j;
                    vertices[i*j].index = i*j;
                }
            }
        }
    }
}