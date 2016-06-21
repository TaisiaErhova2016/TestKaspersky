using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestKaspersky
{
    class Program
    {
        static void Main(string[] args)
        {
            //Задача 2. Первый вариант решения
            Int32 X = 10;
            var numbers = new Int32[] { 1, 2, 3, 4, 5, 6, 7, 8 };
            //Циклом
            Array.Sort(numbers);
            int first = 0;
            int last = numbers.Count() - 1;
            while (first < last)
            {
                int s = numbers[first] + numbers[last];
                if (s == X)
                {
                    Console.WriteLine(numbers[first] + " " + numbers[last]);
                    first++;
                    last--;
                }
                else
                {
                    if (s < X) first++;
                    else last--;
                }
            }
            Console.ReadLine();

            //Linq to Object
            var numbersLinq =
                (from Item in numbers
                 join Item2 in numbers
                 on numbers[Array.IndexOf(numbers, Item)] equals (X - numbers[Array.IndexOf(numbers, Item2)])
                 where Array.IndexOf(numbers, Item) != Array.IndexOf(numbers, Item2)
                        &&
                        numbers[Array.IndexOf(numbers, Item)] <= numbers[Array.IndexOf(numbers, Item2)]
                 select new
                 {
                     FirstNumber = numbers[Array.IndexOf(numbers, Item)],
                     SecondNumber = numbers[Array.IndexOf(numbers, Item2)]
                 }
                 );

            foreach (var n in numbersLinq)
            {
                Console.WriteLine("{0} + {1} = {2}", n.FirstNumber, n.SecondNumber, X);
            }
            Console.ReadLine();
        }
    }

        
}
