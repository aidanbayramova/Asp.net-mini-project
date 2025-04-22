using Asp.net_mini_project.Data;
using Asp.net_mini_project.Models;
using Asp.net_mini_project.Services.Interfaces;
using Asp.net_mini_project.ViewModels.Admin.Review;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Asp.net_mini_project.Services
{
    public class ReviewService :IReviewService
    {
        private readonly AppDbContext _context;

        public ReviewService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<ReviewVM>> GetAllAsync()
        {
            return await _context.Reviews
                .Include(r => r.Customer)
                .Select(r => new ReviewVM
                {
                    Id = r.Id,
                    Comment = r.Comment,
                    CustomerFullName = r.Customer.FullName,
                    CustomerProfileImg = r.Customer.ProfileImg
                })
                .ToListAsync();
        }

        public async Task<ReviewDetailVM> GetByIdAsync(int id)
        {
            var review = await _context.Reviews
                .Include(r => r.Customer)
                .FirstOrDefaultAsync(r => r.Id == id);

            if (review == null) return null;

            return new ReviewDetailVM
            {
                Id = review.Id,
                Comment = review.Comment,
                CustomerFullName = review.Customer.FullName,
                CustomerProfileImg = review.Customer.ProfileImg
            };
        }

        public async Task<ReviewCreateVM> GetCreateModelAsync()
        {
            var customers = await _context.Customers
                .Select(c => new SelectListItem
                {
                    Value = c.Id.ToString(),
                    Text = c.FullName
                })
                .ToListAsync();

            return new ReviewCreateVM
            {
                Consumers = customers  
            };
        }
        public async Task<ReviewEditVM> GetEditModelAsync(int id)
        {
            var review = await _context.Reviews.FindAsync(id);
            if (review == null) return null;

            var customers = await _context.Customers
                .Select(c => new SelectListItem
                {
                    Value = c.Id.ToString(),
                    Text = c.FullName
                })
                .ToListAsync();

            return new ReviewEditVM
            {
                Id = review.Id,
                Comment = review.Comment,
                CustomerId = review.ConsumerId,
                Customers = customers
            };
        }

        public async Task CreateAsync(ReviewCreateVM createVM)
        {
            if (createVM == null)
            {
                throw new ArgumentNullException(nameof(createVM), "The create model is null.");
            }

            if (string.IsNullOrEmpty(createVM.Comment))
            {
                throw new ArgumentException("Comment cannot be null or empty.");
            }

            if (createVM.ConsumerId == 0)
            {
                throw new ArgumentException("ConsumerId must be provided.");
            }

            var review = new Review
            {
                Comment = createVM.Comment,
                ConsumerId = (int)createVM.ConsumerId,
            };

           
            if (review == null)
            {
                throw new NullReferenceException("Failed to create review.");
            }

            await _context.Reviews.AddAsync(review);
            await _context.SaveChangesAsync();
        }

        public async Task EditAsync(ReviewEditVM editVM)
        {
            var review = await _context.Reviews.FindAsync(editVM.Id);
            if (review == null) return;

            review.Comment = editVM.Comment;
            review.ConsumerId = editVM.CustomerId;

            _context.Reviews.Update(review);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Review review)
        {
            _context.Reviews.Remove(review);
            await _context.SaveChangesAsync();
        }
    }
}
