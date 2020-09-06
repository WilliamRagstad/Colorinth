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
        #region Properties

        private static readonly int _spriteWidth = 195;

        #endregion
        #region Textures
        
        private static Texture2D _startTexture, _finishTexture;

        #endregion
        public static void Initialize(ContentManager content)
        {
            _startTexture = content.Load<Texture2D>("start");
            _finishTexture = content.Load<Texture2D>("finish");
        }

        public static void DrawLevel(this SpriteBatch spriteBatch, GraphicsDevice graphicsDevice, Level level, Rectangle area)
        {
            int tileOffset = area.Width / level.SizeX / 2;
            float spriteScale = 1;

            for (int y = 0; y < level.SizeY; y++)
            {
                for (int x = 0; x < level.SizeX; x++)
                {
                    char tile = level.GetTileAt(y * level.SizeY + x);
                    
                    int cx = area.X + x - _spriteWidth / 2 + tileOffset;
                    int cy = area.Y + y - _spriteWidth / 2 + tileOffset;

                    switch (tile)
                    {
                        case 'S':
                            spriteBatch.Draw(_startTexture, new Vector2(cx, cy), null, Color.White, 0, Vector2.Zero, spriteScale, SpriteEffects.None, 0);
                            break;
                        case 'F':
                            spriteBatch.Draw(_finishTexture, new Vector2(cx, cy), null, Color.White, 0, Vector2.Zero, spriteScale, SpriteEffects.None, 0);
                            break;
                    }
                }
            }
        }
    }
}
