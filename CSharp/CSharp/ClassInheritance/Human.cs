using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassInheritance
{
    // 상속 
    // 클래스이름 뒤에 : 쓰고 상속받을 클래스 이름 씀
    internal class Human : Creature
    {
        // override 키워드
        // 부모 클래스의 멤버를 재정의 할 때 쓰는 키워드
        public override void Breath()
        {
            Console.WriteLine("Human is breathing");
        }
    }
}
