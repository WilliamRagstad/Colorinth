using System;
using System.Drawing;
using Colorinth.Extensions;
using Colorinth.Generators;
using Colorinth.Managers;
using Colorinth.Model;
using Colorinth.Updater;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
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
        private int _gameAreaScale;
        private int _wallThickness => _gameAreaScale / 8; // = 5;
        private Rectangle _gameArea => new Rectangle((int)_center.X - _currentLevel.SizeX * _gameAreaScale, (int)_center.Y - _currentLevel.SizeY * _gameAreaScale, _currentLevel.SizeX * _gameAreaScale * 2, _currentLevel.SizeY * _gameAreaScale * 2);

        private int _levelNr = 0;
        private Level _currentLevel;
        private Player _player;

        private Song _themeSong;
        private SoundEffect _startSoundEffect, _finishSoundEffect;

        private SpriteFont _defaultFont;

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
            
            _gameAreaScale = CalculateGameScale(_currentLevel, 50, 3/5f);
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            _graphics.PreferredBackBufferWidth = _gameWindowedSize.Width;
            _graphics.PreferredBackBufferHeight = _gameWindowedSize.Height;
            _graphics.IsFullScreen = _isFullscreen;
            _graphics.ApplyChanges();

            _defaultFont = Content.Load<SpriteFont>("default_font");

            Player.Initialize(Content);
            LevelDrawer.Initialize(Content);
            PlayerDrawer.Initialize(Content);
            base.Initialize();

            // Generate Level
            _currentLevel = LevelGenerator.GenerateLevel(6, 11, 11, 0.5, false);
            _player = new Player(_currentLevel.StartX, _currentLevel.StartY);
            _gameAreaScale = CalculateGameScale(_currentLevel, 50, 3/5f);

        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _themeSong = Content.Load<Song>("theme_song");
            _startSoundEffect = Content.Load<SoundEffect>("start_effect");
            _finishSoundEffect = Content.Load<SoundEffect>("finish_effect");
            Start();
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

        /// <summary>
        /// Graph of function: https://www.desmos.com/calculator/9xpkzghvpl
        /// </summary>
        /// <param name="level">level data</param>
        /// <param name="baseScale">Original Scale factor</param>
        /// <param name="fraction">The fraction of the screen to take up</param>
        /// <returns>scale</returns>
        private int CalculateGameScale(Level level, int baseScale, float fraction)
        {
            /*
            int x = Math.Max(level.SizeY, level.SizeX);
            return (int) ((2.2f * Math.Exp( -x / 3.3f ) + .4f)  *  baseScale);
            */

            int x = Math.Max(level.SizeY, level.SizeX);
            int y = Math.Min(_graphics.PreferredBackBufferWidth, _graphics.PreferredBackBufferHeight);
            return (int)( fraction * y / x / 2 );
        }

        #endregion

        private void Start()
        {
            AudioManager.PlayLoop(_themeSong);
        }

        private TimeSpan _previousMove;
        private TimeSpan _previousPress;
        protected override void Update(GameTime gameTime)
        {
            KeyboardState kb = Keyboard.GetState();
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || kb.IsKeyDown(Keys.Escape))
                Exit();

            if (kb.IsKeyDown(Keys.F11)) ToggleFullscreen();

            if (gameTime.TotalGameTime.Subtract(_previousMove).TotalMilliseconds > 200 &&
                (kb.IsKeyDown(Keys.Left) || kb.IsKeyDown(Keys.Right) || kb.IsKeyDown(Keys.Up) || kb.IsKeyDown(Keys.Down)))
            {
                _player.Move(kb, _currentLevel);
                _previousMove = gameTime.TotalGameTime;
            }

            if (kb.IsKeyDown(Keys.Space) && gameTime.TotalGameTime.Subtract(_previousPress).TotalMilliseconds > 200)
            {
                LevelUpdater.ButtonUpdateLevel(_player, _currentLevel);
                _previousPress = gameTime.TotalGameTime;
            }
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(_backgroundColor);

            _spriteBatch.Begin();

            // Draw the background game area
            _spriteBatch.DrawRect(GraphicsDevice, _gameArea, _gameBackgroundColor);
            _spriteBatch.DrawGrid(GraphicsDevice, _gameArea, _currentLevel.SizeX, _currentLevel.SizeY, _gridColor, _wallThickness);
            // Draw the level
            _spriteBatch.DrawLevel(GraphicsDevice, _currentLevel, _gameArea, _gameAreaScale, _wallThickness * 2, Color.White);
            // Draw player
            _spriteBatch.DrawPlayer(GraphicsDevice, _currentLevel, _player, _gameArea, _gameAreaScale);

            // Draw UI
            _spriteBatch.DrawString(_defaultFont, "Level: " + _levelNr, new Vector2(20), Color.White);

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
