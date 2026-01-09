using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    public static class InputManager
    {
        private static ConsoleKey _current; // 현재 입력키

        private static readonly ConsoleKey[] _keys = // 유효키 목록
        {
            ConsoleKey.UpArrow,
            ConsoleKey.DownArrow,
            ConsoleKey.LeftArrow,
            ConsoleKey.RightArrow,
            ConsoleKey.Enter,
            ConsoleKey.I,
            ConsoleKey.L,
            ConsoleKey.T
        };

        // 현재 입력키 확인용
        public static bool GetKey(ConsoleKey input)
        {
            return _current == input;
        }

        // GameManager에서만 호출
        public static void GetUserInput()
        {
            ConsoleKey input = Console.ReadKey(true).Key;
            _current = ConsoleKey.Clear;

            foreach (ConsoleKey key in _keys)
            {
                if (key == input)
                {
                    _current = key;
                    break;
                }
            }
        }

        // 입력키 초기화
        public static void ResetKey()
        {
            _current = ConsoleKey.Clear;
        }
    }
}