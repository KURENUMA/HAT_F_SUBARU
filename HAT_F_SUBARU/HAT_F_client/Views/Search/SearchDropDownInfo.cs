using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HatFClient.Views.Search
{
    public class SearchDropDownInfo
    {
        public string FieldName { get; set; }
        public Dictionary<string, string> DropDownItems { get; set; }
    }
}
