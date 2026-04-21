using System;
using Lab7Lib;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            List myList = new List();
            bool initialized = false;

            do
            {
                try
                {
                    Console.WriteLine("Ініціалізація списку");
                    Console.WriteLine("1. Ввести елементи вручну");
                    Console.WriteLine("2. Згенерувати випадково");
                    string initChoice = Console.ReadLine();

                    if (initChoice == "1")
                    {
                        string input;
                        Console.WriteLine("Вводьте числа (для завершення введіть 'q'):");
                        do
                        {
                            input = Console.ReadLine();
                            if (input.ToLower() != "q")
                            {
                                if (!int.TryParse(input, out int val)) throw new ArgumentException("Значення має бути цілим числом.");
                                myList.AddElementAfterHead(val);
                            }
                        } while (input.ToLower() != "q");
                        initialized = true;
                    }
                    else if (initChoice == "2")
                    {
                        Console.Write("Кількість: ");
                        if (!int.TryParse(Console.ReadLine(), out int count) || count < 0) throw new ArgumentException("Кількість має бути додатним числом.");
                        
                        Console.Write("Мін значення: ");
                        if (!int.TryParse(Console.ReadLine(), out int min)) throw new ArgumentException("Некоректне мінімальне значення.");
                        
                        Console.Write("Макс значення: ");
                        if (!int.TryParse(Console.ReadLine(), out int max)) throw new ArgumentException("Некоректне максимальне значення.");

                        if (min > max) throw new ArgumentException("Мінімум не може бути більшим за максимум.");

                        Random rnd = new Random();
                        for (int i = 0; i < count; i++) myList.AddElementAfterHead(rnd.Next(min, max + 1));
                        initialized = true;
                    }
                    else throw new ArgumentException("Некоректний варіант ініціалізації.");
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine($"Помилка: {ex.Message}");
                }
            } while (!initialized);

            string choice;
            do
            {
                Console.WriteLine("\nПоточний список: " + string.Join(" -> ", myList));
                Console.WriteLine($"Розмір: {myList.GetSize()}");
                Console.WriteLine("Оберіть дію:");
                Console.WriteLine("1. Додати елемент після голови");
                Console.WriteLine("2. Видалити за індексом");
                Console.WriteLine("3. Отримати за індексом (через [])");
                Console.WriteLine("4. Знайти перший елемент, більший за X");
                Console.WriteLine("5. Знайти суму елементів, менших за X");
                Console.WriteLine("6. Створити новий список з елементів, більших за X");
                Console.WriteLine("7. Видалити елементи після максимального");
                Console.WriteLine("0. Вихід");

                choice = Console.ReadLine();

                if (choice != "0")
                {
                    try
                    {
                        switch (choice)
                        {
                            case "1":
                                Console.Write("Значення: ");
                                if (!int.TryParse(Console.ReadLine(), out int val1))
                                {
                                    throw new ArgumentException("Некоректне число.");
                                }

                                myList.AddElementAfterHead(val1);
                                break;
                            case "2":
                                Console.Write("Індекс: ");
                                if (!int.TryParse(Console.ReadLine(), out int idx2))
                                {
                                    throw new ArgumentException("Некоректний індекс.");
                                }

                                myList.DeleteElementByIndex(idx2);
                                break;
                            case "3":
                                Console.Write("Індекс: ");
                                if (!int.TryParse(Console.ReadLine(), out int idx3))
                                {
                                    throw new ArgumentException("Некоректний індекс.");
                                }

                                Console.WriteLine($"Елемент: {myList[idx3]}");
                                break;
                            case "4":
                                Console.Write("X: ");
                                if (!int.TryParse(Console.ReadLine(), out int x4))
                                {
                                    throw new ArgumentException("Некоректне число.");
                                }

                                Console.WriteLine($"Результат: {myList.ReturnFirstElementBiggerThan(x4)}");
                                break;
                            case "5":
                                Console.Write("X: ");
                                if (!int.TryParse(Console.ReadLine(), out int x5))
                                {
                                    throw new ArgumentException("Некоректне число.");
                                }

                                Console.WriteLine($"Сума: {myList.FindSumOfElementsLessThan(x5)}");
                                break;
                            case "6":
                                Console.Write("X: ");
                                if (!int.TryParse(Console.ReadLine(), out int x6))
                                {
                                    throw new ArgumentException("Некоректне число.");
                                }

                                var newList = myList.ReturnListOfElementsGreaterThan(x6);
                                Console.WriteLine("Новий список: " + string.Join(" -> ", newList));
                                break;
                            case "7":
                                myList.DeleteElementsLessThanBiggest();
                                Console.WriteLine("Хвіст після максимуму видалено.");
                                break;
                            default:
                                throw new ArgumentException("Некоректний пункт меню.");
                        }
                    }
                    catch (ArgumentException ex)
                    {
                        Console.WriteLine($"Помилка: {ex.Message}");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Помилка: {ex.Message}");
                    }
                }
            } while (choice != "0");
        }
    }
}
