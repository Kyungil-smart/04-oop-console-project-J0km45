using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    public class TitleScene : Scene
    {
        private MenuList _titleMenu;

        public TitleScene() => Init();

        public void Init()
        {
            _titleMenu = new MenuList();
            _titleMenu.Add("게임 시작", GameStart);
            _titleMenu.Add("크레딧", ViewCredits);
            _titleMenu.Add("게임 종료", GameQuit);
        }

        public override void Enter()
        {
            // 특정 씬에서 타이틀 씬으로 돌아올 때 _currentIndex 초기화
            _titleMenu.Reset();
            Debug.Log("타이틀 씬 진입");
        }

        public override void Update()
        {
            if (InputManager.GetKey(ConsoleKey.UpArrow))
            {
                _titleMenu.SelectUp();
            }
            
            if (InputManager.GetKey(ConsoleKey.DownArrow))
            {
                _titleMenu.SelectDown();
            }

            if (InputManager.GetKey(ConsoleKey.Enter))
            {
                _titleMenu.Select();
            }
        }

        public override void Render()
        {
            Console.SetCursorPosition(5, 1);
            GameManager.GameName.Print(ConsoleColor.Yellow);

            _titleMenu.Render(8, 5);
        }

        public override void Exit()
        {

        }

        public void GameQuit()
        {
            GameManager.IsGameOver = true; // 게임 종료
        }

        public void GameStart()
        {
            SceneManager.Change("Town");
        }

        public void ViewCredits()
        {
            // 크레딧 메소드
        }
    }
}