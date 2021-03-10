using System.ComponentModel.DataAnnotations;

namespace Domain.Dtos
{
    public class LoginDto
    {
        [Required(ErrorMessage = "O email é um campo obrigatório.")]
        [EmailAddress(ErrorMessage = "O email precisa ser válido.")]
        [StringLength(100, ErrorMessage = "Email deve ter no máximo {0} caracteres.")]
        public string Email { get; set; }
    }
}
