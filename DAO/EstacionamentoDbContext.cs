using DAO.Mappings;
using Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DAO
{
    public class EstacionamentoDbContext : DbContext
    {
        //Construtor padrão da classe que, quando invocado, chama o construtor da classe pai
        //que inicializa a connectionstring que contém as informações da base que iremos trabalhar
        public EstacionamentoDbContext():base(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\hbsis\Documents\EstacionamentoDB.mdf;Integrated Security=True;Connect Timeout=30")
        {
            Database.SetInitializer(new EstacionamentoTesteStrategy());
        }

        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Vaga> Vagas { get; set; }
        public DbSet<Movimentacao> Movimentacoes { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //Remove a convenção que pluraliza em inglês o nome das tabelas.
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            //Remove a convenção de delete on cascade
            //modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();

            //Exemplo de criação de convenção customizável
            //modelBuilder.Conventions.Add<HandleConvention>();

            //Adiciona as configurações locais no DbContext
            //A parte do Assembly.GetExecutingAssembly, faz com que o C#
            //retorne todos os tipos que são EntityTypeConfiguration dentro do DAO
            modelBuilder.Configurations
                        .AddFromAssembly(Assembly.GetExecutingAssembly());


            //Adição de uma configuração global que sobrescreve
            //a convenção padrão da string e a faz já ser VARCHAR NOT NULL
            modelBuilder.Properties()
                        .Where(c => c.PropertyType == typeof(string))
                        .Configure(c => c.IsRequired().IsUnicode(false));

            //Configuração global que determina que toda
            //propriedade DateTime deve será mapeada para 
            //datetime2.
            //Lembrando que é possível sobrescrever este comportamento
            //utilizando configurações locais na classes EntityTypeConfiguration<T>
            modelBuilder.Properties()
            .Where(c => c.PropertyType == typeof(DateTime))
            .Configure(c => c.HasColumnType("datetime2"));

            //Irá comparar as suas entidades (que estão encapsuladas nos DbSet<T> acima)
            //com a estrutura do banco. Se a base não existir, o EF irá criar. Se a base existir,
            //mas estiver diferente (o banco com uma ou mais diferença(s) de coluna(s)), o EF
            //ira executar uma política de DROP.
            base.OnModelCreating(modelBuilder);
        }
    }
    //public class HandleConvention : Convention
    //{
    //    public HandleConvention()
    //    {
    //        this.Properties()
    //            .Where(c => c.PropertyType == typeof(int) && c.Name == "Handle")
    //            .Configure(c => c.HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity)
    //            .IsKey());
    //    }
    //}
}
