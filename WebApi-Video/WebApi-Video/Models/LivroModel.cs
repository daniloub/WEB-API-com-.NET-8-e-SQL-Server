using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi_Video.Models
{
    public class LivroModel
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Titulo { get; set; } = string.Empty;
        public AutorModel Autor { get; set; }

    }
}
