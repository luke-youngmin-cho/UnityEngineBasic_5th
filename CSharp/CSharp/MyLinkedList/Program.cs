using System;

namespace MyLinkedList
{
    class Program
    {
        static void Main(string[] args)
        {
            MyLinkedList<float> list = new MyLinkedList<float>();

            list.AddFirst(3.0f);
            list.AddLast(5.0f);
            list.AddAfter(list.First, 4.0f);

            foreach (var item in list)
            {
                Console.WriteLine(item);
            }
        }
    }
}
