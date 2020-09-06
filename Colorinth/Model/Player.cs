using System;
using System.Collections.Generic;
using System.Text;
using Colorinth.Managers;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;

namespace Colorinth.Model
{
    public class Player
    {
        /// <summary>
        /// Level X or Y position.
        /// </summary>
        public byte X, Y;

        private static SoundEffect _walkSoundEffect;

        public Player(byte x, byte y)
        {
            X = x;
            Y = y;
        }

        public static void Initialize(ContentManager content)
        {
            _walkSoundEffect = content.Load<SoundEffect>("walk_effect");
        }

        public void Move(KeyboardState kb, Level level)
        {
            if (kb.IsKeyDown(Keys.Left) && X > 0 && !WallLeft(level))
            {
                X--;
                SoundEffectManager.Play(_walkSoundEffect);
            }
            else if (kb.IsKeyDown(Keys.Right) && X < level.SizeX - 1 && !WallRight(level))
            {
                X++;
                SoundEffectManager.Play(_walkSoundEffect);
            }

            if (kb.IsKeyDown(Keys.Up) && Y > 0 && !WallAbove(level))
            {
                Y--;
                SoundEffectManager.Play(_walkSoundEffect);
            }
            else if (kb.IsKeyDown(Keys.Down) && Y < level.SizeY - 1 && !WallUnder(level))
            {
                Y++;
                SoundEffectManager.Play(_walkSoundEffect);
            }
        }

        private bool WallRight(Level level)
        {
            int i = Y * level.SizeX + X;
            return !level.G.vertices[i].edges.Contains(level.G.vertices[i + 1]);
        }

        private bool WallLeft(Level level)
        {
            int i = Y * level.SizeX + X;
            return !level.G.vertices[i].edges.Contains(level.G.vertices[i - 1]);
        }

        private bool WallAbove(Level level)
        {
            int i = Y * level.SizeX + X;
            return !level.G.vertices[i].edges.Contains(level.G.vertices[i - level.SizeX]);
        }
        private bool WallUnder(Level level)
        {
            int i = Y * level.SizeX + X;
            return !level.G.vertices[i].edges.Contains(level.G.vertices[i + level.SizeX]);
        }
    }
}
