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
        public const string GameName = "Dungeon Life";
        private PlayerCharacter _player;

        public void Run()
        {
            Init();

            // 게임 루틴 가동
            while (!IsGameOver)
            {
                // 출력화면 초기화
                Console.Clear();  
                // 렌더링
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
            SceneManager.AddScene("Credit", new CreditScene());
            SceneManager.AddScene("Town", new TownScene(_player));
            SceneManager.AddScene("Story", new StoryScene(_player));
            SceneManager.AddScene("GameOver", new GameOverScene(_player, this));
            SceneManager.AddScene("Log", new LogScene());

            // 타이틀 씬부터 시작
            SceneManager.Change("Title");

            Debug.Log("게임 데이터 초기화 완료");
        }

        // 게임 재시작
        public void RestartGame()
        {
            _player = new PlayerCharacter();

            SceneManager.ResetScene("Town", new TownScene(_player));
            SceneManager.ResetScene("Story", new StoryScene(_player));
            SceneManager.ResetScene("GameOver", new GameOverScene(_player, this));

            SceneManager.Change("Title");
        }
    }
}