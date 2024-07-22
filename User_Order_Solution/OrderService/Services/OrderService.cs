using System.Collections.Generic;
using System.Threading.Tasks;
using OrderService.Models;
using Microsoft.EntityFrameworkCore;
using OrderService.Data;
using System;

namespace OrderService.Services
{
    public class OrderService
    {
        private readonly OrderContext _context;

        public OrderService(OrderContext context)
        {
            _context = context;
        }

        public async Task<List<Order>> GetOrdersAsync()
        {
            return await _context.Orders.ToListAsync();
        }

        public async Task<Order> GetOrderByIdAsync(int id)
        {
            return await _context.Orders.FindAsync(id);
        }

        public async Task<Order> CreateOrderAsync(Order order)
        {
            order.CreatedAt = DateTime.UtcNow;
            order.UpdatedAt = DateTime.UtcNow;
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();
            return order;
        }

        public async Task<Order> UpdateOrderAsync(Order order)
        {
            var existingOrder = await _context.Orders.FindAsync(order.Id);
            if (existingOrder == null)
            {
                return null;
            }
            existingOrder.UserId = order.UserId;
            existingOrder.ItemDescription = order.ItemDescription;
            existingOrder.ItemQuantity = order.ItemQuantity;
            existingOrder.ItemPrice = order.ItemPrice;
            existingOrder.TotalValue = order.TotalValue;
            existingOrder.UpdatedAt = DateTime.UtcNow;
            await _context.SaveChangesAsync();
            return existingOrder;
        }

        public async Task<bool> DeleteOrderAsync(int id)
        {
            var order = await _context.Orders.FindAsync(id);
            if (order == null)
            {
                return false;
            }
            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}