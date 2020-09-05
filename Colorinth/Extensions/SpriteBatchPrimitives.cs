using System;
using Colorinth.Factories;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Colorinth.Extensions
{
    public static class SpriteBatchPrimitives
    {
        #region Helper Functions

        private static double AngleBetween(Vector2 v1, Vector2 v2) =>
            Math.Acos(Vector2.Dot(Vector2.Normalize(v1), Vector2.Normalize(v2)));

        #endregion

        #region Rectangle
        public static void DrawRect(this SpriteBatch spriteBatch, Vector2 position, int width, int height,
            Texture2D texture, Color color, float rotation, Vector2 origin, Vector2 scale, SpriteEffects spriteEffects, float layerDepth)
            => spriteBatch.Draw(texture, position, new Rectangle(position.ToPoint(), new Point(width, height)), color, rotation, origin, scale, spriteEffects, layerDepth);
        public static void DrawRect(this SpriteBatch spriteBatch, Rectangle rectangle,
            Texture2D texture, Color color, float rotation, Vector2 origin, Vector2 scale, SpriteEffects spriteEffects, float layerDepth)
            => spriteBatch.Draw(texture, new Vector2(rectangle.Left, rectangle.Top), rectangle, color, rotation, origin, scale, spriteEffects, layerDepth);
        
        public static void DrawRect(this SpriteBatch spriteBatch, Vector2 position, int width, int height,
            Texture2D texture, Color color) =>
            DrawRect(spriteBatch, position, width, height, texture, color, 0, Vector2.Zero, Vector2.One, SpriteEffects.None, 0f);
        public static void DrawRect(this SpriteBatch spriteBatch, Rectangle rectangle,
            Texture2D texture, Color color) =>
            DrawRect(spriteBatch, rectangle, texture, color, 0, Vector2.Zero, Vector2.One, SpriteEffects.None, 0f);
        public static void DrawRect(this SpriteBatch spriteBatch, Vector2 position, int width, int height,
            Texture2D texture) =>
            DrawRect(spriteBatch, position, width, height, texture, Color.White, 0, Vector2.Zero, Vector2.One, SpriteEffects.None, 0f);
        public static void DrawRect(this SpriteBatch spriteBatch, Rectangle rectangle, Texture2D texture) =>
            DrawRect(spriteBatch, rectangle, texture, Color.White, 0, Vector2.Zero, Vector2.One, SpriteEffects.None, 0f);
        
        public static void DrawRect(this SpriteBatch spriteBatch, GraphicsDevice graphicsDevice, Vector2 position, int width, int height, Color color)
            => DrawRect(spriteBatch, position, width, height, Texture2DFactory.CreateSolid(graphicsDevice, width, height, color));
        public static void DrawRect(this SpriteBatch spriteBatch, GraphicsDevice graphicsDevice, Rectangle rectangle, Color color)
            => DrawRect(spriteBatch, rectangle, Texture2DFactory.CreateSolid(graphicsDevice, rectangle.Right - rectangle.Left, rectangle.Bottom - rectangle.Top, color));

        #endregion

        #region Line

        public static void DrawLine(this SpriteBatch spriteBatch, GraphicsDevice graphicsDevice, Vector2 start,
            int length, float angle, Color color, int thickness, float layerDepth)
            => DrawRect(spriteBatch, start, thickness, length,
                Texture2DFactory.CreateSolid(graphicsDevice, thickness, length, color), Color.White, angle, new Vector2(thickness / 2f, 0), 
                Vector2.One, SpriteEffects.None, layerDepth);
        public static void DrawLine(this SpriteBatch spriteBatch, GraphicsDevice graphicsDevice, Vector2 start,
            int length, float angle, Color color, int thickness) =>
            DrawLine(spriteBatch, graphicsDevice, start, length, angle, color, thickness, 1f);
        public static void DrawLine(this SpriteBatch spriteBatch, GraphicsDevice graphicsDevice, Vector2 start, Vector2 end, Color color, int thickness, float layerDepth)
            => DrawLine(spriteBatch, graphicsDevice, start, (int)Math.Abs((start - end).Length()), -(float)AngleBetween(end - start, Vector2.UnitX), color, thickness, layerDepth);
        public static void DrawLine(this SpriteBatch spriteBatch, GraphicsDevice graphicsDevice, Vector2 start, Vector2 end, Color color, int thickness)
            => DrawLine(spriteBatch, graphicsDevice, start, end, color, thickness, 1f);

        #endregion
    }
}
