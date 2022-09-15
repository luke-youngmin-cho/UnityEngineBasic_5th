using System;

namespace Methods
{
    internal class Program
    {
        // 함수 정의 
        // 반환타입 함수이름(매개변수1타입 매개변수1이름, 매개변수2타입 매개변수2이름 ...)
        // {
        //      ...
        //      연산내용
        //      ...
        //      return 반환값;
        // }
        //
        // void 반환형 함수에서는 함수 마지막에 return 을 생략할 수 있다.
        static void Main(string[] args)
        {
            // 함수 호출 형태
            // 함수이름(인자1, 인자2 ...);
            PrintHelloWorld();
            PrintNum(3);
            // 지역변수 : Local variable, 함수 내에 선언된 변수
            // 전역변수 : 외부 클래스/ 객체등에서 (지역상관없이) 접근할수 있는 변수
            int result;
            //PrintNum(result); // 지역변수는 초기화 후에 사용해야한다.
            result = Sum(1, 2);
            PrintNum(result);
            // L-Value :
            // 메모리 위치를 참조하는식 
            // 보통 대입연산자의 왼쪽에 위치함

            // R-Value : 보통 대입연산자의 오른쪽에 위치함
            // 식의 값을 나타내는 식
        }

        static void PrintHelloWorld()
        {
            Console.WriteLine("Hello World!");
        }
        static void PrintNum(int num)
        {
            Console.WriteLine(num);
        }

        static int Sum(int a, int b)
        {
            return a + b;
        }
    }
}
