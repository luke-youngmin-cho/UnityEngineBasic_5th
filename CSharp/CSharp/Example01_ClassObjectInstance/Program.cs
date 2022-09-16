using System;

// Orc 클래스 작성 예제
// Orc 는 
// 이름, 키, 몸무게, 나이, 성별문자, 쉬고있는지아닌지? 에 대한 특징을 가지고 있으며 (멤버변수)
// 휘두르기 (Smash), 점프하기 (Jump) 라는 기능 (멤버함수) 를 가지고 있습니다.
// Orc 클래스를 작성한 후 
//
// 1. Main 함수 내에서 Orc 객체를 두개 만듭니다.
// 첫번째 객체는 
// 이름 : 상급오크
// 키 : 240.2
// 몸무게 : 200.0
// 나이 : 140 
// 성별문자 : '남'
// 쉬고있는지? : false
//
// 두번째 객체는
// 이름 : 하급오크
// 키 : 140.4
// 몸무게 : 120.0
// 나이 60
// 성별문자 : '여'
// 쉬고있는지? : true
//
// 2. 각 오크들이 쉬고있는지 체크한 후, 쉬고있는 오크에게 점프와 휘두르기를 시전시킵니다 
// (멤버함수 호출)
namespace Example01_ClassObjectInstance
{

    // Naming convention
    // PascalCase
    // camelCase
    // snake_case
    // UPPER_SNAKE_CASE
    //
    // 클래스이름 , 함수이름 : PascalCase 
    // public, protected 변수 : PascalCase
    // private 변수 : _camelCase
    // 지역변수 : camelCase
    // 상수 : UPPER_SNAKE_CASE / SNAKE_CASE
    internal class Program
    {
        static void Main(string[] args)
        {
            Orc orc1 = new Orc();
            orc1.Name = "상급오크";
            orc1.Height = 240.2f;
            orc1.Weight = 200.0;
            orc1.Age = 140;
            orc1.GenderChar = '남';
            orc1.IsResting = false;

            Orc orc2 = new Orc();
            orc2.Name = "하급오크";
            orc2.Height = 140.4f;
            orc2.Weight = 120.0;
            orc2.Age = 60;
            orc2.GenderChar = '여';
            orc2.IsResting = true;

            orc1.Jump();
            orc2.Smash();
        }
    }

    public class Orc
    {
        private bool _isDataSaved;
        public string Name;
        public float Height;
        public double Weight;
        public int Age;
        public char GenderChar;
        public bool IsResting;

        public void Smash()
        {
            Console.WriteLine($"{Name} (이)가 휘둘렀다..!");
        }

        public void Jump()
        {
            Console.WriteLine($"{Name} (이)가 점프했다..!");
        }
    }
}
