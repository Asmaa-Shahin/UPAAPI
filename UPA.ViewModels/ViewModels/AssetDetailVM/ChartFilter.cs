using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UPA.ViewModels.ViewModels.AssetDetailVM
{
    public class ChartFilter
    {

        public List<int?>? CategoryIds { get; set; }
         public List<int?>? BrandIds { get; set; }
        public List<int?> ? OrganizationIds { get; set; }
    }
}
