using System;

namespace Array
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // 배열
            // * 참조 형식
            // 연속적인 메모리
            // 자료형[] 변수이름 = new 자료형[배열크기];
            // 자료형[] 변수이름 = { 원소1, 원소2, 원소3 ...};
            int[] arrInt = new int[3];
            // arrInt2 는 int 형 배열 참조 변수
            int[] arrInt2 = {1, 2, 3};
            float[] arrFloat = new float[4];
            int a = 1;
            // 인덱서를 통한 요소 접근
            arrInt[0] = 3;
            arrInt[1] = 2;
            arrInt[2] = 1;
            Console.WriteLine(arrInt[0]);
            Console.WriteLine(arrInt[1]);
            Console.WriteLine(arrInt[2]);
            //arrInt[x] = arrInt주소로가서 arrInt 제일 앞의 주소부터 x * int 타입 크기 만큼 뒤에있는 주소로부터 int 타입 크기만큼 읽는다
       
            string name = "Luke";
            Console.WriteLine(name[1]); 
            char[] arrChar = new char[5];
            arrChar[0] = 'L';
            arrChar[1] = 'u';
            arrChar[2] = 'k';
            arrChar[3] = 'e';
            arrChar[4] = '\0'; // null 문자. 모든비트 0 인 문자. 문자열의 끝을 식별하기위한 용도

            string[] arrString = new string[3];
            arrString[0] = "김아무개";
            arrString[1] = "이아무개";
            arrString[2] = "박아무개";
            Console.WriteLine(arrString[0]);

        }
    }
}
