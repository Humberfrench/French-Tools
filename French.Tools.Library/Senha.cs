using System;
using System.Linq;

namespace Credpay.Tools.Library
{
    public static class Senha
    {
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
    }
}