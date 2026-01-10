using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    public class TownScene : Scene
    {
        private Tile[,] _field = new Tile[25, 60];
        private PlayerCharacter _player;
        private bool _isSetItem;
        // 카메라 너비, 높이
        private int camWidth = 45;
        private int camHeight = 15;

        public TownScene(PlayerCharacter player) => Init(player);

        public void Init(PlayerCharacter player)
        {
            _player = player;

            for (int y = 0; y < _field.GetLength(0); y++)
            {
                for (int x = 0; x < _field.GetLength(1); x++)
                {
                    Vector pos = new Vector(x, y);
                    _field[y, x] = new Tile(pos);
                }
            }
        }

        public override void Enter()
        {
            // 플레이어에게 맵 연결
            _player.Field = _field;
            // 잔디 생성
            Grass.SetGrassPosition(_field, 7, 3, 6, 3);

            if(!_isSetItem)
            {
                _field[7, 21].OnTileObject = new Gold();
                _field[3, 5].OnTileObject = new Potion() { Name = "Potion1" };
                _field[2, 15].OnTileObject = new Potion() { Name = "Potion2" };
                _field[7, 3].OnTileObject = new Potion() { Name = "Potion3" };
                _field[24, 59].OnTileObject = new Potion() { Name = "Potion4" };

                _isSetItem = true;
            }
            _field[10, 10].OnTileObject = new Dungeon();
            // 플레이어 위치 설정 / 타일에 등록
            _player.Position = new Vector(4, 2);
            _field[_player.Position.Y, _player.Position.X].OnTileObject = _player;

            Debug.Log("타운 씬 진입");
        }

        public override void Update()
        {
            _player.Update();
        }

        public override void Render()
        {
            PrintField();
            _player.Render();
        }

        public override void Exit()
        {
            // 필드에 등록 해제
            _field[_player.Position.Y, _player.Position.X].OnTileObject = null;
            //_player.Field = null;
        }

        private void PrintField()
        {
            // 카메라 왼쪽 위 좌표
            int camX = _player.Position.X - camWidth / 2;
            int camY = _player.Position.Y - camHeight / 2;

            // 맵 범위 벗어난 인덱스 조정
            if (camX < 0) camX = 0;  // 최소
            if (camY < 0) camY = 0;
            if (camX > _field.GetLength(1) - camWidth) camX = _field.GetLength(1) - camWidth;  // 최대
            if (camY > _field.GetLength(0) - camHeight) camY = _field.GetLength(0) - camHeight;

            for (int y = 0; y < camHeight; y++)
            {
                for (int x = 0; x < camWidth; x++)
                {
                    _field[camY + y, camX + x].Print();
                }
                Console.WriteLine();
            }
        }
    }
}