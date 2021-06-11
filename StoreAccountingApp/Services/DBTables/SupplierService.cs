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
    public class SupplierService : BaseService<SupplierDTO,Supplier>
    {
        public SupplierService()
        {

        }
        public override SupplierDTO CopyDBtoDTO(Supplier source)
        {
            SupplierDTO newSupplierDTO = ObjMethods.CopyProperties<Supplier, SupplierDTO>(source);
            if (newSupplierDTO.PostalCodeId != null)
            {
                DistrictService districtService = new DistrictService();
                newSupplierDTO.DistrictDTO = districtService.Search(newSupplierDTO.PostalCodeId);
                newSupplierDTO.DistrictName = newSupplierDTO.DistrictDTO?.Name;
                newSupplierDTO.CountryName = newSupplierDTO.DistrictDTO?.CountryName;
            }
            return newSupplierDTO;
        }
        public override Supplier CopyDTOtoDB(SupplierDTO dtoModel)
        {
            Supplier newSupplier = ObjMethods.CopyProperties<SupplierDTO, Supplier>(dtoModel);
            if (dtoModel.PostalCodeId != "")
            {
                District newDistrict = ctx.Districts.Find(dtoModel.PostalCodeId);
                if (newDistrict == null)
                {
                    Country currentDistrictCountry = null;
                    if (dtoModel.CountryName != null)
                    {
                        currentDistrictCountry = ctx.Countries.FirstOrDefault(c => c.Name.Equals(dtoModel.CountryName, StringComparison.OrdinalIgnoreCase));
                        if (currentDistrictCountry == null)
                        {
                            currentDistrictCountry = new Country() { Name = dtoModel.CountryName };
                        }
                    }
                    newDistrict = new District()
                    {
                        PostalCodeId = dtoModel.PostalCodeId,
                        Name = dtoModel.DistrictName
                    };
                    newDistrict.Country = currentDistrictCountry;
                }
                newSupplier.District = newDistrict;
            }
            return newSupplier;

            //Supplier newSupplier = ObjMethods.CopyProperties<SupplierDTO, Supplier>(dtoModel);
            //if ((dtoModel.PostalCodeId != "") &&
            //    (dtoModel.CountryName != "") &&
            //    (ctx.Districts.Find(dtoModel.PostalCodeId) == null))
            //{
            //    Country country = ctx.Countries.FirstOrDefault(c => c.Name == dtoModel.CountryName);
            //    if (country == null)
            //    {
            //        CountryService countryService = new CountryService();
            //        if (countryService.Add(new CountryDTO() { Name = dtoModel.CountryName }))
            //            country = ctx.Countries.FirstOrDefault(c => c.Name == dtoModel.CountryName);
            //        else
            //            throw new ArgumentException($"Country add operation failed for countryname: {dtoModel.CountryName}");
            //    }
            //    District newDistrict = new District()
            //    {
            //        PostalCodeId = dtoModel.PostalCodeId,
            //        Name = dtoModel.DistrictName,
            //        Country = country
            //    };
            //    newSupplier.District = newDistrict;
            //}
            //return newSupplier;
        }

    }
}
