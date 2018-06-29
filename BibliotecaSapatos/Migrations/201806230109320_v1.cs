namespace BibliotecaSapatos.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Pessoas",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Nome = c.String(nullable: false),
                        CPF = c.String(),
                        DataNascimento = c.DateTime(),
                        RazaoSocial = c.String(),
                        CNPJ = c.String(),
                        Endereco = c.String(),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Sapatoes",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Nome = c.String(nullable: false),
                        Cadarco = c.String(nullable: false),
                        Material = c.String(nullable: false),
                        Cor = c.String(nullable: false),
                        Preco = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Imagem = c.Binary(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.TamanhoSapatoes",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Tamanho = c.String(nullable: false),
                        Quantidade = c.Int(nullable: false),
                        SapatoId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Sapatoes", t => t.SapatoId, cascadeDelete: true)
                .Index(t => t.SapatoId);
            
            CreateTable(
                "dbo.Vendas",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        sapatoId = c.Int(nullable: false),
                        Modelo = c.String(nullable: false),
                        Tamanho = c.String(nullable: false),
                        Quantidade = c.Int(nullable: false),
                        PrecoUnitario = c.Decimal(nullable: false, precision: 18, scale: 2),
                        PrecoFinal = c.Decimal(nullable: false, precision: 18, scale: 2),
                        pessoaId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Pessoas", t => t.pessoaId, cascadeDelete: true)
                .ForeignKey("dbo.Sapatoes", t => t.sapatoId, cascadeDelete: true)
                .Index(t => t.sapatoId)
                .Index(t => t.pessoaId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Vendas", "sapatoId", "dbo.Sapatoes");
            DropForeignKey("dbo.Vendas", "pessoaId", "dbo.Pessoas");
            DropForeignKey("dbo.TamanhoSapatoes", "SapatoId", "dbo.Sapatoes");
            DropIndex("dbo.Vendas", new[] { "pessoaId" });
            DropIndex("dbo.Vendas", new[] { "sapatoId" });
            DropIndex("dbo.TamanhoSapatoes", new[] { "SapatoId" });
            DropTable("dbo.Vendas");
            DropTable("dbo.TamanhoSapatoes");
            DropTable("dbo.Sapatoes");
            DropTable("dbo.Pessoas");
        }
    }
}
