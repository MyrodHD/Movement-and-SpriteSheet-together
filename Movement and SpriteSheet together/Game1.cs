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

        BattleSystem _battleSystem;
        private bool _battleStarted = false;

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

            _battleSystem = new BattleSystem();
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

                    if (Keyboard.GetState().IsKeyDown(Keys.B) && !_battleStarted)
                    {
                        var hero = new Hero("Hero", 30, 6);
                        var enemy = new Enemy("Goblin", 18, 3);
                        _battleSystem.BattleStart(hero, enemy);
                        _battleStarted = true;
                        _currentState = GameState.Battle;
                    }

                    break;
                
                case GameState.Controls:
                    if (Keyboard.GetState().IsKeyDown(Keys.R))
                        _currentState = GameState.MainMenu;
                    break;
                
                case GameState.Battle:
                    _battleSystem.Update(gameTime);

                    if (Keyboard.GetState().IsKeyDown(Keys.Space))
                    {
                        _battleSystem.HeroAttack();
                    }

                    if ((_battleSystem.State == BattleState.Win || _battleSystem.State == BattleState.Lose && Keyboard.GetState().IsKeyDown(Keys.R)))
                    {
                        _currentState = GameState.Playing;
                        _battleStarted = false;
                    }
                        
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
                var hero = _battleSystem.Hero;
                var enemy = _battleSystem.Enemy;

                _spriteBatch.DrawString(_battleFont, $"Player: {hero.Name}  HP: {hero.HP}/{hero.MaxHP}", new Vector2(50, 50), Color.White);
                _spriteBatch.DrawString(_battleFont, $"Enemy: {enemy.Name}  HP: {enemy.HP}", new Vector2(50, 90), Color.White);
                _spriteBatch.DrawString(_battleFont, $"State: {_battleSystem.State}", new Vector2(50, 140), Color.Yellow);
                _spriteBatch.DrawString(_battleFont, $"Action: {_battleSystem.LastAction}", new Vector2(50, 180), Color.White);
                _spriteBatch.DrawString(_battleFont, "Press SPACE to attack (PlayerTurn). Press R after Win/Lose to return.", new Vector2(50, 220), Color.LightGray);
            }
        

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
