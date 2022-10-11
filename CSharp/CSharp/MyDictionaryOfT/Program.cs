using System;
using System.Collections.Generic;
namespace MyDictionaryOfT
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            Dictionary<string, int> dictionary = new Dictionary<string, int>();
            dictionary.Add("철수", 94);
            Console.WriteLine(dictionary["철수"]);
        }
    }
}
