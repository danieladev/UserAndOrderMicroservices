using System.Collections.Generic;
using System.Threading.Tasks;
using UserService.Models;
using Microsoft.EntityFrameworkCore;
using UserService.Data;
using System;

namespace UserService.Services
{
    public class OrderService
    {
        private readonly UserContext _context;

        public OrderService(UserContext context)
        {
            _context = context;
        }

        public async Task<List<Order>> GetUsersAsync()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<Order> GetUserByIdAsync(int id)
        {
            return await _context.Users.FindAsync(id);
        }

        public async Task<Order> CreateUserAsync(Order order)
        {
            order.CreatedAt = DateTime.UtcNow;
            order.UpdatedAt = DateTime.UtcNow;
            _context.Users.Add(order);
            await _context.SaveChangesAsync();
            return order;
        }

        public async Task<Order> UpdateUserAsync(Order order)
        {
            var existingUser = await _context.Users.FindAsync(order.Id);
            if (existingUser == null)
            {
                return null;
            }
            existingUser.Name = order.Name;
            existingUser.Cpf = order.Cpf;
            existingUser.Email = order.Email;
            existingUser.PhoneNumber = order.PhoneNumber;
            existingUser.UpdatedAt = DateTime.UtcNow;
            await _context.SaveChangesAsync();
            return existingUser;
        }

        public async Task<bool> DeleteUserAsync(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return false;
            }
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}