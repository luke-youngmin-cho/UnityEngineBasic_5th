using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyDynamicArray
{
    internal class MyDynamicArray
    {
        private const int DEFAULT_SIZE = 1;
        private int[] _data = new int[DEFAULT_SIZE];
        public int this[int index]
        {
            get
            {
                return _data[index];
            }

            set
            {
                _data[index] = value;
            }
        }
        public int Count;
        public int Capacity;

        public void Add(int item)
        {
            // 배열의 크기가 모자라면
            if (Count >= Capacity)
            {
                Capacity *= 2;
                // 2배짜리 새로운 배열 생성
                int[] tmp = new int[Capacity];

                // 기존 데이터를 새로운 배열에 복제
                for (int i = 0; i < Count; i++)
                {
                    tmp[i] = _data[i];
                }

                // 새로운 배열로 참조 변경
                _data = tmp;
            }

            _data[Count] = item;
            Count++;
        }
        
        public bool RemoveAt(int index)
        {
            if (index < 0 || index > Count - 1)
                return false;

            for (int i = index; i < Count - 1; i++)
            {
                _data[i] = _data[i + 1];
            }
            
            Count--;
            _data[Count] = 0;
            return true;
        }
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
    }
}

//프로퍼티 ?

//private int _hp;
//
//public int Hp
//{
//    get
//    {
//        return _hp;
//    }
//
//    private set
//    {
//        if (value < 0)
//            value = 0;
//
//        _hp = value;
//        _hpBar.value = _hp / _hpMax;
//    }
//}
//
//public int Mp { get; private set; }
//
//
//private UISlider _hpBar;
//private int _hpMax;
//
//public int GetHp()
//{
//    return _hp;
//}
//
//public void SetHp(int value)
//{
//    if (value < 0)
//        value = 0;
//
//    _hp = value;
//    _hpBar.value = _hp / _hpMax;
//}
//
//class UISlider { public float value; }