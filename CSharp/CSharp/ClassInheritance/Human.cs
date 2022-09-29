using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassInheritance
{
    // 상속 
    // 클래스이름 뒤에 : 쓰고 상속받을 클래스 이름 씀
    internal class Human : Creature, ITwoLeggedWalker, IFourLeggedWalker
    {
        public string Name;
        public int PhoneNumber;
        public string EmailAddress;

        public Human()
        {

        }

        // 생성자 오버로딩
        public Human(string name, int phoneNumber, string emailAddress)
        {
            Name = name;
            PhoneNumber = phoneNumber;
            EmailAddress = emailAddress;
        }

        // override 키워드
        // 부모 클래스의 멤버를 재정의 할 때 쓰는 키워드
        public override void Breath()
        {
            Console.WriteLine("Human is breathing");
        }

        public void FourLeggedWalk()
        {
            Console.WriteLine($"{Name} (이)가 사족보행한다");
        }

        public void TwoLeggedWalk()
        {
            Console.WriteLine($"{Name} (이)가 이족보행한다");
        }

        // virtual 키워드 
        // override 가능하도록 하며 
        // abstract 처럼 가장 자식 인스턴스에서 재정의된 멤버를 호출할 수 있도록 해준다
        public virtual void PurchaseParkingPass()
        {
            Console.WriteLine($"{Name} 이 주차권을 구매 했습니다");
        }

        // 할인권을 적용받을 수 있는 오버로딩
        public virtual void PurchaseParkingPass(float discountRatio)
        {
            Console.WriteLine($"{Name} 이 주차권을 구매 했습니다. {discountRatio} % 할인 쿠폰 적용 !!");
        }

    }
}
