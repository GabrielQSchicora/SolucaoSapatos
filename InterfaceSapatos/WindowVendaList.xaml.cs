using BibliotecaSapatos;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    /// Interaction logic for WindowVendaList.xaml
    /// </summary>
    public partial class WindowVendaList : Window, INotifyPropertyChanged
    {
        //Atributos
        private Venda _Venda = new Venda();
        public Venda VendaSelecionada
        {
            get
            {
                return _Venda;
            }
            set
            {
                _Venda = value;
                this.NotifyPropertyChanged("VendaSelecionada");
            }
        }
        public IList<Venda> Vendas { get; set; }
        public Pessoa pessoa { get; set; }

        //Construtor
        public WindowVendaList()
        {
            InitializeComponent();

            this.DataContext = this;

            //Variavel de contexto
            ModelSapatos ctx = new ModelSapatos();

            //Pega uma lista de vendas
            this.Vendas = ctx.vendas.ToList();

            foreach(Venda venda in Vendas)
            {
                venda.cliente = ctx.pessoas.Find(venda.pessoaId);
                venda.sapato = ctx.sapatos.Find(venda.sapatoId);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged(String Property)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(Property));
            }
        }

        //Ação de apertar tecla no grid
        private void dataGridVenda_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            //Verifica se foi apertado o delete
            if (e.Key == Key.Delete)
            {
                //Menssagem de confirmação
                if (MessageBox.Show("Deseja apagar a venda de " + VendaSelecionada.Quantidade + " itens do tipo "+ VendaSelecionada.Modelo +"?", "Confirmação", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    //Habilita deletar as linhas
                    dataGridVenda.CanUserDeleteRows = true;

                    //Variavel de contexto
                    ModelSapatos ctx = new ModelSapatos();

                    //Busca o dado no contexto pelo ID
                    Venda ToDelete = ctx.vendas.Find(VendaSelecionada.ID);

                    //Marca como deletado
                    ctx.Entry(ToDelete).State =
                        System.Data.Entity.EntityState.Deleted;

                    //Salva alterações
                    ctx.SaveChanges();
                }
                else
                {
                    //Impede de deletar os dados do grid
                    dataGridVenda.CanUserDeleteRows = false;
                }
            }
        }
    }
}
