using System.Collections.Generic;

namespace FavoDeMel.Domain.Extensions
{
    public static class ListExtensions
    {
        /// <summary>
        /// Converter o dictionary de chave e valor ao tipo informado.
        /// </summary>
        /// <typeparam name="T">Tipo do retorno</typeparam>
        /// <param name="keys">Chaves e valores</param>
        /// <returns>Retorna as chaves e valores ao objeto no tipo informado.</returns>
        public static T GetSetting<T>(this IDictionary<string, object> keys)
        {
            return (T)keys[typeof(T).Name];
        }
    }
}
