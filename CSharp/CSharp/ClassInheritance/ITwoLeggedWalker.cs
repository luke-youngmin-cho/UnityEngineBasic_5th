using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// interface
// 추상화 를 위한 용도로 사용함
// 함수, 이벤트, 인덱서, 프로퍼티 만 멤버로 가질수 있고
// 외부에서 접근하기 위한 용도로 보통 사용하기때문에 접근제한자 명시되지 않았을 경우 기본적으로 public 이다.
// 추상화이기때문에 보통은 선언만 하고, 구현을 쓸 수 있는데 구현부를 쓰게되면 자식객체에서 재정의할때 해당 구현부를 기본적으로 사용하게된다.
// interface 를 상속받는 interface 도 만들 수 있다. 
// interface 는 다중상속이 가능하다
namespace ClassInheritance
{
    internal interface ITwoLeggedWalker
    {
        public void TwoLeggedWalk();
    }
}
