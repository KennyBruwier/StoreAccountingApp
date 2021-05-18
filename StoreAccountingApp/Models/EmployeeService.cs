using StoreAccountingApp.DBModels;
using StoreAccountingApp.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace StoreAccountingApp.Models
{
    public class EmployeeService
    {
        _DBStoreAccountingContext ctx;

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
                employeeList.Add(new EmployeeDTO(
                        employeeId:employee.EmployeeId,
                        firstname:employee.Firstname,
                        lastname:employee.Lastname,
                        gender:employee.Gender,
                        status:employee.Status,
                        streetname:employee.Streetname,
                        houseNr:employee.HouseNr,
                        boxNr:employee.BoxNr,
                        postalCodeId:employee.PostalCodeId,
                        phoneNumber:employee.PhoneNumber,
                        faxNumber:employee.FaxNumber,
                        emailAddress:employee.EmailAddress,
                        website:employee.Website,
                        facebook:employee.Facebook,
                        linkedin:employee.LinkedIn,
                        birthday:employee.Birthday
                    ));
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
                var objEmployee = new Employee()
                {
                    Firstname = newEmployeeDTO.Firstname,
                    Lastname = newEmployeeDTO.Lastname,
                    Gender = newEmployeeDTO.Gender,
                    Streetname = newEmployeeDTO.Streetname,
                    HouseNr = newEmployeeDTO.HouseNr,
                    BoxNr = newEmployeeDTO.BoxNr,
                    PostalCodeId = newEmployeeDTO.PostalCodeId,
                    EmailAddress = newEmployeeDTO.EmailAddress,
                    Facebook = newEmployeeDTO.Facebook,
                    LinkedIn = newEmployeeDTO.LinkedIn,
                    Website = newEmployeeDTO.Website,
                    PhoneNumber = newEmployeeDTO.PhoneNumber,
                    FaxNumber = newEmployeeDTO.FaxNumber,
                    Birthday = newEmployeeDTO.Birthday,
                    Status = newEmployeeDTO.Status,
                    InService = newEmployeeDTO.InService,
                    OutOfService = newEmployeeDTO.OutOfService,
                    CreatedAt = newEmployeeDTO.CreatedAt,
                    ClosedAt = newEmployeeDTO.ClosedAt,
                    UpdateAt = newEmployeeDTO.UpdateAt,
                };
                ctx.Employees.Add(objEmployee);
                return ctx.SaveChanges() > 0;
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
                ObjEmployee = new EmployeeDTO()
                {
                    Firstname = ObjEmployeeToFind.Firstname,
                    Lastname = ObjEmployeeToFind.Lastname,
                    Gender = ObjEmployeeToFind.Gender,
                    Streetname = ObjEmployeeToFind.Streetname,
                    HouseNr = ObjEmployeeToFind.HouseNr,
                    BoxNr = ObjEmployeeToFind.BoxNr,
                    PostalCodeId = ObjEmployeeToFind.PostalCodeId,
                    EmailAddress = ObjEmployeeToFind.EmailAddress,
                    Facebook = ObjEmployeeToFind.Facebook,
                    LinkedIn = ObjEmployeeToFind.LinkedIn,
                    Website = ObjEmployeeToFind.Website,
                    PhoneNumber = ObjEmployeeToFind.PhoneNumber,
                    FaxNumber = ObjEmployeeToFind.FaxNumber,
                    Birthday = ObjEmployeeToFind.Birthday,
                    Status = ObjEmployeeToFind.Status,
                    InService = ObjEmployeeToFind.InService,
                    OutOfService = ObjEmployeeToFind.OutOfService,
                    CreatedAt = ObjEmployeeToFind.CreatedAt,
                    ClosedAt = ObjEmployeeToFind.ClosedAt,
                    UpdateAt = ObjEmployeeToFind.UpdateAt,
                };
            }
            return ObjEmployee;
        }
        public bool Update(EmployeeDTO objEmployeeToUpdate)
        {
            var ObjEmployee = ctx.Employees.Find(objEmployeeToUpdate.EmployeeId);
            if (ObjEmployee != null)
            {
                ObjEmployee.Firstname = objEmployeeToUpdate.Firstname;
                ObjEmployee.Lastname = objEmployeeToUpdate.Lastname;
                ObjEmployee.Gender = objEmployeeToUpdate.Gender;
                ObjEmployee.Streetname = objEmployeeToUpdate.Streetname;
                ObjEmployee.HouseNr = objEmployeeToUpdate.HouseNr;
                ObjEmployee.BoxNr = objEmployeeToUpdate.BoxNr;
                ObjEmployee.PostalCodeId = objEmployeeToUpdate.PostalCodeId;
                ObjEmployee.EmailAddress = objEmployeeToUpdate.EmailAddress;
                ObjEmployee.Facebook = objEmployeeToUpdate.Facebook;
                ObjEmployee.LinkedIn = objEmployeeToUpdate.LinkedIn;
                ObjEmployee.Website = objEmployeeToUpdate.Website;
                ObjEmployee.PhoneNumber = objEmployeeToUpdate.PhoneNumber;
                ObjEmployee.FaxNumber = objEmployeeToUpdate.FaxNumber;
                ObjEmployee.Birthday = objEmployeeToUpdate.Birthday;
                ObjEmployee.Status = objEmployeeToUpdate.Status;
                ObjEmployee.InService = objEmployeeToUpdate.InService;
                ObjEmployee.OutOfService = objEmployeeToUpdate.OutOfService;
                ObjEmployee.CreatedAt = objEmployeeToUpdate.CreatedAt;
                ObjEmployee.ClosedAt = objEmployeeToUpdate.ClosedAt;
                ObjEmployee.UpdateAt = objEmployeeToUpdate.UpdateAt;
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
