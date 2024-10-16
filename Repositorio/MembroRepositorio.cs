using WEB_API_LIA.Model;
using WEB_API_LIA.ORM;

namespace WEB_API_LIA.Repositorio
{
    public class MembroRepositorio
    {
        private readonly BdBibliotecaContext _context;

        public MembroRepositorio(BdBibliotecaContext context)
        {
            _context = context;
        }

        public void Add(Membro membro)
        {

            // Cria uma nova entidade do tipo TbMembro a partir do objeto Membro recebido
            var TbMembro = new TbMembro()
            {

                Nome = membro.Nome,
                Email = membro.Email,
                Telefone = membro.Telefone,
                DataCadastro = membro.DataCadastro,
                TipoMembro = membro.TipoMembro,
            };

            // Adiciona a entidade ao contexto
            _context.Add(TbMembro);

            // Salva as mudanças no banco de dados
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            try
            {
                // Busca a entidade existente no banco de dados pelo Id
                var tbMembro = _context.TbMembros.FirstOrDefault(f => f.Id == id);

                // Verifica se a entidade foi encontrada
                if (tbMembro != null)
                {
                    // Verifica se o membro possui reservas associadas
                    var hasReservations = _context.TbReservas.Any(r => r.FkMembro == id);
                    if (hasReservations)
                    {
                        throw new Exception("Não é possível excluir o membro, pois ele possui relacionamento com a tabela de reservas.");
                    }

                    // Verifica se o membro possui empréstimos associados
                    var hasEmprestimos = _context.TbEmprestimos.Any(r => r.FkMembro == id);
                    if (hasEmprestimos)
                    {
                        throw new Exception("Não é possível excluir o membro, pois ele possui relacionamento com a tabela de empréstimos.");
                    }

                    // Remove a entidade do contexto
                    _context.TbMembros.Remove(tbMembro);

                    // Salva as mudanças no banco de dados
                    _context.SaveChanges();
                }
                else
                {
                    throw new Exception("Membro não encontrado.");
                }
            }
            catch (Exception ex)
            {
                // Aqui você pode registrar o erro ou simplesmente repassar a mensagem para ser tratada em outro lugar
                throw new Exception($"Erro ao tentar excluir o membro: {ex.Message}", ex);
            }
        }

        public List<Membro> GetAll()
        {
            List<Membro> listFun = new List<Membro>();

            var listTb = _context.TbMembros.ToList();

            foreach (var item in listTb)
            {
                var membro = new Membro
                {
                    Id = item.Id,
                    Nome = item.Nome,
                    Email= item.Email,
                    Telefone = item.Telefone,
                    DataCadastro = item.DataCadastro,
                    TipoMembro = item.TipoMembro
                };

                listFun.Add(membro);
            }

            return listFun;
        }

        public Membro GetById(int id)
        {
            // Busca o membro pelo ID no banco de dados
            var item = _context.TbMembros.FirstOrDefault(f => f.Id == id);

            // Verifica se o membro foi encontrado
            if (item == null)
            {
                return null; // Retorna null se não encontrar
            }

            // Mapeia o objeto encontrado para a classe Livro
            var membro = new Membro
            {
                Id = item.Id,
                Nome = item.Nome,
                Email = item.Email,
                Telefone = item.Telefone,
                DataCadastro = item.DataCadastro,
                TipoMembro = item.TipoMembro
            };

            return membro; // Retorna o membro encontrado
        }

        public void Update(Membro membro)
        {
            // Busca a entidade existente no banco de dados pelo Id
            var tbMembro = _context.TbMembros.FirstOrDefault(f => f.Id == membro.Id);

            // Verifica se a entidade foi encontrada
            if (tbMembro != null)
            {
                // Atualiza os campos da entidade com o endereço do Membro
                tbMembro.Nome = membro.Nome;
                tbMembro.Email = membro.Email;
                tbMembro.Telefone = membro.Telefone;
                tbMembro.DataCadastro = membro.DataCadastro;
                tbMembro.TipoMembro = membro.TipoMembro;
                
                // Atualiza as informações no contexto
                _context.TbMembros.Update(tbMembro);

                // Salva as mudanças no banco de dados
                _context.SaveChanges();
            }
            else
            {
                throw new Exception("Membro não encontrado.");
            }
        }
    }
}
