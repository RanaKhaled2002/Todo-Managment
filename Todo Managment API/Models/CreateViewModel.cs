using System.ComponentModel.DataAnnotations;

namespace Todo_Managment_API.Models
{
    public class CreateViewModel
    {

        [Required]
        [MaxLength(100)]
        public string Title { get; set; }

        public string? Description { get; set; }

        public string Status { get; set; } = "Pending";

        public string Priority { get; set; } = "Medium";

        public DateTime? DueDate { get; set; }
    }
}
