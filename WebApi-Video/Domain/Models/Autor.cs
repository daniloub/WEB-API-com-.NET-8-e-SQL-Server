using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Domain.Models
{
    public class Autor
    {
        public int Id { get; set; }
        
        [Required(ErrorMessage = "Nome é obrigatório")]
        [StringLength(100, ErrorMessage = "Nome deve ter no máximo 100 caracteres")]
        public string Nome { get; set; } = string.Empty;
        
        [Required(ErrorMessage = "Sobrenome é obrigatório")]
        [StringLength(100, ErrorMessage = "Sobrenome deve ter no máximo 100 caracteres")]
        public string Sobrenome { get; set; } = string.Empty;
        
        public string NomeCompleto => $"{Nome} {Sobrenome}".Trim();
        
        // Propriedade de navegação
        [JsonIgnore]
        public virtual ICollection<Livro> Livros { get; set; } = new List<Livro>();
        
        // Métodos de domínio
        public void AtualizarNome(string nome, string sobrenome)
        {
            if (string.IsNullOrWhiteSpace(nome))
                throw new ArgumentException("Nome não pode ser vazio", nameof(nome));
                
            if (string.IsNullOrWhiteSpace(sobrenome))
                throw new ArgumentException("Sobrenome não pode ser vazio", nameof(sobrenome));
                
            Nome = nome.Trim();
            Sobrenome = sobrenome.Trim();
        }
        
        public bool PossuiLivros => Livros?.Any() == true;
    }
}
