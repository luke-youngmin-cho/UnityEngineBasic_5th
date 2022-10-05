using System;
using System.Collections.Generic;

namespace MyDynamicArray
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<int> list = new List<int>();
            list.Add(1);
            list.Add(2);
            list.Add(3);
            list.Add(4);
            list.Remove(1);
            Console.WriteLine(list[0]);

            for (int i = 0; i < list.Count; i++)
            {
                Console.WriteLine(list[i]);
            }

            foreach (int item in list)
            {
                Console.WriteLine(item);
            }

            // foreach 문 구현 원리
            using (IEnumerator<int> enumerator = list.GetEnumerator())
            {
                while (enumerator.MoveNext())
                {
                    Console.WriteLine(enumerator.Current);
                }
            }

            MyDynamicArray<int> dynamicArray = new MyDynamicArray<int>();
            dynamicArray.Add(1);
            dynamicArray.Add(2);
            dynamicArray.Add(3);
            dynamicArray.Add(4);
            dynamicArray.Remove(1);
            Console.WriteLine(dynamicArray[0]);

            for (int i = 0; i < dynamicArray.Count; i++)
            {
                Console.WriteLine(dynamicArray[i]);
            }

            foreach (int item in dynamicArray)
            {
                Console.WriteLine(item);
            }
        }
    }
}
