using System;
using System.Collections.Generic;

namespace FavoDeMel.Api.Dtos
{
    public class ComandaDto : DtoBase<int>
    {
        public Guid GarcomId { get; set; }
        public IList<ComandaPedidoDto> Pedidos { get; set; }

        public ComandaDto()
        {
            Pedidos = new List<ComandaPedidoDto>();
        }
    }
}
