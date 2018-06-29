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
    /// Interaction logic for WindowTamanhoSapato.xaml
    /// </summary>
    public partial class WindowTamanhoSapato : Window, INotifyPropertyChanged
    {
        //Atributos
        private Sapato _Sapato = new Sapato();
        public Sapato SapatoSelecionado
        {
            get
            {
                return _Sapato;
            }
            set
            {
                _Sapato = value;
                this.NotifyPropertyChanged("SapatoSelecionado");
            }
        }
        private TamanhoSapato _TamanhoTempo = new TamanhoSapato();
        public TamanhoSapato TamanhoTemp
        {
            get
            {
                return _TamanhoTempo;
            }
            set
            {
                _TamanhoTempo = value;
                this.NotifyPropertyChanged("TamanhoTemp");
            }
        }
        public TamanhoSapato TamanhoSelecionado { get; set; }
        public int QuantidadeInfo { get; set; }

        //Construtor
        public WindowTamanhoSapato()
        {
            InitializeComponent();

            this.DataContext = this;

            //Variavel de contexto
            ModelSapatos ctx = new ModelSapatos();

        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged(String Property)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(Property));
            }
        }

        //Ação de clicar no botão salvar
        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            //Variavel de contexto
            ModelSapatos ctx = new ModelSapatos();

            //Verifica se todos os campos estão preenchidos
            if (TamanhoTemp == null || TamanhoTemp.Tamanho == null)
            {
                MessageBox.Show("Preencha todos os campos", "Aviso", MessageBoxButton.OK);
            }
            else
            {

                //Verifica se o sapato é nulo
                if (SapatoSelecionado != null && SapatoSelecionado.ID > 0)
                {
                    //Busca no banco um tamanho igual ao informado e que pertença ao sapato informado
                    var tamanho = ctx.tamanhosSapatos.Where(t => t.SapatoId == SapatoSelecionado.ID).Where(t => t.Tamanho == TamanhoTemp.Tamanho).Select(t => t).ToList();

                    //Verifica se retornou resultados
                    if (tamanho.Count() == 0)
                    {
                        //Instancia novo tamanho de sapato
                        TamanhoSelecionado = new TamanhoSapato();

                        //Atribui valores
                        TamanhoSelecionado.SapatoId = SapatoSelecionado.ID;
                        TamanhoSelecionado.Tamanho = TamanhoTemp.Tamanho;
                        TamanhoSelecionado.Quantidade = QuantidadeInfo;
                        TamanhoSelecionado.Sapato = SapatoSelecionado;

                        //Adciona ao contexto
                        ctx.Entry(SapatoSelecionado).State = System.Data.Entity.EntityState.Unchanged;
                        ctx.tamanhosSapatos.Add(TamanhoSelecionado);
                    }
                    else
                    {
                        //Busca no contexto um tamanho pelo ID
                        TamanhoSapato ToSave = ctx.tamanhosSapatos.Find(tamanho[0].ID);

                        //Altera a quantidade
                        ToSave.Quantidade = ToSave.Quantidade + QuantidadeInfo;
                    }

                    //Salva alterações
                    ctx.SaveChanges();

                    //Instancia nova janela de sapatos
                    WindowSapato window = new WindowSapato();

                    //Menssagem de sucesso
                    MessageBox.Show("Tamanho cadastrado com sucesso", "Sucesso", MessageBoxButton.OK);

                    //Mostra a janela de sapatos
                    window.Show();

                    //Fecha essa janela
                    this.Close();
                }
            }

        }

        //Ação do botão de cancelar
        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            //instancia janela de sapatos
            WindowSapato window = new WindowSapato();

            //Mostra a janela
            window.Show();

            //Fecha a janela atual
            this.Close();

        }

        //Função de apertar tecla no grid
        private void GridTamanho_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            //Verifica se foi apertado delete
            if (e.Key == Key.Delete && TamanhoTemp != null)
            {
                //Menssagem de confirmação
                if (MessageBox.Show("Deseja apagar o tamanho " + TamanhoTemp.Tamanho + "?", "Confirmação", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    //Habilita deletar da grid
                    GridTamanho.CanUserDeleteRows = true;

                    //Variavel de contexto
                    ModelSapatos ctx = new ModelSapatos();

                    //Busca o sapato no contexto pelo ID
                    TamanhoSapato ToDelete = ctx.tamanhosSapatos.Find(TamanhoTemp.ID);

                    //Marca como deleted
                    ctx.Entry(ToDelete).State =
                        System.Data.Entity.EntityState.Deleted;

                    //Salva as alterações
                    ctx.SaveChanges();
                }
                else
                {
                    //Desabilita o usuário deletar linhas
                    GridTamanho.CanUserDeleteRows = false;
                }
            }
        }
    }
}
