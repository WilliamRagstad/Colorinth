using System;
using System.Collections.Generic;

namespace Colorinth.LevelGeneration
{
    public static class LevelGenerator
    {
        public static Level GenerateLevel(byte numOfColors, byte sizeX, byte sizeY, double buttonRatio, bool randomStartFinish)
        {
            Level level = new Level(sizeX, sizeY);

            Random rand = new Random();
            int totalSize = sizeX*sizeY;

            // Sets random start and finish tiles
            if (randomStartFinish)
            {
                int startTileIndex = rand.Next(totalSize);
                level.SetTileAt(startTileIndex, 'S');
                int finishTileIndex = startTileIndex;
                while (startTileIndex == finishTileIndex)
                {
                    finishTileIndex = rand.Next(totalSize);
                }
                level.SetTileAt(finishTileIndex, 'F');
            }
            // Sets start tile in the middle of the left-most column and finish tile in the middle of the right-most column
            else
            {
                level.SetTileAt(sizeX*(sizeY/2-1)-1, 'S');
                level.SetTileAt(sizeX*(sizeY/2-1)-1+sizeX, 'F');
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
            Console.WriteLine("{0}", numButtonsLeft);
            for (int i = 0; i < numOfColors; i++)
            {
                numOfEachButton.Add((int)(Math.Ceiling((double)numButtonsLeft/numColorsLeft)));
                numButtonsLeft = numButtonsLeft - numOfEachButton[i];
                numColorsLeft--;
                Console.WriteLine("{0}", numOfEachButton[i]);
            }

            return level;
        }
    }
}