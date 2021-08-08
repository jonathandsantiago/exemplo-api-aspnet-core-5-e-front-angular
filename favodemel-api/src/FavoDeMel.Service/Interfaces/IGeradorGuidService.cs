using System;

namespace FavoDeMel.Service.Interfaces
{
    public interface IGeradorGuidService
    {
        /// <summary>
        /// Resposável por gerar um novo GUID para o registro que será cadastrado.
        /// </summary>
        /// <returns></returns>
        Guid GetNexGuid();
    }
}