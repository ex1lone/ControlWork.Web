using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ControlWork.Web.Models;
using ControlWork.Web.Services;
using System.Collections.Generic;
using System.Linq;

namespace ControlWork.Web.Pages
{
    public class ResourceCalculatorModel : PageModel
    {
        private readonly ResourceService _resourceService;

        public ResourceCalculatorModel(ResourceService resourceService)
        {
            _resourceService = resourceService;
        }

        public List<Resource> Resources { get; set; } = new();

        [BindProperty(SupportsGet = true)]
        public string SearchBrand { get; set; } = string.Empty;

        [BindProperty(SupportsGet = true)]
        public decimal MaxPrice { get; set; } = 50000;

        public void OnGet()
        {
            Resources = _resourceService.GetAllResources();
        }

        public IEnumerable<Resource> FilteredResources =>
            Resources.Where(r =>
                (string.IsNullOrWhiteSpace(SearchBrand) || r.Brand.Contains(SearchBrand, System.StringComparison.OrdinalIgnoreCase)) &&
                r.Price <= MaxPrice
            );
    }
}