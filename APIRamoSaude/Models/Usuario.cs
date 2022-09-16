using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace APIRamoSaude.Models
{
    public class Usuario
    {

        [Key]
        public int Id { get; set; }

        // [Required] = Valida que a propriedade tenha um valor
        [Required(ErrorMessage = "Informe um nome.")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Digite um email.")]
        // Valida o formato do email
        [RegularExpression(".+\\@.+\\..+", ErrorMessage = "Informe um email válido.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Digite uma senha.")]
        // Define um tamanho mínimo a ser digitado
        [MinLength(8)]
        public string Senha { get; set; }


        [ForeignKey("TipoUsuario")]
        public int IdTipoUsuario { get; set; }
        public TipoUsuario TipoUsuario { get; set; }

    }
}
