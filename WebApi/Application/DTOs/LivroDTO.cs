using System.ComponentModel.DataAnnotations;

namespace Application.DTOs
{
    public class LivroDTO
    {
        [Required(ErrorMessage = "Título é obrigatório")]
        [StringLength(200, ErrorMessage = "Título deve ter no máximo 200 caracteres")]
        public string Titulo { get; set; } = string.Empty;
        
        [Required(ErrorMessage = "Autor é obrigatório")]
        [Range(1, int.MaxValue, ErrorMessage = "ID do autor deve ser maior que 0")]
        public int AutorId { get; set; }
    }
    
    public class LivroResponseDTO
    {
        public int Id { get; set; }
        public string Titulo { get; set; } = string.Empty;
        public AutorResponseDTO Autor { get; set; } = null!;
    }
    
    public class LivroUpdateDTO
    {
        [Required(ErrorMessage = "Título é obrigatório")]
        [StringLength(200, ErrorMessage = "Título deve ter no máximo 200 caracteres")]
        public string Titulo { get; set; } = string.Empty;
        
        [Required(ErrorMessage = "Autor é obrigatório")]
        [Range(1, int.MaxValue, ErrorMessage = "ID do autor deve ser maior que 0")]
        public int AutorId { get; set; }
    }
}
