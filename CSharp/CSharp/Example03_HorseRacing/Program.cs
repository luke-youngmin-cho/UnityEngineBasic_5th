using System;
using System.Threading;
/*
### - 진행방식 -

프로그램 시작시
말 다섯마리를 만들고
각 다섯마리는 초당 10~20 (정수형) 범위 거리를 랜덤하게 움직임
각각의 말이 거리 200 에 도달하면 말의 이름과 등수를 출력해줌

말은
이름, 달린거리, 등수, 경주마쳤는지? 를 멤버변수로
달리기 를 멤버 함수로 가짐.
달리기 멤버함수는 입력받은 거리를 달린거리에 더해주어서 달린거리를 누적시키는 역할을 함.

매초 달릴 때 마다 각 말들이 얼마나 거리를 이동했는지 콘솔창에 출력해줌.
경주가 끝나면 1,2,3,4,5 등 말의 이름을 등수 순서대로 콘솔창에 출력해줌.

### - Hint -

System.Threading namespace 에 있는 Thread.Sleep(1000); 를 사용하면 현재 프로그램을 1초 지연시킬수 있음
While 반복문에서 Thread.Sleep(1000); 을 추가하면 1초에 한번씩 반복문을 실행함.
*/
namespace Example03_HorseRacing
{
    public class Horse
    {
        public string Name;
        public int Position;
        public int Grade;
        public bool IsFinished;

        public void Run(int distance)
        {
            Position += distance;
        }
    }

    internal class Program
    {
        // const 키워드
        // constant (상수) 처리 키워드 
        // 초기화 이후 값을 수정할수 없도록 하는 키워드
        const int TOTAL_HORSES_NUMBER = 5;
        const int DISTANCE_MIN = 10;
        const int DISTANCE_MAX = 20;
        const int GOAL_POSITION = 200;
        static void Main(string[] args)
        {
            Horse[] horses = new Horse[TOTAL_HORSES_NUMBER];
            Horse[] finishedHorses = new Horse[TOTAL_HORSES_NUMBER];
            int grade = 0;
            Random rand = new Random();

            for (int i = 0; i < TOTAL_HORSES_NUMBER; i++)
            {
                horses[i] = new Horse();
                horses[i].Name = $"경주마{i + 1}";
            }

            int count = 0;
            // 경주 
            while (grade < TOTAL_HORSES_NUMBER)
            {
                // 말 달리기
                for (int i = 0; i < TOTAL_HORSES_NUMBER; i++)
                {
                    if (horses[i].IsFinished == false)
                    {
                        horses[i].Run(rand.Next(DISTANCE_MIN, DISTANCE_MAX + 1));

                        if (horses[i].Position >= GOAL_POSITION)
                        {
                            horses[i].IsFinished = true;
                            finishedHorses[grade] = horses[i];
                            grade++;
                            horses[i].Grade = grade;
                        }
                    }

                    if (horses[i].IsFinished)
                        Console.WriteLine($"{horses[i].Name} 은 도착함");
                    else
                        Console.WriteLine($"{horses[i].Name} 의 현재 위치 : {horses[i].Position}");
                }

                Thread.Sleep(1000); // 1초 슬립
                count++;
                Console.WriteLine($"============================== {count} 초 경과 ===================================");
            }

            for (int i = 0; i < TOTAL_HORSES_NUMBER; i++)
            {
                Console.WriteLine($"{finishedHorses[i].Grade} 등 : {finishedHorses[i].Name} !");
            }

            /*
             * for (0 ~ 4) new Horse()

             Horse[] finishedHorses = new Horse[5]
             grade
             
             while (grade < total horse num)
             {
             	for (0 ~ 4)
             	{
             		if (horse[i].finished == false)
             		{
             			horse[i].Run(Random(10~20))
             
             			if (horses[i].finished)
             			{
             				finishedHorses[Length - 1] = horses[i]			
             				horses[i].grade = ++grade
             			}
             		}
             
             		Display(horse[i].distance);
             	}
             
             	Sleep(1 sec)
             }
             
             for (0 ~ 4) 
             {
             	Display (finishedHorses[i].name, finishedHorses[i].grade);
             
             }
                          * 
             */
        }
    }
}
