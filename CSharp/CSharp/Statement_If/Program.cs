using System;

namespace Statement_If
{
    class Program
    {
        static void Main(string[] args)
        {
            bool condition1 = false;
            bool condition2 = true;
            bool condition3 = true;

            if (condition1)
            {
                // 조건1이 참일때 실행할 내용
                Console.WriteLine("조건 1은 참이야");
            }
            else if (condition2)
            {
                Console.WriteLine("조건 1은 거짓이고, 조건 2는 참이야");
            }
            else if (condition3)
            {
                Console.WriteLine("조건 1, 2는 거짓이고, 조건 3은 참이야");
            }
            else
            {
                // 조건이 참이 아닐때 실행할 내용
                Console.WriteLine("조건 1, 2, 3 모두 거짓이야");
            }
        }
    }
}
