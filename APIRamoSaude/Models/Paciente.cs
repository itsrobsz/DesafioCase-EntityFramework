using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace APIRamoSaude.Models
{
    public class Paciente
    {
        [Key]
        public int Id { get; set; }

        // [Required] = Valida que a propriedade tenha um valor
        [Required(ErrorMessage = "É necessário informar o número de carteirinha.")]
        public string Carteirinha { get; set; }

        [Required]
        public DateTime DataNascimento { get; set; }

        public bool Ativo { get; set; }


        [ForeignKey("Usuario")]
        public int IdUsuario { get; set; }
        public Usuario Usuario { get; set; }


        public virtual ICollection<Consulta> Consultas { get; set; }
    }
}
