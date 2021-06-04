using StoreAccountingApp.DTO;
using StoreAccountingApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreAccountingApp.Services.Abstracts
{
    public interface IBaseService<DTOModel, DBModel> 
        where DTOModel : BaseDTO
        where DBModel : BaseModel
    {
        List<DTOModel> GetAll();
        bool Add(DBModel dTOModelToAdd);
        DTOModel Search(object[] idToSearch);
        bool Update(DBModel dTOModelToUpdate);
        bool Delete(object[] idToDelete);
    }
}
