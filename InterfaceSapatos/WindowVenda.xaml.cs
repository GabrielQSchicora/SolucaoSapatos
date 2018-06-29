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
    /// Interaction logic for WindowVenda.xaml
    /// </summary>
    public partial class WindowVenda : Window, INotifyPropertyChanged
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
                this.NotifyPropertyChanged("ClienteSelecionado");
                this.NotifyPropertyChanged("SapatoSelecionado");
            }
        }
        public Pessoa ClienteSelecionado { get; set; }
        public Sapato SapatoSelecionado { get; set; }
        public Pessoa PessoaToTransfer { get; set; }
        public IList<String> Tamanhos { get; set; }
        public IList<Venda> Vendas { get; set; }
        public IList<Sapato> Sapatos { get; set; }
        public IList<Pessoa> Pessoas { get; set; } = new List<Pessoa>();
        public IList<PessoaFisica> PessoasFisicas { get; set; }
        public IList<PessoaJuridica> PessoasJuridicas { get; set; }
        public TamanhoSapato TamanhoSelecionado { get; set; }

        //Construtor
        public WindowVenda()
        {
            InitializeComponent();

            this.DataContext = this;

            //Variavel de contexto
            ModelSapatos ctx = new ModelSapatos();

            //Pega lista de pessoas fisicas
            this.PessoasFisicas = ctx.pessoasFisicas.ToList();

            //Pega lista de pessoas juridicas
            this.PessoasJuridicas = ctx.pessoasJuridicas.ToList();

            //Pega lista de sapatos
            this.Sapatos = ctx.sapatos.ToList();

            //Percorre a lista de pessoas fisicas
            foreach (PessoaFisica pessoa in PessoasFisicas)
            {
                //Instancia nova pessoa
                PessoaToTransfer = new Pessoa();

                //Atribui dados
                PessoaToTransfer.ID = pessoa.ID;
                PessoaToTransfer.Nome = pessoa.Nome;

                //Adciona a lista de pessoas
                Pessoas.Add(PessoaToTransfer);
            }

            //Percorre a lista de pessoas juridicas
            foreach (PessoaJuridica pessoa in PessoasJuridicas)
            {
                //Instancia nova pessoa
                PessoaToTransfer = new Pessoa();

                //Atribui dados
                PessoaToTransfer.ID = pessoa.ID;
                PessoaToTransfer.Nome = pessoa.Nome;

                //Adciona a lista de pessoas
                Pessoas.Add(PessoaToTransfer);
            }

            //Percorre a lista de sapatos
            foreach (Sapato sapato in Sapatos)
            {
                //Instancia uma nova lista
                sapato.Tamanhos = new List<TamanhoSapato>();

                //Adciona os tamanhos pertencentes a esse sapato a lista
                sapato.Tamanhos = ctx.tamanhosSapatos.Where(t => t.SapatoId == sapato.ID).Select(t => t).ToList();
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
    
        //Ação do botão de salvar
        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            //Variavel de contexto
            ModelSapatos ctx = new ModelSapatos();

            //Busca na lista de tamanhos do sapato, o tamanho selecionado
            var tamanhoToSelect = VendaSelecionada.sapato.Tamanhos.Where(t => t == TamanhoSelecionado).Select(t => t).ToList();
            
            //Verifica se tem a quantidade em estoque
            if (tamanhoToSelect[0].Quantidade < VendaSelecionada.Quantidade)
            {
                //Menssagem de erro
                MessageBox.Show("Quantidade insuficiente desse item, temos " + tamanhoToSelect[0].Quantidade + " no estoque!", "Aviso", MessageBoxButton.OK);
            }
            else
            {
                //Seta valores
                VendaSelecionada.sapatoId = VendaSelecionada.sapato.ID;
                VendaSelecionada.Modelo = VendaSelecionada.sapato.Nome;
                VendaSelecionada.PrecoUnitario = VendaSelecionada.sapato.Preco;
                VendaSelecionada.PrecoFinal = VendaSelecionada.sapato.Preco * VendaSelecionada.Quantidade;
                VendaSelecionada.sapato = null;
                VendaSelecionada.pessoaId = VendaSelecionada.cliente.ID;
                VendaSelecionada.cliente = null;
                VendaSelecionada.Tamanho = tamanhoToSelect[0].Tamanho;

                //Instancia tamanho selecinado buscando ele do banco
                TamanhoSapato ToEdit = ctx.tamanhosSapatos.Find(tamanhoToSelect[0].ID);

                //Retira a quantidade vendida
                ToEdit.Quantidade = ToEdit.Quantidade - VendaSelecionada.Quantidade;

                //Adciona a venda a lista
                ctx.vendas.Add(VendaSelecionada);

                //Salva alterações
                ctx.SaveChanges();

                //Menssagem de erro
                MessageBox.Show("Venda realizada com sucesso","Sucesso",MessageBoxButton.OK);

                //Fecha a janela
                this.Close();
            }
        }

        //Botão de cancelar
        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            //Fecha a janela
            this.Close();

        }
    }
}
