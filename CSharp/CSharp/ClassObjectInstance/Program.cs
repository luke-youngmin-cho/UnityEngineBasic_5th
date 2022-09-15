using System;

namespace ClassObjectInstance
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // new 키워드 
            // 동적할당 키워드 
            Human human = new Human();
            int a = 3;
        }
    }

    // 클래스도 사용자정의 '자료형'
    public class Human
    { 
        int age = 1;
        float height; 
        double weight;
        bool isReseting;        
        char genderCharacter;
        string name; 

        // 생성자 
        // 객체를 생성하는 함수
        // 객체를 생성한다는 의미 : 해당 클래스타입의 멤버들을 위한 메모리공간을 전부 확보함.
        // 생성자도 기본적으로 함수
        // 반환형은 힙 영역에 동적할당한 객체의 참조

        // 참조란 ? 
        // 특정 메모리의 주소를 가지고 있으며 해당 주소의 데이터를 읽고 쓸 수 있는 형태

        // 값 형식 Vs 참조 형식
        // 값 형식 : 스택영역에 데이터가 할당됨.
        // ex) int, float, double, char, struct ..
        // 참조 형식 : 힙영역에 데이터가 할당됨.
        // ex) class, array, string ..
        public Human()
        {

        }

        void Breath()
        {
            Console.WriteLine($"{name} (이)가 숨을 쉰다");
        }
    }
}
