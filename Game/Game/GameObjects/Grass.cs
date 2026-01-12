using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    public class Grass : GameObject
    {
        public Grass() => Init();

        private void Init()
        {
            Symbol = 'γ';
        }  
    }
}