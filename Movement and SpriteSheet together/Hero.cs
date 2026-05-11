using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movement_and_SpriteSheet_together
{
    public class Hero
    {
        private string _name;
        private int _HP;
        private int _maxHP;
        private int _attackPower;

        public string Name => _name;
        public int HP => _HP;
        public int MaxHP => _maxHP;
        public int AttackPower => _attackPower;

        public Hero(string name, int HP, int attackPower)
        {
            _name = name;
            _HP = HP;
            _maxHP = HP;
            _maxHP = HP;
            _attackPower = attackPower;
        }

        public void TakeDamage(int dmg)
        {
            _HP -= dmg;
            if (_HP < 0)
                _HP = 0;
        }

        public void Heal(int amount)
        {
            _HP += amount;
            if (_HP > _maxHP)
                _HP = _maxHP;
        }

        public bool IsDead()
        {
            return _HP <= 0;
        }
    }
}
