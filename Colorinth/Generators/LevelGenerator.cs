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
            }

            // Assign buttons to tiles
            int randomTileIndex;
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
            }

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

            for (int i = 0; i < totalSize; i++)
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
            
            int currTileIndex = startTileIndex;
            curr = vertices[currTileIndex];
            while(true)
            {
                if (currColors[0] == 'r') redList[currTileIndex] = 'r';
                else RedList[currTileIndex] = 'R';
                if (currColors[1] == 'b') blueList[currTileIndex] = 'b';
                else BlueList[currTileIndex] = 'B';
                if (currColors[2] == 'y') blueList[currTileIndex] = 'y';
                else BlueList[currTileIndex] = 'Y';
                if (currColors[3] == 'g') blueList[currTileIndex] = 'g';
                else BlueList[currTileIndex] = 'G';
                if (currColors[4] == 'p') blueList[currTileIndex] = 'p';
                else BlueList[currTileIndex] = 'P';
                if (currColors[5] == 'o') blueList[currTileIndex] = 'o';
                else BlueList[currTileIndex] = 'O';

                if (curr.index == finishTileIndex) break;
                
                curr = curr.edges[rand.Next(curr.edges.Count)];
            }
            
            List<char> currColorList = null;
            for (int i = 0; i < level.horizontalEdgeList.Count; i++)
            {
                if (level.horizontalEdgeList[i] == '.')
                {
                    for (int j = 0; j < 10*numOfColors; j++)
                    {
                        int colorIndex = rand.Next(numOfColors);
                        int closed = rand.Next(2);

                        if (closed == 0)
                        {
                            switch (colorIndex)
                            {
                                case 0: currColorList = redList; break;
                                case 1: currColorList = blueList; break;
                                case 2: currColorList = yellowList; break;
                                case 3: currColorList = greenList; break;
                                case 4: currColorList = purpleList; break;
                                case 5: currColorList = orangeList; break;
                            } 
                        }
                        else
                        {
                            switch (colorIndex)
                            {
                                case 0: currColorList = RedList; break;
                                case 1: currColorList = BlueList; break;
                                case 2: currColorList = YellowList; break;
                                case 3: currColorList = GreenList; break;
                                case 4: currColorList = PurpleList; break;
                                case 5: currColorList = OrangeList; break;
                            } 
                        }

                        if (closed == 1)
                        {
                            if (currColorList[i] == allColors[colorIndex] && currColorList[i+sizeX] == allColors[colorIndex])
                            {
                                level.horizontalEdgeList[i] = allColors[colorIndex];
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

                        if (closed == 0)
                        {
                            switch (colorIndex)
                            {
                                case 0: currColorList = redList; break;
                                case 1: currColorList = blueList; break;
                                case 2: currColorList = yellowList; break;
                                case 3: currColorList = greenList; break;
                                case 4: currColorList = purpleList; break;
                                case 5: currColorList = orangeList; break;
                            } 
                        }
                        else
                        {
                            switch (colorIndex)
                            {
                                case 0: currColorList = RedList; break;
                                case 1: currColorList = BlueList; break;
                                case 2: currColorList = YellowList; break;
                                case 3: currColorList = GreenList; break;
                                case 4: currColorList = PurpleList; break;
                                case 5: currColorList = OrangeList; break;
                            } 
                        }

                        if (closed == 1)
                        {
                            if (currColorList[i/((i/sizeX)*-1)] == allColors[colorIndex] && currColorList[i/((i/sizeX)*-1)+1] == allColors[colorIndex])
                            {
                                level.horizontalEdgeList[i] = allColors[colorIndex];
                            }
                        }
                    }
                }
            }

            return level;
        }
    }
}