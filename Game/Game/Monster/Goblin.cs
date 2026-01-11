using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    public class Goblin : Monster
    {
        public Goblin() : base("고블린", 12, 4)
        {
           
        }

        public override void Skill()
        {
            Console.WriteLine($"{Name}이 단검으로 찔렀습니다. ({SkillDamage} Damage)");
        }
    }
}