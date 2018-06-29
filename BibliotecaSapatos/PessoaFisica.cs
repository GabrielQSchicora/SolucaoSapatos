using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibliotecaSapatos
{
    public class PessoaFisica : Pessoa
    {
        //Atributos
        [Required]
        public String CPF { get; set; }
        [Required]
        public DateTime DataNascimento { get; set; }
    }
}
