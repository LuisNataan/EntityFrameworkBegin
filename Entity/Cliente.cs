using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public class Cliente
    {
        //Por convention, no EF um campo inteiro chamado ID
        //gerará uma Primary KEY Identity
        public int ID { get; set; }
        public string Nome { get; set; }
        public string CPF { get; set; }
        public DateTime DataNascimento { get; set; }
        public bool Ativo { get; set; }
        public virtual ICollection<Movimentacao> Movimentacoes{ get; set; }

    }
}
