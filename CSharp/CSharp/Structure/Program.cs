using System;

// 구조체 
// 사용자 정의 자료형
// 멤버 변수, 멤버 함수를 가짐
// 생성자가 있음.
// (단, 생성자는 매개변수로 모든 멤버 변수에 대응되는 파라미터를 가지고 있어야 하며, 생성자호출시 모든멤버변수는 초기화 되어야한다.)
// 왜 모든멤버변수가 꼭 초기화되어야하나 ? 
// 구조체는 값 형식 이고 스택영역에 할당되기 때문.

// 구조체 vs 클래스 
// 모든 멤버의 크기 합이 16byte 를 초과할 경우 클래스를 사용한다. (최우선 조건)
// 값의 전달이 빈번하게 일어나는 경우 구조체를 쓴다. (예. 함수에 인자로 전달해야하는경우)
// 잠깐 썼다가 사라지는 경우가 잦을때 구조체를 쓴다. (클래스타입 객체 해제시 가비지컬렉션 부하 증가)

namespace Structure
{
    public struct Stats
    {
        public int STR;
        public int DEX;
        public int INT;
        public int LUK;

        public int CalcCombatPoint()
        {
            return STR + DEX + INT + LUK;
        }
    }

    public class Stats_Class
    {
        public int STR;
        public int DEX;
        public int INT;
        public int LUK;

        public int CalcCombatPoint()
        {
            return STR + DEX + INT + LUK;
        }
    }

    public class Warrior
    {
        public Stats Stats;
    }

    public struct WarriorFamily
    {
        public Warrior Warrior1; // 스택영역에 참조 주소만 가지고있음. 
        public Warrior Warrior2;
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            // 스택영역에 할당
            Stats stats = new Stats()
            { 
                STR = 100,
                DEX = 200,
                INT = 400,
                LUK = 800
            };

            Stats_Class statsClass = new Stats_Class()
            {
                STR = 100,
                DEX = 200,
                INT = 400,
                LUK = 800
            };

            Warrior warrior = new Warrior();
            // warrior.Stats 는 힙영역에 할당.
            Console.WriteLine(warrior.Stats.CalcCombatPoint());
        }
    }
}
