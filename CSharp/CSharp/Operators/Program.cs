using System;

namespace Operators
{
    class Program
    {
        static void Main(string[] args)
        {
            int a = 14;
            int b = 6;
            int c = 0;

            // 산술 연산자
            // 더하기, 빼기, 곱하기, 나누기, 나머지
            //==================================================================

            // 더하기 
            c = a + b;
            Console.WriteLine(c);

            // 빼기
            c = a - b;
            Console.WriteLine(c);

            // 곱하기
            c = a * b;
            Console.WriteLine(c);

            // 나누기
            // 정수연산시 몫만 반환, 실수연산시 소수점까지 연산
            c = a / b;
            Console.WriteLine(c);

            // 나머지
            // 정수든 실수든 정수 나머지결과 반환
            c = a % b;
            Console.WriteLine(c);

            // 증감 연산자
            // 증가 연산자, 감소연산자
            //==================================================================

            // 증가연산
            ++c; // 전위연산 : 해당 라인에서 연산을 먼저 수행한 후에 명령 실행
            c++; // 후위연산 : 해당 라인에서 명령을 먼저 수행한 후에 연산 실행
            c = 0;
            Console.WriteLine(++c);
            Console.WriteLine(c++);
            Console.WriteLine(c);

            // 감소연산
            --c;
            c--;

            // 관계 연산자
            // 같음, 다름 , 크기 비교 연산
            // 연산 결과가 참일경우 true , 거짓일경우 false 논리형 반환
            //==================================================================

            // 같음 비교
            Console.WriteLine(a == b);

            // 다름 비교
            Console.WriteLine(a != b);

            // 큰지 비교
            Console.WriteLine(a > b);

            // 크거나 같은지 비교
            Console.WriteLine(a >= b);

            // 작은지 비교
            Console.WriteLine(a < b);

            // 작거나 같은지 비교
            Console.WriteLine(a <= b);

            // 대입 연산자
            // 더해서/ 빼서 / 곱해서/ 나누어서/ 나머지연산후 대입하는 연산
            //==================================================================
            c = 20;
            c += b; // == c = c + b;
            c -= b;
            c *= b;
            c /= b;
            c %= b;

            // 논리 연산자
            // or / and / xor/ not
            //==================================================================
            bool A = true;
            bool B = false;

            // or 
            // a 와 b 둘중 하나라도 true 면 true 반환, 나머지경우 false 반환
            Console.WriteLine(A | B);

            // and
            // a 와 b 둘다 true 면 true 반환, 나머지경우 false 반환
            Console.WriteLine(A & B);

            // xor
            // a 와 b 둘중 하나만 true 일때 true 반환, 나머지경우 false 반환
            Console.WriteLine(A ^ B);

            // not
            // 피연산자가 true 이면 false 반환, false 이면 true 반환
            Console.WriteLine(!A);

            // 조건부 논리 연산자
            // conditional or / conditional and 
            //==================================================================

            // conditional or 
            // 앞쪽 피연산자가 true 일 경우 뒤의 피연산자 연산하지 않고 바로 true 반환
            Console.WriteLine(A || B);

            // conditional and
            // 앞쪽 피연산자가 false 일 경우 뒤의 피연산자 연산하지 않고 바로 false 반환
            Console.WriteLine(A && B);

            // 비트 연산자
            // or, and, xor, not, shift-left, shift-right
            //==================================================================

            // or
            Console.WriteLine(a | b);
            // a == 14 == 2^3 + 2^2 + 2^1 == ...00001110
            // b == 6  ==       2^2 + 2^1 == ...00000110
            // or result                  == ...00001110 == 14

            // and
            Console.WriteLine(a & b);
            // a == 14 == 2^3 + 2^2 + 2^1 == ...00001110
            // b == 6  ==       2^2 + 2^1 == ...00000110
            // and result                 == ...00000110 == 6

            // 324 == (10^2 * 3) + (10^1 * 2) + (10^0 * 4)
            // 324 == 256 + 64 + 4 = (2^8 * 1) + (2^6 * 1) + (2^2 * 1)
            //     == .. 00000001 01000100

            // xor
            Console.WriteLine(a ^ b);
            // a == 14 == 2^3 + 2^2 + 2^1 == ...00001110
            // b == 6  ==       2^2 + 2^1 == ...00000110
            // xor result                 == ...00001000 == 8

            // not
            Console.WriteLine(~a);
            // a == 14 == 2^3 + 2^2 + 2^1 == ...00001110
            // not result                 == 11111111111111111111111111110001;
            // 2의 보수 : 이진법에서 모든자리수를 반전하고 + 1 
            //(a = -a) == (a = ~a + 1);

            // shift - left
            Console.WriteLine(a << 1);
            // a == 14 == 2^3 + 2^2 + 2^1 == ...00001110
            // shift - left result        == ...00011100

            // shift - right
            Console.WriteLine(a >> 2);
            // a == 14 == 2^3 + 2^2 + 2^1 == ...00001110
            // shift - right result       == ...00000011
        }
    }
}
