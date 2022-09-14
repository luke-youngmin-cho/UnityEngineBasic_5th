// 파란색글자 :
// 키워드(예약어)란 ? 미리 정의되어있는 단어
// 이미 문법용도로 쓰고있기 때문에 임의로 개발자가 사용할 수 없다.
//
// 흰글자 :
// 식별문자 (이름)
//
// 청록색글자:
// 클래스타입의 (이름)이름,
// 생성자/ 소멸자 등
//
// 노란색글자:
// 함수 식별문자(이름)
//
// 주황색글자:
// 문자/문자열 상수

// using 키워드
// 1. 특정 namespace를 사용하기위한 키워드
// 2. namespace들 간에 겹치는 멤버가 있을때 default 설정
// 3. IDisposable 인터페이스의 Dispose() 함수 호출 보장 - 몰라도됨아직
using System; // System 이라는 namespace 를 가져다 쓰겠다

// namespace 키워드
// 이름으로 메모리공간을 구분하기위한 키워드
// 내부식별자를 통해 namespace로 묶인 변수,함수,클래스,인터페이스 등등을 구분함
namespace FirstProject
{
    // internal 접근제한자
    // 동일 어셈블리에서 접근 가능한 키워드

    // class 키워드
    // 객체를 만들기위한 타입을 정의하는 키워드
    internal class Program
    {
        // static 키워드
        // 정적 키워드 : 메모리에 동적으로 할당하는것이 불가능하도록 만드는 키워드

        // void 키워드
        // 반환 타입이 아무것도 없음
        // 연산만 수행하고 반환을 따로 하지 않는 함수의 반환형태로 사용함
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
        }
    }
}
