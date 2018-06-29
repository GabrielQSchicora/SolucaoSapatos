using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibliotecaSapatos
{
    public class DataExporter
    {
        //Instancia o contexto
        ModelSapatos ctx = new ModelSapatos();

        //Metodo para criar planilha de pessoa fisica
        public Boolean CriarPlanilhaPF(IList<PessoaFisica> pessoasFisicas, String caminho)
        {
            //Instancia o objeto
            var workbook = new XLWorkbook();

            //Adciona a pasta de trabalho chamada "Vendas"
            var worksheet = workbook.Worksheets.Add("Pessoas Físicas");

            //Define as colunas
            var columnId = worksheet.Column("A");
            var columnNome = worksheet.Column("B");
            var columnCPF = worksheet.Column("C");
            var columnDataNascimento = worksheet.Column("D");
            var columnIdade = worksheet.Column("E");

            //Define a largura da coluna
            columnId.Width = 5;
            columnNome.Width = 18;
            columnCPF.Width = 18;
            columnDataNascimento.Width = 18;
            columnIdade.Width = 18;

            //Define uma linha para iniciar o preenchimento
            int line = 1;

            //Define os textos do cabeçalho da planilha
            columnId.Cell(line).Value = "ID";
            columnNome.Cell(line).Value = "Nome";
            columnCPF.Cell(line).Value = "CPF";
            columnDataNascimento.Cell(line).Value = "Data de Nascimento";
            columnIdade.Cell(line).Value = "Idade";

            //Informa que a cor de fundo da linha sera cinza e a fonte negrito
            worksheet.Row(line).Style.Fill.BackgroundColor = XLColor.Gray;
            worksheet.Row(line).Style.Font.Bold = true;

            //Acrescenta a linha
            line++;

            //Percorre a lista de vendas
            foreach (PessoaFisica pessoa in pessoasFisicas)
            {
                //Atribui os dados a cada linha
                columnId.Cell(line).Value = pessoa.ID;
                columnNome.Cell(line).Value = pessoa.Nome;
                columnCPF.Cell(line).Value = pessoa.CPF;
                columnDataNascimento.Cell(line).Value = pessoa.DataNascimento;
                columnIdade.Cell(line).Value = "=ARREDMULTB(FRAÇÃOANO(HOJE();d" + line + ");1) ";

                //Acrescenta a linha
                line++;
            }

            //Estilo da planilha
            workbook.ReferenceStyle = XLReferenceStyle.A1;

            //Salva a planilha
            workbook.SaveAs(caminho, true, evaluateFormulae: true);

            return true;
        }

        //Metodo para criar planilha de pessoa juridica
        public Boolean CriarPlanilhaPJ(IList<PessoaJuridica> pessoasJuridicas, String caminho)
        {
            //Instancia o objeto
            var workbook = new XLWorkbook();

            //Adciona a pasta de trabalho chamada "Vendas"
            var worksheet = workbook.Worksheets.Add("Pessoas Jurídica");

            //Define as colunas
            var columnId = worksheet.Column("A");
            var columnNome = worksheet.Column("B");
            var columnRazaoSocial = worksheet.Column("C");
            var columnCNPJ = worksheet.Column("D");
            var ColumnEndereco = worksheet.Column("E"); 

            //Define a largura da coluna
            columnId.Width = 5;
            columnNome.Width = 18;
            columnRazaoSocial.Width = 18;
            columnCNPJ.Width = 14;
            ColumnEndereco.Width = 18;

            //Define uma linha para iniciar o preenchimento
            int line = 1;

            //Define os textos do cabeçalho da planilha
            columnId.Cell(line).Value = "ID";
            columnNome.Cell(line).Value = "Nome";
            columnRazaoSocial.Cell(line).Value = "Razão Social";
            columnCNPJ.Cell(line).Value = "CNPJ";
            ColumnEndereco.Cell(line).Value = "Endereço";

            //Informa que a cor de fundo da linha sera cinza e a fonte negrito
            worksheet.Row(line).Style.Fill.BackgroundColor = XLColor.Gray;
            worksheet.Row(line).Style.Font.Bold = true;

            //Acrescenta a linha
            line++;

            //Percorre a lista de vendas
            foreach (PessoaJuridica pessoa in pessoasJuridicas)
            {
                //Atribui os dados a cada linha
                columnId.Cell(line).Value = pessoa.ID;
                columnNome.Cell(line).Value = pessoa.Nome;
                columnRazaoSocial.Cell(line).Value = pessoa.RazaoSocial;
                columnCNPJ.Cell(line).Value = pessoa.CNPJ;
                ColumnEndereco.Cell(line).Value = pessoa.Endereco;

                //Acrescenta a linha
                line++;
            }

            //Estilo da planilha
            workbook.ReferenceStyle = XLReferenceStyle.A1;

            //Salva a planilha
            workbook.SaveAs(caminho, true, evaluateFormulae: true);

            return true;
        }

        //Metodo para criar planilha de sapato
        public Boolean CriarPlanilhaSapato(IList<Sapato> sapatos, String caminho)
        {
            //Instancia o objeto
            var workbook = new XLWorkbook();

            //Adciona a pasta de trabalho chamada "Vendas"
            var worksheet = workbook.Worksheets.Add("Sapatos");

            //Define as colunas
            var columnId = worksheet.Column("A");
            var columnNome = worksheet.Column("B");
            var columnCadarco = worksheet.Column("C");
            var columnMaterial = worksheet.Column("D");
            var columnCor = worksheet.Column("E");
            var columnPreco = worksheet.Column("F");
            var columnTamanhos = worksheet.Column("G");

            //Define a largura da coluna
            columnId.Width = 5;
            columnNome.Width = 16;
            columnCadarco.Width = 14;
            columnMaterial.Width = 14;
            columnCor.Width = 11;
            columnPreco.Width = 8;
            columnTamanhos.Width = 30;

            //Define uma linha para iniciar o preenchimento
            int line = 1;

            //Define os textos do cabeçalho da planilha
            columnId.Cell(line).Value = "ID";
            columnNome.Cell(line).Value = "Nome";
            columnCadarco.Cell(line).Value = "Possui Cadarço";
            columnMaterial.Cell(line).Value = "Material";
            columnCor.Cell(line).Value = "Cor";
            columnPreco.Cell(line).Value = "Preço";
            columnTamanhos.Cell(line).Value = "Tamanhos";

            //Informa que a cor de fundo da linha sera cinza e a fonte negrito
            worksheet.Row(line).Style.Fill.BackgroundColor = XLColor.Gray;
            worksheet.Row(line).Style.Font.Bold = true;

            //Acrescenta a linha
            line++;

            //Percorre a lista de vendas
            foreach (Sapato sapato in sapatos)
            {
                //Seleciona todos os tamanhos daquele sapato
                sapato.Tamanhos = ctx.tamanhosSapatos.Where(t => t.SapatoId == sapato.ID).Select(t => t).ToList();

                //Atribui os dados a cada linha
                columnId.Cell(line).Value = sapato.ID;
                columnNome.Cell(line).Value = sapato.Nome;
                columnCadarco.Cell(line).Value = sapato.Cadarco;
                columnMaterial.Cell(line).Value = sapato.Material;
                columnCor.Cell(line).Value = sapato.Cor;
                columnPreco.Cell(line).Value = sapato.Preco;
                columnTamanhos.Cell(line).Value = sapato.tamanhosToString();

                //Acrescenta a linha
                line++;
            }

            //Estilo da planilha
            workbook.ReferenceStyle = XLReferenceStyle.A1;

            //Salva a planilha
            workbook.SaveAs(caminho, true, evaluateFormulae: true);

            return true;
        }

        //Metodo para criar planilha de tamanhos de sapato
        public Boolean CriarPlanilhaEstoque(IList<Sapato> sapatos, String caminho)
        {

            //Instancia o objeto
            var workbook = new XLWorkbook();

            //Cria variavel contadora
            int quantidadeTotal;

            foreach (Sapato sapato in sapatos)
            {
                //Zera a quantidade total
                quantidadeTotal = 0;

                //Seleciona todos os tamanhos daquele sapato
                sapato.Tamanhos = ctx.tamanhosSapatos.Where(t => t.SapatoId == sapato.ID).Select(t => t).ToList();
                
                //Adciona a pasta de trabalho chamada "Vendas"
                var worksheet = workbook.Worksheets.Add(sapato.ID + " - " +sapato.Nome);

                //Define as colunas
                var columnTamanho = worksheet.Column("A");
                var columnQuantidade = worksheet.Column("B");

                //Define a largura da coluna
                columnTamanho.Width = 16;
                columnQuantidade.Width = 10;
                
                //Define uma linha para iniciar o preenchimento
                int line = 1;

                //Define os textos do cabeçalho da planilha
                columnTamanho.Cell(line).Value = "Tamanho";
                columnQuantidade.Cell(line).Value = "Quantidade";

                //Informa que a cor de fundo da linha sera cinza e a fonte negrito
                worksheet.Row(line).Style.Fill.BackgroundColor = XLColor.Gray;
                worksheet.Row(line).Style.Font.Bold = true;

                //Acrescenta a linha
                line++;

                //Percorre a lista de vendas
                foreach (TamanhoSapato tamanho in sapato.Tamanhos)
                {
                    //Atribui os dados a cada linha
                    columnTamanho.Cell(line).Value = tamanho.Tamanho;
                    columnQuantidade.Cell(line).Value = tamanho.Quantidade;

                    quantidadeTotal = quantidadeTotal + tamanho.Quantidade;

                    //Acrescenta a linha
                    line++;
                }

                //Define os textos do rodapé da planilha
                columnTamanho.Cell(line).Value = "Quantidade Total";
                columnQuantidade.Cell(line).Value = quantidadeTotal;

                //Informa que a cor de fundo da linha sera cinza e a fonte negrito
                worksheet.Row(line).Style.Fill.BackgroundColor = XLColor.LightGray;
                worksheet.Row(line).Style.Font.Bold = true;

                //Estilo da planilha
                workbook.ReferenceStyle = XLReferenceStyle.A1;

                //Salva a planilha
                workbook.SaveAs(caminho, true, evaluateFormulae: true);
            }
            
            return true;
        }

        //Metodo para criar planilha de vendas
        public Boolean CriarPlanilhaVendas(IList<Venda> vendas, String caminho)
        {
            foreach (Venda venda in vendas)
            {
                venda.cliente = ctx.pessoas.Find(venda.pessoaId);
                venda.sapato = ctx.sapatos.Find(venda.sapatoId);
            }

            //Instancia o objeto
            var workbook = new XLWorkbook();
            
            //Adciona a pasta de trabalho chamada "Vendas"
            var worksheet = workbook.Worksheets.Add("Vendas");

            //Define as colunas
            var columnId = worksheet.Column("A");
            var columnCliente = worksheet.Column("B");
            var columnSapato = worksheet.Column("C");
            var columnTamanho = worksheet.Column("D");
            var columnQuantidade = worksheet.Column("E");
            var columnPrecoUnitario = worksheet.Column("F");
            var columnPrecoTotal = worksheet.Column("G");

            //Define a largura da coluna
            columnId.Width = 5;
            columnCliente.Width = 17.5;
            columnSapato.Width = 15;
            columnTamanho.Width = 9;
            columnQuantidade.Width = 11;
            columnPrecoUnitario.Width = 13;
            columnPrecoTotal.Width = 13;

            //Define uma linha para iniciar o preenchimento
            int line = 1;

            //Define os textos do cabeçalho da planilha
            columnId.Cell(line).Value = "ID";
            columnCliente.Cell(line).Value = "Cliente";
            columnSapato.Cell(line).Value = "Sapato";
            columnTamanho.Cell(line).Value = "Tamanho";
            columnQuantidade.Cell(line).Value = "Quantidade";
            columnPrecoUnitario.Cell(line).Value = "Preço Unitário";
            columnPrecoTotal.Cell(line).Value = "Preço Final";

            //Informa que a cor de fundo da linha sera cinza e a fonte negrito
            worksheet.Row(line).Style.Fill.BackgroundColor = XLColor.Gray;
            worksheet.Row(line).Style.Font.Bold = true;

            //Acrescenta a linha
            line++;

            //Percorre a lista de vendas
            foreach (Venda venda in vendas)
            {
                //Atribui os dados a cada linha
                columnId.Cell(line).Value = venda.ID;
                columnCliente.Cell(line).Value = venda.cliente.Nome;
                columnSapato.Cell(line).Value = venda.sapato.Nome;
                columnTamanho.Cell(line).Value = venda.Tamanho;
                columnQuantidade.Cell(line).Value = venda.Quantidade;
                columnPrecoUnitario.Cell(line).Value = venda.PrecoUnitario;
                columnPrecoTotal.Cell(line).Value = venda.PrecoFinal;

                //Acrescenta a linha
                line++;
            }

            //Estilo da planilha
            workbook.ReferenceStyle = XLReferenceStyle.A1;

            //Salva a planilha
            workbook.SaveAs(caminho, true, evaluateFormulae: true);

            return true;
        }

        //Metodo para criar planilha de vendas por sapato
        public Boolean CriarPlanilhaVendaPorSapato(IList<Venda> vendas, Sapato sapatoSelecionado, String caminho)
        {
            foreach (Venda venda in vendas)
            {
                venda.cliente = ctx.pessoas.Find(venda.pessoaId);
                venda.sapato = ctx.sapatos.Find(venda.sapatoId);
            }

            //Instancia o objeto
            var workbook = new XLWorkbook();

            //Adciona a pasta de trabalho chamada "Vendas"
            var worksheet = workbook.Worksheets.Add("Vendas do sapato"+sapatoSelecionado.Nome);

            //Define as colunas
            var columnId = worksheet.Column("A");
            var columnCliente = worksheet.Column("B");
            var columnSapato = worksheet.Column("C");
            var columnTamanho = worksheet.Column("D");
            var columnQuantidade = worksheet.Column("E");
            var columnPrecoUnitario = worksheet.Column("F");
            var columnPrecoTotal = worksheet.Column("G");

            //Define a largura da coluna
            columnId.Width = 5;
            columnCliente.Width = 17.5;
            columnSapato.Width = 15;
            columnTamanho.Width = 9;
            columnQuantidade.Width = 11;
            columnPrecoUnitario.Width = 13;
            columnPrecoTotal.Width = 13;

            //Define uma linha para iniciar o preenchimento
            int line = 1;

            //Define os textos do cabeçalho da planilha
            columnId.Cell(line).Value = "ID";
            columnCliente.Cell(line).Value = "Cliente";
            columnSapato.Cell(line).Value = "Sapato";
            columnTamanho.Cell(line).Value = "Tamanho";
            columnQuantidade.Cell(line).Value = "Quantidade";
            columnPrecoUnitario.Cell(line).Value = "Preço Unitário";
            columnPrecoTotal.Cell(line).Value = "Preço Final";

            //Informa que a cor de fundo da linha sera cinza e a fonte negrito
            worksheet.Row(line).Style.Fill.BackgroundColor = XLColor.Gray;
            worksheet.Row(line).Style.Font.Bold = true;

            //Acrescenta a linha
            line++;

            //Percorre a lista de vendas
            foreach (Venda venda in vendas)
            {
                //Atribui os dados a cada linha
                columnId.Cell(line).Value = venda.ID;
                columnCliente.Cell(line).Value = venda.cliente.Nome;
                columnSapato.Cell(line).Value = venda.sapato.Nome;
                columnTamanho.Cell(line).Value = venda.Tamanho;
                columnQuantidade.Cell(line).Value = venda.Quantidade;
                columnPrecoUnitario.Cell(line).Value = venda.PrecoUnitario;
                columnPrecoTotal.Cell(line).Value = venda.PrecoFinal;

                //Acrescenta a linha
                line++;
            }

            //Estilo da planilha
            workbook.ReferenceStyle = XLReferenceStyle.A1;

            //Salva a planilha
            workbook.SaveAs(caminho, true, evaluateFormulae: true);

            return true;
        }

        //Metodo para criar planilha de vendas por pessoa fisica
        public Boolean CriarPlanilhaVendaPorPF(IList<Venda> vendas, PessoaFisica pessoaFisicaSelecionada, String caminho)
        {
            foreach (Venda venda in vendas)
            {
                venda.cliente = ctx.pessoas.Find(venda.pessoaId);
                venda.sapato = ctx.sapatos.Find(venda.sapatoId);
            }

            //Instancia o objeto
            var workbook = new XLWorkbook();

            //Adciona a pasta de trabalho chamada "Vendas"
            var worksheet = workbook.Worksheets.Add("Vendas do cliente" + pessoaFisicaSelecionada.Nome);

            //Define as colunas
            var columnId = worksheet.Column("A");
            var columnCliente = worksheet.Column("B");
            var columnSapato = worksheet.Column("C");
            var columnTamanho = worksheet.Column("D");
            var columnQuantidade = worksheet.Column("E");
            var columnPrecoUnitario = worksheet.Column("F");
            var columnPrecoTotal = worksheet.Column("G");

            //Define a largura da coluna
            columnId.Width = 5;
            columnCliente.Width = 17.5;
            columnSapato.Width = 15;
            columnTamanho.Width = 9;
            columnQuantidade.Width = 11;
            columnPrecoUnitario.Width = 13;
            columnPrecoTotal.Width = 13;

            //Define uma linha para iniciar o preenchimento
            int line = 1;

            //Define os textos do cabeçalho da planilha
            columnId.Cell(line).Value = "ID";
            columnCliente.Cell(line).Value = "Cliente";
            columnSapato.Cell(line).Value = "Sapato";
            columnTamanho.Cell(line).Value = "Tamanho";
            columnQuantidade.Cell(line).Value = "Quantidade";
            columnPrecoUnitario.Cell(line).Value = "Preço Unitário";
            columnPrecoTotal.Cell(line).Value = "Preço Final";

            //Informa que a cor de fundo da linha sera cinza e a fonte negrito
            worksheet.Row(line).Style.Fill.BackgroundColor = XLColor.Gray;
            worksheet.Row(line).Style.Font.Bold = true;

            //Acrescenta a linha
            line++;

            //Percorre a lista de vendas
            foreach (Venda venda in vendas)
            {
                //Atribui os dados a cada linha
                columnId.Cell(line).Value = venda.ID;
                columnCliente.Cell(line).Value = venda.cliente.Nome;
                columnSapato.Cell(line).Value = venda.sapato.Nome;
                columnTamanho.Cell(line).Value = venda.Tamanho;
                columnQuantidade.Cell(line).Value = venda.Quantidade;
                columnPrecoUnitario.Cell(line).Value = venda.PrecoUnitario;
                columnPrecoTotal.Cell(line).Value = venda.PrecoFinal;

                //Acrescenta a linha
                line++;
            }

            //Estilo da planilha
            workbook.ReferenceStyle = XLReferenceStyle.A1;

            //Salva a planilha
            workbook.SaveAs(caminho, true, evaluateFormulae: true);

            return true;
        }

        //Metodo para criar planilha de vendas por pessoa fisica
        public Boolean CriarPlanilhaVendaPorPJ(IList<Venda> vendas, PessoaJuridica pessoaJuridicaSelecionada, String caminho)
        {
            foreach (Venda venda in vendas)
            {
                venda.cliente = ctx.pessoas.Find(venda.pessoaId);
                venda.sapato = ctx.sapatos.Find(venda.sapatoId);
            }

            //Instancia o objeto
            var workbook = new XLWorkbook();

            //Adciona a pasta de trabalho chamada "Vendas"
            var worksheet = workbook.Worksheets.Add("Vendas do cliente" + pessoaJuridicaSelecionada.Nome);

            //Define as colunas
            var columnId = worksheet.Column("A");
            var columnCliente = worksheet.Column("B");
            var columnSapato = worksheet.Column("C");
            var columnTamanho = worksheet.Column("D");
            var columnQuantidade = worksheet.Column("E");
            var columnPrecoUnitario = worksheet.Column("F");
            var columnPrecoTotal = worksheet.Column("G");

            //Define a largura da coluna
            columnId.Width = 5;
            columnCliente.Width = 17.5;
            columnSapato.Width = 15;
            columnTamanho.Width = 9;
            columnQuantidade.Width = 11;
            columnPrecoUnitario.Width = 13;
            columnPrecoTotal.Width = 13;

            //Define uma linha para iniciar o preenchimento
            int line = 1;

            //Define os textos do cabeçalho da planilha
            columnId.Cell(line).Value = "ID";
            columnCliente.Cell(line).Value = "Cliente";
            columnSapato.Cell(line).Value = "Sapato";
            columnTamanho.Cell(line).Value = "Tamanho";
            columnQuantidade.Cell(line).Value = "Quantidade";
            columnPrecoUnitario.Cell(line).Value = "Preço Unitário";
            columnPrecoTotal.Cell(line).Value = "Preço Final";

            //Informa que a cor de fundo da linha sera cinza e a fonte negrito
            worksheet.Row(line).Style.Fill.BackgroundColor = XLColor.Gray;
            worksheet.Row(line).Style.Font.Bold = true;

            //Acrescenta a linha
            line++;

            //Percorre a lista de vendas
            foreach (Venda venda in vendas)
            {
                //Atribui os dados a cada linha
                columnId.Cell(line).Value = venda.ID;
                columnCliente.Cell(line).Value = venda.cliente.Nome;
                columnSapato.Cell(line).Value = venda.sapato.Nome;
                columnTamanho.Cell(line).Value = venda.Tamanho;
                columnQuantidade.Cell(line).Value = venda.Quantidade;
                columnPrecoUnitario.Cell(line).Value = venda.PrecoUnitario;
                columnPrecoTotal.Cell(line).Value = venda.PrecoFinal;

                //Acrescenta a linha
                line++;
            }

            //Estilo da planilha
            workbook.ReferenceStyle = XLReferenceStyle.A1;

            //Salva a planilha
            workbook.SaveAs(caminho, true, evaluateFormulae: true);

            return true;
        }
    }
}
