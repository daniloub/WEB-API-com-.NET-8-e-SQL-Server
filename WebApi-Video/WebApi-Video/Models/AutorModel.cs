using System.Text.Json.Serialization;

namespace WebApi_Video.Models
{
    public class AutorModel
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Sobrenome { get; set; } = string.Empty;
        [JsonIgnore] // Evita referência circular na serialização JSON
        public ICollection<LivroModel> Livros { get; set; }
    }
}
