using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    public class CreditScene : Scene
    {
        public override void Enter()
        {
            
        }

        public override void Update()
        {
            if (InputManager.GetKey(ConsoleKey.Enter))
            {
                SceneManager.Change("Title");
            }
        }

        public override void Render()
        {
            Console.SetCursorPosition(8, 3);
            Console.WriteLine("개발자: 조경민");
        }

        public override void Exit()
        {
            
        }     
    }
}