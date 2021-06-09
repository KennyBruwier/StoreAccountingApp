using StoreAccountingApp.CustomMethods;
using StoreAccountingApp.Models;
using StoreAccountingApp.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Data.Entity.Validation;
using StoreAccountingApp.Services.DBTables;

namespace StoreAccountingApp.Services
{
    public class DistrictService : BaseService<DistrictDTO, District>
    {
        public DistrictService()
        {

        }
        public override DistrictDTO CopyDBtoDTO(District source)
        {
            DistrictDTO newDistrict = ObjMethods.CopyProperties<District, DistrictDTO>(source);
            if (newDistrict.CountryId != 0)
            {
                CountryService countryService = new CountryService();
                newDistrict.CountryDTO = countryService.Search(newDistrict.CountryId);
                newDistrict.CountryName = newDistrict.CountryDTO.Name;
            }
            return newDistrict;
        }
        public override District CopyDTOtoDB(DistrictDTO dtoModel)
        {
            District newDistrict = ObjMethods.CopyProperties<DistrictDTO, District>(dtoModel);
            if (dtoModel.CountryDTO != null && (dtoModel.CountryDTO.Name != null) && (dtoModel.CountryDTO.Name != ""))
            {
                Country country = ctx.Countries.FirstOrDefault(c => c.Name == dtoModel.CountryDTO.Name);
                if (country == null)
                {
                    CountryService countryService = new CountryService();
                    if (countryService.Add(new CountryDTO() { Name = dtoModel.CountryDTO.Name }))
                        country = ctx.Countries.FirstOrDefault(c => c.Name == dtoModel.CountryDTO.Name);
                }
                newDistrict.Country = country;
            }
            return newDistrict;
        }
    }
}
