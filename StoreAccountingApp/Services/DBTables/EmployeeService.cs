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
                employeeList.Add(ObjMethods.CopyProperties<Employee, EmployeeDTO>(employee));
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
                        (newEmployeeDTO.Country != "") && 
                        (ctx.Districts.Find(newEmployeeDTO.PostalCodeId)==null))
                {
                    Country country = ctx.Countries.FirstOrDefault(c => c.Name == newEmployeeDTO.Country);
                    if (country == null) country = new Country() { Name = newEmployeeDTO.Country };
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
                ObjEmployee = ObjMethods.CopyProperties<EmployeeDTO, Employee>(objEmployeeToUpdate);
            }
            return ctx.SaveChanges() > 0;
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
