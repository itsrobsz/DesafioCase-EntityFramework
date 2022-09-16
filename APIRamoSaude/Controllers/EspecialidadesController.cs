using APIRamoSaude.Interfaces;
using APIRamoSaude.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace APIRamoSaude.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EspecialidadesController : ControllerBase
    {

        // Injeção de dependência do repositório
        private readonly IEspecialidadeRepository repositorio;

        public EspecialidadesController(IEspecialidadeRepository _repositorio)
        {
            repositorio = _repositorio;
        }

        /// <summary>
        /// Cadastra especialidade na aplicação
        /// </summary>
        /// <param name="especialidade">Dados da especialidade</param>
        /// <returns>Dados da especialidade cadastrado</returns>
        [HttpPost]
        public IActionResult Cadastrar(Especialidade especialidade)
        {
            // Tratamento de exceção
            try
            {
                var retorno = repositorio.Inserir(especialidade);
                return Ok(retorno);

            }
            catch (System.Exception ex)
            {

                return StatusCode(500, new
                {
                    Error = "Falha na transação.",
                    Message = ex.Message,
                });
            }


        }

        /// <summary>
        /// Lista as especialidades da aplicação
        /// </summary>
        /// <returns>Lista de especialidades</returns>
        [HttpGet]
        public IActionResult Listar()
        {
            // Tratamento de exceção
            try
            {
                var retorno = repositorio.ListarTodos();
                return Ok(retorno);
            }
            catch (System.Exception ex)
            {

                return StatusCode(500, new
                {
                    Error = "Falha na transação.",
                    Message = ex.Message,
                });
            }
        }

        /// <summary>
        /// Lista especialidade de acordo com o id
        /// </summary>
        /// <param name="id">Id da especialidade</param>
        /// <returns>Lista especialidade do id informado</returns>
        [HttpGet("{id}")]
        public IActionResult BuscarEspecialidadePorId(int id)
        {
            // Tratamento de exceção
            try
            {
                var retorno = repositorio.BuscarPorId(id);

                if (retorno == null)
                {
                    return NotFound(new { Message = "Especialidade não encontrada." });
                }

                return Ok(retorno);
            }
            catch (System.Exception ex)
            {

                return StatusCode(500, new
                {
                    Error = "Falha na transação.",
                    Message = ex.Message,
                });
            }
        }

        /// <summary>
        /// Altera os dados de uma especialidade 
        /// </summary>
        /// <param name="id">Id do usuário</param>
        /// <param name="especialidade">Dados da especialidade</param>
        /// <returns>Especialidade alterada</returns>
        [HttpPut("{id}")]
        public IActionResult Alterar(int id, Especialidade especialidade)
        {
            // Tratamento de exceção
            try
            {
                // Verifica se os ids batem
                if (id != especialidade.Id)
                {
                    return BadRequest();
                }

                // Verifica se o id existe no banco
                var retorno = repositorio.BuscarPorId(id);

                if (retorno == null)
                {
                    return NotFound(new { Message = "Especialidade não encontrada." });
                }

                // Altera efitivamente a especialidade
                repositorio.Alterar(especialidade);

                return NoContent();

            }
            catch (System.Exception ex)
            {
                return StatusCode(500, new
                {
                    Error = "Falha na transação.",
                    Message = ex.Message,
                });
            }
        }

        /// <summary>
        /// Altera parcialmente os dados de uma especialidade 
        /// </summary>
        /// <param name="id">Id da especialidade</param>
        /// <param name="patchEspecialidade">Alguns atributos da especialidade</param>
        /// <returns>Especialidade parcialmente alterada</returns>
        [HttpPatch("{id}")]
        public IActionResult Patch(int id, [FromBody] JsonPatchDocument patchEspecialidade)
        {
            // Tratamento de exceção
            try
            {
                if (patchEspecialidade == null)
                {
                    return BadRequest();
                }

                // Busca obrigatória do objeto
                var especialidade = repositorio.BuscarPorId(id);
                if (especialidade == null)
                {
                    return NotFound(new { Message = "Especialidade não encontrada." });
                }

                repositorio.AlterarParcialmente(patchEspecialidade, especialidade);

                return Ok(especialidade);
            }
            catch (System.Exception ex)
            {

                return StatusCode(500, new
                {
                    Error = "Falha na transação.",
                    Message = ex.Message,
                });
            }
        }

        /// <summary>
        /// Exclui uma especialidade da aplicação
        /// </summary>
        /// <param name="id">Id da especialidade</param>
        /// <returns>Mensagem de exclusão</returns>
        [HttpDelete("{id}")]
        public IActionResult Excluir(int id)
        {
            // Tratamento de exceção
            try
            {
                // Verifica a existência do objeto a ser excluído
                var busca = repositorio.BuscarPorId(id);

                if (busca == null)
                {
                    return NotFound(new { Message = "Especialidade não encontrada." });
                }

                repositorio.Excluir(busca);
                return Ok(new
                {
                    msg = "Especialidade excluída com sucesso."
                });

            }
            catch (System.Exception ex)
            {
                return StatusCode(500, new
                {
                    Error = "Falha na transação.",
                    Message = ex.Message,
                });
            }
        }
    }
}
