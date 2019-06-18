using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Web.Helpers;
using System.Web.Mvc;

namespace French.Tools.Extensions
{
    public static class Extensions
    {

        public static string ToJson(this object dado)
        {
            return Json.Serialize(dado);
        }
        public static string ToJson(this string dado)
        {
            return "{" + nameof(dado) + ":'" + dado + "'}";
        }

        /// <summary>
        /// transforma a primeira letra e a primeira letra apos white-space em maiuscula, 
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string ToCapitalize(this string text)
        {
            if (string.IsNullOrEmpty(text))
            {
                return string.Empty;
            }

            var formattedText = new StringBuilder();
            var arWords = text.ToLower().Split(' ');

            foreach (string word in arWords)
            {
                if (word.Trim().Length == 0)
                {
                    continue;
                }

                if (formattedText.Length > 0)
                {
                    formattedText.Append(" ");
                }

                var capLetter = word[0].ToString().ToUpper();

                formattedText.Append(capLetter);

                if (word.Length > 1)
                {
                    formattedText.Append(word.Substring(1));
                }
            }

            return formattedText.ToString();
        }

        /// <summary>
        /// Método para retirar acentos e espaço de uma string.
        /// </summary>
        /// <param name="text">Texto a ser fotrmatado</param>
        /// <returns>Texto sem acento</returns>                    
        public static string RemoveAcentos(this string text)
        {
            if (string.IsNullOrEmpty(text))
            {
                return string.Empty;
            }

            string[] arCharacter = {"á","é","í","ó","ú","à","ã","õ","â","ê","î","ô","û","ä","ë","ï","ö","ü","ç",
                "Á","É","Í","Ó","Ú","À","Ã","Õ","Â","Ê","Î","Ô","Û","Ä","Ë","Ï","Ö","Ü","Ç"};

            string[] arNewCharacter = {"a","e","i","o","u","a","a","o","a","e","i","o","u","a","e","i","o","u","c",
                "A","E","I","O","U","A","A","O","A","E","I","O","U","A","E","I","O","U","C"};

            for (int i = 0; i < arCharacter.Length; i++)
            {
                text = text.Replace(arCharacter[i], arNewCharacter[i]);
            }

            return text;
        }

