using System;
using System.Collections.Generic;
using Colorinth.Model;
using Colorinth.MazeGraph;

namespace Colorinth.Generators
{
    public static class LevelGenerator
    {
        public static Level GenerateLevel(byte numOfColors, byte sizeX, byte sizeY, double buttonRatio, bool randomStartFinish)
        {
            Level level = new Level(sizeX, sizeY);

            Random rand = new Random();
            int totalSize = sizeX*sizeY;

            int startTileIndex;
            // Sets random start and finish tiles
            if (randomStartFinish)
            {
                startTileIndex = rand.Next(totalSize);
                level.tileList[startTileIndex] = 'S';
                int finishTileIndex = startTileIndex;
                while (startTileIndex == finishTileIndex)
                {
                    finishTileIndex = rand.Next(totalSize);
                }
                level.tileList[finishTileIndex] = 'F';
            }
            // Sets start tile in the middle of the left-most column and finish tile in the middle of the right-most column
            else
            {
                startTileIndex = sizeX*(sizeY/2);
                level.tileList[sizeX*(sizeY/2+1)-1] = 'F';
            }

            Graph G = new Graph(sizeX, sizeY);
            level.G = G;
            List<Vertex> vertices = G.vertices;
            Prim.RunPrims(startTileIndex, G);

            for (int i = 0; i < sizeY; i++)
            {
                for (int j = 0; j < sizeX; j++)
                {
                    int currIndex = i*sizeX+j;
                    Vertex curr = vertices[currIndex];
                    if (j < sizeX-1)
                    {
                        if (!curr.edges.Contains(vertices[currIndex+1]))
                        {
                            level.verticalEdgeList[currIndex-curr.coordinateY] = 'W';
                        }
                    }
                    if (i < sizeY-1)
                    {
                        if (!curr.edges.Contains(vertices[currIndex+sizeX]))
                        {
                            level.horizontalEdgeList[currIndex] = 'W';
                        }
                    }
                }
            }

            // Creates lists containing all colors that will be used, based on the argument numOfColors
            // Red, blue, yellow, green, purple, orange
            List<char> colors = new List<char>();
            List<char> colorsOpen = new List<char>();
            char[] allColors = new char[] {'R', 'B', 'Y', 'G', 'P', 'O'};
            char[] allColorsOpen = new char[] {'r', 'b', 'y', 'g', 'p', 'o'};
            for (int i = 0; i < numOfColors; i++) {
                colors.Add(allColors[i]);
                colorsOpen.Add(allColorsOpen[i]);
            }

            // Calculate exact number of buttons of each color
            int numColorsLeft = numOfColors;
            int numButtonsLeft = (int) (buttonRatio*totalSize);
            // Red, blue, yellow, green, purple, orange
            List<int> numOfEachButton = new List<int>();
            for (int i = 0; i < numOfColors; i++)
            {
                numOfEachButton.Add((int)(Math.Ceiling((double)numButtonsLeft/numColorsLeft)));
                numButtonsLeft -= numOfEachButton[i];
                numColorsLeft--;
            }

            // Assign buttons to tiles
            int randomTileIndex;
            for (int i = 0; i < numOfColors; i++)
            {
                for (int j = 0; j < numOfEachButton[i]; j++)
                {
                    while (true) {
                        randomTileIndex = rand.Next(totalSize);
                        if (level.tileList[randomTileIndex] == '.')
                        {
                            level.tileList[randomTileIndex] = allColors[i];
                            break;
                        }
                }
                }
            }

            for (int i = 0; i < vertices.Count; i++)
            {
                vertices[i].visited = false;
            }



            return level;
        }
    }
}