namespace WebApi_Video.Models
{
    public class LivroModel
    {
        public int Id { get; set; }
        public string Titulo { get; set; } = string.Empty;
        public AutorModel Autor { get; set; }

    }
}
