using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movement_and_SpriteSheet_together
{
    public class Combatant
    {
        public string _name { get; set; }
        public int _hp { get; set; }
        public int _maxHp { get; set; }
        public int _mp { get; set; }
        public int _maxMp { get; set; }
        public int _attack { get; set; }
        public int _defense { get; set; }
        public int _speed { get; set; } // Determines turn order in battle

        public bool IsAlive => _hp > 0;

        public void TakeDamage(int amount)
        {
            int dmg = Math.Max(1, amount - _defense);
            _hp = Math.Max(0, _hp - dmg);
        }

        public void Heal(int amount)
        {
            _hp = Math.Min(_maxHp, _hp + amount);
        }

        public class Hero : Combatant
        {
            public List<BattleAction> Actions { get; set; }
        }

        public class Enemy : Combatant
        {
            public BattleAction ChooseAction(List<Combatant> targets)
            {
                return new BattleAction
                {
                    Name = "Attack",
                    Type = ActionType.Attack,
                    Target = targets[Random.Shared.Next(targets.Count)]
                };
            }
        }
    }
}
