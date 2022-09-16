using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace APIRamoSaude.Models
{
    public class Especialidade
    {
        [Key]
        public int Id { get; set; }

        // [Required] = Valida que a propriedade tenha um valor
        [Required(ErrorMessage = "É necessário informar uma especialidade.")]
        // Limita o tamanho máximo de caracteres a 30
        [MaxLength(30)]
        public string Categoria { get; set; }

        public virtual ICollection<Medico> Medicos { get; set; }
    }
}
