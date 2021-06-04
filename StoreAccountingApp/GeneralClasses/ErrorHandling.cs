using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreAccountingApp.GeneralClasses
{
    public abstract class ErrorHandling : NotifyPropertyChanged
    {
        private CheckValidation currentValidation;
        public CheckValidation CurrentValidation
        {
            get { return currentValidation; }
            set { currentValidation = value; OnPropertyChanged(nameof(CurrentValidation)); }
        }
        protected string tableName;
        private string message;
        public string Message
        {
            get { return message; }
            set { message = value; OnPropertyChanged("Message"); }
        }
        public ErrorHandling()
        {
            this.tableName = GetClassName();
        }
        public virtual void LoadValidation()
        {

        }        
        protected string GetClassName()
        {
            string typeName = this.GetType().Name;
            if (typeName.Contains("DB")) typeName = typeName.Replace("DB", "");
            if (typeName.Contains("ViewModel")) typeName = typeName.Replace("ViewModel", "");
            if (typeName.Contains("Service")) typeName = typeName.Replace("Service", "");
            return typeName;
        }
        #region ErrorCatching
        protected void CatchOperation(Func<bool> operationToCatch)
        {
            TryCatchResult operationResult = TryCatch(operationToCatch, tableName);
            if (operationResult.OperationMessage != null)
                Message = operationResult.OperationMessage;
            else
                Message = operationResult.ErrMessage;
        }
        private bool ValidationSuccess(string functionName)
        {
            bool validationSuccess = true;
            switch (functionName.ToLower())
            {
                case "save":
                    if (currentValidation.NullFields) validationSuccess = false;
                    break;
                case "delete": 
                    if (currentValidation.EmptyPK) validationSuccess = false;
                    break;
                case "update": 
                    if (currentValidation.EmptyPK || currentValidation.NullFields) validationSuccess = false;
                    break;
            }
            return validationSuccess;
        }
        private TryCatchResult TryCatch(Func<bool> functionToCatch, string recordName)
        {
            string functionName = functionToCatch.Method.Name.ToLower();
            string[] operationNames = new string[2]
            {
                functionName,
                functionName.EndsWith("e") ? functionName + 'd': functionName + "ed"
            };
            TryCatchResult tryCatchResult = new TryCatchResult();
            LoadValidation();
            currentValidation.ValidationErrors = String.Empty;
            if (!(tryCatchResult.Result = ValidationSuccess(functionName)))
            {
                tryCatchResult.ErrMessage = currentValidation.ValidationErrors;
            }
            else
            {
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
            }
            return tryCatchResult;
        }
        private string CreateValidationErrorMsg(DbEntityValidationException e)
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
        #endregion

    }
}
