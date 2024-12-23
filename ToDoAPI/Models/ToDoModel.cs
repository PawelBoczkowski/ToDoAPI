namespace ToDoAPI.Models
{
    public class ToDoModel
    {
        public int? Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public DateTime? ExpireDateTime { get; set; }
        public int? PercentComplete { get; set; }
        public bool? isDone { get; set; }
    }
}
