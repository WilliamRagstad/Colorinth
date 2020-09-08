using System;
using Colorinth.Model;
using System.Collections.Generic;

namespace Colorinth.Updater
{
    public class LevelUpdater
    {
        public static void ButtonUpdateLevel(Player player, Level level)
        {
            byte sizeX = level.SizeX;
            List<char> tileList = level.tileList;
            int currIndex = player.Y*sizeX + player.X;
            if (tileList[currIndex] != '.')
            {
                List<Vertex> vertices = level.G.vertices;
                char closedDoorColor = tileList[currIndex];
                char openDoorColor = (char) (closedDoorColor + 32);
                
                for (int i = 0; i < level.horizontalEdgeList.Count; i++)
                {
                    if (level.horizontalEdgeList[i] == closedDoorColor)
                    {
                        vertices[i].edges.Remove(vertices[i+sizeX]);
                        vertices[i+sizeX].edges.Remove(vertices[i]);
                        level.horizontalEdgeList[i] = openDoorColor;
                    }
                    else if (level.horizontalEdgeList[i] == openDoorColor)
                    {
                        vertices[i].edges.Add(vertices[i+sizeX]);
                        vertices[i+sizeX].edges.Add(vertices[i]);
                        level.horizontalEdgeList[i] = closedDoorColor;
                    }
                }
                for (int i = 0; i < level.verticalEdgeList.Count; i++)
                {
                    if (level.verticalEdgeList[i] == closedDoorColor)
                    {
                        vertices[(i/(sizeX-1))*sizeX + (i%(sizeX-1))].edges.Remove(vertices[(i/(sizeX-1))*sizeX + (i%(sizeX-1))+1]);
                        vertices[(i/(sizeX-1))*sizeX + (i%(sizeX-1))+1].edges.Remove(vertices[(i/(sizeX-1))*sizeX + (i%(sizeX-1))]);
                        level.verticalEdgeList[i] = openDoorColor;
                    }
                    else if (level.verticalEdgeList[i] == openDoorColor)
                    {
                        vertices[(i/(sizeX-1))*sizeX + (i%(sizeX-1))].edges.Add(vertices[(i/(sizeX-1))*sizeX + (i%(sizeX-1))+1]);
                        vertices[(i/(sizeX-1))*sizeX + (i%(sizeX-1))+1].edges.Add(vertices[(i/(sizeX-1))*sizeX + (i%(sizeX-1))]);
                        level.verticalEdgeList[i] = closedDoorColor;
                    }
                }
            }
        }
    }
}