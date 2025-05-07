using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Todo_Managment_DAL.Models
{
    public enum TodoStatus { Pending, InProgress, Completed }
    public enum TodoPriority { Low, Medium, High }
    public class Todo
    {
        public Guid Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Title { get; set; }

        public string? Description { get; set; }

        public TodoStatus Status { get; set; } = TodoStatus.Pending;

        public TodoPriority Priority { get; set; } = TodoPriority.Medium;

        public DateTime? DueDate { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

        public DateTime LastModifiedDate { get; set; } = DateTime.UtcNow;
    }
}
