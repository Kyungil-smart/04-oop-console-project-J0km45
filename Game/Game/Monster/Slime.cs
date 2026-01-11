using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    public class Slime : Monster
    {
        public Slime() : base("슬라임", 10, 3)
        {

        }

        public override void Skill()
        {
            Console.WriteLine($"{Name}이 독성 점액을 발사했습니다. ({SkillDamage} Damage)");
        }
    }
}