using System.ComponentModel.DataAnnotations;

namespace Lab11.Domain.Entities
{
    public class Usuario
    {
        [Key]
        public int UserId { get; set; }

        public string Username { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string Email { get; set; } = null!;
        public DateTime FechaCreacion { get; set; }
    }
}