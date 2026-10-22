using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ControlWork.Web.Models;
using ControlWork.Web.Services;
using System.Collections.Generic;

namespace ControlWork.Web.Pages
{
    public class CreateRequestModel : PageModel
    {
        private readonly ResourceService _resourceService;

        public CreateRequestModel(ResourceService resourceService)
        {
            _resourceService = resourceService;
        }

        [BindProperty]
        public Request ResourceRequest { get; set; } = new();

        public List<Resource> Resources { get; set; } = new();

        public void OnGet()
        {
            Resources = _resourceService.GetAllResources();
        }

        public IActionResult OnPost()
        {
            Resources = _resourceService.GetAllResources();

            if (!ModelState.IsValid)
            {
                return Page();
            }

            bool success = _resourceService.CreateRequest(ResourceRequest);

            if (success)
            {
                return RedirectToPage("/Resources");
            }

            ModelState.AddModelError(string.Empty, "Помилка при створенні заявки.");
            return Page();
        }
    }
}