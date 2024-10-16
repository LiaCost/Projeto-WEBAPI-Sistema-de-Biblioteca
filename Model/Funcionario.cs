namespace WEB_API_LIA.Model
{
    public class Funcionario
    {
        public int Id { get; set; }

        public string Nome { get; set; } = null!;

        public string Email { get; set; } = null!;

        public string Telefone { get; set; } = null!;

        public string Cargo { get; set; } = null!;
    }
}
