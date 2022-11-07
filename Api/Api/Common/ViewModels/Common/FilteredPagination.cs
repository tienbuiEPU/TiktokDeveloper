using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Api.Common.ViewModels.Common
{
    public class FilteredPagination : BasePagination
    {
        [System.ComponentModel.DefaultValue("1=1")]
        public string query { get; set; }

        [System.ComponentModel.DefaultValue("")]
        public string select { get; set; }

        [System.ComponentModel.DefaultValue("")]
        public string search { get; set; }
    }
}