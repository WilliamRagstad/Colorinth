using System;
using System.Drawing;
using Colorinth.Extensions;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Color = Microsoft.Xna.Framework.Color;
using Rectangle = Microsoft.Xna.Framework.Rectangle;

namespace Colorinth
{
    public class Colorinth : Game
    {
        private readonly GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        #region Globals

        private bool isFullscreen = false;

        #endregion

        #region Properties

        private Vector2 _center => new Vector2(_graphics.PreferredBackBufferWidth / 2f, _graphics.PreferredBackBufferHeight / 2f);

        private Size _gameWindowedSize = new Size(800, 800);
        private int _gameAreaWidth = 200;
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
            _graphics.PreferredBackBufferWidth = _gameWindowedSize.Width;
            _graphics.PreferredBackBufferHeight = _gameWindowedSize.Height;
            _graphics.IsFullScreen = isFullscreen;
            _graphics.ApplyChanges();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
        }

        #region Game Logic

        #region Window Handling

        private void ToggleFullscreen()
        {
            if (isFullscreen)
            {
                _graphics.PreferredBackBufferWidth = _gameWindowedSize.Width;
                _graphics.PreferredBackBufferHeight = _gameWindowedSize.Height;
            }
            else
            {
                _graphics.PreferredBackBufferWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
                _graphics.PreferredBackBufferHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
            }
                
            isFullscreen = !isFullscreen;
            _graphics.IsFullScreen = isFullscreen;
            _graphics.ApplyChanges();
        }

        #endregion

        #endregion

        protected override void Update(GameTime gameTime)
        {
            KeyboardState kb = Keyboard.GetState();
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || kb.IsKeyDown(Keys.Escape))
                Exit();

            if (kb.IsKeyDown(Keys.F11)) ToggleFullscreen();

            // _gameAreaWidth = (int)(Math.Sin(gameTime.TotalGameTime.TotalMilliseconds * 0.005) * 100 + 200); // Good for testing drawing functions


            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(_backgroundColor);

            _spriteBatch.Begin();

            _spriteBatch.DrawRect(GraphicsDevice, _gameArea, _gameBackgroundColor);
            _spriteBatch.DrawLine(GraphicsDevice, new Vector2(_gameArea.Left, _gameArea.Top), new Vector2(_gameArea.Bottom, _gameArea.Left), Color.White, 5);
            //_spriteBatch.DrawLine(GraphicsDevice, new Vector2(_gameArea.Left, _gameArea.Top), 400, (float)(0 * (Math.PI / 180)), Color.White, 5);

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
