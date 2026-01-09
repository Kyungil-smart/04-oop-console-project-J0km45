using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    public interface IInteractable
    {
        // player와 상호작용 시 실행
        void Interact(PlayerCharacter player);
    }
}