using System;
using System.Collections.Generic;
using System.Linq;

namespace Project
{
    public class Task
    {
        public int[] num = new int[20];
        
        public List<(int Index, int Value)> a = new List<(int Index, int Value)>(); 
        public List<(int Index, int Value)> b = new List<(int Index, int Value)>();

        public Task()
        {
            Random random = new Random();
            for (int i = 0; i < num.Length; i++)
            {
                num[i] = random.Next(-30, 54);
            }
        }

        public static void Main()
        {
            Task task = new Task();
            task.PrintNum();
            task.FindAndDistribute();
        }

        public void FindAndDistribute()
        {
            List<List<(int Index, int Value)>> allSubsequences = new List<List<(int Index, int Value)>>();
            List<(int Index, int Value)> currentSubsequence = new List<(int Index, int Value)> { (0, num[0]) };

            for (int i = 1; i < num.Length; i++)
            {
                if (num[i] > num[i - 1])
                {
                    currentSubsequence.Add((i, num[i]));
                }
                else
                {
                    allSubsequences.Add(new List<(int Index, int Value)>(currentSubsequence));
                    currentSubsequence.Clear();
                    currentSubsequence.Add((i, num[i]));
                }
            }
            
            allSubsequences.Add(new List<(int Index, int Value)>(currentSubsequence));

            Console.WriteLine("\nЗнайдені підпослідовності:");
            
            for (int i = 0; i < allSubsequences.Count; i++)
            {
                string formattedSubsequence = string.Join(", ", allSubsequences[i].Select(item => $"[{item.Index}]={item.Value}"));
                Console.WriteLine($"Підпослідовність №{i}: {formattedSubsequence}");
                
                if (i % 2 == 0)
                {
                    a.AddRange(allSubsequences[i]);
                }
                else
                {
                    b.AddRange(allSubsequences[i]);
                }
            }

            (int Index, int Value)[] arrayA = a.ToArray();
            (int Index, int Value)[] arrayB = b.ToArray();

            Console.WriteLine("\nРезультат:");
            Console.WriteLine("Масив A (парні підпослідовності):");
            Console.WriteLine(string.Join(", ", arrayA.Select(item => $"[{item.Index}]={item.Value}")));
            
            Console.WriteLine("\nМасив B (непарні підпослідовності):");
            Console.WriteLine(string.Join(", ", arrayB.Select(item => $"[{item.Index}]={item.Value}")));
        }

        public void PrintNum()
        {
            Console.WriteLine("Початковий масив num (індекс: значення):");
            for (int i = 0; i < num.Length; i++)
            {
                Console.Write($"[{i}]:{num[i]}  ");
            }
            Console.WriteLine();
        }
    }
}