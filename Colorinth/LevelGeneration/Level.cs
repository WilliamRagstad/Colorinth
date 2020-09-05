using System.Collections.Generic;

namespace Colorinth.Generators
{
    public class Level
    {
        private byte sizeX, sizeY;
        private List<char> tileList;
        private List<char> horizontalEdgeList;
        private List<char> verticalEdgeList;

        public void initialize(byte sizeX, byte sizeY)
        {
            this.sizeX = sizeX;
            this.sizeY = sizeY;

            for (int i = 0; i < sizeX*sizeY; i++)
            {
                tileList.Add(' ');
            }
            for (int i = 0; i < sizeX*(sizeY-1); i++)
            {
                horizontalEdgeList.Add(' ');
            }
            for (int i = 0; i < (sizeX-1)*sizeY; i++)
            {
                verticalEdgeList.Add(' ');
            }
        }
        public void setSizeX(byte size)
        {
            sizeX = size;
        }
        public byte getSizeX()
        {
            return sizeX;
        }
        public void setSizeY(byte size)
        {
            sizeY = size;
        }
        public byte getSizeY()
        {
            return sizeY;
        }
        public void setTileAt(int index, char tile)
        {
            tileList[index] = tile;
        }
        public char getTileAt(int index)
        {
            return tileList[index];
        }
        public void setHorizontalEdgeAt(int index, char edge)
        {
            horizontalEdgeList[index] = edge;
        }
        public char getHorizontalEdgeAt(int index)
        {
            return horizontalEdgeList[index];
        }
        public void setVerticalEdgeAt(int index, char edge)
        {
            verticalEdgeList[index] = edge;
        }
        public char getVerticalEdgeAt(int index)
        {
            return verticalEdgeList[index];
        }
    }
}