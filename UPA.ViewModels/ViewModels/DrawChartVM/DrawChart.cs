using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UPA.ViewModels.ViewModels.DrawChartVM
{
    public class DrawChart
    {
        public int OrganizationId { get; set; }
        public string OrganizationName { get; set; }
        public string OrganizationNameAr { get; set; }


        public List<DrawBarChart> ListBars { get; set; }
    }

 
}
