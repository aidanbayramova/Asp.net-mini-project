using Asp.net_mini_project.Data;
using Asp.net_mini_project.Models;
using Asp.net_mini_project.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Asp.net_mini_project.Services
{
    public class NewsletterService : INewsletterService
    {
        private readonly AppDbContext _context;

        public NewsletterService(AppDbContext context)
        {
            _context = context;
        }


        public async Task AddAsync(string email)
        {
            if (!string.IsNullOrWhiteSpace(email))
            {
                var newsletter = new Newsletter { Email = email };
                _context.Newsletters.Add(newsletter);
                await _context.SaveChangesAsync();
            }
        }

      
        public async Task<List<Newsletter>> GetAllAsync()
        {
            return await _context.Newsletters
                .OrderByDescending(n => n.CreateDate)
                .ToListAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var newsletter = await _context.Newsletters.FindAsync(id);
            if (newsletter != null)
            {
                _context.Newsletters.Remove(newsletter);
                await _context.SaveChangesAsync();
            }
        }
        public async Task<bool> CheckEmailExistsAsync(string email)
        {
            return await _context.Newsletters.AnyAsync(n => n.Email.ToLower() == email.ToLower());
        }
       
    }
}
