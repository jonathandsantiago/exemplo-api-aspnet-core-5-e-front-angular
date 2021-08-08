using FavoDeMel.Domain.Entities.Comandas;
using System;
using System.Collections.Generic;

namespace FavoDeMel.Domain.Dtos
{
    public class ComandaDto : DtoBase<Guid?>
    {
        public Guid? GarcomId { get; set; }
        public decimal TotalAPagar { get; set; }
        public decimal GorjetaGarcom { get; set; }
        public ComandaSituacao Situacao { get; set; }
        public IList<ComandaPedidoDto> Pedidos { get; set; }

        public ComandaDto()
        {
            Pedidos = new List<ComandaPedidoDto>();
        }
    }
}