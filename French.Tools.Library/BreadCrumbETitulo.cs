using System;
using System.Collections.Generic;

namespace Credpay.Tools.Library
{
    [Serializable]
    public class BreadCrumbETitulo
    {
        public string Titulo { get; set; }

        public List<BreadCrumb> BreadCrumbs { get; set; }
    }

}
