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
    public class EmprestimoController : ControllerBase
    {
        private readonly EmprestimoRepositorio _emprestimoRepo;

        public EmprestimoController(EmprestimoRepositorio emprestimoRepo)
        {
            _emprestimoRepo = emprestimoRepo;
        }

        // GET: api/Emprestimo
        [HttpGet]
        public ActionResult<List<Emprestimo>> GetAll()
        {
            try
            {
                // Chama o repositório para obter todos os emprestimos
                var emprestimos = _emprestimoRepo.GetAll();

                // Verifica se a lista de emprestimos está vazia
                if (emprestimos == null || !emprestimos.Any())
                {
                    return NotFound(new { Mensagem = "Nenhum emprestimo encontrado." });
                }

                // Mapeia a lista de emprestimos
                var listaComUrl = emprestimos.Select(emprestimo => new Emprestimo
                {
                    Id = emprestimo.Id,
                    DataEmprestimo = emprestimo.DataEmprestimo,
                    DataDevolucao = emprestimo.DataDevolucao,
                    FkMembro = emprestimo.FkMembro,
                    FkLivro = emprestimo.FkLivro
                });

                // Retorna a lista de emprestimos com status 200 OK
                return Ok(listaComUrl);
            }
            catch (Exception ex)
            {
                // Retorna um erro genérico com status 500
                return StatusCode(500, new { Mensagem = "Erro ao obter empréstimos.", Erro = ex.Message });
            }
        }

        // GET: api/Emprestimo/{id}
        [HttpGet("{id}")]
        public ActionResult<Emprestimo> GetById(int id)
        {
            try
            {
                // Chama o repositório para obter o emprestimo pelo ID
                var emprestimo = _emprestimoRepo.GetById(id);

                // Se o emprestimo não for encontrado, retorna uma resposta 404
                if (emprestimo == null)
                {
                    return NotFound(new { Mensagem = "Emprestimo não encontrado." });
                }

                // Mapeia o emprestimo encontrado
                var emprestimoComUrl = new Emprestimo
                {
                    Id = emprestimo.Id,
                    DataEmprestimo = emprestimo.DataEmprestimo,
                    DataDevolucao = emprestimo.DataDevolucao,
                    FkMembro = emprestimo.FkMembro,
                    FkLivro = emprestimo.FkLivro
                };

                // Retorna o emprestimo com status 200 OK
                return Ok(emprestimoComUrl);
            }
            catch (Exception ex)
            {
                // Retorna um erro genérico com status 500
                return StatusCode(500, new { Mensagem = "Erro ao obter o empréstimo.", Erro = ex.Message });
            }
        }

        // POST api/<EmprestimoController>        
        [HttpPost]
        public ActionResult<object> Post([FromForm] EmprestimoDto novoEmprestimo)
        {
            try
            {
                // Cria uma nova instância do modelo Emprestimo a partir do DTO recebido
                var emprestimo = new Emprestimo
                {
                    DataEmprestimo = novoEmprestimo.DataEmprestimo,
                    DataDevolucao = novoEmprestimo.DataDevolucao,
                    FkMembro = novoEmprestimo.FkMembro,
                    FkLivro = novoEmprestimo.FkLivro
                };

                // Chama o método de adicionar do repositório
                _emprestimoRepo.Add(emprestimo);

                // Cria um objeto anônimo para retornar
                var resultado = new
                {
                    Mensagem = "Emprestimo cadastrado com sucesso!",
                    DataEmprestimo = emprestimo.DataEmprestimo,
                    DataDevolucao = emprestimo.DataDevolucao,
                    FkMembro = emprestimo.FkMembro,
                    FkLivro = emprestimo.FkLivro
                };

                // Retorna o objeto com status 200 OK
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                // Retorna um erro genérico com status 500
                return StatusCode(500, new { Mensagem = "Erro ao cadastrar empréstimo.", Erro = ex.Message });
            }
        }

        // PUT api/<EmprestimoController>        
        [HttpPut("{id}")]
        public ActionResult<object> Put(int id, [FromForm] EmprestimoDto emprestimoAtualizado)
        {
            try
            {
                // Busca o emprestimo existente pelo Id
                var emprestimoExistente = _emprestimoRepo.GetById(id);

                // Verifica se o emprestimo foi encontrado
                if (emprestimoExistente == null)
                {
                    return NotFound(new { Mensagem = "Emprestimo não encontrado." });
                }

                // Atualiza os dados do emprestimo existente
                emprestimoExistente.DataEmprestimo = emprestimoAtualizado.DataEmprestimo;
                emprestimoExistente.DataDevolucao = emprestimoAtualizado.DataDevolucao;
                emprestimoExistente.FkMembro = emprestimoAtualizado.FkMembro;
                emprestimoExistente.FkLivro = emprestimoAtualizado.FkLivro;

                // Chama o método de atualização do repositório
                _emprestimoRepo.Update(emprestimoExistente);

                // Cria um objeto anônimo para retornar
                var resultado = new
                {
                    Mensagem = "Emprestimo atualizado com sucesso!",
                    DataEmprestimo = emprestimoExistente.DataEmprestimo,
                    DataDevolucao = emprestimoExistente.DataDevolucao,
                    FkMembro = emprestimoExistente.FkMembro,
                    FkLivro = emprestimoExistente.FkLivro
                };

                // Retorna o objeto com status 200 OK
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                // Retorna um erro genérico com status 500
                return StatusCode(500, new { Mensagem = "Erro ao atualizar empréstimo.", Erro = ex.Message });
            }
        }

        // DELETE api/<EmprestimoController>/5
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            try
            {
                // Busca o emprestimo existente pelo Id
                var emprestimoExistente = _emprestimoRepo.GetById(id);

                // Verifica se o emprestimo foi encontrado
                if (emprestimoExistente == null)
                {
                    return NotFound(new { Mensagem = "Emprestimo não encontrado." });
                }

                // Chama o método de exclusão do repositório
                _emprestimoRepo.Delete(id);

                // Cria um objeto anônimo para retornar
                var resultado = new
                {
                    Mensagem = "Emprestimo excluído com sucesso!",
                    DataEmprestimo = emprestimoExistente.DataEmprestimo,
                    DataDevolucao = emprestimoExistente.DataDevolucao,
                    FkMembro = emprestimoExistente.FkMembro,
                    FkLivro = emprestimoExistente.FkLivro
                };

                // Retorna o objeto com status 200 OK
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                // Retorna um erro genérico com status 500
                return StatusCode(500, new { Mensagem = "Erro ao excluir empréstimo.", Erro = ex.Message });
            }
        }
    }
}

