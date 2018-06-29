using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibliotecaSapatos
{
    public class TamanhoSapato
    {
        //Atributos
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        [Required]
        public String Tamanho { get; set; }
        [Required]
        public int Quantidade { get; set; }

        public int SapatoId { get; set; }

        [ForeignKey("SapatoId")]
        public Sapato Sapato { get; set; }
    }
}
