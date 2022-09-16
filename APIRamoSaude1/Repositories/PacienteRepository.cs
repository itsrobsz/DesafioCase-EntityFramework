using APIRamoSaude.Contexts;
using APIRamoSaude.Interfaces;
using APIRamoSaude.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace APIRamoSaude.Repositories
{
    public class PacienteRepository : IPacienteRepository
    {

        // Injeção de Dependência
        CodeFirstContext ctx;


        // Método Construtor
        public PacienteRepository(CodeFirstContext _ctx)
        {
            ctx = _ctx;
        }


        public void Alterar(Paciente paciente)
        {
            // Compara o objeto no banco e verifica existência de modificações
            ctx.Entry(paciente).State = EntityState.Modified;
            ctx.SaveChanges();
        }

        public void AlterarParcialmente(JsonPatchDocument patchPaciente, Paciente paciente)
        {
            // Pega as informações apenas do que foi alterado e mantém o restante
            patchPaciente.ApplyTo(paciente);
            ctx.Entry(paciente).State = EntityState.Modified;
            ctx.SaveChanges();
        }

        public Paciente BuscarPorId(int id)
        {
            return ctx.Paciente.Find(id);
        }

        public void Excluir(Paciente paciente)
        {
            ctx.Paciente.Remove(paciente);
            ctx.SaveChanges();
        }

        public Paciente Inserir(Paciente paciente)
        {
            ctx.Paciente.Add(paciente);
            ctx.SaveChanges();
            return paciente;
        }

        public ICollection<Paciente> ListarTodos()
        {
            // Retorna a lista de pacientes usando a biblioteca linq
            return ctx.Paciente.ToList();
        }
    }
}
