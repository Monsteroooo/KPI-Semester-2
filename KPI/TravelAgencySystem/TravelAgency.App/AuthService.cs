using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using TravelAgency.Models;

namespace TravelAgency.App
{
    public static class AuthService
    {
        private static string HashPassword(string password)
        {
            byte[] bytes = SHA256.HashData(Encoding.UTF8.GetBytes(password));
            return Convert.ToHexString(bytes).ToLower();
        }

        public static User? Register(AppData data, string username, string password, string fullName, UserRole role)
        {
            bool alreadyExists = data.Users.Any(u => u.Username.Equals(username, StringComparison.OrdinalIgnoreCase));
            if (alreadyExists) return null;

            int newId = data.Users.Count > 0 ? data.Users.Max(u => u.Id) + 1 : 1;

            User newUser = new()
            {
                Id = newId,
                Username = username,
                PasswordHash = HashPassword(password),
                FullName = fullName,
                Role = role
            };

            data.Users.Add(newUser);
            DataStorage.Save(data);
            return newUser;
        }

        public static User? Login(AppData data, string username, string password)
        {
            string hash = HashPassword(password);
            return data.Users.FirstOrDefault(u =>
                u.Username.Equals(username, StringComparison.OrdinalIgnoreCase) &&
                u.PasswordHash == hash);
        }
    }
}
