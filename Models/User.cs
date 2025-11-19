using System.ComponentModel.DataAnnotations;

namespace ASP.NETTask.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;
        [Required]
        [StringLength(100)]
        public string Username { get; set; } = string.Empty;
        [Required]
        [StringLength(200)]
        public string Password { get; set; } =string.Empty;
        [Required]
        [StringLength(200)]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;
        [Required]
        [StringLength(30)]
        [Phone]
        public string Phone { get; set; } = string.Empty;
        [Required]
        public string Website { get; set; } = string.Empty;
        public string Note { get; set; } = string.Empty;
        public byte IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
        public Address? Address { get; set; }
    }
}
