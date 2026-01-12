using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    public class Gold : GameObject, IInteractable
    {
        public int Amount { get; set; } = 1;

        public Gold() => Init();

        private void Init()
        {
            Symbol = '◎';
        }

        public void Interact(PlayerCharacter player)
        {
            player.AddGold(Amount);
        }
    }
}