using APIRamoSaude.Models;
using Microsoft.EntityFrameworkCore;

namespace APIRamoSaude.Contexts
{
    public class CodeFirstContext : DbContext
    {
        // Método construtor para fazer inserção de dependências
        public CodeFirstContext(DbContextOptions<CodeFirstContext> options) : base(options)
        {

        }

        // Apontamento de classes a serem mapeadas
        public DbSet<TipoUsuario> TipoUsuario { get; set; }
        public DbSet<Especialidade> Especialidade { get; set; }
        public DbSet<Usuario> Usuario { get; set; }
        public DbSet<Medico> Medico { get; set; }
        public DbSet<Paciente> Paciente { get; set; }
        public DbSet<Consulta> Consulta { get; set; }


    }
}
