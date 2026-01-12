using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    public class Potion : Item, IInteractable
    {
        public Potion() => Init();

        private void Init()
        {
            Symbol = '♥';
        }

        public override void Use()
        {
            Owner.Heal(1); // 체력 회복

            // Inventory에서 포션 제거
            Inventory.Remove(this);
            // 참조 끊기
            Inventory = null;
            Owner = null;
        }

        public void Interact(PlayerCharacter player)
        {
            // player의 인벤토리에 포션 추가
            player.AddItem(this);
        }
    }
}