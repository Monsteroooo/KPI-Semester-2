using System;
using System.Linq;
using TravelAgency.Models;

namespace TravelAgency.App
{
    internal class Program
    {
        static void Main(string[] args)
        {
            AppData data = DataStorage.Load();
            if (data.Users.Count == 0) SeedTestData(data);

            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.WriteLine("=== Система бронювання авіаквитків ===\n");

            User? currentUser = null;
            while (currentUser == null)
            {
                currentUser = ShowAuthMenu(data);
            }

            if (currentUser.Role == UserRole.Agent)
                ShowAgentMenu(data, currentUser);
            else
                ShowClientMenu(data, currentUser);

            Console.WriteLine("\nДо побачення!");
        }

        static User? ShowAuthMenu(AppData data)
        {
            Console.WriteLine("1 — Увійти");
            Console.WriteLine("2 — Зареєструватися");
            Console.WriteLine("0 — Вийти з програми");
            Console.Write("\nВаш вибір: ");
            string choice = Console.ReadLine() ?? "";

            switch (choice)
            {
                case "1": return HandleLogin(data);
                case "2": return HandleRegister(data);
                case "0": Environment.Exit(0); return null;
                default: Console.WriteLine("Невірний вибір.\n"); return null;
            }
        }

        static User? HandleLogin(AppData data)
        {
            Console.Write("Логін: ");
            string username = Console.ReadLine() ?? "";
            Console.Write("Пароль: ");
            string password = ReadPassword();

            User? user = AuthService.Login(data, username, password);
            if (user == null) { Console.WriteLine("Невірний логін або пароль.\n"); return null; }

            Console.WriteLine($"\nВітаємо, {user.FullName}! Роль: {user.Role}\n");
            return user;
        }

        static User? HandleRegister(AppData data)
        {
            Console.Write("Повне ім'я: ");
            string fullName = Console.ReadLine() ?? "";
            Console.Write("Логін: ");
            string username = Console.ReadLine() ?? "";
            Console.Write("Пароль: ");
            string password = ReadPassword();

            Console.WriteLine("Оберіть роль: 1 — Клієнт, 2 — Турагент");
            string roleChoice = Console.ReadLine() ?? "";
            UserRole role = roleChoice == "2" ? UserRole.Agent : UserRole.Client;

            User? newUser = AuthService.Register(data, username, password, fullName, role);
            if (newUser == null) { Console.WriteLine("Логін зайнятий.\n"); return null; }

            Console.WriteLine($"\nРеєстрація успішна! Роль: {newUser.Role}\n");
            return newUser;
        }

        static void ShowClientMenu(AppData data, User user)
        {
            while (true)
            {
                Console.WriteLine("\n=== Меню клієнта ===");
                Console.WriteLine("1 — Переглянути доступні рейси");
                Console.WriteLine("2 — Забронювати рейс");
                Console.WriteLine("3 — Мої бронювання");
                Console.WriteLine("4 — Оплатити бронювання");
                Console.WriteLine("5 — Скасувати бронювання");
                Console.WriteLine("0 — Вийти");
                Console.Write("Вибір: ");
                
                switch (Console.ReadLine())
                {
                    case "1": ListFlights(data); break;
                    case "2": BookFlight(data, user); break;
                    case "3": ListMyBookings(data, user); break;
                    case "4": PayBooking(data, user); break;
                    case "5": CancelBooking(data, user); break;
                    case "0": return;
                    default: Console.WriteLine("Невірний вибір."); break;
                }
            }
        }

        static void ShowAgentMenu(AppData data, User user)
        {
            while (true)
            {
                Console.WriteLine("\n=== Меню турагента ===");
                Console.WriteLine("1 — Переглянути всі рейси");
                Console.WriteLine("2 — Додати рейс");
                Console.WriteLine("3 — Всі бронювання");
                Console.WriteLine("0 — Вийти");
                Console.Write("Вибір: ");

                switch (Console.ReadLine())
                {
                    case "1": ListFlights(data); break;
                    case "2": AddFlight(data); break;
                    case "3": ListAllBookings(data); break;
                    case "0": return;
                    default: Console.WriteLine("Невірний вибір."); break;
                }
            }
        }

        static void ListFlights(AppData data)
        {
            Console.WriteLine("\n--- Доступні рейси ---");
            foreach (Flight f in data.Flights)
                Console.WriteLine($"[{f.Id}] {f.FlightNumber}: {f.Origin} → {f.Destination} | Ціна: {f.Price} грн | Місць: {f.AvailableSeats}");
        }

