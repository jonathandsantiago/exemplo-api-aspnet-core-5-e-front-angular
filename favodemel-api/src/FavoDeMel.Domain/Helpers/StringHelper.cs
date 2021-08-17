using FavoDeMel.Domain.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace FavoDeMel.Domain.Helpers
{
    public static class StringHelper
    {
        /// <summary>
        /// Concaternar chave e valor
        /// </summary>
        /// <param name="chave">Chave</param>
        /// <param name="valor">Valor</param>
        /// <returns>Retorna a chave e o valor concatenado</returns>
        public static string ConcatChaveValor(string chave, string valor)
        {
            var str = valor.IsNotEmpty() ? $"{chave}: {valor}" : $"{chave} não encontrado.";

            return str;
        }

        /// <summary>
        /// Concaternar todas as propriedades e campos da entidade pelo nome e valor
        /// </summary>
        /// <param name="entidade">Entidade</param>
        /// <returns>Retorna string de todas as propriedades e campos da entidade pelo nome e valor</returns>
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

        /// <summary>
        /// Calcular hash MD5
        /// </summary>
        /// <param name="input">Input</param>
        /// <returns>Retorna o hash MD5 calculado</returns>
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

        /// <summary>
        /// Unificar todas as mensagens com quebra de linha html
        /// </summary>
        /// <param name="mensagens">Mensagens</param>
        /// <returns>Retorna as mensagens unificadas com quebra de linha html</returns>
        public static string JoinHtmlMensagem(IEnumerable<string> mensagens)
        {
            return string.Join("<br>", mensagens);
        }

        /// <summary>
        /// Retornar somente os números do valor informado
        /// </summary>
        /// <param name="valor">Mensagens</param>
        /// <returns>Retorna somente os números do valor informado</returns>
        public static string ApenasNumeros(string valor)
        {
            return valor == null ? null : string.Join(string.Empty, valor.ToCharArray().Where(char.IsDigit));
        }

        /// <summary>
        /// Retornar o valor +1 atribundo zero a esquerda, conforme a quantidade informada.
        /// </summary>
        /// <param name="value">Valor</param>
        /// <param name="qtdZeros">Quantidade de zero a ser atribuda</param>
        /// <returns>Retorna o valor +1 com zero a esquerda, conforme a quantidade informada.</returns>
        public static string MaxAddPadLeft(string value, int qtdZeros)
        {
            if (string.IsNullOrEmpty(value))
            {
                return value;
            }

            return PadLeft($"{Convert.ToInt32(ApenasNumeros(value)) + 1}", qtdZeros);
        }

        /// <summary>
        /// Retornar o valor atribundo zero a esquerda, conforme a quantidade informada.
        /// </summary>
        /// <param name="value">Valor</param>
        /// <param name="qtdZeros">Quantidade de zero a ser atribuda</param>
        /// <param name="onlyNumbers">Somente números?</param>
        /// <returns>Retorna o valor com zero a esquerda, conforme a quantidade informada.</returns>
        public static string PadLeft(string value, int qtdZeros, bool onlyNumbers = true)
        {
            if (string.IsNullOrEmpty(value))
            {
                return value;
            }

            if (onlyNumbers && value.Any(c => !Char.IsDigit(c)))
            {
                return value;
            }

            return value.PadLeft(qtdZeros, '0');
        }
    }
}
