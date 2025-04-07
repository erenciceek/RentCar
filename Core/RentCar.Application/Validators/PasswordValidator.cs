using RentCar.Domain.Helpers;
using System;

namespace RentCar.Application.Validators
{
    public static class PasswordValidator
    {
        public static void ValidatePassword(string password)
        {
            if (string.IsNullOrWhiteSpace(password))
                throw new ArgumentException("Password cannot be empty", nameof(password));

            if (!PasswordHashHelper.IsPasswordValid(password))
                throw new ArgumentException("Password does not meet security requirements. Password must be at least 8 characters long and contain at least one uppercase letter, one lowercase letter, one number and one special character.");
        }

        public static string HashPassword(string password)
        {
            return PasswordHashHelper.HashPassword(password);
        }
    }
} 