using FavoDeMel.Domain.Extensions;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace FavoDeMel.Domain.Helpers
{
    public static class StringHelper
    {
        public static string ConcatChaveValor(string chave, string valor)
        {
            var str = valor.IsNotEmpty() ? $"{chave}: {valor}" : $"{chave} não encontrado.";

            return str;
        }

        public static string ConcatLogConfig<T>(T entidade)
        {
            var str = new StringBuilder();
            str.AppendLine("___________________________________________");
            str.AppendLine($"________{typeof(T).Name}__________");

            foreach (var field in typeof(T).GetFields())
            {
                str.AppendLine(ConcatChaveValor(field.Name, $"{field.GetValue(entidade)}"));
            }

            foreach (var property in typeof(T).GetProperties())
            {
                str.AppendLine(ConcatChaveValor(property.Name, $"{property.GetValue(entidade)}"));
            }

            str.AppendLine("___________________________________________");
            return str.ToString();
        }

        public static string CalculateMD5Hash(string input)
        {
            using (MD5 md5 = MD5.Create())
            {
                byte[] inputBytes = Encoding.ASCII.GetBytes(input);
                byte[] hash = md5.ComputeHash(inputBytes);

                StringBuilder sb = new StringBuilder();

                for (int i = 0; i < hash.Length; i++)
                {
                    sb.Append(hash[i].ToString("X2"));
                }

                return sb.ToString();
            }
        }

        public static string JoinHtmlMensagem(IEnumerable<string> mensagem)
        {
            return string.Join("<br>", mensagem);
        }
    }
}
