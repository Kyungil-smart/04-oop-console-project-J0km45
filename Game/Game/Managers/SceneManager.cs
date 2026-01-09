using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    public static class SceneManager
    {
        // Action -> 제네릭 델리게이트
        public static Action OnChangeScene;
        // 현재 씬
        public static Scene Current { get; private set; }
        // 이전 씬
        private static Scene _prev;

        // 어떤 씬이 있는지
        private static Dictionary<string, Scene> _scenes = new Dictionary<string, Scene>();

        // 씬 추가
        public static void AddScene(string key, Scene state)
        {
            // 이미 추가된 상태면 return
            if (_scenes.ContainsKey(key)) return;

            _scenes.Add(key, state);

        }

        public static void ChangePrevScene()
        {
            // 이전 씬으로 돌아가기
            Change(_prev);
        }

        // 씬 바꾸는 기능
        public static void Change(string key)
        {
            // 추가되어 있지 않으면 return
            if (!_scenes.ContainsKey(key)) return;

            Change(_scenes[key]);
        }

        // 씬 전환
        public static void Change(Scene scene)
        {
            Scene next = scene;

            // 기존 현재 상태와 같으면 return
            if (Current == next) return;

            // 이전 씬에서 Exit 호출
            Current?.Exit(); // 현재 상태 null아니면 Exit 호출
            // 새로운 상태에서 Enter 호출
            next.Enter();

            // 이전 씬 저장
            _prev = Current;
            // 현재 상태 변경
            Current = next;
            // InputManager.ResetKey 실행
            OnChangeScene?.Invoke();
        }

        public static void Update()
        {
            // 현재 씬이 null이 아니면 해당 씬의 Update호출
            Current?.Update();
        }

        public static void Render()
        {
            Current.Render();
        }
    }
}