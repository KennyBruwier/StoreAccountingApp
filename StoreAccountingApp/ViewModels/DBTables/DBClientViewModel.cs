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
    public class DBClientViewModel : DBViewModelBase
    {
        public ICommand NavigateHomeCommand { get; }
        private readonly ClientService clientService;
        private ClientDTO currentClientDTO;
        public ClientDTO CurrentClientDTO
        {
            get { return currentClientDTO; }
            set { currentClientDTO = value; OnPropertyChanged("CurrentClientDTO"); }
        }
        public DBClientViewModel()
        {
            //NavigateHomeCommand = new NavigateCommand<HomeViewModel>(homeNavigationService);
            clientService = new ClientService();
            LoadData();
            CurrentClientDTO = new ClientDTO();
            saveCommand = new RelayCommand(Save);
            searchCommand = new RelayCommand(Search);
            updateCommand = new RelayCommand(Update);
            deleteCommand = new RelayCommand(Delete);
        }
        #region DisplayOperation
        private List<ClientDTO> clientList;
        public List<ClientDTO> ClientList
        {
            get { return clientList; }
            set { clientList = value; OnPropertyChanged("ClientList"); }
        }
        private void LoadData()
        {
            ClientList = clientService.GetAll();
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
                var IsSaved = clientService.Add(CurrentClientDTO);
                LoadData();
                if (IsSaved)
                    Message = "Client saved";
                else
                    Message = "Save operation failed";
            }
            catch (DbEntityValidationException ex)
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
                var client = clientService.Search(CurrentClientDTO.ClientId);
                if (client != null)
                {
                    CurrentClientDTO = client;
                    Message = "Client found";
                }
                else
                {
                    CurrentClientDTO = new ClientDTO(); // empty the textbox fields
                    Message = "Client not found";
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
        public void Update()
        {
            try
            {
                if (clientService.Update(CurrentClientDTO))
                {
                    Message = "Client updated";
                    LoadData();
                }
                else
                {
                    Message = "Update operation failed";
                }
            }
            catch (DbEntityValidationException ex)
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
                if (clientService.Delete(CurrentClientDTO.ClientId))
                {
                    Message = "Client Deleted";
                    LoadData();
                }
                else
                    Message = "Delete operation failed";
            }
            catch (DbEntityValidationException ex)
            {
                Message = ex.Message;
            }
        }
        #endregion
    }
}
