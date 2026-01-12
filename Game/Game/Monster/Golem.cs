using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    public class Golem : Monster
    {
        public Golem() : base("골렘", 15, 5)
        {
        }

        public override void Skill()
        {
            Console.WriteLine($"{Name}이 핵주먹으로 내려찍었습니다. ({SkillDamage} Damage)");
        }
    }
}