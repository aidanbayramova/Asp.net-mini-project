using Asp.net_mini_project.Models;
using Asp.net_mini_project.ViewModels.Admin.Customer;

namespace Asp.net_mini_project.Services.Interfaces
{
    public interface ICustomerService
    {
        Task<IEnumerable<Customer>> GetAllAsync();
        Task<Customer> GetByIdAsync(int id);
        Task CreateAsync(CustomerCreateVM request);
        Task EditAsync(CustomerEditVM request);
        Task DeleteAsync(Customer request);
        Task<CustomerDetailVM> GetCustomerDetailAsync(int id);
    }
}
