using APIRamoSaude.Interfaces;
using APIRamoSaude.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace APIRamoSaude.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConsultasController : ControllerBase
    {

        // Injeção de dependência do repositório
        private readonly IConsultaRepository repositorio;

        public ConsultasController(IConsultaRepository _repositorio)
        {
            repositorio = _repositorio;
        }

        /// <summary>
        /// Cadastra consulta na aplicação
        /// </summary>
        /// <param name="consulta">Dados da consulta</param>
        /// <returns>Dados da consulta cadastrado</returns>
        [HttpPost]
        public IActionResult Cadastrar(Consulta consulta)
        {
            // Tratamento de exceção
            try
            {
                var retorno = repositorio.Inserir(consulta);
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
        /// Lista as consultas da aplicação
        /// </summary>
        /// <returns>Lista de consulta</returns>
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
        /// Lista consulta de acordo com o id
        /// </summary>
        /// <param name="id">Id da consulta</param>
        /// <returns>Lista consulta do id informado</returns>
        [HttpGet("{id}")]
        public IActionResult BuscarConsultaPorId(int id)
        {
            // Tratamento de exceção
            try
            {
                var retorno = repositorio.BuscarPorId(id);

                if (retorno == null)
                {
                    return NotFound(new { Message = "Consulta não encontrada." });
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
        /// Altera os dados de uma consulta
        /// </summary>
        /// <param name="id">Id da consulta</param>
        /// <param name="consulta">Dados da consulta</param>
        /// <returns>Consulta alterada</returns>
        [HttpPut("{id}")]
        public IActionResult Alterar(int id, Consulta consulta)
        {
            // Tratamento de exceção
            try
            {
                // Verifica se os ids batem
                if (id != consulta.Id)
                {
                    return BadRequest();
                }

                // Verifica se o id existe no banco
                var retorno = repositorio.BuscarPorId(id);

                if (retorno == null)
                {
                    return NotFound(new { Message = "Consulta não encontrada." });
                }

                // Altera efitivamente a consulta
                repositorio.Alterar(consulta);

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
        /// Altera parcialmente os dados de uma consulta
        /// </summary>
        /// <param name="id">Id da consulta</param>
        /// <param name="patchConsulta">Alguns atributos da consulta</param>
        /// <returns>Consulta parcialmente alterada</returns>
        [HttpPatch("{id}")]
        public IActionResult Patch(int id, [FromBody] JsonPatchDocument patchConsulta)
        {
            // Tratamento de exceção
            try
            {
                if (patchConsulta == null)
                {
                    return BadRequest();
                }

                // Busca obrigatório do objeto
                var consulta = repositorio.BuscarPorId(id);
                if (consulta == null)
                {
                    return NotFound(new { Message = "Consulta não encontrada." });
                }

                repositorio.AlterarParcialmente(patchConsulta, consulta);

                return Ok(consulta);
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
        /// Exclui uma consulta da aplicação
        /// </summary>
        /// <param name="id">Id da consulta</param>
        /// <returns>Consulta excluída</returns>
        [HttpDelete("{id}")]
        public IActionResult Excluir(int id)
        {
            // Tratamento de exceção
            try
            {
                var busca = repositorio.BuscarPorId(id);

                if (busca == null)
                {
                    return NotFound(new { Message = "Consulta não encontrada." });
                }

                repositorio.Excluir(busca);
                return Ok(new
                {
                    msg = "Consulta excluída com sucesso."
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
