using System;

namespace FavoDeMel.Service.Interfaces
{
    public interface IGeradorGuidService
    {
        /// <summary>
        /// Gera novo Guid para o registro que será cadastrado.
        /// </summary>
        /// <returns>Retorna novo Guid</returns>
        Guid GetNexGuid();
    }
}