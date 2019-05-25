using System;

namespace French.Tools.Library
{
    public class Validacao
    {

        public static string DataRangeValido(string dataValidar)
        {
            var min = DateTime.Now.AddYears(-18);
            var max = DateTime.Now.AddYears(-100);
            var msg = string.Format("Data Inválida, entrar com uma data entre {0:dd/MM/yyyy} e {1:dd/MM/yyyy}", max, min);

            try
            {
                var date = DateTime.Parse(dataValidar);

                if (date > min || date < max)
                {
                    return msg;
                }
                return "true";
            }
            catch (Exception)
            {
                return msg;
            }
        }

        public static string DataRangeValido(DateTime dataValidar)
        {
            var min = DateTime.Now.AddYears(-18);
            var max = DateTime.Now.AddYears(-100);
            var msg = string.Format("Data Inválida, entrar com uma data entre {0:dd/MM/yyyy} e {1:dd/MM/yyyy}", max, min);

            try
            {
                if (dataValidar > min || dataValidar < max)
                {
                    return msg;
                }
                return "true";
            }
            catch (Exception)
            {
                return msg;
            }
        }

        public static string DataDocumentoRangeValido(string dataValidar, string dataNascimento)
        {
            var hoje = DateTime.Now;
            var msg = String.Empty;
            try
            {
                var date = DateTime.Parse(dataValidar);
                var dateNasc = DateTime.Parse(dataNascimento);
                msg = string.Format("Data Inválida, entrar com uma data entre {0:dd/MM/yyyy} e {1:dd/MM/yyyy}", dateNasc, hoje);

                if (date > hoje || date < dateNasc)
                {
                    return msg;
                }
                return "true";
            }
            catch (Exception)
            {
                return msg;
            }
        }

        public static string DataDocumentoRangeValido(DateTime dataValidar, DateTime dataNascimento)
        {
            var hoje = DateTime.Now;
            var msg = String.Empty;
            try
            {
                msg = string.Format("Data Inválida, entrar com uma data entre {0:dd/MM/yyyy} e {1:dd/MM/yyyy}", dataNascimento, hoje);

                if (dataValidar > hoje || dataValidar < dataNascimento)
                {
                    return msg;
                }
                return "true";
            }
            catch (Exception)
            {
                return msg;
            }
        }

    }
}