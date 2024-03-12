using System;
using System.Data;
using System.Data.SqlClient;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using StaycationHotel.Context;
using StaycationHotel.Helper;

namespace StaycationHotel.Models
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;

        public UserRepository(AppDbContext context)
        {
            _context = context;
        }

        //public async Task<User> AddUser(User user)
        //{
        //    _context.Users.Add(user);
        //    _context.SaveChanges();
        //    return user;
        //}
        public async Task<Response> AddUserAsync(User user)
        {
            var existingUser = _context.Users.FirstOrDefault(u => u.Email == user.Email);
            if (existingUser != null)
            {
                return Response.EmailAlreadyExist;
            }
            else
            {
                _context.Users.Add(user);
                _context.SaveChanges();
            }
         
            return Response.Success;
            
            
        }

        public async Task<User> LoginUser(string email, string password)
        {
            var loggedInUser = _context.Users.FirstOrDefault(x => x.Email == email &&  x.Password == password);
            return loggedInUser;
        }

        private bool IsValidPassword(string password)
        {
            // Add your password validation rules here
            // For example, you can check length, complexity, etc.
            return !string.IsNullOrWhiteSpace(password) && password.Length >= 8;
        }

        private string HashPassword(string password)
        {
            // Implement a secure password hashing mechanism here
            // Consider using a library like BCrypt or Identity's PasswordHasher
            // For demonstration purposes, we'll use a simple hash
            return Convert.ToBase64String(System.Security.Cryptography.SHA256.Create().ComputeHash(System.Text.Encoding.UTF8.GetBytes(password)));
        }


        private bool IsValidEmail(string email)
        {
            try
            {
                // Use a simple regular expression for email validation
                var regex = new Regex(@"^[\w-]+(\.[\w-]+)*@([\w-]+\.)+[a-zA-Z]{2,7}$");
                return regex.IsMatch(email);
            }
            catch (RegexMatchTimeoutException)
            {
                return false;
            }
        }



    }
}
