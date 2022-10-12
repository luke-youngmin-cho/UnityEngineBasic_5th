using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SortingArgorithms
{
    // O(N^2)
    public static class SelectionSort
    {
        public static void Sort(int[] arr)
        {
            int i, j, min;
            for (i = 0; i < arr.Length; i++)
            {
                min = i;
                for (j = i + 1; j < arr.Length; j++)
                {
                    if (arr[j] < arr[min])
                        min = j;        
                }
                int tmp = arr[i];
                arr[min] = arr[i];
                arr[i] = tmp;
            }
        }
    }
}
