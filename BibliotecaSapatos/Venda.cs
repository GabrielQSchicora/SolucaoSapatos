using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibliotecaSapatos
{
    public class Venda
    {
        //Atributos
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [Required]
        public int sapatoId { get; set; }

        [ForeignKey("sapatoId")]
        public Sapato sapato { get; set; }
        [Required]
        public String Modelo { get; set; }
        [Required]
        public String Tamanho { get; set; }
        [Required]
        public int Quantidade { get; set; }
        [Required]
        public Decimal PrecoUnitario { get; set; }
        [Required]
        public Decimal PrecoFinal { get; set; }

        [Required]
        public int pessoaId { get; set; }

        [ForeignKey("pessoaId")]
        public Pessoa cliente { get; set; }
    }
}
