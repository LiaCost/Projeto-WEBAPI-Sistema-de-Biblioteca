namespace WEB_API_LIA.Model
{
    public class Livro
    {
        public int Id { get; set; }

        public string Titulo { get; set; } = null!;

        public string Autor { get; set; } = null!;

        public int AnoPublicacao { get; set; }

        public int FkCategoria { get; set; }

        public bool Disponibilidade { get; set; }

    }
}
