using System;

namespace Credpay.Tools.Library.DapperMapper
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
    public class ColumnAttribute : Attribute
    {
        public string Name { get; set; }
        public ColumnAttribute() { }
        public ColumnAttribute(string Name) { this.Name = Name; }
    }

}