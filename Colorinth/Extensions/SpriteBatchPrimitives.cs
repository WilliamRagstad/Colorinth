using System;
using System.Runtime.InteropServices;
using Colorinth.Factories;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Colorinth.Extensions
{
    public static class SpriteBatchPrimitives
    {
        #region Helper Functions

        private static double AngleBetween(Vector2 v1, Vector2 v2)
        {
            Vector2 vn1 = Vector2.Normalize(v1);
            Vector2 vn2 = Vector2.Normalize(v2);

            return Math.Acos(Vector2.Dot(vn1, vn2));
        }

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
        [Obsolete("This method is not accurate unless the window is of square size. Ie both sides of the window is equal")]
        public static void DrawLine(this SpriteBatch spriteBatch, GraphicsDevice graphicsDevice, Vector2 start, Vector2 end, Color color, int thickness, float layerDepth)
            => DrawLine(spriteBatch, graphicsDevice, start, (int)Math.Abs((end - start).Length()), (float)AngleBetween(end - start, Vector2.UnitX), color, thickness, layerDepth);
        [Obsolete("This method is not accurate unless the window is of square size. Ie both sides of the window is equal")]
        public static void DrawLine(this SpriteBatch spriteBatch, GraphicsDevice graphicsDevice, Vector2 start, Vector2 end, Color color, int thickness)
            => DrawLine(spriteBatch, graphicsDevice, start, end, color, thickness, 1f);

        #endregion

        #region Grid

        public static void DrawGrid(this SpriteBatch spriteBatch, GraphicsDevice graphicsDevice, Vector2 position, int size, int width, int height, Color color)
        {
            for (int i = 0; i < size + 1; i++)
            {
                int j = i * width / size;
                int k = i * height / size;
                DrawLine(spriteBatch, graphicsDevice, position + new Vector2(j, 0), height, (float)(0 * Math.PI / 180), color, 5);
                DrawLine(spriteBatch, graphicsDevice, position + new Vector2(0, k), width, (float)(-90 * Math.PI / 180), color, 5);
            }
        }

        #endregion
    }
}
