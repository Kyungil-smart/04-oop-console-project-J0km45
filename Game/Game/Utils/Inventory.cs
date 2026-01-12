using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    public class Inventory
    {
        private List<Item> _items = new List<Item>(); // 보유 아이템 저장
        public bool IsActive { get; set; } // 인벤토리 열림/닫힘
        public MenuList _itemMenu = new MenuList(); // 아이템 메뉴
        private PlayerCharacter _owner; // 인벤토리 주인

        public Inventory(PlayerCharacter owner)
        {
            _owner = owner;
        }

        public void Add(Item item)
        {
            // 인벤토리 최대 10칸
            if (_items.Count >= 10) return;

            // 아이템 리스트에 추가
            _items.Add(item); 
            // 메뉴에 텍스트, 액션 등록
            _itemMenu.Add(item.Name, item.Use);
            item.Inventory = this;
            item.Owner = _owner;
        }

        public void Remove(Item item)
        {
            _items.Remove(item); // 리스트에서 제거
            _itemMenu.Remove(); // 메뉴에서 제거
        }

        public void Render()
        {
            if (!IsActive) return;

            _itemMenu.Render(15, 2);
        }

        public void Select()
        {
            if (!IsActive) return;
            // 아이템의 Use() 실행
            _itemMenu.Select();
        }

        public void SelectUp()
        {
            if (!IsActive) return;

            _itemMenu.SelectUp();
        }

        public void SelectDown()
        {
            if (!IsActive) return;

            _itemMenu.SelectDown();
        }
    }
}
