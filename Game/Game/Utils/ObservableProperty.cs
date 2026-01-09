using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    // 값(Value)이 바뀌면 자동으로 알림 보내줌
    public class ObservableProperty<T> where T : struct // 값타입만
    {
        private T _value;

        // 밖에서 Value로 접근
        public T Value
        {
            get => _value;
            set 
            {  
                _value = value;
                OnValueChanged?.Invoke(value);
            }
        }

        public event Action<T> OnValueChanged;

        // 매개변수 없으면 기본값 시작
        public ObservableProperty(T value = default)
        {
            _value = value;
        }

        public void AddListener(Action<T> action)
        {
            // 함수 추가
            OnValueChanged += action;
        }

        public void RemoveListener(Action<T> action)
        {
            // 함수 제거
            OnValueChanged -= action;
        }

        public void RemoveListenerAll()
        {
            // 등록된 함수 전부 제거
            OnValueChanged = null;
        }
    }
}