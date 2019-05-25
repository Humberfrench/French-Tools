using System;
using System.Linq;

namespace French.Tools.Library
{
    public static class GeracaoDeChaves
    {
        public static string ObterReferencia()
        {

            var rand = new Random();
            return (rand.Next(1, 999999).ToString());

        }
        public static string GerarSenhaAleatoria(int tamanho)
        {
            var chars = "ABCDEFGHIJKLMNOPQRSTUVXYWZabcdefghijklmnopqrstuvxywz0123456789";
            //var chars = "0123456789";
            var random = new Random();
            var result = new string(
                Enumerable.Repeat(chars, tamanho)
                    .Select(s => s[random.Next(s.Length)])
                    .ToArray());
            return result;
        }
        public static long GerarNumeroUnico(int tamanho)
        {
            var chars = "0123456789";
            //var chars = "0123456789";
            var random = new Random();
            var result = new string(
                Enumerable.Repeat(chars, tamanho)
                    .Select(s => s[random.Next(s.Length)])
                    .ToArray());

            return Convert.ToInt64(result);
        }
    }
}