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
        private bool _isSetItem; // 아이템 1번만 설치됨
        // 카메라 너비, 높이
        public int _camWidth = 30;
        public int _camHeight = 15;

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

            // 벽 생성
            SetWalls();
            // 잔디 생성
            SetGrassPosition(6, 4, 20, 6);
            SetGrassPosition(34, 4, 20, 6);
            SetGrassPosition(6, 15, 20, 6);
            SetGrassPosition(34, 15, 20, 6);
        }

        public override void Enter()
        {
            // 플레이어에게 맵 연결
            _player.Field = _field;

            if(!_isSetItem)
            {
                _field[19, 9].OnTileObject = new Gold();
                _field[2, 40].OnTileObject = new Gold();
                _field[1, 1].OnTileObject = new Potion() { Name = "Potion" };
                _field[18, 46].OnTileObject = new Potion() { Name = "Potion" };

                _isSetItem = true;
            }

            _field[12, 56].OnTileObject = new Dungeon(); // 던전

            // 플레이어 위치 설정 / 타일에 등록
            _player.Position = new Vector(30, 12);
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
            int camX = _player.Position.X - _camWidth / 2;
            int camY = _player.Position.Y - _camHeight / 2;

            // 맵 범위 벗어난 인덱스 조정
            if (camX < 0) camX = 0;  // 최소
            if (camY < 0) camY = 0;
            if (camX > _field.GetLength(1) - _camWidth) camX = _field.GetLength(1) - _camWidth;  // 최대
            if (camY > _field.GetLength(0) - _camHeight) camY = _field.GetLength(0) - _camHeight;

            for (int y = 0; y < _camHeight; y++)
            {
                for (int x = 0; x < _camWidth; x++)
                {
                    _field[camY + y, camX + x].Print();
                }
                Console.WriteLine();
            }
        }

        // x, y -> 잔디 시작(왼쪽 위) 좌표 / width 너비 / height 높이
        public void SetGrassPosition(int startX, int startY, int width, int height)
        {
            // x, y 마지막 좌표
            int endX = startX + width - 1;
            int endY = startY + height - 1;

            if (startX < 0) startX = 0;
            if (endX >= _field.GetLength(1)) endX = _field.GetLength(1) - 1;
            if (startY < 0) startY = 0;
            if (endY >= _field.GetLength(0)) endY = _field.GetLength(0) - 1;

            for (int i = startY; i <= endY; i++)
            {
                for (int j = startX; j <= endX; j++)
                {
                    if (_field[i, j].OnTileObject == null)
                    {
                        _field[i, j].HasGrass = true; // 해당위치 잔디로 체크
                        _field[i, j].OnTileObject = new Grass();
                    }
                }
            }
        }

        // 테두리 벽 설정
        private void SetWalls()
        {
            // 가로
            for(int x = 0; x < _field.GetLength(1); x++)
            {
                _field[0, x].OnTileObject = new Wall();
                _field[_field.GetLength(0) - 1, x].OnTileObject = new Wall();
            }

            // 세로
            for(int y = 0; y < _field.GetLength(0); y++)
            {
                _field[y, 0].OnTileObject = new Wall();
                _field[y, _field.GetLength(1) - 1].OnTileObject = new Wall();
            }
        }
    }
}