using Asp.net_mini_project.Services.Interfaces;
using Asp.net_mini_project.ViewModels.Admin.Customer;
using FiorelloBackendPB103.Helpers.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Asp.net_mini_project.Areas.Admin.Controllers
{
   
        [Area("Admin")]

        public class CustomerController : Controller
        {
            private readonly ICustomerService _customerService;
            private readonly IWebHostEnvironment _env;

            public CustomerController(ICustomerService customerService, IWebHostEnvironment env)
            {
                _customerService = customerService;
                _env = env;
            }
             [Authorize(Roles = "Admin,SuperAdmin")]
            public async Task<IActionResult> Index()
            {
                var customers = await _customerService.GetAllAsync();
                var customerVMs = customers.Select(c => new CustomerVM
                {
                    Id = c.Id,
                    FullName = c.FullName,
                    ProfileImg = c.ProfileImg
                });

                return View(customerVMs);
            }

            [HttpGet]
           [Authorize(Roles = "SuperAdmin")]
            public IActionResult Create()
            {
                return View();
            }

            [HttpPost]
            [Authorize(Roles = "SuperAdmin")]

            public async Task<IActionResult> Create(CustomerCreateVM request)
            {
                if (!ModelState.IsValid)
                    return View(request);

                if (request.ProfileImageFile.CheckFilesSize(200))
                {
                    ModelState.AddModelError("ProfileImageFile", "Image size must be max 200KB");
                    return View(request);
                }

                if (!request.ProfileImageFile.CheckFileType("image/"))
                {
                    ModelState.AddModelError("ProfileImageFile", "Only image files are allowed");
                    return View(request);
                }

                await _customerService.CreateAsync(request);
                return RedirectToAction(nameof(Index));
            }

            [HttpGet]
            [Authorize(Roles = "SuperAdmin")]
            public async Task<IActionResult> Edit(int id)
            {
                var customer = await _customerService.GetByIdAsync(id);
                if (customer == null) return NotFound();

                var customerEditVM = new CustomerEditVM
                {
                    Id = customer.Id,
                    FullName = customer.FullName,
                    ProfileImg = customer.ProfileImg
                };

                return View(customerEditVM);
            }

            [HttpPost]
            [Authorize(Roles = "SuperAdmin")]
            public async Task<IActionResult> Edit(CustomerEditVM request)
            {

                if (request.NewProfileImage != null)
                {
                    if (request.NewProfileImage.CheckFilesSize(200))
                    {
                        ModelState.AddModelError("NewProfileImage", "Image size must be max 200KB");
                        return View(request);
                    }

                    if (!request.NewProfileImage.CheckFileType("image/"))
                    {
                        ModelState.AddModelError("NewProfileImage", "Only image files are allowed");
                        return View(request);
                    }
                }

                await _customerService.EditAsync(request);
                return RedirectToAction(nameof(Index));
            }

            [HttpPost]
            [Authorize(Roles = "SuperAdmin")]
            public async Task<IActionResult> Delete(int id)
            {
                var customer = await _customerService.GetByIdAsync(id);
                if (customer == null) return NotFound();

                await _customerService.DeleteAsync(customer);
                return RedirectToAction(nameof(Index));
            }

            [HttpGet]
            [Authorize(Roles = "Admin,SuperAdmin")]
            public async Task<IActionResult> Detail(int id)
            {
                var customerDetail = await _customerService.GetCustomerDetailAsync(id);
                if (customerDetail == null) return NotFound();

                return View(customerDetail);
            }
        }
    }
