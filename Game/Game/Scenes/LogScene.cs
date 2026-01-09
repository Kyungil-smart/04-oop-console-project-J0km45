using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    public class LogScene : Scene
    {
        public override void Update()
        {
            if (InputManager.GetKey(ConsoleKey.Enter))
            {
                // 이전 씬으로 돌아가기
                SceneManager.ChangePrevScene();
            }
        }

        public override void Render()
        {
            // 로그 출력
            Debug.Render();
        }

        public override void Enter()
        {

        }

        public override void Exit()
        {
            
        }     
    }
}