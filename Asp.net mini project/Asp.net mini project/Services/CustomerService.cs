using Asp.net_mini_project.Data;
using Asp.net_mini_project.Models;
using Asp.net_mini_project.Services.Interfaces;
using Asp.net_mini_project.ViewModels.Admin.Customer;
using Asp.net_mini_project.ViewModels.Admin.Review;
using Microsoft.EntityFrameworkCore;

namespace Asp.net_mini_project.Services
{
    public class CustomerService :ICustomerService
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;

        public CustomerService(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        public async Task<IEnumerable<Customer>> GetAllAsync()
        {
            return await _context.Customers
                                 .Include(entry => entry.Reviews)
                                 .ToListAsync();
        }

        public async Task<Customer> GetByIdAsync(int userId)
        {
            return await _context.Customers.FindAsync(userId);
        }

        public async Task CreateAsync(CustomerCreateVM inputModel)
        {
            string uniqueImageName = Guid.NewGuid().ToString() + "-" + inputModel.ProfileImageFile.FileName;
            string storagePath = Path.Combine(_env.WebRootPath, "img", uniqueImageName);

            using (FileStream fileStream = new(storagePath, FileMode.Create))
            {
                await inputModel.ProfileImageFile.CopyToAsync(fileStream);
            }

            var newCustomer = new Customer
            {
                FullName = inputModel.FullName,
                ProfileImg = uniqueImageName
            };

            await _context.Customers.AddAsync(newCustomer);
            await _context.SaveChangesAsync();
        }

        public async Task EditAsync(CustomerEditVM updateModel)
        {
            var existingCustomer = await _context.Customers.FindAsync(updateModel.Id);

            if (existingCustomer != null)
            {
                if (updateModel.NewProfileImage != null)
                {
                    string previousImagePath = Path.Combine(_env.WebRootPath, "img", existingCustomer.ProfileImg);
                    if (System.IO.File.Exists(previousImagePath))
                    {
                        System.IO.File.Delete(previousImagePath);
                    }

                    string newImageName = Guid.NewGuid().ToString() + "-" + updateModel.NewProfileImage.FileName;
                    string newImagePath = Path.Combine(_env.WebRootPath, "img", newImageName);

                    using (FileStream imgStream = new(newImagePath, FileMode.Create))
                    {
                        await updateModel.NewProfileImage.CopyToAsync(imgStream);
                    }

                    existingCustomer.ProfileImg = newImageName;
                }

                existingCustomer.FullName = updateModel.FullName;

                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteAsync(Customer selectedCustomer)
        {
            string imgPath = Path.Combine(_env.WebRootPath, "img", selectedCustomer.ProfileImg);
            if (System.IO.File.Exists(imgPath))
            {
                System.IO.File.Delete(imgPath);
            }

            _context.Customers.Remove(selectedCustomer);
            await _context.SaveChangesAsync();
        }

        public async Task<CustomerDetailVM> GetCustomerDetailAsync(int customerId)
        {
            var foundCustomer = await _context.Customers
                .Include(x => x.Reviews)
                .FirstOrDefaultAsync(x => x.Id == customerId);

            if (foundCustomer == null) return null;

            return new CustomerDetailVM
            {
                Id = foundCustomer.Id,
                FullName = foundCustomer.FullName,
                ProfileImg = foundCustomer.ProfileImg,
                Reviews = foundCustomer.Reviews.Select(comment => new ReviewVM
                {
                    Id = comment.Id,
                    Comment = comment.Comment
                }).ToList()
            };
        }

    }
}
