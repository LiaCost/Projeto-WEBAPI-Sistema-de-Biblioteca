using Microsoft.VisualBasic;

namespace WEB_API_LIA.Model
{
    public class ReservaDto
    {
        public DateTime DataReserva { get; set; }

        public int FkMembro { get; set; }

        public int FkLivro { get; set; }
    }
}
