using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HatFClient.Views.Search
{
    internal class GenSearchVisiblityAttribute : Attribute
    {
        public bool Visible { get; set; }
        public GenSearchVisiblityAttribute() : this(true) { }
        public GenSearchVisiblityAttribute(bool visible) { this.Visible = visible; }
    }
}
