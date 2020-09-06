using System;
using System.Collections.Generic;
using System.Text;

namespace Colorinth.Model
{
    public class Player
    {
        /// <summary>
        /// Level X or Y position.
        /// </summary>
        public byte X, Y;

        public Player(byte x, byte y)
        {
            X = x;
            Y = y;
        }
    }
}
