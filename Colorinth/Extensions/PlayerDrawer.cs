using System;
using System.Collections.Generic;
using System.Text;
using Colorinth.Model;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Colorinth.Extensions
{
    public static class PlayerDrawer
    {
        #region Properties

        private static readonly int _spriteWidth = 195;
        private static readonly float _spriteScale = 1 / 3f;

        #endregion
        #region Textures
        
        private static Texture2D _playerTexture;

        #endregion
        public static void Initialize(ContentManager content)
        {
            _playerTexture = content.Load<Texture2D>("player");
        }
        public static void DrawPlayer(this SpriteBatch spriteBatch, GraphicsDevice graphicsDevice, Level level, Player player, Rectangle area, int areaScale)
        {
            #region Local Vars
            
            int tileOffsetX = area.Width / level.SizeX / 2;
            int tileOffsetY = area.Height / level.SizeY / 2;
            
            Vector2 spriteOrigin = new Vector2(_spriteWidth / 2f);
            float spriteScale = areaScale / 50f * _spriteScale;

            #endregion

            #region Player

            int cx = area.X + tileOffsetX + player.X * areaScale * 2;
            int cy = area.Y + tileOffsetY + player.Y * areaScale * 2;

            spriteBatch.Draw(_playerTexture, new Vector2(cx, cy), null, Color.White, 0, spriteOrigin, spriteScale, SpriteEffects.None, 0);

            #endregion
        }
    }
}
