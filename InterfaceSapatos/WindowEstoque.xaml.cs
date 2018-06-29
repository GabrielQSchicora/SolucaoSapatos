using BibliotecaSapatos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace InterfaceSapatos
{
    /// <summary>
    /// Interaction logic for WindowEstoque.xaml
    /// </summary>
    public partial class WindowEstoque : Window
    {
        //Atributos
        public int QuantidadeTotal { get; set; }
        public IList<Sapato> Sapatos { get; set; }
        public Estoque estoque { get; set; }
        public IList<Estoque> EstoqueCompleto { get; set; } = new List<Estoque>();

        //Construtor
        public WindowEstoque()
        {
            InitializeComponent();

            this.DataContext = this;

            //Variavel de contexto
            ModelSapatos ctx = new ModelSapatos();

            //Recee uma lista de sapatos
            this.Sapatos = ctx.sapatos.ToList();
            
            //Percorre a lista de sapatos
            foreach (Sapato sapato in Sapatos)
            {
                //Pega todos os tamanhos de sapatos relacionados a esse sapato
                sapato.Tamanhos = ctx.tamanhosSapatos.Where(t => t.SapatoId == sapato.ID).Select(t => t).ToList();

                QuantidadeTotal = 0;

                //Verifica se tem tamanhos cadastrados
                if (sapato.Tamanhos != null)
                {
                    //Percorre essa lista de tamanhos
                    foreach (TamanhoSapato tam in sapato.Tamanhos)
                    {
                        //Soma a quantidade total
                        QuantidadeTotal = QuantidadeTotal + tam.Quantidade;
                    }
                }

                //Instancia a variavel deestoque
                this.estoque = new Estoque();

                //Seta valores
                this.estoque.Modelo = sapato.Nome;
                this.estoque.Cadarco = sapato.Cadarco;
                this.estoque.Material = sapato.Material;
                this.estoque.Cor = sapato.Cor;
                this.estoque.Preco = sapato.Preco;
                this.estoque.QuantidadeTotal = QuantidadeTotal;
                              
                //Adciona a lista
                this.EstoqueCompleto.Add(this.estoque);
                

            }
        }
    }
}
