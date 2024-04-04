using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonsterKampfSimulator
{
    internal class Orc : Monster
    {
        public Orc(string name, float health, float attack, float defense, float speed)
        {
            Name = name;
            Hp = health;
            Ap = attack;
            Dp = defense;
            S = speed;
        }
        override public void Attack(Monster enemy)
        {
            base.Attack(enemy);
        }
        override protected void TakeDamage(float damage)
        {
            base.TakeDamage(damage);
        }
    }
}
