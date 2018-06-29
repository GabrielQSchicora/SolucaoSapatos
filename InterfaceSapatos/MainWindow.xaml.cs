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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace InterfaceSapatos
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //Construtor
        public MainWindow()
        {
            InitializeComponent();
        }

        //Metodo para sair do programa
        private void Sair_Click(object sender, RoutedEventArgs e)
        {
            //Mostra mensagem de confirmação
            if (MessageBox.Show("Sair do programa?", "Confirmação", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                //Fecha o programa
                Application.Current.Shutdown();
            }
        }

        //Tela de gerenciar / Adcionar vendas
        private void GerenciarVendas_Click(object sender, RoutedEventArgs e)
        {
            //Verifica qual botão foi clicado
            if (sender == NovoVenda)
            {
                //Instancia janela de nova venda
                WindowVenda window = new WindowVenda();

                //Mostra janela
                window.ShowDialog();
            }
            else if(sender == GerenciarVendas)
            {
                //Instancia janela de lista de vendas
                WindowVendaList window = new WindowVendaList();

                //Mostra janela
                window.ShowDialog();
            }
        }

        //Tela de Adcionar / Gerenciar Pessoa Fisica
        private void GerenciarPF_Click(object sender, RoutedEventArgs e)
        {
            //Instancia tela de pessoa fisica
            WindowPessoaFisica window =
                       new WindowPessoaFisica();

            //Se foi clicado no botão de adcionar, seta a variavel modo criar como true
            if (sender == NovoPF)
            {
                window.ModoCriar = true;
            }

            //Mostra a tela
            window.ShowDialog();
        }

        //Tela de Adcionar / Gerenciar pessoa juridica
        private void GerenciarPJ_Click(object sender, RoutedEventArgs e)
        {
            //Instancia tela de pessoa juridica
            WindowPessoaJuridica window =
                       new WindowPessoaJuridica();

            //Se foi clicado no botão de adcionar, seta a variavel modo criar como true
            if (sender == NovoPJ)
            {
                window.ModoCriar = true;
            }

            //Mostra a tela
            window.ShowDialog();
        }

        //Tela de Criar / Gerenciar sapatos
        private void GerenciarSapatos_Click(object sender, RoutedEventArgs e)
        {
            //Instancia a tela de sapatos
            WindowSapato window =
                       new WindowSapato();

            //Se foi clicado no botão de adcionar, seta a variavel modo criar como true
            if (sender == novoSapato)
            {
                window.ModoCriar = true;
            }

            //Mostra a tela
            window.ShowDialog();
        }

        //Janela de estoque
        private void Estoque_Click(object sender, RoutedEventArgs e)
        {
            //Instancia janela de estoque
            WindowEstoque window =
                      new WindowEstoque();

            //Mostra a janela
            window.ShowDialog();
        }

        //Função de exportar dados
        private void Exportar_Click(object sender, RoutedEventArgs e)
        {
            //Instancia a classe de exportação e o contexto
            DataExporter exportador = new DataExporter();
            ModelSapatos ctx = new ModelSapatos();

            //Instancia um explorador de arquivos
            Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog();
            
            //Filtro do dialogo
            dlg.Filter = "Excel (.xlsx)|*.xlsx";

            //Verifica qual botão foi clicado para pedir a exportação
            if (sender == exportarPF)
            {
                //Define atributos para o arquivo
                dlg.FileName = "Relatório de pessoas físicas"; // Nome padrão
                dlg.DefaultExt = ".xlsx"; // Extensão do arquivo

                //Pega o resultado, se o usuário apertou cancel ou deu continuidade
                Nullable<bool> result = dlg.ShowDialog();

                //Se o usuário selecionou um local ele execulta o metodo
                if (result == true)
                {
                    //Chama o metodo para gerar a lista
                    if (exportador.CriarPlanilhaPF(ctx.pessoasFisicas.ToList(), dlg.FileName) == true)
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
            else if(sender == exportarPJ)
            {
                //Define atributos para o arquivo
                dlg.FileName = "Relatório de pessoas jurídicas"; // Nome padrão
                dlg.DefaultExt = ".xlsx"; // Extensão do arquivo

                //Pega o resultado, se o usuário apertou cancel ou deu continuidade
                Nullable<bool> result = dlg.ShowDialog();

                //Se o usuário selecionou um local ele execulta o metodo
                if (result == true)
                {
                    //Chama o metodo para gerar a lista
                    if (exportador.CriarPlanilhaPJ(ctx.pessoasJuridicas.ToList(), dlg.FileName) == true)
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
            else if(sender == exportarSapato)
            {
                //Define atributos para o arquivo
                dlg.FileName = "Relatório de sapatos"; // Nome padrão
                dlg.DefaultExt = ".xlsx"; // Extensão do arquivo

                //Pega o resultado, se o usuário apertou cancel ou deu continuidade
                Nullable<bool> result = dlg.ShowDialog();

                //Se o usuário selecionou um local ele execulta o metodo
                if (result == true)
                {
                    //Chama o metodo para gerar a lista
                    if (exportador.CriarPlanilhaSapato(ctx.sapatos.ToList(), dlg.FileName) == true)
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
            else if(sender == exportarEstoque)
            {
                //Define atributos para o arquivo
                dlg.FileName = "Relatório de estoque"; // Nome padrão
                dlg.DefaultExt = ".xlsx"; // Extensão do arquivo

                //Pega o resultado, se o usuário apertou cancel ou deu continuidade
                Nullable<bool> result = dlg.ShowDialog();

                //Se o usuário selecionou um local ele execulta o metodo
                if (result == true)
                {
                    //Chama o metodo para gerar a lista
                    if (exportador.CriarPlanilhaEstoque(ctx.sapatos.ToList(), dlg.FileName) == true)
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
            else if(sender == exportarVenda)
            {
                //Define atributos para o arquivo
                dlg.FileName = "Relatório de venda"; // Nome padrão
                dlg.DefaultExt = ".xlsx"; // Extensão do arquivo

                //Pega o resultado, se o usuário apertou cancel ou deu continuidade
                Nullable<bool> result = dlg.ShowDialog();

                //Se o usuário selecionou um local ele execulta o metodo
                if (result == true) {
                    //Chama o metodo para gerar a lista
                    if(exportador.CriarPlanilhaVendas(ctx.vendas.ToList(), dlg.FileName) == true)
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
