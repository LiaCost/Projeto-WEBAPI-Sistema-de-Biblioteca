using WEB_API_LIA.Model;
using WEB_API_LIA.ORM;

namespace WEB_API_LIA.Repositorio
{
    public class ReservaRepositorio
    {
        private readonly BdBibliotecaContext _context;

        public ReservaRepositorio(BdBibliotecaContext context)
        {
            _context = context;
        }

        public void Add(Reserva reserva)
        {

            // Cria uma nova entidade do tipo TbReserva a partir do objeto Reserva recebido
            var TbReserva = new TbReserva()
            {

                DataReserva = reserva.DataReserva,
                FkMembro = reserva.FkMembro,
                FkLivro = reserva.FkLivro
            };

            // Adiciona a entidade ao contexto
            _context.Add(TbReserva);

            // Salva as mudanças no banco de dados
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            // Busca a entidade existente no banco de dados pelo Id
            var tbReserva = _context.TbReservas.FirstOrDefault(f => f.Id == id);

            // Verifica se a entidade foi encontrada
            if (tbReserva != null)
            {
                // Remove a entidade do contexto
                _context.TbReservas.Remove(tbReserva);

                // Salva as mudanças no banco de dados
                _context.SaveChanges();
            }
            else
            {
                throw new Exception("Reserva não encontrada.");
            }
        }

        public List<Reserva> GetAll()
        {
            List<Reserva> listFun = new List<Reserva>();

            var listTb = _context.TbReservas.ToList();

            foreach (var item in listTb)
            {
                var reserva = new Reserva
                {
                    Id = item.Id,
                    DataReserva = item.DataReserva,
                    FkMembro = item.FkMembro,
                    FkLivro= item.FkLivro
                };

                listFun.Add(reserva);
            }

            return listFun;
        }

        public Reserva GetById(int id)
        {
            // Busca a reserva pelo ID no banco de dados
            var item = _context.TbReservas.FirstOrDefault(f => f.Id == id);

            // Verifica se a reserva foi encontrada
            if (item == null)
            {
                return null; // Retorna null se não encontrar
            }

            // Mapeia o objeto encontrado para a classe Categoria
            var reserva = new Reserva
            {
                Id = item.Id,
                DataReserva = item.DataReserva,
                FkMembro = item.FkMembro,
                FkLivro = item.FkLivro
            };

            return reserva; // Retorna a categoria encontrada
        }

        public void Update(Reserva reserva)
        {
            // Busca a entidade existente no banco de dados pelo Id
            var tbReserva = _context.TbReservas.FirstOrDefault(f => f.Id == reserva.Id);

            // Verifica se a entidade foi encontrada
            if (tbReserva != null)
            {
                // Atualiza os campos da entidade com o endereço da Reserva
                tbReserva.DataReserva = reserva.DataReserva;
                tbReserva.FkMembro = reserva.FkMembro;
                tbReserva.FkLivro = reserva.FkLivro;

                // Atualiza as informações no contexto
                _context.TbReservas.Update(tbReserva);

                // Salva as mudanças no banco de dados
                _context.SaveChanges();
            }
            else
            {
                throw new Exception("Reserva não encontrada.");
            }
        }
    }
}
