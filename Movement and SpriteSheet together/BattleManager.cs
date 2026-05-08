using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movement_and_SpriteSheet_together
{
    public enum BattleState
    {
        Start, //Fade in, show enemy
        PlayerTurn, // waiting for playing input
        EnemyTurn, // enemy picks and executes action
        ResolvingAction, // animate + apply chosen action
        Victory, // all enemies died
        Defeat, // all heroes died
        Flee // player escaped
    }

    public class BattleManager
    {
        public BattleState state { get; private set; } = BattleState.Start;

        private List<Combatant.Hero> _heros;
        private List<Combatant.Enemy> _enemies;

        // Turn Queue
        private Queue<Combatant> _turnQueue = new();

        private Combatant _currentActor;
        private BattleAction _pendingAction;

        private float _stateTimer = 0f; // for timed transitions
        private const float start_delay = 1.5f;

        public List<string> BattleLog { get; } = new(); // Feed to UI

        public BattleManager(List<Combatant.Hero> heroes, List<Combatant.Enemy> enemies)
        {
            _heros = heroes;
            _enemies = enemies;
        }

        public void Update(GameTime gameTime)
        {
            float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;

            switch (state)
            {
                case BattleState.Start:

                    break;

                case BattleState.PlayerTurn:
                    // Input is handled externally - call SubmitPlayerAction()
                    break;

                case BattleState.EnemyTurn:
                    //HandleEnemyTurn();
                    break;
            };
        
        }

        public void HandleStart(float dt)
        {
            _stateTimer += dt;
            if (_stateTimer >= start_delay)
            {
                _stateTimer = 0f;
                //BuildTurnQueue();
                //AdvanceTurn();
            }
        }
    
        public void HandleEnemyTurn()
        {

        }
    }
}
