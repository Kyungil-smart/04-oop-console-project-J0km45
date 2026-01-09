using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    public class GameManager
    {
        public static bool IsGameOver { get; set; }
        public const string GameName = "RPG";
        private PlayerCharacter _player;

        public void Run()
        {
            Init();

            // 게임 루틴 가동
            while (!IsGameOver)
            {
                // 렌더링
                Console.Clear();  // 출력화면 초기화
                SceneManager.Render();
                // 키입력 받기
                InputManager.GetUserInput();

                if (InputManager.GetKey(ConsoleKey.L))
                {
                    SceneManager.Change("Log");
                }

                // 데이터 처리
                SceneManager.Update();
            }
        }
        
        // 게임에 필요한 데이터 초기화
        private void Init()
        {
            IsGameOver = false;
            // 입력키 초기화
            SceneManager.OnChangeScene += InputManager.ResetKey;
            _player = new PlayerCharacter();

            SceneManager.AddScene("Title", new TitleScene());
            SceneManager.AddScene("Story", new StoryScene());
            SceneManager.AddScene("Town", new TownScene(_player));
            SceneManager.AddScene("Log", new LogScene());

            // 타이틀 씬부터 시작
            SceneManager.Change("Title");

            Debug.Log("게임 데이터 초기화 완료");
        }
    }
}