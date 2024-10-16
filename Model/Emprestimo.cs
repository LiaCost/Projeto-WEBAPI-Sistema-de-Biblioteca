namespace WEB_API_LIA.Model
{
    public class Emprestimo
    {
        public int Id { get; set; }

        public DateTime DataEmprestimo { get; set; }

        public DateTime DataDevolucao { get; set; }

        public int FkMembro { get; set; }

        public int FkLivro { get; set; }
    }
}
