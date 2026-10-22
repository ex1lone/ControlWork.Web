using System.ComponentModel.DataAnnotations;

namespace ControlWork.Web.Models
{
    public class Category
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Назва категорії є обов'язковою")]
        [StringLength(100, MinimumLength = 2)]
        public string Name { get; set; } = string.Empty;

        [StringLength(250)]
        public string? Description { get; set; }
    }
}