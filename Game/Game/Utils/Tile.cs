using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.Design;

namespace Game
{
    public struct Tile
    {
        // 타일 위에 뭐가 올라와있는지?
        public GameObject OnTileObject { get; set; }
        // 타일 위에 올라서면 발생해야 하는 이벤트
        public event Action OnStepPlayer;
        // 좌표
        public Vector Position { get; set; }
        // 오브젝트 존재하는지 확인
        public bool HasGameObject => OnTileObject != null;
        // 잔디 확인
        public bool HasGrass { get; set; }

        public Tile(Vector position) : this()
        {
            Position = position;
        }

        public void Print()
        {
            // 게임 오브젝트가 올라와있다면 해당 Symbol 출력
            if (HasGameObject)
            {
                switch(OnTileObject.Symbol)
                {
                    case 'P':  // Player
                        OnTileObject.Symbol.Print(ConsoleColor.White);
                        break;
                    case 'γ':  // Grass
                        OnTileObject.Symbol.Print(ConsoleColor.DarkGreen);
                        break;
                    case '#':  // Wall
                        OnTileObject.Symbol.Print(ConsoleColor.DarkYellow);
                        break;
                    case '♥':  // Potion
                        OnTileObject.Symbol.Print(ConsoleColor.Magenta);
                        break;
                    case '◎':  // Gold
                        OnTileObject.Symbol.Print(ConsoleColor.Yellow);
                        break;
                    case '▥':  // Dungeon
                        OnTileObject.Symbol.Print(ConsoleColor.Red);
                        break;
                    default:
                        OnTileObject.Symbol.Print();
                        break;
                }
            }
            // 아니면 빈칸 ' ' 출력
            else
            {
                '·'.Print();
            }
        }
    }
}