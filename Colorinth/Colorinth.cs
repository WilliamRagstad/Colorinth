using System;
using Colorinth.Extensions;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Colorinth
{
    public class Colorinth : Game
    {
        private readonly GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        #region Properties

        private Vector2 _center => new Vector2(_graphics.PreferredBackBufferWidth / 2f, _graphics.PreferredBackBufferHeight / 2f);


        private int _gameAreaWidth = 100;
        private Rectangle _gameArea => new Rectangle((int)_center.X - _gameAreaWidth, (int)_center.Y - _gameAreaWidth, _gameAreaWidth * 2, _gameAreaWidth * 2);

        #endregion

        #region Colors

        private readonly Color _backgroundColor = new Color(90, 90, 90);
        private readonly Color _gameBackgroundColor = new Color(50, 50, 50);

        #endregion

        public Colorinth()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            _graphics.PreferredBackBufferWidth = 800;
            _graphics.PreferredBackBufferHeight = 800;
            // _graphics.IsFullScreen = true;
            _graphics.ApplyChanges();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            _gameAreaWidth = (int)(Math.Sin(gameTime.TotalGameTime.TotalMilliseconds * 0.005) * 100 + 200);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(_backgroundColor);

            _spriteBatch.Begin();

            _spriteBatch.DrawRect(GraphicsDevice, _gameArea, _gameBackgroundColor);

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
