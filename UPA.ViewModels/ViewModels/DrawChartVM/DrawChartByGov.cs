﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UPA.ViewModels.ViewModels.DrawChartVM
{
    public class DrawChartByGov
    {
        public int GovernorateId { get; set; }
        public string GovernorateName { get; set; }
        public string GovernorateNameAr { get; set; }


        public List<DrawBarChartByGov> ListBars { get; set; }
    }
}
