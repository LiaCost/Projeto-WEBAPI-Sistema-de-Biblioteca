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
    public class FuncionarioController : ControllerBase
    {
        private readonly FuncionarioRepositorio _funcionarioRepo;

        public FuncionarioController(FuncionarioRepositorio funcionarioRepo)
        {
            _funcionarioRepo = funcionarioRepo;
        }

        // GET: api/Funcionario
        [HttpGet]
        public ActionResult<List<Funcionario>> GetAll()
        {
            try
            {
                // Chama o repositório para obter todos os funcionários
                var funcionarios = _funcionarioRepo.GetAll();

                // Verifica se a lista de funcionários está vazia
                if (funcionarios == null || !funcionarios.Any())
                {
                    return NotFound(new { Mensagem = "Nenhum funcionário encontrado." });
                }

                // Mapeia a lista de funcionários
                var listaComUrl = funcionarios.Select(funcionario => new Funcionario
                {
                    Id = funcionario.Id,
                    Nome = funcionario.Nome,
                    Email = funcionario.Email,
                    Telefone = funcionario.Telefone,
                    Cargo = funcionario.Cargo
                });

                // Retorna a lista de funcionários com status 200 OK
                return Ok(listaComUrl);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Mensagem = "Ocorreu um erro ao obter os funcionários.", Detalhes = ex.Message });
            }
        }

        // GET: api/Funcionario/{id}
        [HttpGet("{id}")]
        public ActionResult<Funcionario> GetById(int id)
        {
            try
            {
                // Chama o repositório para obter o funcionario pelo ID
                var funcionario = _funcionarioRepo.GetById(id);

                // Se o funcionario não for encontrado, retorna uma resposta 404
                if (funcionario == null)
                {
                    return NotFound(new { Mensagem = "Funcionário não encontrado." });
                }

                // Mapeia o funcionario encontrado
                var funcionarioComUrl = new Funcionario
                {
                    Id = funcionario.Id,
                    Nome = funcionario.Nome,
                    Email = funcionario.Email,
                    Telefone = funcionario.Telefone,
                    Cargo = funcionario.Cargo
                };

                // Retorna o funcionario com status 200 OK
                return Ok(funcionarioComUrl);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Mensagem = "Ocorreu um erro ao obter o funcionário.", Detalhes = ex.Message });
            }
        }

        // POST api/<FuncionarioController>        
        [HttpPost]
        public ActionResult<object> Post([FromForm] FuncionarioDto novoFuncionario)
        {
            try
            {
                // Cria uma nova instância do modelo Funcionario a partir do DTO recebido
                var funcionario = new Funcionario
                {
                    Nome = novoFuncionario.Nome,
                    Email = novoFuncionario.Email,
                    Telefone = novoFuncionario.Telefone,
                    Cargo = novoFuncionario.Cargo
                };

                // Chama o método de adicionar do repositório
                _funcionarioRepo.Add(funcionario);

                // Cria um objeto anônimo para retornar
                var resultado = new
                {
                    Mensagem = "Funcionário cadastrado com sucesso!",
                    Nome = funcionario.Nome,
                    Email = funcionario.Email,
                    Telefone = funcionario.Telefone,
                    Cargo = funcionario.Cargo
                };

                // Retorna o objeto com status 200 OK
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Mensagem = "Ocorreu um erro ao cadastrar o funcionário.", Detalhes = ex.Message });
            }
        }

        // PUT api/<FuncionarioController>        
        [HttpPut("{id}")]
        public ActionResult<object> Put(int id, [FromForm] FuncionarioDto funcionarioAtualizado)
        {
            try
            {
                // Busca o funcionario existente pelo Id
                var funcionarioExistente = _funcionarioRepo.GetById(id);

                // Verifica se o funcionario foi encontrado
                if (funcionarioExistente == null)
                {
                    return NotFound(new { Mensagem = "Funcionário não encontrado." });
                }

                // Atualiza os dados do funcionário existente com os valores do objeto recebido
                funcionarioExistente.Nome = funcionarioAtualizado.Nome;
                funcionarioExistente.Email = funcionarioAtualizado.Email;
                funcionarioExistente.Telefone = funcionarioAtualizado.Telefone;
                funcionarioExistente.Cargo = funcionarioAtualizado.Cargo;

                // Chama o método de atualização do repositório
                _funcionarioRepo.Update(funcionarioExistente);

                // Cria um objeto anônimo para retornar
                var resultado = new
                {
                    Mensagem = "Funcionário atualizado com sucesso!",
                    Nome = funcionarioExistente.Nome,
                    Email = funcionarioExistente.Email,
                    Telefone = funcionarioExistente.Telefone,
                    Cargo = funcionarioExistente.Cargo
                };

                // Retorna o objeto com status 200 OK
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Mensagem = "Ocorreu um erro ao atualizar o funcionário.", Detalhes = ex.Message });
            }
        }

        // DELETE api/<FuncionarioController>/5
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            try
            {
                // Busca o funcionario existente pelo Id
                var funcionarioExistente = _funcionarioRepo.GetById(id);

                // Verifica se o funcionario foi encontrado
                if (funcionarioExistente == null)
                {
                    return NotFound(new { Mensagem = "Funcionário não encontrado." });
                }

                // Chama o método de exclusão do repositório
                _funcionarioRepo.Delete(id);

                // Cria um objeto anônimo para retornar
                var resultado = new
                {
                    Mensagem = "Funcionário excluído com sucesso!",
                    Nome = funcionarioExistente.Nome,
                    Email = funcionarioExistente.Email,
                    Telefone = funcionarioExistente.Telefone,
                    Cargo = funcionarioExistente.Cargo
                };

                // Retorna o objeto com status 200 OK
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Mensagem = "Ocorreu um erro ao excluir o funcionário.", Detalhes = ex.Message });
            }
        }
    }
}

