using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UPA.BLL.Interfaces;
using UPA.BLL.Specifications;
using UPA.DAl;
using UPA.DAL.Models;
using UPA.ViewModels.ViewModels.GovernorateVM;
using UPA.ViewModels.ViewModels.HospitalVM;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using static System.Runtime.InteropServices.JavaScript.JSType;
using static UPA.ViewModels.ViewModels.CountOfAssetsVM.IndexCountOfAssetsVM;

namespace UPA.BLL.Repository
{
    public class HospitalRepositories : IHospitalService
    {
        private UPAModel _context;
        string msg = "";

        public HospitalRepositories(UPAModel context)
        {
            _context = context;
        }


        public EditHospitalVM GetById(int id)
        {
            var lstHospitals = _context.Hospitals.Include(a => a.Governorate)
                        .Include(a => a.City).Include(a => a.Organization).Include(a => a.SubOrganization).Where(a => a.Id == id).ToList();

            if (lstHospitals.Count > 0)
            {
                Hospital item = lstHospitals[0];
                EditHospitalVM hospitalObj = new EditHospitalVM();
                hospitalObj.Id = item.Id;
                hospitalObj.Code = item.Code;
                hospitalObj.Name = item.Name;
                hospitalObj.NameAr = item.NameAr;
                hospitalObj.Address = item.Address;
                hospitalObj.AddressAr = item.AddressAr;
                hospitalObj.Email = item.Email;
                hospitalObj.Mobile = item.Mobile;
                hospitalObj.Latitude = item.Latitude != null ? decimal.Parse(item.Latitude.ToString()) : 0;
                hospitalObj.Longtitude = item.Longtitude != null ? decimal.Parse(item.Longtitude.ToString()) : 0;
                hospitalObj.ManagerName = item.ManagerName;
                hospitalObj.ManagerNameAr = item.ManagerNameAr;
                hospitalObj.GovernorateId = item.GovernorateId != null ? item.GovernorateId : 0;
                hospitalObj.CityId = item.CityId != null ? item.CityId : 0;
                hospitalObj.OrganizationId = item.OrganizationId != null ? item.OrganizationId : 0;
                hospitalObj.SubOrganizationId = item.SubOrganizationId != null ? item.SubOrganizationId : 0;
                hospitalObj.ContractName = item.ContractName;
                hospitalObj.StrContractStart = item.ContractStart.ToString();
                hospitalObj.StrContractEnd = item.ContractEnd.ToString();
                hospitalObj.ContractStart = item.ContractStart;
                hospitalObj.ContractEnd = item.ContractEnd;


                hospitalObj.GovernorateName = item.Governorate?.Name;
                hospitalObj.GovernorateNameAr = item.Governorate?.NameAr;

                hospitalObj.CityName = item.City?.Name;
                hospitalObj.CityNameAr = item.City?.NameAr;

                hospitalObj.OrgName = item.Organization?.Name;
                hospitalObj.OrgNameAr = item.Organization?.NameAr;

                hospitalObj.SubOrgName = item.SubOrganization?.Name;
                hospitalObj.SubOrgNameAr = item.SubOrganization?.NameAr;

                return hospitalObj;
            }

            return new EditHospitalVM();
        }


