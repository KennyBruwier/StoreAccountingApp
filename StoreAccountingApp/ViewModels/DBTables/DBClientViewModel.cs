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
    public class DBClientViewModel : ViewModelBase
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
            saveCommand = new RelayCommand(SaveAndCatch);
            searchCommand = new RelayCommand(Search);
            updateCommand = new RelayCommand(UpdateAndCatch);
            deleteCommand = new RelayCommand(DeleteAndCatch);
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
        public void SaveAndCatch()
        {
            CatchOperation(Save);
            LoadData();
        }
        public bool Save()
        {
            return clientService.Add(CurrentClientDTO);
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
        public void UpdateAndCatch()
        {
            CatchOperation(Update);
            LoadData();
        }
        public bool Update()
        {
            return clientService.Update(CurrentClientDTO);
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
            return clientService.Delete(CurrentClientDTO.ClientId);
        }
        #endregion
    }
}
