using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UPA.ViewModels.ViewModels.MasterAssetVM
{
    public class FilterAndSortVM
    {
        public string SortBy { get; set; }
        public SearchVM? SearchBy { get; set; }
        public string  SortStatus { get; set; }
        
        public class SearchVM
        {

            public List<int?>? BrandId { get; set; }
            public List<int?>? AssetPeriorityId { get; set; }
            public List<int?>? OriginId { get; set; }
            public List<int?>? ECRIId { get; set; }
            public List<string?>? Code { get; set; }
            public List<string?>? Name { get; set; }
            public List<string?>? Model { get; set; }
        }
    }
}
