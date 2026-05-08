using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace Movement_and_SpriteSheet_together
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        public enum GameState
        {
            MainMenu,
            Playing,
            Controls,
            Battle
        }

        GameState _currentState = GameState.MainMenu;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        MenuManager _menuManager;
        SpriteManager _playerSprite;
        MovementManager _movement;
        ParticleSystem _particleSystem;

        Texture2D playerTexture;
        Texture2D rectangleTexure;
        Texture2D particleTexure;

        SpriteFont _font;
        SpriteFont _battleFont;

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            _currentState = GameState.MainMenu;

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            playerTexture = Content.Load<Texture2D>("player_hat_spritesheet");
            rectangleTexure = Content.Load<Texture2D>("rectangle");
            particleTexure = Content.Load<Texture2D>("circle");

            _font = Content.Load<SpriteFont>("TitleFont");
            _battleFont = Content.Load<SpriteFont>("BattleFont");

            List<string> menuItems = new List<string> { "Start Game", "Controls" };
            
            _menuManager = new MenuManager(_font, menuItems);

            _playerSprite = new SpriteManager(playerTexture, 4, 4);
            
            _particleSystem = new ParticleSystem(particleTexure);

            _movement = new MovementManager(new Vector2(100,100), _particleSystem);
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            switch (_currentState)
            {
                case GameState.MainMenu:
                    _menuManager.Update(gameTime, ref _currentState);

                    break;
      
                case GameState.Playing:
                    _movement.Update(gameTime);
                    _particleSystem.Update(gameTime);

                    if (_movement.currentDirection != Vector2.Zero)
                    {
                        _playerSprite.Update(gameTime);

                        if (_movement.currentDirection.Y > 0)
                            _playerSprite.currentRow = 0;
                        else if (_movement.currentDirection.X < 0)
                            _playerSprite.currentRow = 1;
                        else if (_movement.currentDirection.X > 0)
                            _playerSprite.currentRow = 2;
                        else if (_movement.currentDirection.Y < 0)
                            _playerSprite.currentRow = 3;
                    }

                    else
                        _playerSprite.Reset();

                    if (Keyboard.GetState().IsKeyDown(Keys.B))
                        _currentState = GameState.Battle;

                    break;
                
                case GameState.Controls:
                    if (Keyboard.GetState().IsKeyDown(Keys.R))
                        _currentState = GameState.MainMenu;

                    break;
                
                case GameState.Battle:

                    break;
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here

            _spriteBatch.Begin();

            if (_currentState == GameState.MainMenu)
            {
                _menuManager.Draw(_spriteBatch);
            }

            if (_currentState == GameState.Playing)
            {
                _playerSprite.Draw(_spriteBatch, _movement.position);
                _particleSystem.Draw(_spriteBatch);
            }

            if (_currentState == GameState.Battle)
            {

            }

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
