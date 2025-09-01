using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Domain.Models
{
    public class Livro
    {
        public int Id { get; set; }
        
        [Required(ErrorMessage = "Título é obrigatório")]
        [StringLength(200, ErrorMessage = "Título deve ter no máximo 200 caracteres")]
        public string Titulo { get; set; } = string.Empty;
        
        [Required(ErrorMessage = "Autor é obrigatório")]
        public int AutorId { get; set; }
        
        // Propriedade de navegação
        [JsonIgnore]
        public virtual Autor Autor { get; set; } = null!;
        
        // Métodos de domínio
        public void AtualizarTitulo(string titulo)
        {
            if (string.IsNullOrWhiteSpace(titulo))
                throw new ArgumentException("Título não pode ser vazio", nameof(titulo));
                
            Titulo = titulo.Trim();
        }
        
        public void AtribuirAutor(Autor autor)
        {
            Autor = autor ?? throw new ArgumentNullException(nameof(autor));
            AutorId = autor.Id;
        }
        
        public bool TemAutor => Autor != null;
    }
}
