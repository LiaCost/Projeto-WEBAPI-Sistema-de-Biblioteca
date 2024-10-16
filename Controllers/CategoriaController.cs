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
    public class CategoriaController : ControllerBase
    {
        private readonly CategoriaRepositorio _categoriaRepo;

        public CategoriaController(CategoriaRepositorio categoriaRepo)
        {
            _categoriaRepo = categoriaRepo;
        }

        // GET: api/Categoria
        [HttpGet]
        public ActionResult<List<Categoria>> GetAll()
        {
            try
            {
                var categorias = _categoriaRepo.GetAll();

                if (categorias == null || !categorias.Any())
                {
                    return NotFound(new { Mensagem = "Nenhuma categoria encontrada." });
                }

                var listaComUrl = categorias.Select(categoria => new Categoria
                {
                    Id = categoria.Id,
                    Nome = categoria.Nome,
                    Descricao = categoria.Descricao
                });

                return Ok(listaComUrl);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Mensagem = "Erro ao obter categorias.", Detalhes = ex.Message });
            }
        }

        // GET: api/Categoria/{id}
        [HttpGet("{id}")]
        public ActionResult<Categoria> GetById(int id)
        {
            try
            {
                var categoria = _categoriaRepo.GetById(id);

                if (categoria == null)
                {
                    return NotFound(new { Mensagem = "Categoria não encontrada." });
                }

                var categoriaComUrl = new Categoria
                {
                    Id = categoria.Id,
                    Nome = categoria.Nome,
                    Descricao = categoria.Descricao
                };

                return Ok(categoriaComUrl);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Mensagem = "Erro ao obter a categoria.", Detalhes = ex.Message });
            }
        }

        // POST api/<CategoriaController>
        [HttpPost]
        public ActionResult<object> Post([FromForm] CategoriaDto novoCategoria)
        {
            try
            {
                var categoria = new Categoria
                {
                    Nome = novoCategoria.Nome,
                    Descricao = novoCategoria.Descricao
                };

                _categoriaRepo.Add(categoria);

                var resultado = new
                {
                    Mensagem = "Categoria cadastrada com sucesso!",
                    Nome = categoria.Nome,
                    Descricao = categoria.Descricao
                };

                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Mensagem = "Erro ao cadastrar a categoria.", Detalhes = ex.Message });
            }
        }

        // PUT api/<CategoriaController>
        [HttpPut("{id}")]
        public ActionResult<object> Put(int id, [FromForm] CategoriaDto categoriaAtualizado)
        {
            try
            {
                var categoriaExistente = _categoriaRepo.GetById(id);

                if (categoriaExistente == null)
                {
                    return NotFound(new { Mensagem = "Categoria não encontrada." });
                }

                categoriaExistente.Nome = categoriaAtualizado.Nome;
                categoriaExistente.Descricao = categoriaAtualizado.Descricao;

                _categoriaRepo.Update(categoriaExistente);

                var resultado = new
                {
                    Mensagem = "Categoria atualizada com sucesso!",
                    Nome = categoriaExistente.Nome,
                    Descricao = categoriaExistente.Descricao
                };

                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Mensagem = "Erro ao atualizar a categoria.", Detalhes = ex.Message });
            }
        }

        // DELETE api/<CategoriaController>/5
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            try
            {
                var categoriaExistente = _categoriaRepo.GetById(id);

                if (categoriaExistente == null)
                {
                    return NotFound(new { Mensagem = "Categoria não encontrada." });
                }

                _categoriaRepo.Delete(id);

                var resultado = new
                {
                    Mensagem = "Categoria excluída com sucesso!",
                    Nome = categoriaExistente.Nome,
                    Descricao = categoriaExistente.Descricao
                };

                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Mensagem = "Erro ao excluir a categoria.", Detalhes = ex.Message });
            }
        }
    }
}
