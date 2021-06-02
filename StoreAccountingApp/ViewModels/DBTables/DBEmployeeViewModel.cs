using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using StoreAccountingApp.Services;
using StoreAccountingApp.Commands;
using StoreAccountingApp.DTO;
using System.Windows.Input;
using System.Data.Entity.Validation;
using System.Diagnostics;

namespace StoreAccountingApp.ViewModels
{
    public class DBEmployeeViewModel : DBViewModelBase
    {

        private readonly EmployeeService _employeeService;
        private EmployeeDTO currentEmployeeDTO;
        public EmployeeDTO CurrentEmployeeDTO
        {
            get { return currentEmployeeDTO; }
            set { currentEmployeeDTO = value; OnPropertyChanged("CurrentEmployeeDTO"); }
        }
        public DBEmployeeViewModel()
        {
            _employeeService = new EmployeeService();
            LoadData();
            CurrentEmployeeDTO = new EmployeeDTO();
            saveCommand = new RelayCommand(Save);
            searchCommand = new RelayCommand(Search);
            updateCommand = new RelayCommand(Update);
            deleteCommand = new RelayCommand(Delete);
        }
        #region DisplayOperation
        private List<EmployeeDTO> employeeList;
        public List<EmployeeDTO> EmployeeList
        {
            get { return employeeList; }
            set { employeeList = value; OnPropertyChanged("EmployeeList"); }
        }
        private void LoadData()
        {
            EmployeeList = _employeeService.GetAll();
        }
        #endregion
        #region SaveOperation
        private readonly RelayCommand saveCommand;
        public RelayCommand SaveCommand
        {
            get { return saveCommand; }
        }
        public void Save()
        {
            try
            {
                Message = "";
                var IsSaved = _employeeService.Add(CurrentEmployeeDTO);
                LoadData();
                if (IsSaved)
                    Message = "Employee saved";
                else
                    Message = "Save operation failed";
            }
                        catch (Exception ex)
            {
                Message = CreateValidationErrorMsg(ex);
            }
        }
        private string message;
        public string Message
        {
            get { return message; }
            set { message = value; OnPropertyChanged("Message"); }
        }
        #endregion
        #region SearchOperation
        private readonly RelayCommand searchCommand;
        public RelayCommand SearchCommand
        {
            get { return searchCommand; }
        }
        public void Search()
        {
            try
            {
                var _employee = _employeeService.Search(CurrentEmployeeDTO.EmployeeId);
                if (_employee != null)
                {
                    CurrentEmployeeDTO = _employee;
                    Message = "Employee found";
                }
                else
                {
                    CurrentEmployeeDTO = new EmployeeDTO(); // empty the textbox fields
                    Message = "Employee not found";
                }
            }
            catch (Exception ex)
            {
                Message = ex.Message;
            }
        }
        #endregion
        #region UpdateOperation
        private readonly RelayCommand updateCommand;
        public RelayCommand UpdateCommand
        {
            get { return updateCommand; }
        }
        public void Update()
        {
            try
            {
                if (_employeeService.Update(CurrentEmployeeDTO))
                {
                    Message = "Employee updated";
                    LoadData();
                }
                else
                {
                    Message = "Update operation failed";
                }
            }
            catch (Exception ex)
            {
                Message = CreateValidationErrorMsg(ex);
            }
        }
        #endregion
        #region DeleteOperation
        private readonly RelayCommand deleteCommand;

        public RelayCommand DeleteCommand
        {
            get { return deleteCommand; }
        }
        public void Delete()
        {
            try
            {
                if (_employeeService.Delete(CurrentEmployeeDTO.EmployeeId))
                {
                    Message = "Employee Deleted";
                    LoadData();
                }
                else
                    Message = "Delete operation failed";
            }
            catch (Exception ex)
            {

                Message = ex.Message;
            }
        }
        #endregion
    }
}
