using APIRamoSaude.Contexts;
using APIRamoSaude.Interfaces;
using APIRamoSaude.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace APIRamoSaude.Repositories
{
    public class EspecialidadeRepository : IEspecialidadeRepository
    {

        // Injeção de Dependência
        CodeFirstContext ctx;


        // Método Construtor
        public EspecialidadeRepository(CodeFirstContext _ctx)
        {
            ctx = _ctx;
        }

        public void Alterar(Especialidade especialidade)
        {
            // Compara o objeto no banco e verifica existência de modificações
            ctx.Entry(especialidade).State = EntityState.Modified;
            ctx.SaveChanges();
        }

        public void AlterarParcialmente(JsonPatchDocument patchEspecialidade, Especialidade especialidade)
        {
            // Pega as informações apenas do que foi alterado e mantém o restante
            patchEspecialidade.ApplyTo(especialidade);
            ctx.Entry(especialidade).State = EntityState.Modified;
            ctx.SaveChanges();
        }

        public Especialidade BuscarPorId(int id)
        {
            return ctx.Especialidade.Find(id);
        }

        public void Excluir(Especialidade especialidade)
        {
            ctx.Especialidade.Remove(especialidade);
            ctx.SaveChanges();
        }

        public Especialidade Inserir(Especialidade especialidade)
        {
            ctx.Especialidade.Add(especialidade);
            ctx.SaveChanges();
            return especialidade;
        }

        public ICollection<Especialidade> ListarTodos()
        {
            return ctx.Especialidade.ToList();
        }
    }
}