        public static bool IsValidEmail(this string strIn)
        {
            //Retorna o e-mail quando valido
            return Regex.IsMatch(strIn,
                @"^(?("")("".+?""@)|(([0-9a-zA-Z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-zA-Z])@))" +
                @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-zA-Z][-\w]*[0-9a-zA-Z]\.)+[a-zA-Z]{2,6}))$");

        }

        /// <summary>
        /// Método para retirar caracteres especiais de uma string
        /// </summary>
        /// <param name="text">Texto a ser fotrmatado</param>
        /// <returns>Texto sem caracteres especiais</returns>                    
        public static string RemoveSpecialCharacter(this string text)
        {
            if (string.IsNullOrEmpty(text))
            {
                return string.Empty;
            }

            //regex para encontrar qualquer coisa exceto letras, acentuadas, numeros
            var rgx = new Regex(@"[^\w]");

            //tudo vira hifen
            var strReturn = rgx.Replace(text, "-");

            //removo hifens repetidos seguidos
            strReturn = strReturn.MergeRepeatedCharacters('-');

            return strReturn;
        }

        public static string MergeRepeatedCharacters(this string text, char repeatedChar)
        {
            var reducedString = Regex.Replace(text, repeatedChar + "+", repeatedChar.ToString());
            var finalString = reducedString.Trim(repeatedChar);

            return finalString;
        }

        public static string ToAlphaNumeric(this string text)
        {
            return text.RemoveAcentos().RemoveSpecialCharacter();
        }

        /// <summary>
        /// converte para hash MD5 de 32 caracteres
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string ToHashMd5(this string text)
        {
            var md5Hasher = MD5.Create();
            var data = md5Hasher.ComputeHash(Encoding.Default.GetBytes(text));

            var sBuilder = new StringBuilder();

            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            return sBuilder.ToString();
        }

        //experimental
        public static string ToClockFormat(this DateTime data)
        {
            var tsClock = (data - DateTime.Now);

            int itSegundos = tsClock.Seconds;
            int itMinutos = tsClock.Minutes;
            int itHoras = (tsClock.Days * 24) + tsClock.Hours;

            return itHoras.ToString("D2") + ":" + itMinutos.ToString("D2") + ":" + itSegundos.ToString("D2");
        }

        /// <summary>
        /// Converte string em array de bytes com correcao de caracters
        /// </summary>
        /// <param name="vcString">recebe string a ser convertida</param>
        /// <param name="isXml">indica que e xml na string para tratar caracteres invalidos </param>
        /// <returns>devolve uma array de bytes</returns>
        public static byte[] ToPostBytes(this string vcString, bool isXml)
        {
            var urlEncode = new StringBuilder();

            // Separando cada parâmetro 
            char[] reserved = { '?', '=', '&' };

            int i = 0;
            while (i < vcString.Length)
            {
                int j;
                if (isXml)
                {
                    j = vcString.IndexOfAny(reserved, i);
                }
                else
                {
                    j = -1;
                }
                if (j == -1)
                {
                    urlEncode.Append(vcString.Substring(i, vcString.Length - i));
                    break;
                }
                urlEncode.Append(vcString.Substring(i, j - i));
                urlEncode.Append(vcString.Substring(j, 1));
                i = j + 1;
            }

            // codificando em UTF8 (evita que sejam mostrados códigos malucos em caracteres especiais 
            return Encoding.UTF8.GetBytes(urlEncode.ToString());
        }

        public static byte[] ToPostBytes(this string vcString)
        {
            return ToPostBytes(vcString, false);
        }

        /// <summary>
        /// utilizar # para forma a mascara
        /// </summary>
        /// <param name="stString">string a ser formatada</param>
        /// <param name="maskString">mascara com # para os caracteres a serem mantidos ex: ###/### retorna 123/456</param>
        /// <returns>retorna uma string formatada de acordo com a mascara</returns>
        public static string Mask(this string stString, string maskString)
        {
            var stFormatted = string.Empty;

            int itIndexString = 0;

            for (int i = 0; i < maskString.Length; i++)
            {
                if (maskString.Substring(i, 1) == "#")
                {
                    stFormatted += (itIndexString < stString.Length) ? stString.Substring(itIndexString, 1) : " ";
                    itIndexString++;
                }
                else
                {
                    stFormatted += maskString.Substring(i, 1);
                }
            }

            return stFormatted;
        }

        /// <summary>
        /// corta uma string e completa com reticencias
        /// </summary>
        /// <param name="text"></param>
        /// <param name="length">comprimento final</param>
        /// <returns></returns>
        public static string ToLength(this string text, int length)
        {
            if (text.Length > length)
            {
                text = text.Substring(0, length - 3) + "...";
            }
            return text;
        }

        /// <summary>
        /// Completa uma string com caracteres escolhidos ate o comprimento desejado
        /// </summary>
        /// <param name="stString">string a ser completada</param>
        /// <param name="lado">Adicionar caracteres a direita ou esquerda</param>
        /// <param name="length">comprimento do resultado</param>
        /// <param name="caracter">caracter a adicionar até o comprimento desejado</param>
        /// <returns>string com caracateres adicionados a esquerda ou direita</returns>
        public static string ToLength(this string stString, StringDirectionEnum lado, int length, char caracter)
        {
            var completeStb = new StringBuilder();

            for (int i = length; i > stString.Length; i--)
            {
                completeStb.Append(caracter.ToString());
            }

            var completeSt = (lado == StringDirectionEnum.Right) ? stString + completeStb : completeStb + stString;

            return completeSt;
        }

        /// <summary>
        /// Extends the <code>String</code> class with this <code>ToFixedString</code> method.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="length">The prefered fixed string size</param>
        /// <param name="appendChar">The <code>char</code> to append</param>
        /// <returns></returns>
        public static string ToFixedString(this string value, int length, char appendChar = ' ')
        {
            int currlen = value.Length;
            int needed = length == currlen ? 0 : (length - currlen);

            return needed == 0 ? value :
                (needed > 0 ? value + new string(' ', needed) :
                    new string(new string(value.ToCharArray().Reverse().ToArray()).
                        Substring(needed * -1, value.Length - (needed * -1)).ToCharArray().Reverse().ToArray()));
        }

        /// <summary>
        /// converte uma lista de string em uma string separadas por separadores
        /// </summary>
        /// <param name="list">lista</param>
        /// <param name="separator">separador inicial</param>
        /// <param name="lastSeparator">separador do ultimo item</param>
        /// <returns></returns>
        public static string ToGrammarianText(this List<string> list, string separator, string lastSeparator)
        {
            if (list == null || list.Count == 0)
                return string.Empty;

            var format = string.Join("{0}", list.ToArray());

            string result;

            if (list.Count > 2)
                result = string.Format(format.ReplaceLastOccurrence("{0}", "{1}"), separator, lastSeparator);
            else
                result = string.Format(format, lastSeparator);

            return result;
        }

        /// <summary>
        /// converte uma lista de string em uma string separadas por virgula e pela preposicao E
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public static string ToGrammarianText(this List<string> list)
        {
            return list.ToGrammarianText(", ", " e ");
        }

        public static string ToGrammarianText(this IEnumerable<string> list)
        {
            return list.ToList().ToGrammarianText(", ", " e ");
        }

        /// <summary>
        /// replace na ultima ocorrencia somente
        /// </summary>
        /// <param name="text"></param>
        /// <param name="oldString"></param>
        /// <param name="newString"></param>
        /// <returns></returns>
        public static string ReplaceLastOccurrence(this string text, string oldString, string newString)
        {
            int index = text.LastIndexOf(oldString);
            var result = text.Remove(index, oldString.Length).Insert(index, newString);
            return result;
        }

        public static string FormatoCpfouCnpj(this string text)
        {
            //CNPJ
            if (text.Length == 14)
                return Convert.ToUInt64(text).ToString(@"00\.000\.000\/0000\-00");

            //CPF
            if (text.Length == 11)
                return Convert.ToUInt64(text).ToString(@"000\.000\.000\-00");

            return text;
        }

        public static string FormatCurrency(this HtmlHelper helper, decimal? val)
        {
            if (val == null)
                return "Falha na comunicação";

            return $"{val:c}".Replace("R$", string.Empty);
        }
        public static string FormatCurrency(this HtmlHelper helper, double? val)
        {
            if (val == null)
                return "Falha na comunicação";

            return $"{val:c}".Replace("R$", string.Empty);
        }

        public static string ToMoeda(this double? valor)
        {
            if (valor.HasValue)
            {
                return valor.Value.ToString("C");

            }
            return 0.ToString("C");
        }

        public static string ToMoeda(this decimal? valor)
        {
            if (valor.HasValue)
            {
                return valor.Value.ToString("C");

            }
            return 0.ToString("C");
        }
        public static string ToMoeda(this double valor)
        {
            return valor.ToString("C");
        }

        public static string ToMoeda(this decimal valor)
        {
            return valor.ToString("C");
        }

        public static string ToSexo(this string stringValue)
        {
            if (stringValue == "M")
            {
                return "Masculino";
            }
            else
            {
                return "Feminino";
            }
        }

        public static string ToTipoDePagamento(this int intValue)
        {
            return intValue == 1 ? "Débito" : "Crédito";
        }
        public static string ToTipoDePagamento(this int? intValue)
        {
            if (!intValue.HasValue) return "Indefinido";
            return intValue.Value == 1 ? "Débito" : "Crédito";
        }

        public static string ToString(this decimal? valor, string format)
        {
            return valor.HasValue ? valor.Value.ToString(format) : 0.ToString(format);
        }

        public static bool IsNumeric(this string text)
        {
            try
            {
                Convert.ToInt32(text);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static string ObterSeHabilitaBotao(this bool? situacao)
        {
            if (situacao.HasValue)
            {
                return situacao.Value ? "" : "disabled=disabled";
            }
            return "disabled='disabled'";
        }
        public static string ObterSeHabilitaBotaoLink(this bool? situacao)
        {
            if (situacao.HasValue)
            {
                return situacao.Value ? "" : "style=display:none;";
            }
            return "style=display:none;";
        }

        public static string ObterSeHabilitaBotao(this bool situacao)
        {
            return situacao ? "" : "disabled=disabled";
        }

        public static string ObterSeHabilitaBotaoLink(this bool situacao)
        {
            return situacao ? "" : "style=display:none;";
        }

        public static string ToSituacao(this bool? situacao)
        {
            if (situacao.HasValue)
            {
                return situacao.Value ? "Ativo" : "Inativo";
            }
            return "Inativo";
        }

        public static string ToSituacao(this bool situacao)
        {
            return situacao ? "Ativo" : "Inativo";
        }

        public static string ToResultado(this int? situacao)
        {
            if (situacao.HasValue)
            {
                return situacao.Value == 1 ? "Sucesso" : "Erro";
            }
            return "Erro";
        }

        public static string ToResultado(this int situacao)
        {
            return situacao == 1 ? "Sucesso" : "Erro";
        }

        public static DateTime ToDate(this string dateValue)
        {
            var data = dateValue.Split('/');

            var dia = Convert.ToInt16(data[0]);
            var mes = Convert.ToInt16(data[1]);
            var ano = Convert.ToInt16(data[2]);

            return new DateTime(ano, mes, dia);
        }

        public static DateTime? ToNullableDate(this string dateValue)
        {
            if (dateValue.IsNullOrEmptyOrWhiteSpace())
            {
                return null;

            }
            var data = dateValue.Split('/');

            var dia = Convert.ToInt16(data[0]);
            var mes = Convert.ToInt16(data[1]);
            var ano = Convert.ToInt16(data[2]);

            return new DateTime(ano, mes, dia);
        }

        public static string ToDateFormated(this DateTime dateValue)
        {
            return dateValue.ToString("dd/MM/yyyy");
        }

        public static string ToTimeFormated(this DateTime dateValue)
        {
            return dateValue.ToString("HH:mm");
        }

        public static string ToDateTimeFormated(this DateTime dateValue)
        {
            return $"{dateValue:dd/MM/yyyy} | {dateValue:HH:mm}";
        }

        public static string ToDateTimeWithSecondsFormated(this DateTime dateValue)
        {
            return $"{dateValue:dd/MM/yyyy} | {dateValue:HH:mm:ss}";
        }

        public static string ToDateTimeFormatedLinhaQuebrada(this DateTime? dateValue)
        {
            if (dateValue.HasValue)
            {
                return $"{dateValue.Value:dd/MM/yyyy} <br /> {dateValue.Value:HH:mm}";
            }
            return "";
        }

        public static string ToDateTimeFormatedLinhaQuebrada(this DateTime dateValue)
        {
            return $"{dateValue:dd/MM/yyyy} <br /> {dateValue:HH:mm}";
        }

        public static string ToDateTimeFormated(this DateTime dateValue, string separador)
        {
            return $"{dateValue:dd/MM/yyyy} {separador} {dateValue:HH:mm}";
        }

        public static string ToDateTimeWithSecondsFormated(this DateTime dateValue, string separador)
        {
            return $"{dateValue:dd/MM/yyyy} {separador} {dateValue:HH:mm:ss}";
        }

        public static string ToDateFormated(this DateTime? dateValue)
        {
            if (dateValue.HasValue)
            {
                return dateValue.Value.ToString("dd/MM/yyyy");
            }
            return "";
        }

        public static string ToTimeFormated(this DateTime? dateValue)
        {
            if (dateValue.HasValue)
            {
                return dateValue.Value.ToString("HH:mm");
            }
            return "";
        }

        public static string ToDateTimeFormated(this DateTime? dateValue)
        {
            if (dateValue.HasValue)
            {
                return $"{dateValue.Value:dd/MM/yyyy} | {dateValue.Value:HH:mm}";
            }
            return "";
        }

        public static string ToDateTimeFormated(this DateTime? dateValue, string separador)
        {
            if (dateValue.HasValue)
            {
                return $"{dateValue.Value:dd/MM/yyyy} {separador} {dateValue.Value:HH:mm}";
            }
            return "";
        }

        public static string ToDateTimeWithSecondsFormated(this DateTime? dateValue)
        {
            if (dateValue.HasValue)
            {
                return $"{dateValue.Value:dd/MM/yyyy} | {dateValue.Value:HH:mm:ss}";
            }
            return "";
        }

        public static string ToDateTimeWithSecondsFormated(this DateTime? dateValue, string separador)
        {
            if (dateValue.HasValue)
            {
                return $"{dateValue.Value:dd/MM/yyyy} {separador} {dateValue.Value:HH:mm:ss}";
            }
            return "";
        }

        public static string ToSaudacao(this DateTime dateValue)
        {
            var saudacao = default(string);
            if (dateValue.Hour >= 0 && dateValue.Hour < 12)
            {
                saudacao = "Bom dia";
            }
            else if (dateValue.Hour >= 12 && dateValue.Hour < 18)
            {
                saudacao = "Boa tarde";
            }
            else if (dateValue.Hour >= 18)
            {
                saudacao = "Boa noite";
            }

            return saudacao;
        }

        public static string ToDateAnsi(this DateTime dateValue)
        {
            return $"{dateValue.Year.ToString($"0000")}{dateValue.Month.ToString("00")}{dateValue.Day.ToString("00")}";
        }

        public static WebImage ToImagem(this byte[] byteInformation)
        {
            var imageReturn = new WebImage(byteInformation);
            return imageReturn;
        }

        public static bool IsNullOrEmptyOrWhiteSpace(this string value)
        {
            return string.IsNullOrEmpty(value) || string.IsNullOrWhiteSpace(value);
        }

        public static string TratarStringNull(this string value)
        {
            if (value.IsNullOrEmptyOrWhiteSpace())
            {
                return "";
            }

            return value;
        }

        public static string TratarStringNull(this int? value)
        {
            if (!value.HasValue)
            {
                return "";
            }

            if (value.Value == 0 )
            {
                return "";
            }

            return value.ToString();
        }
        public static bool Preenchido(this string value)
        {
            return !value.IsNullOrEmptyOrWhiteSpace();
        }

        public static bool DocumentoEstaZerado(this string documento)
        {
            var documentoLimpo = documento.Replace("-", "").Replace(".", "").Replace(",", "");
            var numeroValidado = long.TryParse(documentoLimpo, out var numero);

            if (!numeroValidado) return true;
            return Convert.ToInt64(numero) == 0;
        }

        public static bool ToBoolean(this int value)
        {
            return (value == 1);
        }

        public static string GetFirstAndLastName(this string name)
        {

            var names = name.Split(' ');
            return $"{names.First()} {names.Last()}";

        }
        public static string GetFirstName(this string name)
        {

            var names = name.Split(' ');
            return names.First();

        }
        public static string GetLastName(this string name)
        {

            var names = name.Split(' ');
            return names.Last();

        }

        public static string ToSimNao(this string stringValue)
        {
            if (stringValue == "S")
            {
                return "Sim";
            }
            return "Não";
        }

        public static string ToSimNao(this bool boolValue)
        {
            if (boolValue)
            {
                return "Sim";
            }
            return "Não";
        }

        public static string ToSimNao(this bool? boolValue)
        {
            if (boolValue.HasValue)
            {
                if (boolValue.Value)
                {
                    return "Sim";
                }
                return "Não";
            }
            return "Não";
        }

        public static string ToSimNaoIcon(this bool boolValue)
        {
            if (boolValue)
            {
                return "<i class='fa fa-check' data-toggle='tooltip' data-placement='top'>";
            }
            return "<i class='fa fa-ban' data-toggle='tooltip' data-placement='top'>";
        }

        public static int BitToInt(this bool boolValue)
        {
            if (boolValue)
            {
                return 1;
            }
            return 0;
        }

        public static int BitToInt(this bool? boolValue)
        {
            if (boolValue.HasValue)
            {
                if (boolValue.Value)
                {
                    return 1;
                }
                return 0;
            }
            return 0;
        }

        public static int Int(this Enum enumer)
        {
            return Convert.ToInt32(enumer);
        }

        public static string FormatarTelefone(this string telefone, string ddd)
        {
            return $"{ddd}-{telefone}";
        }

        public static string FormatarTelefone(this string telefone, string ddi, string ddd)
        {
            return $"({ddi}) {ddd}-{telefone}";
        }

        public static bool Compare(this string valor, string destino)
        {
            return string.Compare(valor, destino, StringComparison.OrdinalIgnoreCase) == 0;
        }

        public static string ToBase64Encode(this string plainText)
        {
            var plainTextBytes = Encoding.UTF8.GetBytes(plainText);
            return Convert.ToBase64String(plainTextBytes);
        }

        public static string FromBase64Decode(this string base64EncodedData)
        {
            var base64EncodedBytes = Convert.FromBase64String(base64EncodedData);
            return Encoding.UTF8.GetString(base64EncodedBytes);
        }

        public static string ToDecimalString(this decimal valor)
        {
            return valor.ToString("N2").Replace(".", "").Replace(",", ".");
        }

        public static int LastDayOfMonth(this DateTime date)
        {
            switch (date.Month)
            {
                case 1:
                case 3:
                case 5:
                case 7:
                case 8:
                case 10:
                case 12:
                    {
                        return 31;
                    }
                case 4:
                case 6:
                case 9:
                case 11:
                    {
                        return 30;
                    }
                case 2:
                    {
                        return DateTime.IsLeapYear(date.Year) ? 29 : 28;
                    }
                default:
                    {
                        return 31;
                    }
            }
        }

    }

    public enum StringDirectionEnum
    {
        Right, Left
    }
}