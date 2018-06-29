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
    /// Interaction logic for WindowPessoaFisica.xaml
    /// </summary>
    public partial class WindowPessoaFisica : Window, INotifyPropertyChanged
    {
        //Atributos
        private PessoaFisica _PessoaFisica = new PessoaFisica();
        public PessoaFisica PessoaFisicaSelecionada
        {
            get
            {
                return _PessoaFisica;
            }
            set
            {
                _PessoaFisica = value;
                this.NotifyPropertyChanged("PessoaFisicaSelecionada");
            }
        }
        public Boolean ModoCriar { get; set; } = false;
        public Visibility visibilidadeDataGrid
        {
            get
            {
                if (ModoCriar)
                {
                    return Visibility.Hidden;
                }
                else
                {
                    return Visibility.Visible;
                }
            }
        }
        public IList<PessoaFisica> PessoasFisicas { get; set; }

        //Construtor
        public WindowPessoaFisica()
        {
            InitializeComponent();

            //Seta a data de nascimento atual
            this.PessoaFisicaSelecionada.DataNascimento = DateTime.Now;

            this.DataContext = this;

            //Variavel de contexto
            ModelSapatos ctx = new ModelSapatos();

            //Pega lista de Pessoas Fisicas
            this.PessoasFisicas = ctx.pessoasFisicas.ToList();

        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged(String Property)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(Property));
            }
        }

        //Click do botão de salvar
        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            //Variavel de contexto
            ModelSapatos ctx = new ModelSapatos();

            //Verifica se esta criando ou editando
            if (ModoCriar)
            {
                //Adciona a lista
                ctx.pessoasFisicas.Add(PessoaFisicaSelecionada);
                
                //Salva alterações
                ctx.SaveChanges();

                //Mostra mensagem
                MessageBox.Show("Pessoa adcionada com sucesso", "Sucesso", MessageBoxButton.OK);

                //Fecha a tela
                this.Close();
            }
            else
            {
                //Verifica se esta nulo o objeto
                if (PessoaFisicaSelecionada != null && PessoaFisicaSelecionada.ID > 0)
                {
                    //Instancia uma variavel vinda do contexto, procurando pelo ID
                    PessoaFisica ToSave = ctx.pessoasFisicas.Find(PessoaFisicaSelecionada.ID);
                    
                    //Seta valores
                    ToSave.Nome = PessoaFisicaSelecionada.Nome;
                    ToSave.CPF = PessoaFisicaSelecionada.CPF;
                    ToSave.DataNascimento = PessoaFisicaSelecionada.DataNascimento;

                    //Salva alterações
                    ctx.SaveChanges();

                    //Mostra menssagem
                    MessageBox.Show("Dados editados com sucesso", "Sucesso", MessageBoxButton.OK);

                    //Fecha janela
                    this.Close();
                }
            }
        }

        //Botão de cancelar
        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            //Fecha janela
            this.Close();

        }

        //Ação ao apertar tecla no grid
        private void GridPessoaFisica_KeyDown(object sender, KeyEventArgs e)
        {
            //Verifica se foi pressionado o delete
            if(e.Key == Key.Delete)
            {
                //Menssagem de confirmação
                if(MessageBox.Show("Deseja apagar a pessoa "+PessoaFisicaSelecionada.Nome+"?", "Confirmação", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    //Habilita deletar linhas
                    gridPessoaFisica.CanUserDeleteRows = true;

                    //Variavel de contexto
                    ModelSapatos ctx = new ModelSapatos();

                    //Instancia uma variavel vinda do contexto, procurando pelo ID
                    PessoaFisica ToDelete = ctx.pessoasFisicas.Find(PessoaFisicaSelecionada.ID);

                    //Marca o dado como deletado
                    ctx.Entry(ToDelete).State =
                        System.Data.Entity.EntityState.Deleted;

                    //Executa alterações
                    ctx.SaveChanges();
                }
                else
                {
                    //Impossibilita o usuário de deletar as linhas
                    gridPessoaFisica.CanUserDeleteRows = false;
                }
            }
        }

        private void exportarVendaPorPF_Click(object sender, RoutedEventArgs e)
        {
            //Verifica se tem algum sapato delecionado
            if (PessoaFisicaSelecionada.ID == 0)
            {
                //Menssagem de aviso
                MessageBox.Show("Selecione uma pessoa para gerar o relatório", "Aviso", MessageBoxButton.OK);
            }
            else
            {
                //Instancia a classe de exportação e o contexto
                DataExporter exportador = new DataExporter();
                ModelSapatos ctx = new ModelSapatos();

                //Instancia um explorador de arquivos
                Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog();

                //Filtro do dialogo
                dlg.Filter = "Excel (.xlsx)|*.xlsx";

                //Define atributos para o arquivo
                dlg.FileName = "Relatório de vendas do cliente " + PessoaFisicaSelecionada.Nome; // Nome padrão
                dlg.DefaultExt = ".xlsx"; // Extensão do arquivo

                //Pega o resultado, se o usuário apertou cancel ou deu continuidade
                Nullable<bool> result = dlg.ShowDialog();

                //Se o usuário selecionou um local ele execulta o metodo
                if (result == true)
                {
                    //Busca lista de vendas
                    var vendas = ctx.vendas.Where(t => t.pessoaId == PessoaFisicaSelecionada.ID).Select(t => t).ToList();

                    //Chama o metodo para gerar a lista
                    if (exportador.CriarPlanilhaVendaPorPF(vendas, PessoaFisicaSelecionada, dlg.FileName) == true)
                    {
                        //Mostra mensagem
                        MessageBox.Show("Planilha gerada com sucesso", "Sucesso", MessageBoxButton.OK);
                    }
                    else
                    {
                        //Mostra mensagem
                        MessageBox.Show("Erro ao gerar planilha", "Erro", MessageBoxButton.OK);
                    }
                }
            }
        }
    }
}
