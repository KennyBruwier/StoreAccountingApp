using StoreAccountingApp.GeneralClasses;
using StoreAccountingApp.ViewModels.BaseModels;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreAccountingApp.ViewModels
{
    public class DBViewModelBase : ViewModelBase
    {
        protected string tableName;

        public DBViewModelBase()
        {
            this.tableName = GetClassName();
        }
        protected string GetClassName()
        {
            string typeName = this.GetType().Name;
            if (typeName.Contains("DB")) typeName = typeName.Replace("DB", "");
            if (typeName.Contains("ViewModel")) typeName = typeName.Replace("ViewModel", "");
            return typeName;
        }
        private string message;
        public string Message
        {
            get { return message; }
            set { message = value; OnPropertyChanged("Message"); }
        }

        #region ValidationError
        public string CreateValidationErrorMsg(DbEntityValidationException e)
        {
            string message = "";
            foreach (var validationErrors in e.EntityValidationErrors)
            {
                foreach (var validationError in validationErrors.ValidationErrors)
                {
                    Trace.TraceInformation("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage);
                    message += String.Format("Property: {0} Error: {1}\n", validationError.PropertyName, validationError.ErrorMessage);
                }
            }
            return message;
        }
        protected void CatchOperation(Func<bool> operationToCatch)
        {
            TryCatchResult operationResult = TryCatch(operationToCatch, tableName);
            if (operationResult.OperationMessage != null)
                Message = operationResult.OperationMessage;
            else
                Message = operationResult.ErrMessage;
        }
        protected TryCatchResult TryCatch(Func<bool> functionToCatch, string recordName)
        {
            string functionName = functionToCatch.Method.Name.ToLower();
            string[] operationNames = new string[2]
            {
                functionName,
                functionName.EndsWith("e") ? functionName + 'd': functionName + "ed"
            };

            TryCatchResult tryCatchResult = new TryCatchResult();
            try
            {
                if (tryCatchResult.Result = functionToCatch())
                    tryCatchResult.OperationMessage = string.Format("{0} {1}", recordName, operationNames[1]);
                else
                    tryCatchResult.OperationMessage = string.Format("{0} operation failed", operationNames[0]);
            }
            catch (DbEntityValidationException ex)
            {
                tryCatchResult.ErrMessage = CreateValidationErrorMsg(ex);
            }
            catch (Exception ex)
            {
                if (ex is ApplicationException)
                {
                    tryCatchResult.ErrMessage = String.Format("Error {0}: ", ex.Message);
                    if (ex.InnerException != null)
                        tryCatchResult.ErrMessage += String.Format("\nInner exception: {0}", ex.InnerException.Message);
                }
                else
                    tryCatchResult.ErrMessage = ex.Message;
            }
            return tryCatchResult;
        }
        #endregion
    }
}
