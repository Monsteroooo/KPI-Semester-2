using System;

//Задано рядок слів розділених пробілом, знайти найдовше й найкоротше слово.

namespace Project;

public class Task
{
    public string[] text = new string[] { };

    public Task()
    {
        text = Console.ReadLine().Split(' ');
    }

    public static void Main()
    {
        Task task = new Task();
        task.Find();
    }

    public void Find()
    {
            string longest = text[0];
            string shortest = text[0];
            for (int i = 1; i < text.Length; i++)
            {
                if (text[i].Length > longest.Length)
                {
                    longest = text[i];
                }
                if (text[i].Length < shortest.Length)
                {
                    shortest = text[i];
                }
            }
    
            Console.WriteLine($"Найдовше слово: {longest}");
            Console.WriteLine($"Найкоротше слово: {shortest}");   
    }
}