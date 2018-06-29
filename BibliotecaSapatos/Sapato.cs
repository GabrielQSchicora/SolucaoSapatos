using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using static System.Net.Mime.MediaTypeNames;

namespace BibliotecaSapatos
{
    public class Sapato
    {
        //Atributos
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        [Required]
        public String Nome { get; set; }
        [Required]
        public String Cadarco { get; set; }
        [Required]
        public String Material { get; set; }
        [Required]
        public String Cor { get; set; }
        [Required]
        public Decimal Preco { get; set; }
        [Required]
        public byte[] Imagem { get; set; }

        [InverseProperty("Sapato")]
        public IList<TamanhoSapato> Tamanhos { get; set; }

        public String tamanhosToString()
        {

            String tamanhoInString = "";
            int i = 1;

            foreach(TamanhoSapato tamanho in this.Tamanhos)
            {
                tamanhoInString = tamanhoInString + tamanho.Tamanho;

                if(i < this.Tamanhos.Count())
                {
                    tamanhoInString = tamanhoInString + ", ";
                }

                i++;

            }

            return tamanhoInString;
        }

    }
}