        static void BookFlight(AppData data, User user)
        {
            ListFlights(data);
            Console.Write("\nВведіть ID рейсу: ");
            if (!int.TryParse(Console.ReadLine(), out int flightId)) return;

            Flight? flight = data.Flights.FirstOrDefault(f => f.Id == flightId);
            if (flight == null || flight.AvailableSeats <= 0) { Console.WriteLine("Рейс не знайдено або немає місць."); return; }

            int newId = data.Bookings.Count > 0 ? data.Bookings.Max(b => b.Id) + 1 : 1;
            Booking booking = new() { Id = newId, UserId = user.Id, FlightId = flight.Id, BookingDate = DateTime.Now, Status = "Pending Payment" };

            flight.AvailableSeats--;
            data.Bookings.Add(booking);
            DataStorage.Save(data);
            Console.WriteLine($"Бронювання #{booking.Id} успішно створено. Очікує оплати.");
        }

        static void ListMyBookings(AppData data, User user)
        {
            Console.WriteLine("\n--- Мої бронювання ---");
            foreach (Booking b in data.Bookings.Where(b => b.UserId == user.Id))
            {
                Flight? f = data.Flights.FirstOrDefault(x => x.Id == b.FlightId);
                Console.WriteLine($"#{b.Id} | Рейс: {f?.FlightNumber} | Статус: {b.Status}");
            }
        }

        static void ListAllBookings(AppData data)
        {
            Console.WriteLine("\n--- Всі бронювання ---");
            foreach (Booking b in data.Bookings)
            {
                User? u = data.Users.FirstOrDefault(x => x.Id == b.UserId);
                Flight? f = data.Flights.FirstOrDefault(x => x.Id == b.FlightId);
                Console.WriteLine($"#{b.Id} | Клієнт: {u?.FullName} | Рейс: {f?.FlightNumber} | Статус: {b.Status}");
            }
        }

        static void AddFlight(AppData data)
        {
            Console.WriteLine("\n--- Додати новий рейс ---");
            Console.Write("Номер рейсу: "); string fn = Console.ReadLine() ?? "";
            Console.Write("Відправлення: "); string orig = Console.ReadLine() ?? "";
            Console.Write("Призначення: "); string dest = Console.ReadLine() ?? "";
            Console.Write("Ціна: "); decimal.TryParse(Console.ReadLine(), out decimal price);
            Console.Write("Кількість місць: "); int.TryParse(Console.ReadLine(), out int seats);

            int newId = data.Flights.Count > 0 ? data.Flights.Max(f => f.Id) + 1 : 1;
            data.Flights.Add(new Flight { Id = newId, FlightNumber = fn, Origin = orig, Destination = dest, Price = price, TotalSeats = seats, AvailableSeats = seats });
            DataStorage.Save(data);
            Console.WriteLine("Рейс додано!");
        }

        static void PayBooking(AppData data, User user)
        {
            ListMyBookings(data, user);
            Console.Write("\nВведіть ID бронювання для оплати: ");
            if (!int.TryParse(Console.ReadLine(), out int bookingId)) return;

            Booking? booking = data.Bookings.FirstOrDefault(b => b.Id == bookingId && b.UserId == user.Id);
            if (booking == null) { Console.WriteLine("Бронювання не знайдено."); return; }
            if (booking.Status == "Paid") { Console.WriteLine("Вже оплачено!"); return; }

            Console.WriteLine("З'єднання з платіжним шлюзом... Оплата пройшла успішно!");
            booking.Status = "Paid";
            DataStorage.Save(data);
        }

        static void CancelBooking(AppData data, User user)
        {
            ListMyBookings(data, user);
            Console.Write("\nВведіть ID бронювання для скасування: ");
            if (!int.TryParse(Console.ReadLine(), out int bookingId)) return;

            Booking? booking = data.Bookings.FirstOrDefault(b => b.Id == bookingId && b.UserId == user.Id);
            if (booking == null) { Console.WriteLine("Бронювання не знайдено."); return; }

            Flight? flight = data.Flights.FirstOrDefault(f => f.Id == booking.FlightId);
            if (flight != null) flight.AvailableSeats++;

            data.Bookings.Remove(booking);
            DataStorage.Save(data);
            Console.WriteLine("Бронювання скасовано. Місце повернено.");
        }

        static string ReadPassword()
        {
            string pass = "";
            ConsoleKeyInfo key;
            do
            {
                key = Console.ReadKey(true);
                if (key.Key != ConsoleKey.Enter && key.Key != ConsoleKey.Backspace) { pass += key.KeyChar; Console.Write("*"); }
                else if (key.Key == ConsoleKey.Backspace && pass.Length > 0) { pass = pass[..^1]; Console.Write("\b \b"); }
            } while (key.Key != ConsoleKey.Enter);
            Console.WriteLine(); return pass;
        }

        static void SeedTestData(AppData data)
        {
            AuthService.Register(data, "agent", "agent123", "Іван Агентів", UserRole.Agent);
            AuthService.Register(data, "client", "client123", "Марія Клієнтова", UserRole.Client);
            data.Flights.Add(new Flight { Id = 1, FlightNumber = "PS101", Origin = "Київ", Destination = "Варшава", Price = 3500, TotalSeats = 150, AvailableSeats = 150 });
            DataStorage.Save(data);
        }
    }
}
