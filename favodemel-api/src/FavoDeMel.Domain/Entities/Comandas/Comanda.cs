using FavoDeMel.Domain.Common;
using FavoDeMel.Domain.Entities.Usuarios;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace FavoDeMel.Domain.Entities.Comandas
{
    public class Comanda : Entity<Guid>
    {
        public decimal TotalAPagar { get; set; }
        public decimal GorjetaGarcom { get; set; }
        public string Codigo { get; set; }
        public DateTimeOffset DataCadastro { get; set; }
        public ComandaSituacao Situacao { get; set; }

        public Usuario Garcom { get; set; }
        [ForeignKey("ComandaId")]
        public ICollection<ComandaPedido> Pedidos { get; set; }

        public Comanda()
        {
            Pedidos = new Collection<ComandaPedido>();
        }

        public void FecharConta()
        {
            TotalAPagar = Pedidos.Sum(c => c.Quantidade * c.Produto.Preco);
            GorjetaGarcom = Garcom != null ? (Garcom.Comissao / 100) * TotalAPagar : 0;
            Situacao = ComandaSituacao.Fechada;
        }

        public void Confirmar()
        {
            Situacao = ComandaSituacao.EmAndamento;
        }
    }
}