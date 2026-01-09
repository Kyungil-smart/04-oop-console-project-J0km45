using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    // 상속을 받을 목적으로 쓰이는 클래스
    public abstract class Scene
    {
        public abstract void Enter();
        public abstract void Update();
        public abstract void Render();
        public abstract void Exit();
    }
}