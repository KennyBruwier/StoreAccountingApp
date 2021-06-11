using StoreAccountingApp.Commands;
using StoreAccountingApp.CustomMethods;
using StoreAccountingApp.DTO;
using StoreAccountingApp.Models;
using StoreAccountingApp.Services.DBTables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreAccountingApp.ViewModels.Overviews
{
    public class BaseOverviewViewModel<DTOModel, ServiceModel, DBModel> : ViewModelBase
        where DTOModel : BaseDTO,new()
        where ServiceModel : BaseService<DTOModel,DBModel>, new()
        where DBModel : BaseModel,new()
    {
        public BaseOverviewViewModel()
        {
            tableName = this.GetClassName();
            currentServiceModel = new ServiceModel();
            cbDTOPropsList = ObjMethods.CreateComboboxListFromProp<DTOModel, ComboboxItem>();
        }
        private ServiceModel currentServiceModel;
        public ServiceModel CurrentServiceModel
        {
            get { return currentServiceModel; }
            set { currentServiceModel = value; }
        }
        private List<ComboboxItem> cbDTOPropsList;

        public List<ComboboxItem> CbDTOPropsList
        {
            get { return cbDTOPropsList; }
            set { cbDTOPropsList = value; }
        }


    }
}
