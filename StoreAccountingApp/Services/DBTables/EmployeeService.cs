using StoreAccountingApp.Models;
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

namespace StoreAccountingApp.Services
{
    public class EmployeeService
    {
        private readonly _DBStoreAccountingContext ctx;

        public object DialogResult { get; private set; }
        public EmployeeService()
        {
            ctx = new _DBStoreAccountingContext();
        }
        public List<EmployeeDTO> GetAll()
        {
            List<EmployeeDTO> employeeList = new List<EmployeeDTO>();
            var ObjQuery = from Employee in ctx.Employees
                           select Employee;
            foreach (var employee in ObjQuery)
            {
                EmployeeDTO newEmployeeDTO = ObjMethods.CopyProperties<Employee, EmployeeDTO>(employee);
                if ((employee.PostalCodeId != null) && (employee.PostalCodeId != ""))
                {
                    DistrictService districtService = new DistrictService();
                    newEmployeeDTO.DistrictDTO = districtService.Search(employee.PostalCodeId);
                    newEmployeeDTO.CountryName = newEmployeeDTO.DistrictDTO.Name;
                }
                employeeList.Add(newEmployeeDTO);
            }
            return employeeList;
        }
        public bool Add(EmployeeDTO newEmployeeDTO)
        {
            //                                                          <----- Add validations here
            if (newEmployeeDTO.EmployeeId != 0)
            {
                if (ctx.Employees.Find(newEmployeeDTO.EmployeeId) != null)
                {
                    MessageBoxResult dialogResult = MessageBox.Show($"An employee with id {newEmployeeDTO.EmployeeId} was already found, do you want to update it instead?",
                                                                    "employee already exists", MessageBoxButton.YesNo);
                    if (dialogResult == MessageBoxResult.Yes)
                        return Update(newEmployeeDTO);
                    else
                        throw new ArgumentException($"Add operation failed, id {newEmployeeDTO.EmployeeId} already exists");
                }
            }
            if (ctx.Employees.FirstOrDefault(a => (a.Firstname == newEmployeeDTO.Firstname) && (a.Lastname == newEmployeeDTO.Lastname)) != null)
                throw new ArgumentException($"Add operation failed, {newEmployeeDTO.Firstname} {newEmployeeDTO.Lastname} already exists");
            try
            {
                Employee newEmployee = ObjMethods.CopyProperties<EmployeeDTO, Employee>(newEmployeeDTO);
                if (    (newEmployeeDTO.PostalCodeId != "") &&
                        (newEmployeeDTO.CountryName != "") && 
                        (ctx.Districts.Find(newEmployeeDTO.PostalCodeId) == null))
                {
                    Country country = ctx.Countries.FirstOrDefault(c => c.Name == newEmployeeDTO.CountryName);
                    if (country == null)
                    {
                        CountryDTO countryDTO = new CountryDTO() { Name = newEmployeeDTO.CountryName };
                        CountryService countryService = new CountryService();
                        if (countryService.Add(countryDTO))
                            country = ctx.Countries.FirstOrDefault(c => c.Name == newEmployeeDTO.CountryName);
                        else
                            throw new ArgumentException($"Country add operation failed for countryname: {newEmployeeDTO.CountryName}");
                    }
                    District newDistrict = new District()
                    {
                        PostalCodeId = newEmployeeDTO.PostalCodeId,
                        Name = newEmployeeDTO.DistrictName,
                        Country = country
                    };
                    newEmployee.District = newDistrict;
                }
                ctx.Employees.Add(newEmployee);
                return ctx.SaveChanges() > 0;
            }
            catch (DbEntityValidationException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public EmployeeDTO Search(int employeeId)
        {
            EmployeeDTO ObjEmployee = null;
            var ObjEmployeeToFind = ctx.Employees.Find(employeeId);
            if (ObjEmployeeToFind != null)
            {
                ObjEmployee = ObjMethods.CopyProperties<Employee, EmployeeDTO>(ObjEmployeeToFind);
            }
            return ObjEmployee;
        }
        public bool Update(EmployeeDTO objEmployeeToUpdate)
        {
            var ObjEmployee = ctx.Employees.Find(objEmployeeToUpdate.EmployeeId);
            if (ObjEmployee != null)
            {
                if (objEmployeeToUpdate.PostalCodeId != null && (ObjEmployee.PostalCodeId != objEmployeeToUpdate.PostalCodeId))
                {
                    District district = ctx.Districts.FirstOrDefault(d=>d.PostalCodeId == objEmployeeToUpdate.PostalCodeId);
                    if (district == null)
                    {
                        DistrictService districtService = new DistrictService();
                        DistrictDTO newDistrictDTO = new DistrictDTO()
                        {
                            PostalCodeId = objEmployeeToUpdate.PostalCodeId,
                            Name = objEmployeeToUpdate.DistrictName,
                            CountryDTO = new CountryDTO() { Name = objEmployeeToUpdate.CountryName }
                        };
                        if (districtService.Add(newDistrictDTO))
                            district = ctx.Districts.FirstOrDefault(d => d.PostalCodeId == objEmployeeToUpdate.PostalCodeId);
                    }
                    ObjEmployee.District = district;
                };
                ObjEmployee = ObjMethods.CopyProperties<EmployeeDTO, Employee>(objEmployeeToUpdate);
            }
            else
            {
                if (MessageBox.Show("Employee not found, do you want to add it instead?", "Employee not found", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                    return Add(objEmployeeToUpdate);
                else
                    throw new ArgumentException("Employee not found");
            }
            try
            {
                return ctx.SaveChanges() > 0;
            }
            catch(DbEntityValidationException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool Delete(int employeeId)
        {
            var ObjEmployeeToDelete = ctx.Employees.Find(employeeId);
            if (ObjEmployeeToDelete != null)
                ctx.Employees.Remove(ObjEmployeeToDelete);
            return ctx.SaveChanges() > 0;
        }
    }
}
