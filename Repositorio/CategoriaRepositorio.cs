using WEB_API_LIA.Model;
using WEB_API_LIA.ORM;

namespace WEB_API_LIA.Repositorio
{
    public class CategoriaRepositorio
    {
        private readonly BdBibliotecaContext _context;

        public CategoriaRepositorio(BdBibliotecaContext context)
        {
            _context = context;
        }

        public void Add(Categoria categoria)
        {

            // Cria uma nova entidade do tipo TbCategoria a partir do objeto Categoria recebido
            var TbCategoria = new TbCategoria()
            {

                Nome = categoria.Nome,
                Descricao = categoria.Descricao
                
            };

            // Adiciona a entidade ao contexto
            _context.Add(TbCategoria);

            // Salva as mudanças no banco de dados
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            // Busca a entidade existente no banco de dados pelo Id
            var tbCategoria = _context.TbCategorias.FirstOrDefault(f => f.Id == id);

            // Verifica se a entidade foi encontrada
            if (tbCategoria != null)
            {
                // Remove a entidade do contexto
                _context.TbCategorias.Remove(tbCategoria);

                // Salva as mudanças no banco de dados
                _context.SaveChanges();
            }
            else
            {
                throw new Exception("Categoria não encontrada.");
            }
        }

        public List<Categoria> GetAll()
        {
            List<Categoria> listFun = new List<Categoria>();

            var listTb = _context.TbCategorias.ToList();

            foreach (var item in listTb)
            {
                var categoria = new Categoria
                {
                    Id = item.Id,
                    Nome = item.Nome,
                    Descricao = item.Descricao                  
                };

                listFun.Add(categoria);
            }

            return listFun;
        }

        public Categoria GetById(int id)
        {
            // Busca a categoria pelo ID no banco de dados
            var item = _context.TbCategorias.FirstOrDefault(f => f.Id == id);

            // Verifica se a categoria foi encontrada
            if (item == null)
            {
                return null; // Retorna null se não encontrar
            }

            // Mapeia o objeto encontrado para a classe Categoria
            var categoria = new Categoria
            {
                Id = item.Id,
                Nome = item.Nome,
                Descricao = item.Descricao
            };

            return categoria; // Retorna a categoria encontrada
        }

        public void Update(Categoria categoria)
        {
            // Busca a entidade existente no banco de dados pelo Id
            var tbCategoria = _context.TbCategorias.FirstOrDefault(f => f.Id == categoria.Id);

            // Verifica se a entidade foi encontrada
            if (tbCategoria != null)
            {
                // Atualiza os campos da entidade com o endereço do Funcionario
                tbCategoria.Nome = categoria.Nome;
                tbCategoria.Descricao = categoria.Descricao;
               

                // Atualiza as informações no contexto
                _context.TbCategorias.Update(tbCategoria);

                // Salva as mudanças no banco de dados
                _context.SaveChanges();
            }
            else
            {
                throw new Exception("Categoria não encontrada.");
            }
        }
    }
}
