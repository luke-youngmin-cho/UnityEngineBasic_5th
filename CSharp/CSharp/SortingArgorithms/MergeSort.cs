using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SortingArgorithms
{
    // O(NLogN)
    public static class MergeSort
    {
        public static void Sort(int[] arr)
        {
            Sort(arr, 0, arr.Length - 1);
        }

        private static void Sort(int[] arr, int start, int end)
        {
            if (start < end)
            {
                int mid = (start + end) / 2;

                Sort(arr, start, mid);
                Sort(arr, mid + 1, end);

                Merge(arr, start, mid, end);
            }
        }

        private static void Merge(int[] arr, int start, int mid, int end)
        {
            int[] tmp = new int[end + 1];

            for (int i = 0; i < end + 1; i++)
                tmp[i] = arr[i];

            int part1 = start;
            int part2 = mid + 1;
            int index = start;

            while (part1 <= mid && part2 <= end)
            {
                if (tmp[part1] <= tmp[part2])
                {
                    arr[index++] = tmp[part1++];
                }
                else
                {
                    arr[index++] = tmp[part2++];
                }
            }

            for (int i = 0; i <= mid - part1; i++)
            {
                arr[index + i] = tmp[part1 + i];
            }
        }
    }
}
