using System.Collections.Generic;
using System;

namespace Colorinth.Graph
{
    public static class Prim
    {
        public static void runPrims(int startIndex, Graph G)
        {
            List<Vertex> cellsInMaze = new List<Vertex>();
            List<Vertex> walls1 = new List<Vertex>();
            List<Vertex> walls2 = new List<Vertex>();
            Random rand = new Random();
            int totalNumVertices = G.totalSize;
            if (startIndex < 0 || startIndex >= G.totalSize)
            {
                startIndex = rand.Next(totalNumVertices);
            }
            Vertex currCell = G.vertices[startIndex];
            cellsInMaze.Add(currCell);
            //if(startIndex)
        }
    }
}