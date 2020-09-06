using System;
using System.Drawing;
using Colorinth.Extensions;
using Colorinth.Generators;
using Colorinth.Model;
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

        private bool _isFullscreen = false;
        private bool _fullscreenChanged = false;

        #endregion

        #region Properties

        private Vector2 _center => new Vector2(_graphics.PreferredBackBufferWidth / 2f, _graphics.PreferredBackBufferHeight / 2f);

        private Size _gameWindowedSize = new Size(1200, 800);
        private int _gameAreaScale = 50;
        private Rectangle _gameArea => new Rectangle((int)_center.X - _currentLevel.SizeX * _gameAreaScale, (int)_center.Y - _currentLevel.SizeY * _gameAreaScale, _currentLevel.SizeX * _gameAreaScale * 2, _currentLevel.SizeY * _gameAreaScale * 2);

        private Level _currentLevel;
        private Player _player;

        #endregion

        #region Colors

        private readonly Color _backgroundColor = new Color(90, 90, 90);
        private readonly Color _gameBackgroundColor = new Color(50, 50, 50);
        private readonly Color _gridColor = new Color(70, 70, 70);

        #endregion

        public Colorinth()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            Window.AllowUserResizing = true;
            Window.ClientSizeChanged += OnResize;
        }

        private void OnResize(object sender, EventArgs e)
        {
            if (!_isFullscreen && !_fullscreenChanged)
            {
                _graphics.PreferredBackBufferWidth = _graphics.GraphicsDevice.Viewport.Width;
                _graphics.PreferredBackBufferHeight = _graphics.GraphicsDevice.Viewport.Height;
                _graphics.ApplyChanges();
            }

            _fullscreenChanged = false;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            _graphics.PreferredBackBufferWidth = _gameWindowedSize.Width;
            _graphics.PreferredBackBufferHeight = _gameWindowedSize.Height;
            _graphics.IsFullScreen = _isFullscreen;
            _graphics.ApplyChanges();
            
            LevelDrawer.Initialize(Content);
            PlayerDrawer.Initialize(Content);
            base.Initialize();

            // Generate Level
            _currentLevel = LevelGenerator.GenerateLevel(6, 2, 2, 0.44, false);
            _player = new Player(0, 0);
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
        }

        #region Game Logic

        #region Window Handling

        private void ToggleFullscreen()
        {
            if (_isFullscreen)
            {
                _graphics.PreferredBackBufferWidth = _gameWindowedSize.Width;
                _graphics.PreferredBackBufferHeight = _gameWindowedSize.Height;
            }
            else
            {
                _graphics.PreferredBackBufferWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
                _graphics.PreferredBackBufferHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
            }

            _fullscreenChanged = true;
            _isFullscreen = !_isFullscreen;
            _graphics.IsFullScreen = _isFullscreen;
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
            
            if (kb.IsKeyDown(Keys.Left) && _player.X > 0) _player.X--;
            if (kb.IsKeyDown(Keys.Right) && _player.X < _currentLevel.SizeX - 1) _player.X++;
            if (kb.IsKeyDown(Keys.Up) && _player.Y > 0) _player.Y--;
            if (kb.IsKeyDown(Keys.Down) && _player.Y < _currentLevel.SizeY - 1) _player.Y++;

                base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(_backgroundColor);

            _spriteBatch.Begin();

            // Draw the background game area
            _spriteBatch.DrawRect(GraphicsDevice, _gameArea, _gameBackgroundColor);
            _spriteBatch.DrawGrid(GraphicsDevice, _gameArea, _currentLevel.SizeX, _currentLevel.SizeY, _gridColor, 5);
            // Draw the level
            _spriteBatch.DrawLevel(GraphicsDevice, _currentLevel, _gameArea, _gameAreaScale);
            // Draw player
            _spriteBatch.DrawPlayer(GraphicsDevice, _currentLevel, _player, _gameArea, _gameAreaScale);

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
