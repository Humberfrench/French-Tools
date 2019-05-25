using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace Credpay.Tools.Library
{
    public static class ViewExtentions
    {
     
        public static MvcHtmlString HidenForAction(this HtmlHelper html, string name, string actionLink)
        {
            return html.Hidden(name, actionLink, new { id = name});        
        }
    }
}