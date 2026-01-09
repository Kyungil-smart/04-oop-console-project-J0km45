using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    public static class Debug
    {
        public enum LogType
        {
            Normal, // 일반 로그
            Warning // 경고 로그(노란색)
        }

        // 로그 저장 (타입, 출력할 텍스트)
        private static List<(LogType type, string text)> _logList = new List<(LogType type, string text)>();

        public static void Log(string text)
        {
            _logList.Add((LogType.Normal, text));
        }

        public static void LogWarning(string text)
        {
            _logList.Add((LogType.Warning, text));
        }

        public static void Render() // 로그 출력
        {
            foreach ((LogType type, string text) in _logList)
            {
                if (type == LogType.Normal) text.Print();
                else if (type == LogType.Warning) text.Print(ConsoleColor.Yellow);
                Console.WriteLine();
            }
        }
    }
}