using APIRamoSaude.Models;
using Microsoft.AspNetCore.JsonPatch;
using System.Collections.Generic;

namespace APIRamoSaude.Interfaces
{
    public interface IMedicoRepository
    {
        //CRUD

        Medico Inserir(Medico medico);
        ICollection<Medico> ListarTodos();
        Medico BuscarPorId(int id);
        void Alterar(Medico medico);
        void Excluir(Medico medico);
        void AlterarParcialmente(JsonPatchDocument patchMedico, Medico medico);
    }
}
