using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonsterKampfSimulator
{
    internal abstract class Monster
    {
        public string Name { get; protected set; }
        public float Hp { get; protected set; }
        public float Ap { get; protected set; }
        public float Dp { get; protected set; }
        public float S { get; protected set; }
        public bool IsAlive { get; protected set; } = true;

        public virtual void Attack(Monster enemy)
        {
            enemy.TakeDamage(Ap);
        }

        protected virtual void TakeDamage(float damage)
        {
            if (damage > Dp)
            {
                Hp = Hp - (damage - Dp);
            }
            if (Hp <= 0)
            {
                IsAlive = false;
            }
        }
    }
}
