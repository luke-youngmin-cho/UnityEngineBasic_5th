using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassInheritance
{
    // abstact 키워드
    // 추상 키워드 
    // 클래스 / 메소드 / 프로퍼티 앞에 붙을 수 잇음
    // 추상 키워드가 붙은 멤버들은 해당 클래스타입으로 인스턴스화가 안됨. 
    // 반드시 해당클래스를 상속받은 자식 클래스에 추상부분을 구현하고 사용해야함.
    internal abstract class Creature
    {
        public string Species;
        public int LifeTime;
        public int Mass;

        public abstract void Breath(); // 함수 선언
        // 추상 함수는 해당 이름의 함수가 존재해야한다는 명시를 하기위한 용도일뿐
        // 실제 함수를 사용해야할때는, 클래스를 상속받고 해당 함수를 상속받은 클래스에서 구현해주어야한다.
    }
}
