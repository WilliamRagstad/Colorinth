using System.Collections.Generic;

namespace Colorinth.Model
{
    public class Level
    {
        public byte SizeX, SizeY;
        public int TotalSize { get; }
        private readonly List<char> _tileList = new List<char>();
        private readonly List<char> _horizontalEdgeList = new List<char>();
        private readonly List<char> _verticalEdgeList = new List<char>();

        public Level(byte sizeX, byte sizeY)
        {
            SizeX = sizeX;
            SizeY = sizeY;
            TotalSize = sizeX * sizeY;

            for (int i = 0; i < TotalSize; i++) _tileList.Add('.');
            for (int i = 0; i < sizeX*(sizeY-1); i++) _horizontalEdgeList.Add('.');
            for (int i = 0; i < (sizeX-1)*sizeY; i++) _verticalEdgeList.Add('.');
        }

        public void SetTileAt(int index, char tile) => _tileList[index] = tile;
        public char GetTileAt(int index) => _tileList[index];
        public void SetHorizontalEdgeAt(int index, char edge) => _horizontalEdgeList[index] = edge;
        public char GetHorizontalEdgeAt(int index) => _horizontalEdgeList[index];
        public void SetVerticalEdgeAt(int index, char edge) => _verticalEdgeList[index] = edge;
        public char GetVerticalEdgeAt(int index) => _verticalEdgeList[index];
    }
}