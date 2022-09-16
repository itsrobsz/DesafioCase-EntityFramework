using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace APIRamoSaude.Models
{
    public class TipoUsuario
    {
        [Key]
        public int Id { get; set; }

        // [Required] = Valida que a propriedade tenha um valor
        [Required]
        public UsuarioTipo Tipo { get; set; }
        public virtual ICollection<Usuario> Usuarios { get; set; }

        public enum UsuarioTipo
        {
            Paciente,
            Medico
        }

    }
}
