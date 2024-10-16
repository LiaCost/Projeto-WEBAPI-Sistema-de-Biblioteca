using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WEB_API_LIA.Model;
using WEB_API_LIA.Repositorio;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WEB_API_LIA.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class MembroController : ControllerBase
    {
        private readonly MembroRepositorio _membroRepo;

        public MembroController(MembroRepositorio membroRepo)
        {
            _membroRepo = membroRepo;
        }

        // GET: api/Membro
        [HttpGet]
        public ActionResult<List<Membro>> GetAll()
        {
            // Chama o repositório para obter todos os membros
            var membros = _membroRepo.GetAll();

            // Verifica se a lista de membros está vazia
            if (membros == null || !membros.Any())
            {
                return NotFound(new { Mensagem = "Nenhum membro encontrado." });
            }

            // Mapeia a lista de membros para incluir a URL da foto
            var listaComUrl = membros.Select(membro => new Membro
            {
                Id = membro.Id,
                Nome = membro.Nome,
                Email = membro.Email,
                Telefone = membro.Telefone,
                DataCadastro = membro.DataCadastro,
                TipoMembro = membro.TipoMembro
            });

            // Retorna a lista de livros com status 200 OK
            return Ok(listaComUrl);
        }

        // GET: api/Membro/{id}
        [HttpGet("{id}")]
        public ActionResult<Membro> GetById(int id)
        {
            // Chama o repositório para obter o membro pelo ID
            var membro = _membroRepo.GetById(id);

            // Se o membro não for encontrado, retorna uma resposta 404
            if (membro == null)
            {
                return NotFound(new { Mensagem = "Membro não encontrado." }); // Retorna 404 com mensagem
            }

            // Mapeia o membro encontrado para incluir a URL da foto
            var membroComUrl = new Membro
            {
                Id = membro.Id,
                Nome = membro.Nome,
                Email = membro.Email,
                Telefone = membro.Telefone,
                DataCadastro = membro.DataCadastro,
                TipoMembro = membro.TipoMembro              
            };

            // Retorna o endereço com status 200 OK
            return Ok(membroComUrl);
        }

        // POST api/<MembroController>        
        [HttpPost]
        public ActionResult<object> Post([FromForm] MembroDto novoMembro)
        {
            // Cria uma nova instância do modelo Membro a partir do DTO recebido
            var membro = new Membro
            {
                Nome = novoMembro.Nome,
                Email = novoMembro.Email,
                Telefone = novoMembro.Telefone,
                DataCadastro = novoMembro.DataCadastro,
                TipoMembro = novoMembro.TipoMembro               
            };

            // Chama o método de adicionar do repositório, passando a foto como parâmetro
            _membroRepo.Add(membro);

            // Cria um objeto anônimo para retornar
            var resultado = new
            {
                Mensagem = "Membro cadastrado com sucesso!",
                Nome = membro.Nome,
                Email = membro.Email,
                Telefone = membro.Telefone,
                DataCadastro = membro.DataCadastro
            };

            // Retorna o objeto com status 200 OK
            return Ok(resultado);
        }

        // PUT api/<MembroController>        
        [HttpPut("{id}")]
        public ActionResult<object> Put(int id, [FromForm] MembroDto membroAtualizado)
        {
            // Busca o membro existente pelo Id
            var membroExistente = _membroRepo.GetById(id);

            // Verifica se o membro foi encontrado
            if (membroExistente == null)
            {
                return NotFound(new { Mensagem = "Membro não encontrado." });
            }

            // Atualiza os dados do membro existente com os valores do objeto recebido
            membroExistente.Nome = membroAtualizado.Nome;
            membroExistente.Email = membroAtualizado.Email;
            membroExistente.Telefone = membroAtualizado.Telefone;
            membroExistente.DataCadastro = membroAtualizado.DataCadastro;
            membroExistente.TipoMembro = membroAtualizado.TipoMembro;

            // Chama o método de atualização do repositório, passando a nova foto
            _membroRepo.Update(membroExistente);

            // Cria um objeto anônimo para retornar
            var resultado = new
            {
                Mensagem = "Membro atualizado com sucesso!",
                Nome = membroExistente.Nome,
                Email = membroExistente.Email,
                Telefone = membroExistente.Telefone,
                DataCadastro = membroExistente.DataCadastro
            };

            // Retorna o objeto com status 200 OK
            return Ok(resultado);
        }

        // DELETE api/<MembroController>/       
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            try
            {
                // Busca o membro existente pelo Id
                var membroExistente = _membroRepo.GetById(id);

                // Verifica se o membro foi encontrado
                if (membroExistente == null)
                {
                    return NotFound(new { Mensagem = "Membro não encontrado." });
                }

                // Chama o método de exclusão do repositório
                _membroRepo.Delete(id);

                // Cria um objeto anônimo para retornar
                var resultado = new
                {
                    Mensagem = "Membro excluído com sucesso!",
                    Nome = membroExistente.Nome,
                    Email = membroExistente.Email,
                    Telefone = membroExistente.Telefone,
                    DataCadastro = membroExistente.DataCadastro,
                    TipoMembro = membroExistente.TipoMembro
                };

                // Retorna o objeto com status 200 OK
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                // Verifica o tipo de exceção e retorna a resposta apropriada
                if (ex.Message.Contains("não é possível excluir o membro"))
                {
                    return BadRequest(new { Mensagem = ex.Message });
                }

                // Em caso de outras exceções, retorna um erro 500
                return StatusCode(500, new { Mensagem = $"Erro ao tentar excluir o membro: {ex.Message}" });
            }
        }
    }
}
