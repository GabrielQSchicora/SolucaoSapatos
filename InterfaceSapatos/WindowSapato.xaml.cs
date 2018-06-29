using BibliotecaSapatos;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
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
    /// Interaction logic for WindowSapato.xaml
    /// </summary>
    public partial class WindowSapato : Window, INotifyPropertyChanged
    {
        //Atrubitos
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
        public IList<Sapato> Sapatos { get; set; }
        public BitmapImage imagemSelecionada { get; set; }
        public IList<String> OpcoesCadarcos { get; set; } = new List<String>() { "Sim", "Não" };

        //Construtor
        public WindowSapato()
        {
            InitializeComponent();

            this.DataContext = this;

            //Variavel de contexto
            ModelSapatos ctx = new ModelSapatos();

            //Pega uma lista dos sapatos
            this.Sapatos = ctx.sapatos.ToList();

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

            //Verifica se esta criando ou editando
            if (ModoCriar)
            {
                //Codifica itmapImage em Array de binario
                JpegBitmapEncoder encoder = new JpegBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create(imagemSelecionada));
                using (MemoryStream ms = new MemoryStream())
                {
                    encoder.Save(ms);
                    SapatoSelecionado.Imagem = ms.ToArray();
                }

                //Adciona o sapato a lista
                ctx.sapatos.Add(SapatoSelecionado);

                //Salva alterações
                ctx.SaveChanges();

                //Menssagem
                MessageBox.Show("Sapato adcionado com sucesso", "Sucesso", MessageBoxButton.OK);

                //Fecha a tela
                this.Close();
            }
            else
            {
                //Verifica se o sapato esta nulo
                if (SapatoSelecionado != null && SapatoSelecionado.ID > 0)
                {
                    //Busca o sapato no contexto pelo id
                    Sapato ToSave = ctx.sapatos.Find(SapatoSelecionado.ID);

                    //Atribui valores
                    ToSave.Nome = SapatoSelecionado.Nome;
                    ToSave.Preco = SapatoSelecionado.Preco;
                    ToSave.Material = SapatoSelecionado.Material;

                    if (imagemSelecionada == null)
                    {
                        ToSave.Imagem = SapatoSelecionado.Imagem;
                    }
                    else
                    {
                        //Codifica itmapImage em Array de binario
                        JpegBitmapEncoder encoder = new JpegBitmapEncoder();
                        encoder.Frames.Add(BitmapFrame.Create(imagemSelecionada));
                        using (MemoryStream ms = new MemoryStream())
                        {
                            encoder.Save(ms);
                            SapatoSelecionado.Imagem = ms.ToArray();
                        }
                    }

                    ToSave.Imagem = SapatoSelecionado.Imagem;
                    ToSave.Cadarco = SapatoSelecionado.Cadarco;
                    ToSave.Cor = SapatoSelecionado.Cor;

                    //Salva alterações
                    ctx.SaveChanges();

                    //Menssagem
                    MessageBox.Show("Dados editados com sucesso", "Sucesso", MessageBoxButton.OK);

                    //Fecha tela
                    this.Close();
                }
            }
        }

        //Botão de cancelar
        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            //Fecha tela
            this.Close();

        }

        //Click do botão de selecionar imagem
        private void ButtonImagem_Click(object sender, RoutedEventArgs e)
        {
            //Instancia variael de dialog
            OpenFileDialog dialog = new OpenFileDialog();

            //Seta os filtros
            dialog.Filter = "jpg files(*.jpg)|*.jpg| PNG files(*.png)|*.png| All Files(*.*)|*.*";

            //Verifica se foi enviado imagem
            if(dialog.ShowDialog() == true)
            {
                //Seta o valor da imagem como bitmapImage para a view e para a variavel auxiliar no code behind
                Image.Source = new BitmapImage(new Uri(dialog.FileName));
                imagemSelecionada = new BitmapImage(new Uri(dialog.FileName));
            }

        }

        //Função de apertar tecla no grid
        private void gridSapato_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            //Verifica se foi apertado delete
            if (e.Key == Key.Delete)
            {
                //Menssagem de confirmação
                if (MessageBox.Show("Deseja apagar o sapato " + SapatoSelecionado.Nome + "?", "Confirmação", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    //Habilita deletar da grid
                    gridSapato.CanUserDeleteRows = true;

                    //Variavel de contexto
                    ModelSapatos ctx = new ModelSapatos();

                    //Busca o sapato no contexto pelo ID
                    Sapato ToDelete = ctx.sapatos.Find(SapatoSelecionado.ID);

                    //Marca como deleted
                    ctx.Entry(ToDelete).State =
                        System.Data.Entity.EntityState.Deleted;

                    //Salva as alterações
                    ctx.SaveChanges();
                }
                else
                {
                    //Desabilita o usuário deletar linhas
                    gridSapato.CanUserDeleteRows = false;
                }
            }
        }

        //Clique do botão para cadastrar tamanho
        private void CadastrarTamanho_Click(object sender, RoutedEventArgs e)
        {
            //Verifica se tem algum sapato delecionado
            if (SapatoSelecionado.ID == 0)
            {
                //Menssagem de aviso
                MessageBox.Show("Selecione um sapato para adcionar tamanhos", "Aviso", MessageBoxButton.OK);
            }
            else
            {
                //Instancia nova janela de tamanho do sapato
                WindowTamanhoSapato windowSapato = new WindowTamanhoSapato();

                //Passa o sapato selecionado para a outra tela
                windowSapato.SapatoSelecionado = SapatoSelecionado;

                //Mostra a outra tela
                windowSapato.Show();

                //Fecha a tela atual
                this.Close();
            }
        }

        private void GerarListaVendas_Click(object sender, RoutedEventArgs e)
        {
            //Verifica se tem algum sapato delecionado
            if (SapatoSelecionado.ID == 0)
            {
                //Menssagem de aviso
                MessageBox.Show("Selecione um sapato para gerar o relatório", "Aviso", MessageBoxButton.OK);
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
                dlg.FileName = "Relatório de vendas do modelo "+SapatoSelecionado.Nome; // Nome padrão
                dlg.DefaultExt = ".xlsx"; // Extensão do arquivo

                //Pega o resultado, se o usuário apertou cancel ou deu continuidade
                Nullable<bool> result = dlg.ShowDialog();

                //Se o usuário selecionou um local ele execulta o metodo
                if (result == true)
                {
                    //Busca lista de vendas
                    var vendas = ctx.vendas.Where(t => t.sapatoId == SapatoSelecionado.ID).Select(t => t).ToList();

                    //Chama o metodo para gerar a lista
                    if (exportador.CriarPlanilhaVendaPorSapato(vendas, SapatoSelecionado, dlg.FileName) == true)
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
