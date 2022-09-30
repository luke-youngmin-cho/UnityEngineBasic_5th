using System;
using System.Collections;
using System.Collections.Generic;

namespace Collections
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int[] array = new int[3];

            // object 타입
            // .Net 의 모든 자료형의 기반 타입
            int a = 3;
            object obj = a; // 박싱 : 객체를 힙영역에 할당하고 기존 값을 가지고있게한다
            a = (int)obj; // 언박싱 : 힙영역에 할당되어있던 객체로부터 기존 값을 가져오는 과정


            // Collections
            //--------------------------------------------------------------

            // ArrayList
            // c# 의 동적배열
            ArrayList arrayList = new ArrayList();
            arrayList.Add(3); // 3을 박싱 후 해당 참조를 추가한다 
            // ArrayList 는 object[] 에다가 데이터를 저장한다.
            // ArrayList 는 object[] 의 크기를 데이터를 저장할때마다 부족하면 동적으로 늘린다.
            arrayList.Add("dfefdf");
            arrayList.Add('D');
            arrayList.Add(array);
            arrayList.Remove('D');
            arrayList.Insert(1, 'A');

            // Stack
            // LIFO (Last Input First Output)
            Stack stack = new Stack();
            stack.Push(3); // 아이템 젤 마지막에 추가
            stack.Push(4);
            Console.WriteLine(stack.Peek()); // 젤먼저 빠질 객체 반환
            stack.Pop(); // 젤 마지막에 들어온 아이템 빼고 반환
            stack.Pop();

            // Queue
            // FIFO (First Input First Output)
            Queue queue = new Queue();
            queue.Enqueue(3); // 아이템 젤 앞에 추가
            queue.Enqueue(4);
            Console.WriteLine(queue.Peek()); // 젤 먼저 빠질 객체 반환
            queue.Dequeue(); // 젤 앞에 있는 아이템 빼고 반환
            queue.Dequeue();

            // Hashtable
            // Hash(Key) 로 Value 에 접근할 수 있는 자료구조
            Hashtable hashtable = new Hashtable();
            hashtable.Add("철수", 240.0f);
            Console.WriteLine((float)hashtable["철수"]);

            // Generic Collections
            //--------------------------------------------------------------

            // List<T> 
            // C# 의 동적배열 일반형
            List<int> list = new List<int>();
            list.Add(3);
            list.Add(4);
            list.Remove(3);
            list.RemoveAt(0);

            // LinkedList<T>
            // C# 의 Doubly-Linked list 일반형
            LinkedList<float> linkedList = new LinkedList<float>();
            linkedList.AddLast(3.0f);
            linkedList.Remove(3.0f);
            linkedList.RemoveFirst();

        }
    } 
}
