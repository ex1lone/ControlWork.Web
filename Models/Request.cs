using System.ComponentModel.DataAnnotations;

namespace ControlWork.Web.Models
{
    public class Request
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Виберіть товар")]
        public int ResourceId { get; set; }

        [Required(ErrorMessage = "Вкажіть кількість")]
        [Range(1, 10000, ErrorMessage = "Кількість для резерву повинна бути не менше 1")]
        public int Quantity { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        // Навігаційна властивість
        public Resource? Resource { get; set; }
    }
}