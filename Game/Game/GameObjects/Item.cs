using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    public abstract class Item : GameObject
    {
        public string Name { get; set; }

        public Inventory Inventory { get; set; }
        public PlayerCharacter Owner { get; set; }

        public abstract void Use(); // 아이템 사용 시 행동
    }
}