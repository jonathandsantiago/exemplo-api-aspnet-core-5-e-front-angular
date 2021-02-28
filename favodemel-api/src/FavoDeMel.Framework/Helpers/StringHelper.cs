using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace FavoDeMel.Framework.Helpers
{
    public static class StringHelper
    {
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

        public static bool IsCnpj(string cnpj)
        {
            if (IsNullOrEmpty(cnpj))
            {
                return false;
            }

            cnpj = ApenasNumeros(cnpj.Trim());

            if (cnpj.Length != 14)
            {
                return false;
            }

            string hasCnpj = cnpj.Substring(0, 12);
            int resto = ObterCalculoResto(12, hasCnpj, new int[12] { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 });

            string digits = resto.ToString();
            hasCnpj = hasCnpj + digits;
            resto = ObterCalculoResto(13, hasCnpj, new int[13] { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 });

            digits = digits + resto.ToString();
            return cnpj.EndsWith(digits);
        }

        public static bool IsCpf(string numeroDocumento)
        {
            if (IsNullOrEmpty(numeroDocumento))
            {
                return false;
            }

            numeroDocumento = ApenasNumeros(numeroDocumento.Trim());

            if (numeroDocumento.Length != 11)
            {
                return false;
            }

            string hasCnpj = numeroDocumento.Substring(0, 9);
            int resto = ObterCalculoResto(9, hasCnpj, new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 });

            string digitos = resto.ToString();
            hasCnpj = hasCnpj + digitos;
            resto = ObterCalculoResto(10, hasCnpj, new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 });

            digitos = digitos + resto.ToString();
            return numeroDocumento.EndsWith(digitos);
        }

        public static bool IsEmail(string str)
        {
            return new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$").Match(str).Success;
        }

        public static bool IsNullOrEmpty(string str)
        {
            return string.IsNullOrEmpty(str) || string.IsNullOrWhiteSpace(str);
        }

        public static bool IsSomenteNumeros(string str)
        {
            if (IsNullOrEmpty(str))
            {
                return false;
            }

            return Regex.IsMatch(str, @"^\d+$");
        }

        public static string ApenasNumeros(string str)
        {
            return str == null ? null : string.Join(string.Empty, str.ToCharArray().Where(char.IsDigit));
        }

        public static string RemoverEspacos(string str)
        {
            return str.Replace(" ", "");
        }

        private static int ObterCalculoResto(int digits, string cnpj, int[] multiplicator)
        {
            int sum = 0;
            for (int index = 0; index < digits; index++)
            {
                sum += int.Parse(cnpj[index].ToString()) * multiplicator[index];
            }

            int rest = (sum % 11);
            return rest < 2 ? 0 : (11 - rest);
        }
    }
}