using FavoDeMel.Domain.Interfaces;

namespace FavoDeMel.Domain.Dtos
{
    public class DtoBase<TId> : IDtoBase
    {
        public TId Id { get; set; }
    }
}