﻿using StoreAccountingApp.CustomMethods;
using StoreAccountingApp.Models;
using StoreAccountingApp.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace StoreAccountingApp.Services
{
    public class DistrictService
    {
        _DBStoreAccountingContext ctx;

        public object DialogResult { get; private set; }
        public DistrictService()
        {
            ctx = new _DBStoreAccountingContext();
        }
        public List<DistrictDTO> GetAll()
        {
            List<DistrictDTO> districtList = new List<DistrictDTO>();
            var ObjQuery = from District in ctx.Districts
                           select District;
            foreach (var district in ObjQuery)
            {
                districtList.Add(ObjMethods.CopyProperties<District, DistrictDTO>(district));
            }
            return districtList;
        }
        public bool Add(DistrictDTO newDistrictDTO)
        {
            //                                                          <----- Add validations here
            if (newDistrictDTO.PostalCodeId != "")
            {
                if (ctx.Districts.Find(newDistrictDTO.PostalCodeId) != null)
                {
                    MessageBoxResult dialogResult = MessageBox.Show($"A district with id {newDistrictDTO.PostalCodeId} was already found, do you want to update it instead?",
                                                                    "district already exists", MessageBoxButton.YesNo);
                    if (dialogResult == MessageBoxResult.Yes)
                        return Update(newDistrictDTO);
                    else
                        throw new ArgumentException($"Add operation failed, id {newDistrictDTO.PostalCodeId} already exists");
                }
            }

            if (ctx.Districts.FirstOrDefault(a => (a.Name == newDistrictDTO.Name)) != null)
                throw new ArgumentException($"Add operation failed, {newDistrictDTO.Name} already exists");
            try
            {
                District newDistrict = ObjMethods.CopyProperties<DistrictDTO, District>(newDistrictDTO);
                if ((newDistrictDTO.CountryDTO.Name != null) && (newDistrictDTO.CountryDTO.Name != ""))
                {
                    Country country = ctx.Countries.FirstOrDefault(c=>c.Name == newDistrictDTO.CountryDTO.Name);
                    if (country == null)
                    {
                        CountryService countryService = new CountryService();
                        if (countryService.Add(new CountryDTO() { Name = newDistrictDTO.CountryDTO.Name }))
                            country = ctx.Countries.FirstOrDefault(c=>c.Name == newDistrictDTO.CountryDTO.Name);
                    }
                    newDistrict.Country = country;
                }
                ctx.Districts.Add(newDistrict);
                return ctx.SaveChanges() > 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DistrictDTO Search(string districtId)
        {
            DistrictDTO ObjDistrict = null;
            var ObjDistrictToFind = ctx.Districts.Find(districtId);
            if (ObjDistrictToFind != null)
            {
                ObjDistrict = ObjMethods.CopyProperties<District, DistrictDTO>(ObjDistrictToFind);
            }
            return ObjDistrict;
        }
        public bool Update(DistrictDTO objDistrictToUpdate)
        {
            var ObjDistrict = ctx.Districts.Find(objDistrictToUpdate.PostalCodeId);
            if (ObjDistrict != null)
            {
                ObjDistrict = ObjMethods.CopyProperties<DistrictDTO, District>(objDistrictToUpdate);
            }
            return ctx.SaveChanges() > 0;
        }
        public bool Delete(string districtId)
        {
            var ObjDistrictToDelete = ctx.Districts.Find(districtId);
            if (ObjDistrictToDelete != null)
                ctx.Districts.Remove(ObjDistrictToDelete);
            return ctx.SaveChanges() > 0;
        }
    }
}
