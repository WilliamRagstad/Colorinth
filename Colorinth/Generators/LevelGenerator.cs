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
            Vertex curr;
            int currIndex;

            Random rand = new Random();
            int totalSize = sizeX*sizeY;

            int startTileIndex;
            int finishTileIndex;
            // Sets random start and finish tiles
            if (randomStartFinish)
            {
                startTileIndex = rand.Next(totalSize);
                level.tileList[startTileIndex] = 'S';
                finishTileIndex = startTileIndex;
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
                level.tileList[startTileIndex] = 'S';
                finishTileIndex = sizeX*(sizeY/2+1)-1;
                level.tileList[finishTileIndex] = 'F';
            }
            level.StartX = (byte)(startTileIndex % sizeX);
            level.StartY = (byte)(startTileIndex / sizeX);

            Graph G = new Graph(sizeX, sizeY);
            level.G = G;
            List<Vertex> vertices = G.vertices;
            Prim.RunPrims(startTileIndex, G);

            for (int i = 0; i < sizeY; i++)
            {
                for (int j = 0; j < sizeX; j++)
                {
                    currIndex = i*sizeX+j;
                    curr = vertices[currIndex];
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
            int numButtonsLeft = (int) (buttonRatio*(totalSize-2));
            // Red, blue, yellow, green, purple, orange
            List<int> numOfEachButton = new List<int>();
            for (int i = 0; i < numOfColors; i++)
            {
                numOfEachButton.Add((int)(Math.Ceiling((double)numButtonsLeft/numColorsLeft)));
                numButtonsLeft -= numOfEachButton[i];
                numColorsLeft--;
                Console.WriteLine("{0}", numOfEachButton[i]);
            }

            // Assign buttons to tiles
            for (int i = 0; i < vertices.Count; i++)
            {
                if (i != startTileIndex && i != finishTileIndex)
                {
                    if (vertices[i].edges.Count == 1)
                    {
                        level.tileList[i] = colors[rand.Next(numOfColors)];
                    }
                }
                
            }

            /* int randomTileIndex;
            for (int i = 0; i < numOfColors; i++)
            {
                for (int j = 0; j < numOfEachButton[i]; j++)
                {
                    while (true) {
                        randomTileIndex = rand.Next(totalSize);
                        if (level.tileList[randomTileIndex] == '.' && randomTileIndex != startTileIndex && randomTileIndex != finishTileIndex)
                        {
                            level.tileList[randomTileIndex] = allColors[i];
                            break;
                        }
                    }
                }
            } */

            for (int i = 0; i < vertices.Count; i++)
            {
                vertices[i].visited = false;
            }

            List<char> RedList = new List<char>();
            List<char> BlueList = new List<char>();
            List<char> YellowList = new List<char>();
            List<char> GreenList = new List<char>();
            List<char> PurpleList = new List<char>();
            List<char> OrangeList = new List<char>();
            List<char> redList = new List<char>();
            List<char> blueList = new List<char>();
            List<char> yellowList = new List<char>();
            List<char> greenList = new List<char>();
            List<char> purpleList = new List<char>();
            List<char> orangeList = new List<char>();

            for (int i = 0; i <= totalSize; i++)
            {
                RedList.Add('.');
                BlueList.Add('.');
                YellowList.Add('.');
                GreenList.Add('.');
                PurpleList.Add('.');
                OrangeList.Add('.');
                redList.Add('.');
                blueList.Add('.');
                yellowList.Add('.');
                greenList.Add('.');
                purpleList.Add('.');
                orangeList.Add('.');
            }

            char[] currColors = {'r', 'b', 'y', 'g', 'p', 'o'};
            
            Vertex currVertex = vertices[startTileIndex];
            while(true)
            {
                Console.WriteLine("({0}, {1}) ({2}, {3}, {4}, {5}, {6}, {7})", currVertex.coordinateX, currVertex.coordinateY, currColors[0],
                currColors[1], currColors[2], currColors[3], currColors[4], currColors[5]);
                if (currColors[0] == 'r') redList[currVertex.index] = 'r';
                else RedList[currVertex.index] = 'R';
                if (currColors[1] == 'b') blueList[currVertex.index] = 'b';
                else BlueList[currVertex.index] = 'B';
                if (currColors[2] == 'y') yellowList[currVertex.index] = 'y';
                else YellowList[currVertex.index] = 'Y';
                if (currColors[3] == 'g') greenList[currVertex.index] = 'g';
                else GreenList[currVertex.index] = 'G';
                if (currColors[4] == 'p') purpleList[currVertex.index] = 'p';
                else PurpleList[currVertex.index] = 'P';
                if (currColors[5] == 'o') orangeList[currVertex.index] = 'o';
                else OrangeList[currVertex.index] = 'O';
            
                if (level.tileList[currVertex.index] == 'R')
                {
                    if (currColors[0] == 'r') currColors[0] = 'R';
                    else currColors[0] = 'r';
                    Console.WriteLine('R');
                }
                else if (level.tileList[currVertex.index] == 'B')
                {
                    if (currColors[1] == 'b') currColors[1] = 'B';
                    else currColors[1] = 'b';
                    Console.WriteLine('B');
                }
                else if (level.tileList[currVertex.index] == 'Y')
                {
                    if (currColors[2] == 'y') currColors[2] = 'Y';
                    else currColors[2] = 'y';
                    Console.WriteLine('Y');
                }
                else if (level.tileList[currVertex.index] == 'G')
                {
                    if (currColors[3] == 'g') currColors[3] = 'G';
                    else currColors[3] = 'g';
                    Console.WriteLine('G');
                }
                else if (level.tileList[currVertex.index] == 'P')
                {
                    if (currColors[4] == 'p') currColors[4] = 'P';
                    else currColors[4] = 'p';
                    Console.WriteLine('P');
                }
                else if (level.tileList[currVertex.index] == 'O')
                {
                    if (currColors[5] == 'o') currColors[5] = 'O';
                    else currColors[5] = 'o';
                    Console.WriteLine('O');
                }

                if (currColors[0] == 'r') redList[currVertex.index] = 'r';
                else RedList[currVertex.index] = 'R';
                if (currColors[1] == 'b') blueList[currVertex.index] = 'b';
                else BlueList[currVertex.index] = 'B';
                if (currColors[2] == 'y') yellowList[currVertex.index] = 'y';
                else YellowList[currVertex.index] = 'Y';
                if (currColors[3] == 'g') greenList[currVertex.index] = 'g';
                else GreenList[currVertex.index] = 'G';
                if (currColors[4] == 'p') purpleList[currVertex.index] = 'p';
                else PurpleList[currVertex.index] = 'P';
                if (currColors[5] == 'o') orangeList[currVertex.index] = 'o';
                else OrangeList[currVertex.index] = 'O';

                if (currVertex.index == finishTileIndex) break;
                
                currVertex = currVertex.edges[rand.Next(currVertex.edges.Count)];
            }
            
            List<char> closedColorList = null;
            List<char> openColorList = null;
            for (int i = 0; i < level.horizontalEdgeList.Count; i++)
            {
                if (level.horizontalEdgeList[i] == '.')
                {
                    for (int j = 0; j < 10*numOfColors; j++)
                    {
                        int colorIndex = rand.Next(numOfColors);
                        int closed = rand.Next(2);

                        switch (colorIndex)
                        {
                            case 0: openColorList = redList; break;
                            case 1: openColorList = blueList; break;
                            case 2: openColorList = yellowList; break;
                            case 3: openColorList = greenList; break;
                            case 4: openColorList = purpleList; break;
                            case 5: openColorList = orangeList; break;
                                
                        }
                        switch (colorIndex)
                        {
                            case 0: closedColorList = RedList; break;
                            case 1: closedColorList = BlueList; break;
                            case 2: closedColorList = YellowList; break;
                            case 3: closedColorList = GreenList; break;
                            case 4: closedColorList = PurpleList; break;
                            case 5: closedColorList = OrangeList; break;
                        } 

                        if (closed == 1)
                        {
                            if ((openColorList[i] != allColorsOpen[colorIndex] || openColorList[i+sizeX] != allColorsOpen[colorIndex]) && (closedColorList[i] == allColors[colorIndex] && closedColorList[i+sizeX] == allColors[colorIndex]))
                            {
                                level.horizontalEdgeList[i] = allColors[colorIndex];
                                vertices[i].edges.Remove(vertices[i+sizeX]);
                                vertices[i+sizeX].edges.Remove(vertices[i]);
                                break;
                            }
                        }
                        else
                        {
                            if ((closedColorList[i] != allColors[colorIndex] || closedColorList[i+sizeX] != allColors[colorIndex]) && (openColorList[i] == allColorsOpen[colorIndex] && openColorList[i+sizeX] == allColorsOpen[colorIndex]))
                            {
                                level.horizontalEdgeList[i] = allColorsOpen[colorIndex];
                                break;
                            }
                        }
                    }
                }
            }
            for (int i = 0; i < level.verticalEdgeList.Count; i++)
            {
                if (level.verticalEdgeList[i] == '.')
                {
                    for (int j = 0; j < 10*numOfColors; j++)
                    {
                        int colorIndex = rand.Next(numOfColors);
                        int closed = rand.Next(2);

                        switch (colorIndex)
                        {
                            case 0: openColorList = redList; break;
                            case 1: openColorList = blueList; break;
                            case 2: openColorList = yellowList; break;
                            case 3: openColorList = greenList; break;
                            case 4: openColorList = purpleList; break;
                            case 5: openColorList = orangeList; break;
                                
                        }
                        switch (colorIndex)
                        {
                            case 0: closedColorList = RedList; break;
                            case 1: closedColorList = BlueList; break;
                            case 2: closedColorList = YellowList; break;
                            case 3: closedColorList = GreenList; break;
                            case 4: closedColorList = PurpleList; break;
                            case 5: closedColorList = OrangeList; break;
                        } 

                        if (closed == 1)
                        {
                            if ((openColorList[(i/(sizeX-1))*sizeX + (i%(sizeX-1))] != allColorsOpen[colorIndex] || openColorList[(i/(sizeX-1))*sizeX + (i%(sizeX-1))+1] != allColorsOpen[colorIndex]) && (closedColorList[(i/(sizeX-1))*sizeX + (i%(sizeX-1))] == allColors[colorIndex] && closedColorList[(i/(sizeX-1))*sizeX + (i%(sizeX-1))+1] == allColors[colorIndex]))
                            {
                                level.verticalEdgeList[i] = allColors[colorIndex];
                                vertices[(i/(sizeX-1))*sizeX + (i%(sizeX-1))].edges.Remove(vertices[(i/(sizeX-1))*sizeX + (i%(sizeX-1))+1]);
                                vertices[(i/(sizeX-1))*sizeX + (i%(sizeX-1))+1].edges.Remove(vertices[(i/(sizeX-1))*sizeX + (i%(sizeX-1))]);
                                break;
                            }
                        }
                        else
                        {
                            if ((closedColorList[(i/(sizeX-1))*sizeX + (i%(sizeX-1))] != allColors[colorIndex] || closedColorList[(i/(sizeX-1))*sizeX + (i%(sizeX-1))+1] != allColors[colorIndex]) && (openColorList[(i/(sizeX-1))*sizeX + (i%(sizeX-1))] != allColorsOpen[colorIndex] || openColorList[(i/(sizeX-1))*sizeX + (i%(sizeX-1))+1] != allColorsOpen[colorIndex]))
                            {
                                level.verticalEdgeList[i] = allColorsOpen[colorIndex];
                                break;
                            }
                        }
                    }
                }
            }

            return level;
        }
    }
}