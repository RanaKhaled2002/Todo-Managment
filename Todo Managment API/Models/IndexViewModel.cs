using System.ComponentModel.DataAnnotations;

namespace Todo_Managment_API.Models
{
    public class IndexViewModel
    {
        public Guid Id { get; set; }

        [MaxLength(100)]
        public string Title { get; set; }

        public string Status { get; set; }

        public string Priority { get; set; }

        public DateTime? DueDate { get; set; }

        public DateTime LastModifiedDate { get; set; }

    }
}
