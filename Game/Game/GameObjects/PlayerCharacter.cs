using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    public class PlayerCharacter : GameObject
    {
        public ObservableProperty<int> Health = new ObservableProperty<int>(5);
        private string _healthGauge; // 체력 게이지

        public Tile[,] Field { get; set; } // 플레이어가 있는 맵
        private Inventory _inventory; // 플레이어 인벤토리
        public bool IsActiveControl { get; private set; } // 플레이어 조작 가능 여부

        public PlayerCharacter() => Init();

        public void Init()
        {
            Symbol = 'P';
            IsActiveControl = true;
            Health.AddListener(SetHealthGauge); // 체력바뀌면 SetHealthGauge 실행
            _healthGauge = "♥♥♥♥♥";
            _inventory = new Inventory(this); // 플레이어 자신을 owner로 넘김
        }

        public void Update()
        {
            if(InputManager.GetKey(ConsoleKey.I))
            {
                // 인벤토리 열기/닫기
                HandleControl();
            }

            if (InputManager.GetKey(ConsoleKey.UpArrow))
            {
                Move(Vector.Up);  // 맵 이동
                _inventory.SelectUp();  // 인벤토리 메뉴 이동
            }
                

            if (InputManager.GetKey(ConsoleKey.DownArrow))
            {   
                Move(Vector.Down);  // 맵 이동
                _inventory.SelectDown();  // 인벤토리 메뉴 이동
            }

            if (InputManager.GetKey(ConsoleKey.LeftArrow))
            {
                Move(Vector.Left);
            }

            if (InputManager.GetKey(ConsoleKey.RightArrow))
            {
                Move(Vector.Right);
            }

            if (InputManager.GetKey(ConsoleKey.Enter))
            {
                // 현재 선택된 아이템의 Use() 실행
                _inventory.Select();
            }

            if (InputManager.GetKey(ConsoleKey.T)) // 수정예정
            {
                Health.Value--; // 체력 감소 
            }
        }

        public void HandleControl()
        {
            // 인벤토리가 열리면 플레이어 조작 안됨
            _inventory.IsActive = !_inventory.IsActive;
            IsActiveControl = !_inventory.IsActive;
        }

        private void Move(Vector direction)
        {
            if (Field == null || !IsActiveControl) return;

            Vector current = Position;  // 현재 위치
            Vector nextPos = Position + direction;  // 다음 위치

            // 맵 벗어나면 이동 안함
            if (nextPos.X < 0 || nextPos.Y < 0 || nextPos.X >= Field.GetLength(1) || nextPos.Y >= Field.GetLength(0))
                return;

            // 다음 칸 오브젝트 확인
            GameObject nextTileObject = Field[nextPos.Y, nextPos.X].OnTileObject;

            if (nextTileObject != null)
            {
                // 다음 칸 오브젝트가 IInteractable면 상호작용 실행
                if (nextTileObject is IInteractable)
                {
                    (nextTileObject as IInteractable).Interact(this);
                }
            }

            // 원래 위치가 잔디있던 자리가 아니라면 null
            if (Field[Position.Y, Position.X].HasGrass)
            {
                Field[Position.Y, Position.X].OnTileObject = new Grass();
            }
            else Field[Position.Y, Position.X].OnTileObject = null;

            // 다음 위치를 자신으로 연결
            Field[nextPos.Y, nextPos.X].OnTileObject = this;
            Position = nextPos;

            Debug.LogWarning($"플레이어 이동 : ({current.X},{current.Y}) -> ({nextPos.X},{nextPos.Y})");
        }

        public void Render()
        {
            DrawHealthGauge();
            _inventory.Render();
        }

        public void AddItem(Item item)
        {
            // 플레이어의 인벤토리에 아이템 추가
            _inventory.Add(item);
        }

        public void DrawHealthGauge()
        {
            Console.SetCursorPosition(0, 0);
            "HP ".Print(ConsoleColor.Gray);
            _healthGauge.Print(ConsoleColor.Red);
        }

        public void SetHealthGauge(int health)
        {
            switch (health)
            {
                case 5:
                    _healthGauge = "♥♥♥♥♥";
                    break;
                case 4:
                    _healthGauge = "♥♥♥♥";
                    break;
                case 3:
                    _healthGauge = "♥♥♥";
                    break;
                case 2:
                    _healthGauge = "♥♥";
                    break;
                case 1:
                    _healthGauge = "♥";
                    break;
            }
        }

        public void Heal(int value)  // 회복
        {
            Health.Value += value;
        }
    }
}