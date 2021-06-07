﻿using StoreAccountingApp.Models;
using StoreAccountingApp.DTO;
using StoreAccountingApp.CustomMethods;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Data.Entity.Validation;
using System.Diagnostics;
using StoreAccountingApp.Services.DBTables;

namespace StoreAccountingApp.Services
{
    public class EmployeeService : BaseService<EmployeeDTO,Employee>
    {
        public EmployeeService()
        {

        }
        public override EmployeeDTO CopyDBtoDTO(Employee source)
        {
            EmployeeDTO newEmployeeDTO = ObjMethods.CopyProperties<Employee, EmployeeDTO>(source);
            if ((source.PostalCodeId != null) && (source.PostalCodeId != ""))
            {
                DistrictService districtService = new DistrictService();
                newEmployeeDTO.DistrictDTO = districtService.Search(source.PostalCodeId);
                newEmployeeDTO.CountryName = newEmployeeDTO.DistrictDTO.Name;
            }
            return newEmployeeDTO;
        }
        public override Employee CopyDTOtoDB(EmployeeDTO dtoModel)
        {
            Employee newEmployee = ObjMethods.CopyProperties<EmployeeDTO, Employee>(dtoModel);
            if ((dtoModel.PostalCodeId != "") &&
                (dtoModel.CountryName != "") &&
                (ctx.Districts.Find(dtoModel.PostalCodeId) == null))
            {
                Country country = ctx.Countries.FirstOrDefault(c => c.Name == dtoModel.CountryName);
                if (country == null)
                {
                    CountryService countryService = new CountryService();
                    if (countryService.Add(new CountryDTO() { Name = dtoModel.CountryName }))
                        country = ctx.Countries.FirstOrDefault(c => c.Name == dtoModel.CountryName);
                    else
                        throw new ArgumentException($"Country add operation failed for countryname: {dtoModel.CountryName}");
                }
                District newDistrict = new District()
                {
                    PostalCodeId = dtoModel.PostalCodeId,
                    Name = dtoModel.DistrictName,
                    Country = country
                };
                newEmployee.District = newDistrict;
            }
            return newEmployee;
        }
        //public bool Update(EmployeeDTO objEmployeeToUpdate)
        //{
        //    var ObjEmployee = ctx.Employees.Find(objEmployeeToUpdate.EmployeeId);
        //    if (ObjEmployee != null)
        //    {
        //        if (objEmployeeToUpdate.PostalCodeId != null && (ObjEmployee.PostalCodeId != objEmployeeToUpdate.PostalCodeId))
        //        {
        //            District district = ctx.Districts.FirstOrDefault(d=>d.PostalCodeId == objEmployeeToUpdate.PostalCodeId);
        //            if (district == null)
        //            {
        //                DistrictService districtService = new DistrictService();
        //                DistrictDTO newDistrictDTO = new DistrictDTO()
        //                {
        //                    PostalCodeId = objEmployeeToUpdate.PostalCodeId,
        //                    Name = objEmployeeToUpdate.DistrictName,
        //                    CountryDTO = new CountryDTO() { Name = objEmployeeToUpdate.CountryName }
        //                };
        //                if (districtService.Add(newDistrictDTO))
        //                    district = ctx.Districts.FirstOrDefault(d => d.PostalCodeId == objEmployeeToUpdate.PostalCodeId);
        //            }
        //            ObjEmployee.District = district;
        //        };
        //        ObjEmployee = ObjMethods.CopyProperties<EmployeeDTO, Employee>(objEmployeeToUpdate);
        //    }
        //    else
        //    {
        //        if (MessageBox.Show("Employee not found, do you want to add it instead?", "Employee not found", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
        //            return Add(objEmployeeToUpdate);
        //        else
        //            throw new ArgumentException("Employee not found");
        //    }
        //    try
        //    {
        //        return ctx.SaveChanges() > 0;
        //    }
        //    catch(DbEntityValidationException ex)
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
