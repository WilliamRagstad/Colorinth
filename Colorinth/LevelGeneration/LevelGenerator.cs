using System;
using System.Collections.Generic;

namespace Colorinth.Generators
{
    public static class LevelGenerator
    {
        private static Level GenerateLevel(byte numOfColors, byte sizeX, byte sizeY, byte buttonRatio, byte pitRatio, byte wallRatio, byte gateRatio, bool randomStartFinish)
        {
            Level level = new Level();
            level.initialize(sizeX, sizeY);

            Random rand = new Random();
            int totalSize = sizeX*sizeY;

            if (randomStartFinish) {
                int startTileIndex = rand.Next(totalSize);
                level.setTileAt(startTileIndex, 'S');
                int finishTileIndex = startTileIndex;
                while (startTileIndex == finishTileIndex) {
                    finishTileIndex = rand.Next(totalSize);
                }
                level.setTileAt(finishTileIndex, 'F');
            }


            return level;
        }
    }
}