using Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.Mappings
{
    internal class MovimentacaoMapConfig : EntityTypeConfiguration<Movimentacao>
    {
        public MovimentacaoMapConfig()
        {
            this.ToTable("MOVIMENTACOES");
            this.Property(macarrao => macarrao.Placa).IsFixedLength().HasMaxLength(8);
            this.Property(goiaba => goiaba.Modelo).HasMaxLength(50);

            this.HasRequired(c => c.Vaga).WithMany(c => c.Movimentacoes).WillCascadeOnDelete(false);
        }

    }
}
