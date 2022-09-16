using APIRamoSaude.Contexts;
using APIRamoSaude.Interfaces;
using APIRamoSaude.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace APIRamoSaude.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {

        // Injeção de Dependência
        CodeFirstContext ctx;


        // Método Construtor
        public UsuarioRepository(CodeFirstContext _ctx)
        {
            ctx = _ctx;
        }

        public void Alterar(Usuario usuario)
        {
            // Compara o objeto no banco e verifica existência de modificações
            ctx.Entry(usuario).State = EntityState.Modified;
            ctx.SaveChanges();
        }

        public void AlterarParcialmente(JsonPatchDocument patchUsuario, Usuario usuario)
        {
            // Pega as informações apenas do que foi alterado e mantém o restante
            patchUsuario.ApplyTo(usuario);
            ctx.Entry(usuario).State = EntityState.Modified;
            ctx.SaveChanges();
        }

        public Usuario BuscarPorId(int id)
        {
            return ctx.Usuario.Find(id);
        }

        public void Excluir(Usuario usuario)
        {
            ctx.Usuario.Remove(usuario);
            ctx.SaveChanges();
        }

        public Usuario Inserir(Usuario usuario)
        {
            ctx.Usuario.Add(usuario);
            ctx.SaveChanges();
            return usuario;
        }

        public ICollection<Usuario> ListarTodos()
        {
            return ctx.Usuario.ToList();
        }
    }
}
