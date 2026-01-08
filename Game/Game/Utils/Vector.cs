using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    public struct Vector
    {
        public int X { get; set; }
        public int Y { get; set; }

        public Vector(int x, int y)
        {
            X = x; 
            Y = y;
        }

        // 방향
        public static Vector Up => new Vector(0, -1);
        public static Vector Down => new Vector(0, 1);
        public static Vector Left => new Vector(-1, 0);
        public static Vector Right => new Vector(1, 0);

        // 현재 좌표 + 이동 방향 = 다음 좌표
        public static Vector operator +(Vector a, Vector b)  // operator - 연산자 재정의
            => new Vector(a.X + b.X, a.Y + b.Y);
    }
}
