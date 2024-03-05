using System.ComponentModel.DataAnnotations;

namespace ChatModels
{
    public class Chat
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string? Message { get; set; }
        [Required]
        public string? UserName { get; set; }

        public DateTime DateTime { get; set; }
    }
}
