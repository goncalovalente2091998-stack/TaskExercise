namespace TaskExercise.Models
{
    public class Dto
    {
        public class UpdateTaskDto
        {
            public bool IsCompleted { get; set; }
        }
        public class CreateTaskRequest
        {
            public string Title { get; set; }
        }
        public class FrontendTaskDto
        {
            public int Id { get; set; }
            public string Title { get; set; }
            public string Status { get; set; }  // "done" | "pending"
            public DateTime CreatedAt { get; set; }
        }
    }
}
