using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movement_and_SpriteSheet_together
{
    public enum ActionType
    {
        Attack,
        Magic,
        Item,
        Guard,
        Flee
    }

    public class BattleAction
    {
        public string Name { get; set; }
        public ActionType Type { get; set; }
        public Combatant Target { get; set; }
        public int MpCost { get; set; }

        public void Execute(Combatant user)
        {
            switch (Type)
            {
                case ActionType.Attack:
                    Target.TakeDamage(user._attack);
                    break;

                case ActionType.Guard:
                    user._defense += 5;
                    break;
            
                case ActionType.Flee:
                    // Handled in battle logic, not here
                    break;
            }

        }

    }
}
