using BLL.Interfaces;
using DAO;
using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class ClienteService : IClienteService
    {
        public void Insert(Cliente cliente)
        {
            //Validar
            using (EstacionamentoDbContext db = new EstacionamentoDbContext())
            {

                //Ao buscar um dado do entity, existe um mecanismo conhecido 
                //como TRACKING. Este mecanismo observa as alterações feitas 
                //no objeto e, quando o método SaveChanges é chamado na base,
                //ele efetuará um update de tudo que foi alterado.
                Cliente c = db.Clientes.Find(8);
                c.Nome += "Bernart";
                db.SaveChanges();

                //Update sem a necessidade de ir no banco a primeira vez pra buscar pelo id
                Vaga vagaASerAtualizada = new Vaga();
                vagaASerAtualizada.ID = 5;
                vagaASerAtualizada.EhCoberta = true;
                vagaASerAtualizada.EhPreferencial = true;
                db.Entry<Vaga>(vagaASerAtualizada).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();

                //Delete
                Vaga vagaASerExcluida = new Vaga();
                vagaASerAtualizada.ID = 3;
                db.Entry<Vaga>(vagaASerAtualizada).State = System.Data.Entity.EntityState.Deleted;
                db.SaveChanges();





                //Exemplo de pesquisa de clientes que contenham a letra 'a' no nome, ordenados
                //por nome decrescentemente e; caso os nomes sejam iguais, ordena por id.
                List<Cliente> clientes = db.Clientes.Where(cli => cli.Nome.Contains("a"))
                                           .OrderByDescending(cli => cli.Nome)
                                           .ThenBy(cli=> cli.ID)
                                           .ToList();

                //Retorna o valor total já pago pelo cliente em todas as movimentações do primeiro
                //cliente que tenha a letra "a" no nome
                //double valor = clientes.First().Movimentacoes.Sum(soma => soma.ValorTotal);
                double valor = clientes[0].Movimentacoes.Sum(soma => soma.ValorTotal);

                //Exemplo de pesquisa de vaga por ID
                //EXTREMAMENTE COMUM
                //O find é mais performático que o FirstOrDefault
                Vaga vv = db.Vagas.Find(6);

                //Exemplo para pesquisar todas as vagas livres.
                List<Vaga> vagas = db.Vagas.Where(vag => vag.VagaLivre).ToList();

                //Exemplo para pesquisar as movimentações da data hoje, apenas para vagas de Helicoptero
                List<Movimentacao> movimentacoes = db.Movimentacoes.Where(m => m.DataEntrada.Date == DateTime.Now.Date && m.Vaga.TipoVaga == Entity.Enums.TipoVeiculo.Helicoptero).ToList();



                Cliente c = new Cliente()
                {
                    Nome = "Danizinho Bernart",
                    Ativo = true,
                    CPF = "901.917.069-49",
                    DataNascimento = DateTime.Now.AddYears(-25)
                };
                Vaga v = new Vaga()
                {
                    EhCoberta = true,
                    EhPreferencial = true,
                    TipoVaga = Entity.Enums.TipoVeiculo.Helicoptero,
                    VagaLivre = true
                };
                db.Clientes.Add(c);
                db.Vagas.Add(v);
                db.SaveChanges();
            }
        }
    }
}
