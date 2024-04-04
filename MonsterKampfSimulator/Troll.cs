using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.Arm;
using System.Text;
using System.Threading.Tasks;

namespace MonsterKampfSimulator
{
    internal class Troll : Monster
    {
        public Troll(string name, float health, float attack, float defense, float speed)
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
