using APIRamoSaude.Interfaces;
using APIRamoSaude.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace APIRamoSaude.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TipoUsuariosController : ControllerBase
    {
        // Injeção de dependência do repositório
        private readonly ITipoUsuarioRepository repositorio;

        public TipoUsuariosController(ITipoUsuarioRepository _repositorio)
        {
            repositorio = _repositorio;
        }

        /// <summary>
        /// Cadastra tipo de usuário na aplicação
        /// </summary>
        /// <param name="tipoUsuario">Dados do tipo usuário</param>
        /// <returns>Tipo de usuário cadastrado</returns>
        [HttpPost]
        public IActionResult Cadastrar(TipoUsuario tipoUsuario)
        {
            // Tratamento de exceção
            try
            {
                // Verifica se o enum bate e insere o enum respectivo
                if(tipoUsuario.Tipo == TipoUsuario.UsuarioTipo.Paciente || tipoUsuario.Tipo == TipoUsuario.UsuarioTipo.Medico)
                {
                    var retorno = repositorio.Inserir(tipoUsuario);
                    return Ok(retorno);
                }

                else
                {
                    return BadRequest(new {Message = "Tipo de usuário inválido."}); 
                }
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
        /// Lista os tipos de usuário da aplicação
        /// </summary>
        /// <returns>Lista de tipos de usuário</returns>
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
        /// Lista o tipo de usuário de acordo com o id
        /// </summary>
        /// <param name="id">Id do tipo de usuário</param>
        /// <returns>Lista tipo de usuário do id informado</returns>
        [HttpGet("{id}")]
        public IActionResult BuscarTipoUsuarioPorId(int id)
        {
            // Tratamento de exceção
            try
            {
                var retorno = repositorio.BuscarPorId(id);

                if (retorno == null)
                {
                    return NotFound( new {Message = "Tipo de usuário não encontrado."});
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
        /// Altera os dados de um tipo de usuário
        /// </summary>
        /// <param name="id">Id do tipo de usuário</param>
        /// <param name="tipoUsuario">Dados do tipo de usuário</param>
        /// <returns>Tipo de usuário alterado</returns>
        [HttpPut("{id}")]
        public IActionResult Alterar(int id, TipoUsuario tipoUsuario)
        {
            // Tratamento de exceção
            try
            {
                // Verifica se os ids batem
                if (id != tipoUsuario.Id)
                {
                    return BadRequest();
                }

                // Verifica se o id existe no banco
                var retorno = repositorio.BuscarPorId(id);

                if (retorno == null)
                {
                    return NotFound(new { Message = "Tipo de usuário não encontrado." });
                }

                // Altera efitivamente o tipo de usuário
                repositorio.Alterar(tipoUsuario);

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
        /// Altera parcialmente um tipo de usuário
        /// </summary>
        /// <param name="id">Id do tipo de usário</param>
        /// <param name="patchTipoUsuario">Alguns atributos do tipo de usuário</param>
        /// <returns>Tipo de usuário parcialmente alterado</returns>
        [HttpPatch("{id}")]
        public IActionResult Patch(int id, [FromBody] JsonPatchDocument patchTipoUsuario)
        {
            // Tratamento de exceção
            try
            {
                if (patchTipoUsuario == null)
                {
                    return BadRequest();
                }

                // Busca obrigatório do objeto
                var tipoUsuario = repositorio.BuscarPorId(id);
                if (tipoUsuario == null)
                {
                    return NotFound(new { Message = "Tipo de usuário não encontrado." });
                }

                repositorio.AlterarParcialmente(patchTipoUsuario, tipoUsuario);

                return Ok(tipoUsuario);
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
        /// Exclui um tipo de usuário da aplicação
        /// </summary>
        /// <param name="id">Id do tipo de usuário</param>
        /// <returns>Tipo de usuário excluído</returns>
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
                    return NotFound(new { Message = "Tipo de usuário não encontrado." });
                }

                repositorio.Excluir(busca);
                return Ok(new
                {
                    msg = "Tipo de usuário excluído com sucesso."
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
