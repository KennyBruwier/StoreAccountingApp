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
        //public bool Add(DistrictDTO newDistrictDTO)
        //{
        //    //                                                          <----- Add validations here
        //    if (newDistrictDTO.PostalCodeId != "")
        //    {
        //        if (ctx.Districts.Find(newDistrictDTO.PostalCodeId) != null)
        //        {
        //            MessageBoxResult dialogResult = MessageBox.Show($"A district with id {newDistrictDTO.PostalCodeId} was already found, do you want to update it instead?",
        //                                                            "district already exists", MessageBoxButton.YesNo);
        //            if (dialogResult == MessageBoxResult.Yes)
        //                return Update(newDistrictDTO);
        //            else
        //                throw new ArgumentException($"Add operation failed, id {newDistrictDTO.PostalCodeId} already exists");
        //        }
        //    }

        //    if (ctx.Districts.FirstOrDefault(a => (a.Name == newDistrictDTO.Name)) != null)
        //        throw new ArgumentException($"Add operation failed, {newDistrictDTO.Name} already exists");
        //    try
        //    {
        //        District newDistrict = ObjMethods.CopyProperties<DistrictDTO, District>(newDistrictDTO);
        //        if ((newDistrictDTO.CountryDTO.Name != null) && (newDistrictDTO.CountryDTO.Name != ""))
        //        {
        //            Country country = ctx.Countries.FirstOrDefault(c=>c.Name == newDistrictDTO.CountryDTO.Name);
        //            if (country == null)
        //            {
        //                CountryService countryService = new CountryService();
        //                if (countryService.Add(new Country() { Name = newDistrictDTO.CountryDTO.Name }))
        //                    country = ctx.Countries.FirstOrDefault(c=>c.Name == newDistrictDTO.CountryDTO.Name);
        //            }
        //            newDistrict.Country = country;
        //        }
        //        ctx.Districts.Add(newDistrict);
        //        return ctx.SaveChanges() > 0;
        //    }
        //    catch (DbEntityValidationException ex)
        //    {
        //        throw ex;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
    }
}
