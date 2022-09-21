using System;

namespace Statement_Loops
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //while (조건)
            //{
            //    조건이 참일떄 반복할 내용
            //}

            // 무한루프 : 끊임없이 도는 반복문

            int[] arrInt = { 23, 42, 12, 21, 37 };

            int count = 0;
            while (count < arrInt.Length)
            {
                Console.WriteLine(arrInt[count]);
                count++;
            }

            string name = "Luke";
            count = 0;
            while (count < name.Length)
            {
                Console.WriteLine(name[count]);
                count++;
            }

            do
            {
                Console.WriteLine("Do while test");
            } while (false);

            // for (인덱스변수 초기화; for문 반복조건; 루프 한번 실행후에 수행할 연산)
            for (int i = 0; i < arrInt.Length; i++)
            {
                Console.WriteLine(arrInt[i]);
            }
        }
    }
}
