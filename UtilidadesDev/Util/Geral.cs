//métodos uteis
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;

namespace UtilidadesDev.Util
{
    public static class Geral
    {
        public static string SemAcento(string text)
        {
            var normalizedString = text.Normalize(NormalizationForm.FormD);
            var stringBuilder = new StringBuilder();

            foreach (var c in from c in normalizedString let unicodeCategory = CharUnicodeInfo.GetUnicodeCategory(c) where unicodeCategory != UnicodeCategory.NonSpacingMark select c)
            {
                stringBuilder.Append(c);
            }

            return stringBuilder.ToString().Normalize(NormalizationForm.FormC);
        }


        public static bool ValidarEmail(string email)
        {
            if (string.IsNullOrEmpty(email))
                return false;

            var rg = new Regex(@"^(?("")("".+?""@)|(([0-9a-zA-Z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-zA-Z])@))(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-zA-Z][-\w]*[0-9a-zA-Z]\.)+[a-zA-Z]{2,6}))$");

            return rg.IsMatch(email);
        }

        public static bool ValidarCpf(string cpf)
        {
            if (string.IsNullOrEmpty(cpf))
                return false;

            int[] multiplicador1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            string tempCpf;
            string digito;
            int soma;
            int resto;

            cpf = cpf.Trim();
            cpf = cpf.Replace(".", "").Replace("-", "");

            if (cpf.Length != 11)
                return false;

            tempCpf = cpf.Substring(0, 9);
            soma = 0;

            for (var i = 0; i < 9; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador1[i];

            resto = soma % 11;

            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;

            digito = resto.ToString();
            tempCpf = tempCpf + digito;
            soma = 0;

            for (var i = 0; i < 10; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador2[i];

            resto = soma % 11;

            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;

            digito += resto.ToString();

            return cpf.EndsWith(digito);
        }

        public static bool ValidarCnpj(string cnpj)
        {
            if (string.IsNullOrEmpty(cnpj))
                return false;

            int[] multiplicador1 = new int[12] { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = new int[13] { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            int soma;
            int resto;
            string digito;
            string tempCnpj;

            cnpj = cnpj.Trim();
            cnpj = cnpj.Replace(".", "").Replace("-", "").Replace("/", "");

            if (cnpj.Length != 14)
                return false;

            tempCnpj = cnpj.Substring(0, 12);
            soma = 0;

            for (var i = 0; i < 12; i++)
                soma += int.Parse(tempCnpj[i].ToString()) * multiplicador1[i];

            resto = (soma % 11);

            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;

            digito = resto.ToString();
            tempCnpj = tempCnpj + digito;
            soma = 0;

            for (var i = 0; i < 13; i++)
                soma += int.Parse(tempCnpj[i].ToString()) * multiplicador2[i];

            resto = (soma % 11);

            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;

            digito += resto.ToString();

            return cnpj.EndsWith(digito);
        }

        public static string ApenasNumeros(string str)
        {
            return string.IsNullOrEmpty(str)
                ? string.Empty
                : new string(str.Where(char.IsDigit).ToArray());
        }

        public static string FormatarCpf(string cpf)
        {
            cpf = ApenasNumeros(cpf);

            return string.IsNullOrEmpty(cpf)
                ? string.Empty
                : Convert.ToUInt64(cpf).ToString(@"000\.000\.000\-00");
        }

        public static string FormatarCnpj(string cnpj)
        {
            cnpj = ApenasNumeros(cnpj);

            return string.IsNullOrEmpty(cnpj)
                ? string.Empty
                : Convert.ToUInt64(cnpj).ToString(@"00\.000\.000\/0000\-00");
        }

        public static string ToUrl(string text)
        {
            return text.Replace("+", "1sm2").Replace("/", "1bi2");
        }

        public static string FromUrl(string text)
        {
            return text.Replace("1sm2", "+").Replace("1bi2", "/");
        }

        public static List<string> ToListString(this string str)
        {
            return new List<string> { str };
        }


        public static string FormatarEmailMascarado(string email)
        {
            if (email.LastIndexOf("@", StringComparison.Ordinal) > -1)
            {
                var partes = email.Split('@');
                var subpartes = partes[1].Split('.');
                var mascarado = $"{partes[0].Substring(0, 2)}***@{subpartes[0].Substring(0, 2)}***.{subpartes[^1]}";

                return mascarado;
            }
            else
                return email;
        }

        public static int ToInt32(this bool value)
        {
            return value ? 1 : 0;
        }

        public static IEnumerable<IEnumerable<T>> DividirSubListas<T>(IEnumerable<T> lista, int qtdSubListas)
        {
            if (qtdSubListas == 0)
                return new List<IEnumerable<T>> { lista };

            return lista
                .Select((entidade, index) => new { entidade, index })
                .GroupBy(x => x.index % qtdSubListas)
                .Select(g => g.Select(x => x.entidade).ToList())
                .ToList();
        }

        public static IEnumerable<IEnumerable<T>> DividirSubListasDois<T>(IEnumerable<T> lista, int qtdSubListas)
        {
            return new List<IEnumerable<T>> { lista };
        }

        /// <summary>
        /// Retira do nome do arquivo os tipos .PDF .JPG .JPEG .SVG .PNG
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string NomeSemTipoArquivo(string text)
        {
            text = text.ToUpper();
            text = text.Replace(".PDF", "");
            text = text.Replace(".JPG", "");
            text = text.Replace(".JPEG", "");
            text = text.Replace(".SVG", "");
            text = text.Replace(".PNG", "");

            return text;
        }

        public static double CalcularDistancia(double latitude1, double longitude1, double latitude2, double longitude2)
        {
            static double Radianos(double value) =>
                value * Math.PI / 180;

            const double raio = 6371; // km
            var senoLatitude1 = Math.Sin(Radianos(latitude1));
            var senoLatitude2 = Math.Sin(Radianos(latitude2));
            var cossenoLatitude1 = Math.Cos(Radianos(latitude1));
            var cossenoLatitude2 = Math.Cos(Radianos(latitude2));
            var cossenoLongitude = Math.Cos(Radianos(longitude1) - Radianos(longitude2));

            var cossenoDistancia = senoLatitude1 * senoLatitude2 + cossenoLatitude1 * cossenoLatitude2 * cossenoLongitude;
            var anguloCosseno = Math.Acos(cossenoDistancia);
            var distancia = raio * anguloCosseno;

            return distancia;
        }

        public  static byte[] BitmapToBytes(Bitmap img)
        {
            using MemoryStream stream = new MemoryStream();
            img.Save(stream, System.Drawing.Imaging.ImageFormat.Png);
            return stream.ToArray();
        }
    }
}
