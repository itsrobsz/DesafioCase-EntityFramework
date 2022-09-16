using APIRamoSaude.Interfaces;
using APIRamoSaude.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace APIRamoSaude.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MedicosController : ControllerBase
    {

        // Injeção de dependência do repositório
        private readonly IMedicoRepository repositorio;

        public MedicosController(IMedicoRepository _repositorio)
        {
            repositorio = _repositorio;
        }

        /// <summary>
        /// Cadastra médico na aplicação
        /// </summary>
        /// <param name="medico">Dados do médico</param>
        /// <returns>Dados do médico cadastrado</returns>
        [HttpPost]
        public IActionResult Cadastrar(Medico medico)
        {
            // Tratamento de exceção
            try
            {
                var retorno = repositorio.Inserir(medico);
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
        /// Lista os médicos da aplicação
        /// </summary>
        /// <returns>Lista de médicos</returns>
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
        /// Lista médico de acordo com o id
        /// </summary>
        /// <param name="id">Id do médico</param>
        /// <returns>Lista médico de id informado</returns>
        [HttpGet("{id}")]
        public IActionResult BuscarMedicoPorId(int id)
        {
            // Tratamento de exceção
            try
            {
                // Verifica a existência do objeto a ser mostrado
                var retorno = repositorio.BuscarPorId(id);

                if (retorno == null)
                {
                    return NotFound(new { Message = "Médico não encontrado." });
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
        /// Altera os dados de um médico
        /// </summary>
        /// <param name="id">Id do médico</param>
        /// <param name="medico">Dados do médico</param>
        /// <returns>Médico alterado</returns>
        [HttpPut("{id}")]
        public IActionResult Alterar(int id, Medico medico)
        {
            // Tratamento de exceção
            try
            {
                // Verifica se os ids batem
                if (id != medico.Id)
                {
                    return BadRequest();
                }

                // Verifica se o id existe no banco
                var retorno = repositorio.BuscarPorId(id);

                if (retorno == null)
                {
                    return NotFound(new { Message = "Médico não encontrado." });
                }

                // Altera efitivamente o médico
                repositorio.Alterar(medico);

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
        /// Altera parcialmente os dados de um médico
        /// </summary>
        /// <param name="id">Id do médico</param>
        /// <param name="patchMedico">Alguns atributos do médico</param>
        /// <returns>Médico parcialmente alterado</returns>
        [HttpPatch("{id}")]
        public IActionResult Patch(int id, [FromBody] JsonPatchDocument patchMedico)
        {
            // Tratamento de exceção
            try
            {
                if (patchMedico == null)
                {
                    return BadRequest();
                }

                // Busca obrigatória do objeto
                var medico = repositorio.BuscarPorId(id);
                if (medico == null)
                {
                    return NotFound(new { Message = "Médico não encontrado." });
                }

                repositorio.AlterarParcialmente(patchMedico, medico);

                return Ok(medico);
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
        /// Exclui um médico da aplicação
        /// </summary>
        /// <param name="id">Id do médico</param>
        /// <returns>Médico excluído</returns>
        [HttpDelete("{id}")]
        public IActionResult Excluir(int id)
        {
            // Tratamento de exceção
            try
            {
                // Verifica existência do objeto a ser excluído
                var busca = repositorio.BuscarPorId(id);

                if (busca == null)
                {
                    return NotFound(new { Message = "Médico não encontrado." });
                }

                repositorio.Excluir(busca);
                return Ok(new
                {
                    msg = "Médico excluído com sucesso."
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
