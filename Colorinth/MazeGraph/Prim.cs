using System.Collections.Generic;
using System;
using Colorinth.Model;

namespace Colorinth.MazeGraph
{
    public static class Prim
    {
        public static void RunPrims(int startIndex, Graph G)
        {
            int sizeX = G.sizeX;
            int sizeY = G.sizeY;
            int totalSize = G.totalSize;
            List<Vertex> vertices = G.vertices;
            List<Vertex> walls2 = new List<Vertex>();
            List<Vertex> walls1 = new List<Vertex>();
            Random rand = new Random();

            if (startIndex < 0 || startIndex >= totalSize)
            {
                startIndex = rand.Next(totalSize);
            }
            Vertex currCell = G.vertices[startIndex];
            Vertex oldCell = null;
            currCell.visited = true;
            if (currCell.coordinateX < G.sizeX-1)
            {
                Vertex rightCell = vertices[currCell.index + 1];
                walls2.Add(currCell);
                walls1.Add(rightCell);
            }
            if (currCell.coordinateX > 0)
            {
                Vertex leftCell = vertices[currCell.index - 1];
                walls2.Add(currCell);
                walls1.Add(leftCell);
            }
            if (currCell.coordinateY < G.sizeY - 1)
            {
                Vertex cellBelow = vertices[currCell.index + sizeX];
                walls2.Add(currCell);
                walls1.Add(cellBelow);
            }
            if (currCell.coordinateY > 0)
            {
                Vertex cellAbove = vertices[currCell.index - sizeX];
                walls2.Add(currCell);
                walls1.Add(cellAbove);
            }

            while (walls1.Count > 0)
            {
                int wallIndex = rand.Next(walls1.Count); 
                currCell = null;
                if (walls1[wallIndex].visited == true && walls2[wallIndex].visited == false)
                {
                    currCell = walls2[wallIndex];
                    oldCell = walls1[wallIndex];
                }
                else if (walls1[wallIndex].visited == false && walls2[wallIndex].visited == true)
                {
                    currCell = walls1[wallIndex];
                    oldCell = walls2[wallIndex];
                }
                if (currCell != null) {
                    currCell.visited = true;
                    currCell.edges.Add(oldCell);
                    oldCell.edges.Add(currCell);
                    
                    if (currCell.coordinateX < G.sizeX-1)
                    {
                        Vertex rightCell = vertices[currCell.index + 1];
                        if (rightCell.visited == false)
                        {
                            walls2.Add(currCell);
                            walls1.Add(rightCell);
                        }
                    }
                    if (currCell.coordinateX > 0)
                    {
                        Vertex leftCell = vertices[currCell.index - 1];
                        if (leftCell.visited == false)
                        {
                            walls2.Add(currCell);
                            walls1.Add(leftCell);
                        }
                    }
                    if (currCell.coordinateY < G.sizeY - 1)
                    {
                        Vertex cellBelow = vertices[currCell.index + sizeX];
                        if (cellBelow.visited == false)
                        {
                            walls2.Add(currCell);
                            walls1.Add(cellBelow);
                        }
                    }
                    if (currCell.coordinateY > 0)
                    {
                        Vertex cellAbove = vertices[currCell.index - sizeX];
                        if (cellAbove.visited == false)
                        {
                            walls2.Add(currCell);
                            walls1.Add(cellAbove);
                        }
                    }
                }
                walls1.RemoveAt(wallIndex);
                walls2.RemoveAt(wallIndex);
            }
        }
    }
}