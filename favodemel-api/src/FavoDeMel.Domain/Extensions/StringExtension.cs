namespace FavoDeMel.Domain.Extensions
{
    public static class StringExtension
    {
        /// <summary>
        /// Validar se o texto é nulo ou vazio ou consiste apenas em espaço em branco
        /// </summary>
        /// <param name="texto">Texto</param>
        /// <returns>Retorna a validação se o texto é nulo ou vazio ou consiste apenas em espaço em branco.</returns>
        public static bool IsEmpty(this string texto)
        {
            return texto == null || string.IsNullOrWhiteSpace(texto);
        }

        /// <summary>
        /// Validar se o texto não é nulo ou vazio ou consiste apenas em espaço em branco
        /// </summary>
        /// <param name="texto">Texto</param>
        /// <returns>Retorna a validação se o valor não é nulo ou vazio ou consiste apenas em espaço em branco.</returns>
        public static bool IsNotEmpty(this string texto)
        {
            return !texto.IsEmpty();
        }
    }
}