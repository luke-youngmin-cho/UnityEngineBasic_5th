using System;

namespace MyListObservable
{
    internal class Program
    {
        public static int AddCount = 0;
        static void Main(string[] args)
        {
            MyListObservable<int> list = new MyListObservable<int>();

            list.OnAdded += (item) => AddCount++;

            list.Add(1);
            Console.WriteLine(AddCount);
        }
    }
}
