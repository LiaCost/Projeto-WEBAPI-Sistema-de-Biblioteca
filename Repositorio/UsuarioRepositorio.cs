using WEB_API_LIA.Model;
using WEB_API_LIA.ORM;

namespace WEB_API_LIA.Repositorio
{
    public class UsuarioRepositorio
    {
        private readonly BdBibliotecaContext _context;

        public UsuarioRepositorio(BdBibliotecaContext context)
        {
            _context = context;
        }

        public TbUsuario GetByCredentials(string usuario, string senha)
        {
            // Aqui você deve usar a lógica de hash para comparar a senha
            return _context.TbUsuarios.FirstOrDefault(u => u.Usuario == usuario && u.Senha == senha);
        }

        // Você pode adicionar métodos adicionais para gerenciar usuários
    }
}
