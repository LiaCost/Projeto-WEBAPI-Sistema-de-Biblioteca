using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WEB_API_LIA.Model;
using WEB_API_LIA.ORM;
using WEB_API_LIA.Repositorio;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WEB_API_LIA.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ReservaController : ControllerBase
    {
        private readonly ReservaRepositorio _reservaRepo;

        public ReservaController(ReservaRepositorio reservaRepo)
        {
            _reservaRepo = reservaRepo;
        }

        // GET: api/Reserva
        [HttpGet]
        public ActionResult<List<Reserva>> GetAll()
        {
            try
            {
                // Chama o repositório para obter todas as reservas
                var reservas = _reservaRepo.GetAll();

                // Verifica se a lista de categorias está vazia
                if (reservas == null || !reservas.Any())
                {
                    return NotFound(new { Mensagem = "Nenhuma reserva encontrada." });
                }

                // Mapeia a lista de reservas
                var listaComUrl = reservas.Select(reserva => new Reserva
                {
                    Id = reserva.Id,
                    DataReserva = reserva.DataReserva,
                    FkMembro = reserva.FkMembro,
                    FkLivro = reserva.FkLivro
                });

                // Retorna a lista de categorias com status 200 OK
                return Ok(listaComUrl);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Mensagem = "Erro ao obter reservas.", Detalhes = ex.Message });
            }
        }

        // GET: api/Reserva/{id}
        [HttpGet("{id}")]
        public ActionResult<Reserva> GetById(int id)
        {
            try
            {
                // Chama o repositório para obter a reserva pelo ID
                var reserva = _reservaRepo.GetById(id);

                // Se a reserva não for encontrada, retorna uma resposta 404
                if (reserva == null)
                {
                    return NotFound(new { Mensagem = "Reserva não encontrada." });
                }

                // Mapeia a reserva encontrada
                var reservaComUrl = new Reserva
                {
                    Id = reserva.Id,
                    DataReserva = reserva.DataReserva,
                    FkMembro = reserva.FkMembro,
                    FkLivro = reserva.FkLivro
                };

                // Retorna o endereço com status 200 OK
                return Ok(reservaComUrl);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Mensagem = "Erro ao obter a reserva.", Detalhes = ex.Message });
            }
        }

        // POST api/<ReservaController>        
        [HttpPost]
        public ActionResult<object> Post([FromForm] ReservaDto novoReserva)
        {
            try
            {
                // Cria uma nova instância da modelo Reserva a partir do DTO recebido
                var reserva = new Reserva
                {
                    DataReserva = novoReserva.DataReserva,
                    FkMembro = novoReserva.FkMembro,
                    FkLivro = novoReserva.FkLivro
                };

                // Chama o método de adicionar do repositório
                _reservaRepo.Add(reserva);

                // Cria um objeto anônimo para retornar
                var resultado = new
                {
                    Mensagem = "Reserva cadastrada com sucesso!",
                    DataReserva = reserva.DataReserva,
                    FkMembro = reserva.FkMembro,
                    FkLivro = reserva.FkLivro
                };

                // Retorna o objeto com status 200 OK
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Mensagem = "Erro ao cadastrar reserva.", Detalhes = ex.Message });
            }
        }

        // PUT api/<ReservaController>        
        [HttpPut("{id}")]
        public ActionResult<object> Put(int id, [FromForm] ReservaDto reservaAtualizado)
        {
            try
            {
                // Busca a reserva existente pelo Id
                var reservaExistente = _reservaRepo.GetById(id);

                // Verifica se a reserva foi encontrada
                if (reservaExistente == null)
                {
                    return NotFound(new { Mensagem = "Reserva não encontrada." });
                }

                // Atualiza os dados da reserva existente
                reservaExistente.DataReserva = reservaAtualizado.DataReserva;
                reservaExistente.FkMembro = reservaAtualizado.FkMembro;
                reservaExistente.FkLivro = reservaAtualizado.FkLivro;

                // Chama o método de atualização do repositório
                _reservaRepo.Update(reservaExistente);

                // Cria um objeto anônimo para retornar
                var resultado = new
                {
                    Mensagem = "Reserva atualizada com sucesso!",
                    DataReserva = reservaExistente.DataReserva,
                    FkMembro = reservaExistente.FkMembro,
                    FkLivro = reservaExistente.FkLivro
                };

                // Retorna o objeto com status 200 OK
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Mensagem = "Erro ao atualizar reserva.", Detalhes = ex.Message });
            }
        }

        // DELETE api/<ReservaController>/5
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            try
            {
                // Busca a reserva existente pelo Id
                var reservaExistente = _reservaRepo.GetById(id);

                // Verifica se a reserva foi encontrada
                if (reservaExistente == null)
                {
                    return NotFound(new { Mensagem = "Reserva não encontrada." });
                }

                // Chama o método de exclusão do repositório
                _reservaRepo.Delete(id);

                // Cria um objeto anônimo para retornar
                var resultado = new
                {
                    Mensagem = "Reserva excluída com sucesso!",
                    DataReserva = reservaExistente.DataReserva,
                    FkMembro = reservaExistente.FkMembro,
                    FkLivro = reservaExistente.FkLivro
                };

                // Retorna o objeto com status 200 OK
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Mensagem = "Erro ao excluir reserva.", Detalhes = ex.Message });
            }
        }
    }
}

