using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Colorinth.Factories
{
    public static class Texture2DFactory
    {
        public static Texture2D CreateSolid(GraphicsDevice graphicsDevice, int width, int height, Color color)
        {
            if (width == 0 || height == 0) return new Texture2D(graphicsDevice, 1, 1);
            if (width < 0) width = -width;
            if (height < 0) height = -height;

            Color[] data = new Color[width * height];
            Texture2D rectTexture = new Texture2D(graphicsDevice, width, height);
            for (int i = 0; i < data.Length; ++i) data[i] = color;
            rectTexture.SetData(data);
            return rectTexture;
        }

        public static Texture2D CreateSolid(GraphicsDevice graphicsDevice, Rectangle rectangle, Color color) =>
            CreateSolid(graphicsDevice, rectangle.Right - rectangle.Left, rectangle.Bottom - rectangle.Top, color);
    }
}
