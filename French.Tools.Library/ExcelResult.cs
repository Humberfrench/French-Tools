using System.Web;
using System.Web.Mvc;

namespace French.Tools.Library
{
    /// <summary>
    /// Classe de recurso para formatar a view para excel
    /// </summary>
    public class ExcelResult : ViewResult
    {
        /// <summary>
        /// Nome do arquivo
        /// </summary>
        private string nomeDoArquivo;

        /// <summary>
        /// Gera a view para o formato excell
        /// </summary>
        /// <param name="view">Nome da view</param>
        /// <param name="model">Modelo de dados da view</param>
        /// <param name="nome">Nome do arquivo a ser gerado</param>
        /// <returns>Retorna view formatada</returns>
        public static ViewResult ViewExcel(string view, object model, string nome)
        {
            // Cria e inicializa a action
            ExcelResult lobjResultado = new ExcelResult
            {
                nomeDoArquivo = nome,
                ViewName = view
            };

            // Adiciona o model a action
            lobjResultado.ViewData.Model = model;

            return lobjResultado;
        }

        /// <summary>
        /// Carrega e formata a view para visualização
        /// </summary>
        /// <param name="context">Contexto da controller responsável pela chamda da view</param>
        public override void ExecuteResult(ControllerContext context)
        {
            if (context != null)
            {
                // Limpa o contetxo da view, icluido o tipo da mesma
                context.HttpContext.Response.Clear();

                // Adiciona o cabeçalho com o nome do arquivo, a ser gerado a apartir da view
                context.HttpContext.Response.AddHeader("content-disposition", "attachment;filename=" + nomeDoArquivo);

                // Remove o cache
                context.HttpContext.Response.Cache.SetCacheability(HttpCacheability.NoCache);

                // Tipa o contexto da view
                context.HttpContext.Response.ContentType = "application/vnd.ms-excel";
                base.ExecuteResult(context);
            }
        }
    }
}