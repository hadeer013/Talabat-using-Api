using System.ComponentModel.DataAnnotations;

namespace Talabat.Pl.Dtos
{
    public class LogInDto
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }    

    }
}
