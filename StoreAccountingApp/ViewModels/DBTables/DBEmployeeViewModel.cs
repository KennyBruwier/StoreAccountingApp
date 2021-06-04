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
using StoreAccountingApp.GeneralClasses;

namespace StoreAccountingApp.ViewModels
{
    public class DBEmployeeViewModel : ViewModelBase
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
            saveCommand = new RelayCommand(SaveAndCatch);
            searchCommand = new RelayCommand(Search);
            updateCommand = new RelayCommand(UpdateAndCatch);
            deleteCommand = new RelayCommand(DeleteAndCatch);
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
        public void SaveAndCatch()
        {
            CatchOperation(Save);
            LoadData();
        }
        public bool Save()
        {
            return _employeeService.Add(CurrentEmployeeDTO);
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
            catch (DbEntityValidationException ex)
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
        public void UpdateAndCatch()
        {
            CatchOperation(Update);
            LoadData();
        }
        public bool Update()
        {
            return _employeeService.Update(CurrentEmployeeDTO);
        }
        #endregion
        #region DeleteOperation
        private readonly RelayCommand deleteCommand;

        public RelayCommand DeleteCommand
        {
            get { return deleteCommand; }
        }
        public void DeleteAndCatch()
        {
            CatchOperation(Delete);
            LoadData();
        }
        public bool Delete()
        {
            return _employeeService.Delete(CurrentEmployeeDTO.EmployeeId);
        }
        #endregion
    }
}
