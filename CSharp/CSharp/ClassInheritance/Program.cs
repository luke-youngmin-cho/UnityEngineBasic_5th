using System;

namespace ClassInheritance
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // 추상화 클래스는 인스턴스화 할 수 없다
            //Creature creature = new Creature();

            Human human = new Human();
            human.Breath();
        }
    }
}
