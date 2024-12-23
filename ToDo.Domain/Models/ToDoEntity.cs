using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDo.Domain.Models
{
    public class ToDoEntity
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public DateTime? ExpireDateTime { get; set; }
        public int? PercentComplete { get; set; }
        public bool? isDone { get; set; }
    }
}
