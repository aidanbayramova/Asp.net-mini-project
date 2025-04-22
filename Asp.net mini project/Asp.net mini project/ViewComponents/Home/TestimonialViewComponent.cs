using Asp.net_mini_project.Models;
using Asp.net_mini_project.Services.Interfaces;
using Asp.net_mini_project.ViewModels.Admin.Review;
using Microsoft.AspNetCore.Mvc;

namespace Asp.net_mini_project.ViewComponents.Home
{
    public class TestimonialViewComponent : ViewComponent
    {
        private readonly ICustomerService _customerService;
        private readonly IReviewService _reviewService;
        public TestimonialViewComponent(ICustomerService customerService,
                                        IReviewService reviewService)
        {
            _customerService = customerService;
            _reviewService = reviewService;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            IEnumerable<Customer> customers = await _customerService.GetAllAsync();
            IEnumerable<ReviewVM> reviews = await _reviewService.GetAllAsync();

            return await Task.FromResult(View(new CustomerVMVC { Customers = customers, Reviews = reviews }));
        }
    }

    public class CustomerVMVC
    {
        public IEnumerable<Customer> Customers { get; set; }
        public IEnumerable<ReviewVM> Reviews { get; set; }
    }
}

