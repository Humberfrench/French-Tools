
using System;

namespace French.Tools.Library
{
    [Serializable]
    public class BreadCrumb
    {
        public string LinkText { get; set; }

        public string ActionName { get; set; }

        public string ControllerName { get; set; }

        public bool Voltar { get; set; }

        public string VoltarUrl { get; set; }

        public string Link { get; set; }

        public bool LinkRoot { get; set; }
    }
}
