using System.ComponentModel.DataAnnotations;

namespace ControlWork.Web.Models
{
    public class Resource
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Категорія є обов'язковою")]
        public int CategoryId { get; set; }

        [Required(ErrorMessage = "Назва товару є обов'язковою")]
        [StringLength(100, MinimumLength = 2)]
        public string Name { get; set; } = string.Empty;

        // Властивість Sku: від 3 до 30 символів
        [Required(ErrorMessage = "Артикул (SKU) є обов'язковим")]
        [StringLength(30, MinimumLength = 3, ErrorMessage = "SKU повинен містити від 3 до 30 символів")]
        public string Sku { get; set; } = string.Empty;

        // Властивість Brand: від 1 до 60 символів
        [Required(ErrorMessage = "Бренд є обов'язковим")]
        [StringLength(60, MinimumLength = 1, ErrorMessage = "Бренд повинен містити від 1 до 60 символів")]
        public string Brand { get; set; } = string.Empty;

        [Range(0.01, 1000000, ErrorMessage = "Ціна повинна бути більше 0")]
        public decimal Price { get; set; }

        [Range(0, 100000, ErrorMessage = "Кількість не може бути від'ємною")]
        public int Quantity { get; set; }

        // Навігаційна властивість
        public Category? Category { get; set; }
    }
}   