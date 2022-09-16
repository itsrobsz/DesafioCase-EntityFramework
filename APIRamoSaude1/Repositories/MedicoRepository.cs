﻿using APIRamoSaude.Contexts;
using APIRamoSaude.Interfaces;
using APIRamoSaude.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace APIRamoSaude.Repositories
{
    public class MedicoRepository : IMedicoRepository
    {

        // Injeção de Dependência
        CodeFirstContext ctx;


        // Método Construtor
        public MedicoRepository(CodeFirstContext _ctx)
        {
            ctx = _ctx;
        }


        public void Alterar(Medico medico)
        {
            // Compara o objeto no banco e verifica existência de modificações
            ctx.Entry(medico).State = EntityState.Modified;
            ctx.SaveChanges();
        }

        public void AlterarParcialmente(JsonPatchDocument patchMedico, Medico medico)
        {
            // Pega as informações apenas do que foi alterado e mantém o restante
            patchMedico.ApplyTo(medico);
            ctx.Entry(medico).State = EntityState.Modified;
            ctx.SaveChanges();
        }

        public Medico BuscarPorId(int id)
        {
            return ctx.Medico.Find(id);
        }

        public void Excluir(Medico medico)
        {
            ctx.Medico.Remove(medico);
            ctx.SaveChanges();
        }

        public Medico Inserir(Medico medico)
        {
            ctx.Medico.Add(medico);
            ctx.SaveChanges();
            return medico;
        }

        public ICollection<Medico> ListarTodos()
        {
            return ctx.Medico.ToList();
        }
    }
}
