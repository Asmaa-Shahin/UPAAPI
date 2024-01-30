using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UPA.ViewModels.ViewModels.AssetDetailVM
{
    public class FilterAndSortVMAssets
    {
        public string SortBy { get; set; }
        public SearchVM? SearchBy { get; set; }
        public string  SortStatus { get; set; }
        public class SearchVM
        {

            public List<int?>? BrandId { get; set; }
            public List<int?>? HospitalId { get; set; }
            public List<int?>? GovId { get; set; }
            public List<int?>? CategoryId { get; set; }
            public List<int?>? SubCategoryId { get; set; }
            public List<int?>? SubOrgId { get; set; }
            public List<int?>? OrgId { get; set; }
            public List<string?>? Name { get; set; }
            public List<string?>? Model { get; set; }
            public DateTime? purchaseDateFrom { get; set; }
            public DateTime? purchaseDateTo { get; set; }

            public double? MinAgeByYear { get; set; }
   
            public double? MaxAgeByYear { get; set; }
            public double? MinAgeByMonth { get; set; }

            public double? MaxAgeByMonth { get; set; }
            public double? MinExpectedLife { get; set; }
            public double? MaxExpectedLife { get; set; }

            public string? Start { get; set; }
            public string? End { get; set; }
  
        }
    }
}
