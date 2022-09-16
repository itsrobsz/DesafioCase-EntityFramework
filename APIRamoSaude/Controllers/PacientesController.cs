using APIRamoSaude.Interfaces;
using APIRamoSaude.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace APIRamoSaude.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PacientesController : ControllerBase
    {

        // Injeção de dependência do repositório
        private readonly IPacienteRepository repositorio;

        public PacientesController(IPacienteRepository _repositorio)
        {
            repositorio = _repositorio;
        }

        /// <summary>
        /// Cadastra paciente na aplicação
        /// </summary>
        /// <param name="paciente">Dados do paciente</param>
        /// <returns>Paciente cadastrado</returns>
        [HttpPost]
        public IActionResult Cadastrar(Paciente paciente)
        {
            // Tratamento de exceção
            try
            {
                var retorno = repositorio.Inserir(paciente);
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
        /// Lista pacientes da aplicação
        /// </summary>
        /// <returns>Lista de pacientes</returns>
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
        /// Lista paciente de acordo com o id
        /// </summary>
        /// <param name="id">Id do paciente</param>
        /// <returns>Lista do paciente de id informado</returns>
        [HttpGet("{id}")]
        public IActionResult BuscarPacientePorId(int id)
        {
            // Tratamento de exceção
            try
            {
                var retorno = repositorio.BuscarPorId(id);

                if (retorno == null)
                {
                    return NotFound(new { Message = "Paciente não encontrado." });
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
        /// Altera os dados do paciente
        /// </summary>
        /// <param name="id">Id do paciente</param>
        /// <param name="paciente">Dados do paciente</param>
        /// <returns>Paciente alterado</returns>
        [HttpPut("{id}")]
        public IActionResult Alterar(int id, Paciente paciente)
        {
            // Tratamento de exceção
            try
            {
                // Verifica se os ids batem
                if (id != paciente.Id)
                {
                    return BadRequest();
                }

                // Verifica se o id existe no banco
                var retorno = repositorio.BuscarPorId(id);

                if (retorno == null)
                {
                    return NotFound(new { Message = "Paciente não encontrado." });
                }

                // Altera efitivamente o paciente
                repositorio.Alterar(paciente);

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
        /// Altera parcialmente os dados de um paciente
        /// </summary>
        /// <param name="id">Id do paciente</param>
        /// <param name="patchPaciente">Alguns atributos do paciente</param>
        /// <returns>Paciente parcialmente alterado</returns>
        [HttpPatch("{id}")]
        public IActionResult Patch(int id, [FromBody] JsonPatchDocument patchPaciente)
        {
            // Tratamento de exceção
            try
            {
                if (patchPaciente == null)
                {
                    return BadRequest();
                }

                // Busca obrigatória do objeto
                var paciente = repositorio.BuscarPorId(id);

                // Caso não exista
                if (paciente == null)
                {
                    return NotFound(new { Message = "Paciente não encontrado." });
                }

                // Caso exista, realiza a alteração
                repositorio.AlterarParcialmente(patchPaciente, paciente);

                return Ok(paciente);
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
        /// Exclui um paciente da aplicação
        /// </summary>
        /// <param name="id">Id do paciente</param>
        /// <returns>Paciente excluído</returns>
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
                    return NotFound(new { Message = "Paciente não encontrado." });
                }

                repositorio.Excluir(busca);
                return Ok(new
                {
                    msg = "Paciente excluído com sucesso."
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
