using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    public class StoryScene : Scene
    {
        public override void Enter()
        {
            
        }

        public override void Update()
        {
            if (InputManager.GetKey(ConsoleKey.Enter))
            {
                SceneManager.Change("Town");
            }
        }

        public override void Render()
        {
            Console.SetCursorPosition(10, 0);
            "Story Scene".Print();
        }

        public override void Exit()
        {
            
        }
    }
}