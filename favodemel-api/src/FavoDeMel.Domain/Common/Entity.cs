using System;
using System.ComponentModel.DataAnnotations;

namespace FavoDeMel.Domain.Common
{
    public class Entity<TId> : Entity
    {
        [Key]
        public TId Id { get; set; }
    }

    public class Entity : ICloneable
    {
        public object Clone()
        {
            return MemberwiseClone();
        }

        public virtual void Prepare()
        { }
    }
}
