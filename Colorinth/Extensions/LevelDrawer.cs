using System;
using System.Collections.Generic;
using System.Text;
using Colorinth.LevelGeneration;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Colorinth.Extensions
{
    public static class LevelDrawer
    {
        private static Texture2D _startTexture, _finishTexture;
        public static void Initialize(ContentManager content)
        {
            _startTexture = content.Load<Texture2D>("start");
            _finishTexture = content.Load<Texture2D>("finish");
        }

        public static void DrawLevel(this SpriteBatch spriteBatch, GraphicsDevice graphicsDevice, Level level, Rectangle area)
        {

        }
    }
}
