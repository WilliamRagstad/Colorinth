using System.Collections.Generic;

namespace Colorinth.Model
{
    public class Level
    {
        public byte SizeX, SizeY;
        public int TotalSize { get; }
        public List<char> tileList = new List<char>();
        public List<char> horizontalEdgeList = new List<char>();
        public List<char> verticalEdgeList = new List<char>();
        public Graph G;

        public Level(byte sizeX, byte sizeY)
        {
            SizeX = sizeX;
            SizeY = sizeY;
            TotalSize = sizeX * sizeY;
            G = null;

            for (int i = 0; i < TotalSize; i++) tileList.Add('.');
            for (int i = 0; i < sizeX*(sizeY-1); i++) horizontalEdgeList.Add('.');
            for (int i = 0; i < (sizeX-1)*sizeY; i++) verticalEdgeList.Add('.');
        }
    }
}