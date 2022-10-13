using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SortingArgorithms
{
    public static class QuickSort
    {
        public static int _operationCount;

        public static void Sort(int[] arr)
        {
            Sort(arr, 0, arr.Length - 1);
        }

        private static void Sort(int[] arr, int start, int end)
        {
            if (start < end)
            {
                int p = Partition(arr, start, end);
                Sort(arr, start, p - 1);
                Sort(arr, p + 1, end);
                Console.WriteLine($"연산횟수 : {_operationCount}");
            }
        }

        private static int Partition(int[] arr, int start, int end)
        {
            int pivot = arr[(start + end) / 2];

            while (true)
            {
                while (arr[start] < pivot) start++;
                while (arr[end] > pivot) end--;

                if (start < end)
                {
                    int tmp = arr[end];
                    arr[end] = arr[start];
                    arr[start] = tmp;
                    _operationCount += 3;
                }
                else
                {
                    return end;
                }
            }

            return -1;
        }
    }
}
