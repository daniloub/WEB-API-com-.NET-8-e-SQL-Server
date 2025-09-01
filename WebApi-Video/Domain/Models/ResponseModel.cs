namespace Domain.Models
{
    public class ResponseModel<T>
    {
        public T? Dados { get; set; }
        public string Mensagem { get; set; } = string.Empty;
        public bool Status { get; set; } = true;
        public List<string> Erros { get; set; } = new List<string>();
        
        // Construtores estáticos para facilitar criação
        public static ResponseModel<T> Success(T dados, string mensagem = "Operação realizada com sucesso")
        {
            return new ResponseModel<T>
            {
                Dados = dados,
                Mensagem = mensagem,
                Status = true
            };
        }
        
        public static ResponseModel<T> Error(string mensagem, List<string>? erros = null)
        {
            return new ResponseModel<T>
            {
                Mensagem = mensagem,
                Status = false,
                Erros = erros ?? new List<string>()
            };
        }
        
        public static ResponseModel<T> NotFound(string mensagem = "Recurso não encontrado")
        {
            return new ResponseModel<T>
            {
                Mensagem = mensagem,
                Status = false
            };
        }
        
        public static ResponseModel<T> ValidationError(string mensagem, List<string> erros)
        {
            return new ResponseModel<T>
            {
                Mensagem = mensagem,
                Status = false,
                Erros = erros
            };
        }
    }
}
