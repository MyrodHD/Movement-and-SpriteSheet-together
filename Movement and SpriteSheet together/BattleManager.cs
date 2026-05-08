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


    }
}
