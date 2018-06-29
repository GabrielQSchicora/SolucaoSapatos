using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibliotecaSapatos
{
    public class Estoque
    {
        //Atributos
        public String Modelo { get; set; }
        public String Cadarco { get; set; }
        public String Material { get; set; }
        public String Cor { get; set; }
        public Decimal Preco { get; set; }
        public int QuantidadeTotal { get; set; }
    }
}
