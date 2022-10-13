using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SortingArgorithms
{
    public static class HeapSort
    {
        public static void Sort(int[] arr)
        {
            int length = arr.Length;
            //Heapify_TopDown(arr, length);
            Heapify_BottomUp(arr, length);

            int end = length - 1;
            while (end > 0)
            {
                int tmp = arr[0];
                arr[0] = arr[end];
                arr[end] = tmp;

                end--;
                SIFT_Down(arr, end, 1);
            }
        }

        public static void Heapify_TopDown(int[] arr, int length)
        {
            int end = 1;

            while (end < length)
            {
                SIFT_Up(arr, 0, end++);
            }
        }

        public static void Heapify_BottomUp(int[] arr, int length)
        {
            int end = length - 1;
            int current = end - 1;

            while (current >= 0)
            {
                SIFT_Down(arr, end, current--);
            }
        }

        private static void SIFT_Up(int[] arr, int root, int current)
        {
            int parent = (current - 1) / 2;
            while (current > root)
            {
                if (arr[parent] < arr[current])
                {
                    int tmp = arr[parent];
                    arr[parent] = arr[current];
                    arr[current] = tmp;

                    current = parent;
                    parent = (current - 1) / 2;
                }
                else
                {
                    break;
                }
            }
        }

        private static void SIFT_Down(int[] arr, int end, int current)
        {
            int parent = (current - 1) / 2;

            while (current <= end)
            {
                // 오른쪽 자식이 더 크면 오른쪽 자식으로 스왑할거임
                if (current + 1 <= end &&
                    arr[current] < arr[current + 1])
                    current++;

                // 부모와 비교해서 스왑할지 판단함
                if (arr[parent] < arr[current])
                {
                    int tmp = arr[parent];
                    arr[parent] = arr[current];
                    arr[current] = tmp;

                    parent = current;
                    current = parent * 2 + 1;
                }
                else
                {
                    break;
                }
            }
        }
    }

}
