using System.Collections.Generic;

namespace FavoDeMel.Domain.Interfaces
{
    public interface ISettings
    {
        public string ToString();
    }

    public interface ISettings<TKey, TValue> : IDictionary<TKey, TValue>
    {
    }
}