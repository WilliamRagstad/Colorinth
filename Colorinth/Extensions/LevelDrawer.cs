using System;
using System.Collections.Generic;
using System.Text;
using Colorinth.Generators;
using Colorinth.Model;
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

        private static readonly Dictionary<char, Color> _colors = new Dictionary<char, Color>
        {
            {'R', new Color(235, 86, 75)}, // 176, 48, 92
            {'B', new Color(75, 91, 171)},
            {'Y', new Color(255, 228, 120)},
            {'P', new Color(90, 38, 94)},
            {'G', new Color(60, 163, 112)},
            {'O', new Color(255, 181, 112)},
        };

        #endregion

        #region Textures
        
        private static Texture2D _startTexture, _finishTexture, _buttonTexture;

        #endregion

        public static void Initialize(ContentManager content)
        {
            _startTexture = content.Load<Texture2D>("start_sprite");
            _finishTexture = content.Load<Texture2D>("finish_sprite");
            _buttonTexture= content.Load<Texture2D>("button_sprite");
        }

        public static void DrawLevel(this SpriteBatch spriteBatch, GraphicsDevice graphicsDevice, Level level, Rectangle area, int areaScale, int wallThickness, Color wallColor)
        {
            #region Local Vars
            
            int tileOffsetX = area.Width / level.SizeX / 2;
            int tileOffsetY = area.Height / level.SizeY / 2;
            
            Vector2 spriteOrigin = new Vector2(_spriteWidth / 2f);
            float spriteScale = areaScale / 50f * _spriteScale;

            #endregion
            #region Lambda Functions
            
            void DrawIcon(Texture2D texture, int x, int y) => spriteBatch.Draw(texture, new Vector2(x, y), null, Color.White, 0, spriteOrigin, spriteScale, SpriteEffects.None, 0);
            void DrawButton(Color color, int x, int y) => spriteBatch.Draw(_buttonTexture, new Vector2(x, y), null, color, 0, spriteOrigin, spriteScale, SpriteEffects.None, 0);
            void DrawWall(Color color, int length, float angleInDegree, int x, int y) => spriteBatch.DrawLine(graphicsDevice, new Vector2(x, y), length + wallThickness, (float)(angleInDegree * Math.PI / 180f), color, wallThickness, 0, new Vector2(wallThickness / 2f));

            #endregion

            #region Icons
            
            for (int y = 0; y < level.SizeY; y++)
            {
                for (int x = 0; x < level.SizeX; x++)
                {
                    char tile = level.tileList[y * level.SizeX + x];

                    int cx = area.X + tileOffsetX + x * areaScale * 2;
                    int cy = area.Y + tileOffsetY + y * areaScale * 2;

                    if (tile == 'F') DrawIcon(_finishTexture, cx, cy);
                    // else if (tile == 'S') DrawIcon(_startTexture, cx, cy);
                    else if (_colors.ContainsKey(tile)) DrawButton(_colors[tile], cx, cy);
                }
            }

            #endregion

            #region Wall Locals

            int wallHorizontalLength = area.Width / level.SizeX;
            int wallVerticalLength = area.Height / level.SizeY;

            #endregion

            #region Horizontal Walls & Doors

            for (int i = 0; i < level.horizontalEdgeList.Count; i++)
            {
                char tile = level.horizontalEdgeList[i];
                int x = area.X + (i % level.SizeX) * areaScale * 2;
                int y = area.Y + i / level.SizeY * areaScale * 2 + wallHorizontalLength;

                if (tile == 'W')
                    DrawWall(wallColor, wallHorizontalLength, -90, x, y);
                    // spriteBatch.DrawLine(graphicsDevice, new Vector2(x, y), wallHorizontalLength + wallThickness, (float)(-90 * Math.PI / 180f), wallColor, wallThickness, 0, new Vector2(wallThickness / 2f));
                else if (_colors.ContainsKey(tile))
                    DrawWall(_colors[tile], wallHorizontalLength, -90, x, y);
                    // spriteBatch.DrawLine(graphicsDevice, new Vector2(x, y), wallHorizontalLength + wallThickness, (float)(-90 * Math.PI / 180f), _colors[tile], wallThickness, 0, new Vector2(wallThickness / 2f));
            }

            #endregion

            #region Vertical Walls & Doors

            for (int i = 0; i < level.verticalEdgeList.Count; i++)
            {
                char tile = level.verticalEdgeList[i];
                int x = area.X + (i % (level.SizeX - 1)) * areaScale * 2 + wallHorizontalLength;
                int y = area.Y + (i / (level.SizeY - 1)) * areaScale * 2;
                if (tile == 'W')
                    DrawWall(wallColor, wallVerticalLength, 0, x, y);
                    // spriteBatch.DrawLine(graphicsDevice, new Vector2(x, y), wallHorizontalLength + wallThickness, (float)(0 * Math.PI / 180f), wallColor, wallThickness, 0, new Vector2(wallThickness / 2f));
                else if (_colors.ContainsKey(tile))
                    DrawWall(_colors[tile], wallVerticalLength, 0, x, y);
                    // spriteBatch.DrawLine(graphicsDevice, new Vector2(x, y), wallHorizontalLength + wallThickness, (float)(0 * Math.PI / 180f),  _colors[tile], wallThickness, 0, new Vector2(wallThickness / 2f));
            }
            #endregion

            
            // Draw outlined rectangle
            spriteBatch.DrawGrid(graphicsDevice, area, 1, 1, wallColor, wallThickness);
        }
    }
}
