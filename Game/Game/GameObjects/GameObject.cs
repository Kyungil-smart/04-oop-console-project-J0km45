using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    public abstract class GameObject
    {
        public char Symbol { get; set; }  // 문자
        public Vector Position { get; set; }  // 위치
    }
}