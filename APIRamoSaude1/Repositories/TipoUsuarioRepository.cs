using APIRamoSaude.Contexts;
using APIRamoSaude.Interfaces;
using APIRamoSaude.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace APIRamoSaude.Repositories
{

    public class TipoUsuarioRepository : ITipoUsuarioRepository
    {
        // Injeção de Dependência
        CodeFirstContext ctx;


        // Método Construtor
        public TipoUsuarioRepository(CodeFirstContext _ctx)
        {
            ctx = _ctx;
        }

        public void Alterar(TipoUsuario tipoUsuario)
        {
            // Compara o objeto no banco e verifica existência de modificações
            ctx.Entry(tipoUsuario).State = EntityState.Modified;
            ctx.SaveChanges();
        }

        public void AlterarParcialmente(JsonPatchDocument patchTipoUsuario, TipoUsuario tipoUsuario)
        {
            // Pega as informações apenas do que foi alterado e mantém o restante
            patchTipoUsuario.ApplyTo(tipoUsuario);
            ctx.Entry(tipoUsuario).State = EntityState.Modified;
            ctx.SaveChanges();
        }

        public TipoUsuario BuscarPorId(int id)
        {
            return ctx.TipoUsuario.Find(id);
        }

        public void Excluir(TipoUsuario tipoUsuario)
        {
            ctx.TipoUsuario.Remove(tipoUsuario);
            ctx.SaveChanges();
            ctx.SaveChanges();
        }

        public TipoUsuario Inserir(TipoUsuario tipoUsuario)
        {
            ctx.TipoUsuario.Add(tipoUsuario);
            ctx.SaveChanges();
            return tipoUsuario;
        }

        public ICollection<TipoUsuario> ListarTodos()
        {
            return ctx.TipoUsuario.ToList();
        }
    }
}
