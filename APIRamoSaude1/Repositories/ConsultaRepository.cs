using APIRamoSaude.Contexts;
using APIRamoSaude.Interfaces;
using APIRamoSaude.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace APIRamoSaude.Repositories
{
    public class ConsultaRepository : IConsultaRepository
    {
        // Injeção de Dependência
        CodeFirstContext ctx;


        // Método Construtor
        public ConsultaRepository(CodeFirstContext _ctx)
        {
            ctx = _ctx;
        }

        public void Alterar(Consulta consulta)
        {
            // Compara o objeto no banco e verifica existência de modificações
            ctx.Entry(consulta).State = EntityState.Modified;
            ctx.SaveChanges();
        }

        public void AlterarParcialmente(JsonPatchDocument patchConsulta, Consulta consulta)
        {
            // Pega as informações apenas do que foi alterado e mantém o restante
            patchConsulta.ApplyTo(consulta);
            ctx.Entry(consulta).State = EntityState.Modified;
            ctx.SaveChanges();
        }

        public Consulta BuscarPorId(int id)
        {
            return ctx.Consulta.Find(id);
        }

        public void Excluir(Consulta consulta)
        {
            ctx.Consulta.Remove(consulta);
            ctx.SaveChanges();
        }

        public Consulta Inserir(Consulta consulta)
        {
            ctx.Consulta.Add(consulta);
            ctx.SaveChanges();
            return consulta;
        }

        public ICollection<Consulta> ListarTodos()
        {
            return ctx.Consulta.ToList();
        }
    }
}
