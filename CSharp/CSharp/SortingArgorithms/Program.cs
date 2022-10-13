using System;

namespace SortingArgorithms
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int[] array = { 5, 2, 3, 7, 1, 10, 4, 6, 9, 8 };
            //BubbleSort.Sort(array);
            //SelectionSort.Sort(array);
            //InsertionSort.Sort(array);
            //QuickSort.Sort(array);
            HeapSort.Sort(array);
            for (int i = 0; i < array.Length; i++)
                Console.Write(array[i]);
        }
    }
}
