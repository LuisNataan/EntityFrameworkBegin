using Entity.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public class Vaga
    {
        public int ID { get; set; }
        public bool VagaLivre { get; set; }
        public bool EhPreferencial { get; set; }
        public TipoVeiculo TipoVaga { get; set; }
        public bool EhCoberta { get; set; }

        public virtual ICollection<Movimentacao> Movimentacoes { get; set; }

        public double CalcularPreco()
        {
            double precoBase = 3;
            if (EhCoberta)
            {
                precoBase += 3;
            }

            switch (TipoVaga)
            {
                case TipoVeiculo.Carro:
                    precoBase += 3;
                    break;
                case TipoVeiculo.Moto:
                    precoBase += 2;
                    break;
                case TipoVeiculo.Camioneta:
                case TipoVeiculo.Caminhão:
                    precoBase += 5;
                    break;
                case TipoVeiculo.Helicoptero:
                    precoBase += 100;
                    break;
            }
            return precoBase;
        }
    }
}
