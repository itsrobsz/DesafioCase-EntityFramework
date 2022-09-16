using APIRamoSaude.Models;
using Microsoft.AspNetCore.JsonPatch;
using System.Collections.Generic;

namespace APIRamoSaude.Interfaces
{
    public interface IEspecialidadeRepository
    {
        //CRUD

        Especialidade Inserir(Especialidade especialidade);
        ICollection<Especialidade> ListarTodos();
        Especialidade BuscarPorId(int id);
        void Alterar(Especialidade especialidade);
        void Excluir(Especialidade especialidade);
        void AlterarParcialmente(JsonPatchDocument patchEspecialidade, Especialidade especialidade);
    }
}
