using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace StoreAccountingApp.DTO.Abstracts
{
    public abstract class RecordTimeStampsDTO : BaseDTO
    {
        private DateTime createdAt = DateTime.Now;
        public DateTime CreatedAt
        {
            get { return createdAt; }
            set { createdAt = value; OnPropertyChanged("CreatedAt"); }
        }
        private DateTime updateAt = DateTime.Now;
        public DateTime UpdateAt
        {
            get { return updateAt; }
            set { updateAt = value; OnPropertyChanged("UpdateAt"); }
        }
        private DateTime? closedAt;
        public DateTime? ClosedAt
        {
            get { return closedAt; }
            set { closedAt = value; OnPropertyChanged("ClosedAt"); }
        }
    }
}
