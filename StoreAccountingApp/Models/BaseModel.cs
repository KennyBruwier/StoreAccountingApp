using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreAccountingApp.Models
{
    public class BaseModel
    {
        public object[] PrimaryKey
        {
            get
            {
                return (from property in this.GetType().GetProperties()
                        where Attribute.IsDefined(property, typeof(KeyAttribute))
                        orderby ((ColumnAttribute)property.GetCustomAttributes(false).Single(
                            attr => attr is ColumnAttribute)).Order ascending
                        select property.GetValue(this)).ToArray();
            }
        }
    }
}