        public IEnumerable<Hospital> GetHospitalByGovId(int govId)
        {
            return _context.Hospitals.Where(h => h.GovernorateId == govId).ToList();
        }
        public List<IndexHospitalVM.GetData> GetAllLstHospitals()
        {
            IEnumerable<Hospital> result = _context.Hospitals.Include(a => a.Governorate)
                        .Include(a => a.City).Include(a => a.Organization).Include(a => a.SubOrganization).ToList();
            IndexHospitalVM mainClass = new IndexHospitalVM();
            List<IndexHospitalVM.GetData> indexCountOfAssetsVMs = new List<IndexHospitalVM.GetData>();

          

           
            foreach (var item in result)
            {
                IndexHospitalVM.GetData data = new IndexHospitalVM.GetData();
                data.Id = item.Id;
                data.Code = item.Code;
                data.Name = item.Name;
                data.NameAr = item.NameAr;
                data.GovernorateName = item.Governorate?.Name;
                data.GovernorateNameAr = item.Governorate?.NameAr;

                data.CityName = item.City?.Name;
                data.CityNameAr = item.City?.NameAr;


                data.OrgName = item.Organization?.Name;
                data.OrgNameAr = item.Organization?.NameAr;

                data.SubOrgName = item.SubOrganization?.Name;
                data.SubOrgNameAr = item.SubOrganization?.NameAr;

                data.ContractName = item.ContractName;
                data.StrContractStart = item.ContractStart.ToString();
                data.StrContractEnd = item.ContractEnd.ToString();
                indexCountOfAssetsVMs.Add(data);


            }
           
            return indexCountOfAssetsVMs;
        }
        public IndexHospitalVM GetAll(CountOfAssetsSpecParams @params)
        {
            IQueryable<Hospital> result = _context.Hospitals.Include(a => a.Governorate)
                        .Include(a => a.City).Include(a => a.Organization).Include(a => a.SubOrganization);
            IndexHospitalVM mainClass = new IndexHospitalVM();
            List<IndexHospitalVM.GetData> indexCountOfAssetsVMs = new List<IndexHospitalVM.GetData>();


            
                if (@params.Name.Count > 0)
                {
                    result = result.Where(a => @params.Name.Contains(a.Name) || @params.Name.Contains(a.NameAr));
                }
                if (@params.GovId.Count > 0)
                {
                result = result.Where(a => @params.GovId.Contains(a.GovernorateId));
                }
                if (@params.Code.Count > 0)
                {
                    result = result.Where(a => @params.Code.Contains(a.Code));
                }
                if (@params.OrgId.Count > 0)
                {
                    result = result.Where(a => @params.OrgId.Contains(a.OrganizationId));
                }
                if (@params.SubOrgId.Count > 0)
                {
                    result = result.Where(a => @params.SubOrgId.Contains(a.SubOrganizationId));
                }
                

            






            if (@params.Sort != "")
            {
                switch (@params.Sort)
                {
                    case "الكود":
                    case "Code":
                        if (@params.SortStatus == "ascending")
                        {

                            result = result.
                              OrderBy(ww => ww.Code);
                        }
                        else
                        {
                            result = result.
                         OrderByDescending(ww => ww.Code);
                        }
                        break;




                    case "Name":
                        if (@params.SortStatus == "ascending")
                        {
                            result = result.
                                 OrderBy(ww => ww.Name);
                        }
                        else
                        {
                            result = result.
                                 OrderByDescending(ww => ww.Name);
                        }
                        break;
                    case "الاسم":
                        if (@params.SortStatus == "ascending")
                        {
                            result = result.
                                 OrderBy(ww => ww.NameAr);
                        }
                        else
                        {
                            result = result.
                                 OrderByDescending(ww => ww.NameAr);
                        }
                        break;
                    case "Governorate":
                        if (@params.SortStatus == "ascending")
                        {
                            result = result.
                                 OrderBy(ww => ww.Governorate.Name);
                        }
                        else
                        {
                            result = result.
                                 OrderByDescending(ww => ww.Governorate.Name);
                        }
                        break;
                    case "المحافظة":
                        if (@params.SortStatus == "ascending")
                        {
                            result = result.
                                 OrderBy(ww => ww.Governorate.NameAr);
                        }
                        else
                        {
                            result = result.
                                 OrderByDescending(ww => ww.Governorate.NameAr);
                        }
                        break;
                    case "Organization":

                        if (@params.SortStatus == "ascending")
                        {
                            result = result.OrderBy(ww => ww.Organization.Name);
                        }


                        else
                        {
                            result = result.
                                 OrderByDescending(ww => ww.Organization.Name);
                        }

                        break;




                    case "الهيئة":
                        if (@params.SortStatus == "ascending")
                        {
                            result = result.OrderBy(ww => ww.Organization.NameAr);
                        }
                        else
                        {
                            result = result.OrderByDescending(ww => ww.Organization.NameAr);
                        }




                        break;
                    case "Sub Organization":
                        if (@params.SortStatus == "ascending")
                        {

                            result = result.OrderBy(ww => ww.SubOrganization.Name);
                        }
                        else
                        {
                            result = result.OrderByDescending(ww => ww.SubOrganization.Name);
                        }





                        break;
                    case " الهيئة الفرعية":
                        if (@params.SortStatus == "ascending")
                        {

                            result = result.OrderBy(ww => ww.SubOrganization.NameAr);
                        }
                        else
                        {
                            result = result.OrderByDescending(ww => ww.SubOrganization.NameAr);
                        }





                        break;
                   





                       
                  
                    




                }



            }

            mainClass.Count = result.Count();
            if (@params.PageSize > 0)
            {
                result = result.Skip(@params.PageSize * (@params.PageIndex - 1)).Take(@params.PageSize);

            }
            else
            {
                result = result;
            }
            foreach (var item in result)
            {
                IndexHospitalVM.GetData data = new IndexHospitalVM.GetData();
                data.Id = item.Id;
                data.Code = item.Code;
                data.Name = item.Name;
                data.NameAr = item.NameAr;
                data.GovernorateName = item.Governorate?.Name;
                data.GovernorateNameAr = item.Governorate?.NameAr;

                data.CityName = item.City?.Name;
                data.CityNameAr = item.City?.NameAr;


                data.OrgName = item.Organization?.Name;
                data.OrgNameAr = item.Organization?.NameAr;

                data.SubOrgName = item.SubOrganization?.Name;
                data.SubOrgNameAr = item.SubOrganization?.NameAr;

                data.ContractName = item.ContractName;
                data.StrContractStart = item.ContractStart.ToString();
                data.StrContractEnd = item.ContractEnd.ToString();
                indexCountOfAssetsVMs.Add(data);


            }

           
            mainClass.Results = indexCountOfAssetsVMs;
            return mainClass;
        }

