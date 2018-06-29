using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibliotecaSapatos
{
    public class PessoaJuridica : Pessoa
    {
        //Atributos
        [Required]
        public String RazaoSocial { get; set; }
        [Required]
        public String CNPJ { get; set; }
        [Required]
        public String Endereco { get; set; }
    }
}
