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
    /// Interaction logic for WindowPessoaJuridica.xaml
    /// </summary>
    public partial class WindowPessoaJuridica : Window, INotifyPropertyChanged
    {
        //Atributos
        private PessoaJuridica _PessoaJuridica = new PessoaJuridica();
        public PessoaJuridica PessoaJuridicaSelecionada
        {
            get
            {
                return _PessoaJuridica;
            }
            set
            {
                _PessoaJuridica = value;
                this.NotifyPropertyChanged("PessoaJuridicaSelecionada");
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
        public IList<PessoaJuridica> PessoasJuridicas { get; set; }

        //Construtor
        public WindowPessoaJuridica()
        {
            InitializeComponent();

            this.DataContext = this;

            //Variavel de contexto
            ModelSapatos ctx = new ModelSapatos();

            //Pega uma lista de pessoas juridicas
            this.PessoasJuridicas = ctx.pessoasJuridicas.ToList();
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

            //Verifica se esta criando ou editando
            if (ModoCriar)
            {
                //Adciona a pessoa preenchida
                ctx.pessoasJuridicas.Add(PessoaJuridicaSelecionada);

                //Salva alterações
                ctx.SaveChanges();

                //Menssagem de sucesso
                MessageBox.Show("Pessoa adcionada com sucesso","Sucesso", MessageBoxButton.OK);

                //Fecha a janela
                this.Close();
            }
            else
            {
                //Verifica se a pessoa é nula
                if (PessoaJuridicaSelecionada != null && PessoaJuridicaSelecionada.ID > 0)
                {
                    //Pega a pessoa do contexto pelo ID
                    PessoaJuridica ToSave = ctx.pessoasJuridicas.Find(PessoaJuridicaSelecionada.ID);
                    
                    //Atribui valores
                    ToSave.Nome = PessoaJuridicaSelecionada.Nome;
                    ToSave.RazaoSocial = PessoaJuridicaSelecionada.RazaoSocial;
                    ToSave.CNPJ = PessoaJuridicaSelecionada.CNPJ;
                    ToSave.Endereco = PessoaJuridicaSelecionada.Endereco;

                    //Salva as alterações
                    ctx.SaveChanges();

                    //Menssagem de sucesso
                    MessageBox.Show("Dados editados com sucesso", "Sucesso", MessageBoxButton.OK);

                    //Fecha a janela
                    this.Close();
                }
            }
        }

        //Ação do botão de cancelar
        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            //Fecha a janela
            this.Close();

        }

        //Ação de apertar tecla no grid
        private void GridPessoaJuridica_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            //Verifica se é a tecla delete
            if (e.Key == Key.Delete)
            {
                //Menssagem de confirmação
                if (MessageBox.Show("Deseja apagar a pessoa " + PessoaJuridicaSelecionada.Nome + "?", "Confirmação", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    //Habilita deletar linhas
                    gridPessoaJuridica.CanUserDeleteRows = true;

                    //Variavel de contexto
                    ModelSapatos ctx = new ModelSapatos();

                    //Busca a pessoa no contexto pelo ID
                    PessoaJuridica ToDelete = ctx.pessoasJuridicas.Find(PessoaJuridicaSelecionada.ID);

                    //Marca como deletada
                    ctx.Entry(ToDelete).State =
                        System.Data.Entity.EntityState.Deleted;

                    //Salva alterações
                    ctx.SaveChanges();
                }
                else
                {
                    //Impede o usuário de deletar linhas
                    gridPessoaJuridica.CanUserDeleteRows = false;
                }
            }
        }

        private void exportarVendaPorPJ_Click(object sender, RoutedEventArgs e)
        {
            //Verifica se tem algum sapato delecionado
            if (PessoaJuridicaSelecionada.ID == 0)
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
                dlg.FileName = "Relatório de vendas do cliente " + PessoaJuridicaSelecionada.Nome; // Nome padrão
                dlg.DefaultExt = ".xlsx"; // Extensão do arquivo

                //Pega o resultado, se o usuário apertou cancel ou deu continuidade
                Nullable<bool> result = dlg.ShowDialog();

                //Se o usuário selecionou um local ele execulta o metodo
                if (result == true)
                {
                    //Busca lista de vendas
                    var vendas = ctx.vendas.Where(t => t.pessoaId == PessoaJuridicaSelecionada.ID).Select(t => t).ToList();

                    //Chama o metodo para gerar a lista
                    if (exportador.CriarPlanilhaVendaPorPJ(vendas, PessoaJuridicaSelecionada, dlg.FileName) == true)
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