        public int Add(CreateHospitalVM HospitalVM)
        {
            Hospital HospitalObj = new Hospital();
            try
            {
                if (HospitalVM != null)
                {
                   
                    HospitalObj.Name = HospitalVM.Name;
                    HospitalObj.NameAr = HospitalVM.NameAr;
                    HospitalObj.Address = HospitalVM.Address;
                    HospitalObj.AddressAr = HospitalVM.AddressAr;
                    HospitalObj.Email = HospitalVM.Email;
                    HospitalObj.Mobile = HospitalVM.Mobile;
                    HospitalObj.ManagerName = HospitalVM.ManagerName;
                    HospitalObj.ManagerNameAr = HospitalVM.ManagerNameAr;
                    if (HospitalVM.Latitude != 0)
                        HospitalObj.Latitude = HospitalVM.Latitude ;
                    if (HospitalVM.Longtitude != 0)
                        HospitalObj.Longtitude = HospitalVM.Longtitude;
                    HospitalObj.GovernorateId = HospitalVM.GovernorateId;
                    HospitalObj.CityId = HospitalVM.CityId;
                    HospitalObj.OrganizationId = HospitalVM.OrganizationId;
                    HospitalObj.SubOrganizationId = (int)HospitalVM.SubOrganizationId;
                    HospitalObj.ContractName = HospitalVM.ContractName;
                    if (HospitalVM.StrContractStart != "")
                        HospitalObj.ContractStart = DateTime.Parse(HospitalVM.StrContractStart);
                    if (HospitalVM.StrContractEnd != "")
                        HospitalObj.ContractEnd = DateTime.Parse(HospitalVM.StrContractEnd);
                    var barcode = (int.Parse(HospitalVM.Code)).ToString();
                    var lastcode = barcode.PadLeft(5, '0');
                    HospitalObj.Code = lastcode;
                    _context.Hospitals.Add(HospitalObj);
                    _context.SaveChanges();


                    int hospitalId = HospitalObj.Id;



                }
            }
            catch (Exception ex)
            {
                msg = ex.Message;
            }
            return HospitalObj.Id;
        }

