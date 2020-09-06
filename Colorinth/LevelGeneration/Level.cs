using System.Collections.Generic;

namespace Colorinth.LevelGeneration
{
    public class Level
    {
        private byte sizeX, sizeY;
        private List<char> tileList;
        private List<char> horizontalEdgeList;
        private List<char> verticalEdgeList;

        public Level(byte sizeX, byte sizeY)
        {
            this.sizeX = sizeX;
            this.sizeY = sizeY;

            tileList = new List<char>();
            horizontalEdgeList = new List<char>();
            verticalEdgeList = new List<char>();

            for (int i = 0; i < sizeX*sizeY; i++)
            {
                tileList.Add('.');
            }
            for (int i = 0; i < sizeX*(sizeY-1); i++)
            {
                horizontalEdgeList.Add('.');
            }
            for (int i = 0; i < (sizeX-1)*sizeY; i++)
            {
                verticalEdgeList.Add('.');
            }
        }
        public void SetSizeX(byte size)
        {
            sizeX = size;
        }
        public byte GetSizeX()
        {
            return sizeX;
        }
        public void SetSizeY(byte size)
        {
            sizeY = size;
        }
        public byte GetSizeY()
        {
            return sizeY;
        }
        public void SetTileAt(int index, char tile)
        {
            tileList[index] = tile;
        }
        public char GetTileAt(int index)
        {
            return tileList[index];
        }
        public void SetHorizontalEdgeAt(int index, char edge)
        {
            horizontalEdgeList[index] = edge;
        }
        public char GetHorizontalEdgeAt(int index)
        {
            return horizontalEdgeList[index];
        }
        public void SetVerticalEdgeAt(int index, char edge)
        {
            verticalEdgeList[index] = edge;
        }
        public char GetVerticalEdgeAt(int index)
        {
            return verticalEdgeList[index];
        }
    }
}