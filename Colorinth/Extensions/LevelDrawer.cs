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
        private static readonly float _spriteScale = 1 / 3f;

        #endregion
        #region Textures
        
        private static Texture2D _startTexture, _finishTexture;

        #endregion
        public static void Initialize(ContentManager content)
        {
            _startTexture = content.Load<Texture2D>("start");
            _finishTexture = content.Load<Texture2D>("finish");
        }

        public static void DrawLevel(this SpriteBatch spriteBatch, GraphicsDevice graphicsDevice, Level level, Rectangle area, int areaScale)
        {
            int tileOffsetX = area.Width / level.SizeX / 2; //  -  ;
            int tileOffsetY = area.Height / level.SizeY / 2; //  -  _spriteWidth / 2;
            
            Vector2 spriteOrigin = new Vector2(_spriteWidth / 2f);
            float spriteScale = areaScale / 50f * _spriteScale;

            for (int y = 0; y < level.SizeY; y++)
            {
                for (int x = 0; x < level.SizeX; x++)
                {
                    char tile = level.GetTileAt(y * level.SizeY + x);

                    int cx = area.X + tileOffsetX + x * areaScale * 2;
                    int cy = area.Y + tileOffsetY + y * areaScale * 2;

                    switch (tile)
                    {
                        case 'S':
                            spriteBatch.Draw(_startTexture, new Vector2(cx, cy), null, Color.White, 0, spriteOrigin, spriteScale, SpriteEffects.None, 0);
                            break;
                        case 'F':
                            spriteBatch.Draw(_finishTexture, new Vector2(cx, cy), null, Color.White, 0, spriteOrigin, spriteScale, SpriteEffects.None, 0);
                            break;
                    }
                }
            }
        }
    }
}
