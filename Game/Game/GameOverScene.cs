using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    public class GameOverScene : Scene
    {
        private PlayerCharacter _player;

        public GameOverScene(PlayerCharacter player) => Init(player);

        public void Init(PlayerCharacter player)
        {
            _player = player;
        }
        public override void Enter()
        {
            
        }

        public override void Update()
        {
            if (InputManager.GetKey(ConsoleKey.Enter))
            {
                GameManager.IsGameOver = true;
            }
        }

        public override void Render()
        {
            Console.SetCursorPosition(10, 3);
            Console.WriteLine("GAME OVER");
            Console.SetCursorPosition(8, 6);
            $"획득 골드: {_player.Gold.Value}G".Print(ConsoleColor.Yellow);

        }

        public override void Exit()
        {
            
        }  
    }
}
