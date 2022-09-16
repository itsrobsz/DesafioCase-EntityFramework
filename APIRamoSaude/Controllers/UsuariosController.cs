using APIRamoSaude.Interfaces;
using APIRamoSaude.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace APIRamoSaude.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        // Injeção de dependência do repositório
        private readonly IUsuarioRepository repositorio;

        public UsuariosController(IUsuarioRepository _repositorio)
        {
            repositorio = _repositorio;
        }

        /// <summary>
        /// Cadastra usuário na aplicação
        /// </summary>
        /// <param name="usuario">Dados do usuário</param>
        /// <returns>Dados do usuário cadastrado</returns>
        [HttpPost]
        public IActionResult Cadastrar(Usuario usuario)
        {
            // Tratamento de exceção
            try
            {
                var retorno = repositorio.Inserir(usuario);
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
        /// Lista os usuários da aplicação
        /// </summary>
        /// <returns>Lista de usuários</returns>
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
        /// Lista usuário de acordo com o id
        /// </summary>
        /// <param name="id">Id do usuário</param>
        /// <returns>Lista usuário do id informado</returns>
        [HttpGet("{id}")]
        public IActionResult BuscarUsuarioPorId(int id)
        {
            // Tratamento de exceção
            try
            {
                // Verifica se o id existe no banco
                var retorno = repositorio.BuscarPorId(id);

                if (retorno == null)
                {
                    return NotFound(new { Message = "Usuário não encontrado." });
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
        /// Altera os dados de um usuário
        /// </summary>
        /// <param name="id">Id do usuário</param>
        /// <param name="usuario">Dados do usuário</param>
        /// <returns>Usuário alterado</returns>
        [HttpPut("{id}")]
        public IActionResult Alterar(int id, Usuario usuario)
        {
            // Tratamento de exceção
            try
            {
                // Verifica se os ids batem
                if (id != usuario.Id)
                {
                    return BadRequest();
                }

                // Verifica se o id existe no banco
                var retorno = repositorio.BuscarPorId(id);

                if (retorno == null)
                {
                    return NotFound(new { Message = "Usuário não encontrado." });
                }

                // Altera efitivamente o usuário
                repositorio.Alterar(usuario);

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
        /// Altera parcialmente os dados do usuário
        /// </summary>
        /// <param name="id">Id do usuário</param>
        /// <param name="patchUsuario">Alguns atributos do usuário</param>
        /// <returns>Usuário parcialmente alterado</returns>
        [HttpPatch("{id}")]
        public IActionResult Patch(int id, [FromBody] JsonPatchDocument patchUsuario)
        {
            // Tratamento de exceção
            try
            {
                // Verifica existência do objeto
                if (patchUsuario == null)
                {
                    return BadRequest();
                }

                // Busca obrigatória do objeto por id
                var usuario = repositorio.BuscarPorId(id);
                if (usuario == null)
                {
                    return NotFound(new { Message = "Usuário não encontrado." });
                }

                // Usuário encontrado e alterado parcialmente
                repositorio.AlterarParcialmente(patchUsuario, usuario);

                return Ok(usuario);

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
        /// Exclui um usuário da aplicação
        /// </summary>
        /// <param name="id">Id do usuário</param>
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
                    return NotFound(new { Message = "Usuário não encontrado." });
                }

                repositorio.Excluir(busca);
                return Ok(new
                {
                    msg = "Usuário excluído com sucesso."
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
