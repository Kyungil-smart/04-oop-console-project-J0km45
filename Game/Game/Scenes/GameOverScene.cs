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
        private GameManager _gm;

        public GameOverScene(PlayerCharacter player, GameManager gm) => Init(player, gm);

        public void Init(PlayerCharacter player, GameManager gm)
        {
            _player = player;
            _gm = gm;
        }

        public override void Enter()
        {
            
        }

        public override void Update()
        {
            if (InputManager.GetKey(ConsoleKey.Enter))
            {
                _gm.RestartGame(); // 재시작
            }
        }

        public override void Render()
        {
            Console.SetCursorPosition(10, 3);
            Console.WriteLine("GAME OVER");
            Console.SetCursorPosition(8, 6);
            $"획득 골드: ".Print();
            $"{_player.Gold.Value}G".Print(ConsoleColor.Yellow);
        }

        public override void Exit()
        {
            
        }  
    }
}