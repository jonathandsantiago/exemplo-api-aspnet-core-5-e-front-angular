using FavoDeMel.Domain.Entities.Comandas;
using System;

namespace FavoDeMel.Domain.Dtos.Filtros
{
    public class FiltroComanda : Filtro
    {
        public ComandaSituacao Situacao { get; set; }
        public DateTime Data { get; set; }
    }
}