using StoreAccountingApp.DBModels.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreAccountingApp.Models.Interfaces
{
    public interface IService<DBEntity, DTOEntity> 
        where DBEntity : Entity
        where DTOEntity : class
    {
        //DBEntity Add(DBEntity dBEntity);
        bool Add(DTOEntity dTOEntity);
        //bool Add(DBEntity dBEntity);
    }
}