        public int Delete(int id)
        {
            var HospitalObj = _context.Hospitals.Find(id);
            try
            {
                if (HospitalObj != null)
                {

                    _context.Hospitals.Remove(HospitalObj);
                    return _context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                msg = ex.Message;
            }

            return 0;
        }

        public int Update(EditHospitalVM HospitalVM)
        {
            try
            {

                var HospitalObj = _context.Hospitals.Find(HospitalVM.Id);
                if (HospitalObj != null)
                {
                    HospitalObj.Id = HospitalVM.Id;
                    HospitalObj.Code = HospitalVM.Code;
                    HospitalObj.Name = HospitalVM.Name;
                    HospitalObj.NameAr = HospitalVM.NameAr;
                    HospitalObj.Address = HospitalVM.Address;
                    HospitalObj.AddressAr = HospitalVM.AddressAr;
                    HospitalObj.Email = HospitalVM.Email;
                    HospitalObj.Mobile = HospitalVM.Mobile;
                    HospitalObj.ManagerName = HospitalVM.ManagerName;
                    HospitalObj.ManagerNameAr = HospitalVM.ManagerNameAr;
                    if (HospitalVM.Latitude != 0)
                        HospitalObj.Latitude = HospitalVM.Latitude;
                    if (HospitalVM.Longtitude != 0)
                        HospitalObj.Longtitude = HospitalVM.Longtitude;
                    HospitalObj.GovernorateId = HospitalVM.GovernorateId;
                    HospitalObj.CityId = HospitalVM.CityId;
                    HospitalObj.OrganizationId = HospitalVM.OrganizationId;
                    HospitalObj.SubOrganizationId = HospitalVM.SubOrganizationId;

                    HospitalObj.ContractName = HospitalVM.ContractName;
                    if (HospitalVM.StrContractStart != "")
                        HospitalObj.ContractStart = DateTime.Parse(HospitalVM.StrContractStart);
                    if (HospitalVM.StrContractEnd != "")
                        HospitalObj.ContractEnd = DateTime.Parse(HospitalVM.StrContractEnd);

                    _context.Entry(HospitalObj).State = EntityState.Modified;
                    _context.SaveChanges();
                    return HospitalObj.Id;
                }
            }
            catch (Exception ex)
            {
                msg = ex.Message;
            }

            return 0;
        }

        public IEnumerable<Hospital> GetAllHospitals()
        {
            return _context.Hospitals.ToList();
        }

        public IEnumerable<IndexHospitalVM.GetData> GetHospitalsByUserId(string userId)
        {
            List<IndexHospitalVM.GetData> lstHospitals = new List<IndexHospitalVM.GetData>();
            if (userId != null)
            {
                // var userObj = _context.ApplicationUser.Find(userId);


                lstHospitals = _context.Hospitals.Include(a => a.Governorate).Include(a => a.City).Include(a => a.Organization).Include(a => a.SubOrganization)
                     .Select(item => new IndexHospitalVM.GetData
                     {
                         Id = item.Id,
                         Code = item.Code,
                         Name = item.Name,
                         NameAr = item.NameAr,
                         CityId = item.City != null ? item.City.Id : 0,
                         CityName = (item.City != null) ? item.City.Name : "",
                         CityNameAr = (item.City != null) ? item.City.NameAr : "",
                         GovernorateId = item.Governorate != null ? item.Governorate.Id : 0,
                         GovernorateName = (item.Governorate != null) ? item.Governorate.Name : "",
                         GovernorateNameAr = (item.Governorate != null) ? item.Governorate.NameAr : "",
                         OrganizationId = item.Organization != null ? item.Organization.Id : 0,
                         OrgName = (item.Organization != null) ? item.Organization.Name : "",
                         OrgNameAr = (item.Organization != null) ? item.Organization.NameAr : "",
                         SubOrganizationId = item.SubOrganization != null ? item.SubOrganization.Id : 0,
                         SubOrgName = (item.SubOrganization != null) ? item.SubOrganization.Name : "",
                         SubOrgNameAr = (item.SubOrganization != null) ? item.SubOrganization.NameAr : "",
                         ContractName = item.ContractName,
                         StrContractStart = item.ContractStart.ToString(),
                         StrContractEnd = item.ContractEnd.ToString(),
                     }).ToList();


            }
            return lstHospitals;
        }

        public IEnumerable<Hospital> GetHospitalsByCityId(int cityId)
        {
            var lstHospitals = _context.Hospitals.ToList().Where(a => a.CityId == cityId).OrderBy(a => a.Id).ToList();
            return lstHospitals;

        }

        public IEnumerable<Hospital> GetHospitalsBySubOrganizationId(int subOrgId)
        {
            return _context.Hospitals.ToList().Where(a => a.SubOrganizationId == subOrgId).ToList();
        }



        public List<SubOrganization> GetSubOrganizationsByHospitalId(int hospitalId)
        {
            return _context.Hospitals.Include(a => a.SubOrganization).Where(a => a.Id == hospitalId).Select(a => a.SubOrganization).ToList();
            //join sub in _context.SubOrganizations on hospital.SubOrganizationId equals sub.Id
            //where hospital.Id == hospitalId
            //select sub).ToList();
        }




       

        public GenerateHospitalCodeVM GenerateHospitalCode()
        {
            GenerateHospitalCodeVM numberObj = new GenerateHospitalCodeVM();
            int code = 0;

            var lastId = _context.Hospitals.ToList();
            if (lastId.Count > 0)
            {
                var lastHospitalCode = lastId.Max(a =>a.Code!=null? int.Parse( a.Code):0);
                if (lastHospitalCode != null)
                {
                    var hospitalCode = (lastHospitalCode + 1).ToString();
                    var lastcode = hospitalCode.ToString().PadLeft(3, '0');
                    numberObj.Code = lastcode;
                }
            }
            else
            {
                numberObj.Code = (code + 1).ToString();
            }

            return numberObj;
        }

        public EditHospitalVM GetHospitalByAssetId(int id)
        {
            var lstHospitals = _context.AssetDetails.Include(a => a.Hospital)
                       .Where(a => a.Id == id).ToList();

            if (lstHospitals.Count > 0)
            {
                AssetDetail item = lstHospitals[0];
                EditHospitalVM hospitalObj = new EditHospitalVM();
                hospitalObj.Id = (int)item.HospitalId;
                hospitalObj.Code = item.Code;
                hospitalObj.Name = item.Hospital?.Name;
                hospitalObj.NameAr = item.Hospital?.NameAr;

                return hospitalObj;
            }

            return new EditHospitalVM();
        }



        public IEnumerable<IndexHospitalVM.GetData> AutoCompleteHospitalName(string name)
        {
            var lst = _context.Hospitals.Where(a => a.Name.Contains(name) || a.NameAr.Contains(name)).Select(item => new IndexHospitalVM.GetData
            {
                Id = item.Id,
                Name = item.Name,
                NameAr = item.NameAr
            }).ToList();
            return lst;
        }

    }
}