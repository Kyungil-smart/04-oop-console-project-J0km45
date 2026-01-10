using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    public class Dungeon : GameObject, IInteractable
    {
        public Dungeon() => Init();

        private void Init()
        {
            Symbol = 'D';
        }

        public void Interact(PlayerCharacter player)
        {
            SceneManager.Change("Story");
        }
    }
}
