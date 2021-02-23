using System;

namespace FavoDeMel.Framework.Extensions
{
    public static class StringExtension
    {
        public static bool IsEmpty(this string texto)
        {
            return texto == null || String.IsNullOrWhiteSpace(texto);
        }

        public static bool IsNotEmpty(this string texto)
        {
            return !texto.IsEmpty();
        }
    }
}
