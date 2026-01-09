using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    public class MenuList
    {
        // 튜플로 넣을때 ()안에 넣어주기 -> <()>
        private List<(string text, Action action)> _menus; // 메뉴 항목들 리스트
        private int _currentIndex;  // 현재 메뉴 인덱스
        public int CurrentIndex { get => _currentIndex; } // _currentIndex 읽기용
        private Ractangle _outline; // 박스 테두리
        private int _maxLength; // 메뉴 글자들 중 가장 긴 길이

        public MenuList(params (string, Action)[] menuTexts)
        {
            // 전달 받은게 없으면 빈 리스트
            if (menuTexts.Length == 0)
            {
                _menus = new List<(string, Action)>();
            }
            // 전달받은 메뉴가 있을 때 리스트로 바꿔서 저장
            else
            {
                _menus = menuTexts.ToList();
            }

            // 글자 수 가장 긴 메뉴 길이 계산
            for (int i = 0; i < _menus.Count; i++)
            {
                int textWidth = _menus[i].text.GetTextWidth();

                if (_maxLength < textWidth)
                {
                    _maxLength = textWidth;
                }
            }
            // 테두리 생성
            _outline = new Ractangle(width: _maxLength + 4, height: _menus.Count + 2);
        }

        public void Reset()
        {
            // 현재 인덱스 초기화(맨 위로)
            _currentIndex = 0;
        }

        public void Select()
        {
            if (_menus.Count == 0) return;

            // 현재 인덱스의 action이 null이 아니면 invoke
            _menus[_currentIndex].action?.Invoke();

            // 범위 벗어난 인덱스 조정
            if (_menus.Count == 0) _currentIndex = 0;
            else if (_currentIndex >= _menus.Count)
            {
                _currentIndex = _menus.Count - 1;
            }
        }

        public void Add(string text, Action action)
        {
            _menus.Add((text, action)); // 추가

            // 글자 수 크면 _maxLength 갱신
            int textWidth = text.GetTextWidth();
            if (_maxLength < textWidth)
            {
                _maxLength = textWidth;
            }

            // 테두리 변경
            _outline.Width = _maxLength + 6;
            _outline.Height++;
        }

        public void Remove()
        {
            _menus.RemoveAt(_currentIndex); // 삭제

            int max = 0;

            // 가장 큰 글자 수 다시 계산
            foreach ((string text, Action action) in _menus)
            {
                int textWidth = text.GetTextWidth();

                if (max < textWidth) max = textWidth;
            }

            if (_maxLength != max) _maxLength = max;

            // 테두리 변경
            _outline.Width = _maxLength + 6;
            _outline.Height--;
        }

        public void SelectUp()
        {
            _currentIndex--; // 위로

            // 범위 벗어난 인덱스 조정
            if (_currentIndex < 0) _currentIndex = 0;
        }

        public void SelectDown()
        {
            _currentIndex++; // 아래로

            // 범위 벗어난 인덱스 조정
            if (_currentIndex >= _menus.Count) _currentIndex = _menus.Count - 1;
        }

        public void Render(int x, int y)
        {
            _outline.X = x;
            _outline.Y = y;
            _outline.Draw();

            for (int i = 0; i < _menus.Count; i++)
            {
                y++; // 아래 줄로 내려감
                Console.SetCursorPosition(x + 1, y); // (x+1 -> 테두리 안)

                if (i == _currentIndex) // 선택줄
                {
                    "-> ".Print(ConsoleColor.Green);
                    _menus[i].text.Print(ConsoleColor.Green);
                    continue;
                }
                else
                {
                    Console.Write("   ");
                    _menus[i].text.Print();
                }
            }
        }
    }
}