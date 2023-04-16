using krediku_be.Models;

namespace krediku_be.Utilities
{
    public static class clsHelper
    {
        public static bool isDataTransactionValid(Transaction input, out string validationMsg)
        {
            validationMsg = "";
            bool isValid = true;
            try
            {
                if (input == null)
                    throw new Exception("Parameter Input Cannot be null");

                if (string.IsNullOrEmpty(input.AgreementNumber))
                    throw new Exception("Agreement Number cannot be empty");

                if(!IsDigitsOnly(input.BranchId))
                    throw new Exception("Branch ID must be numeric");
            }
            catch(Exception ex)
            { 
                isValid = false;
                validationMsg = ex.Message;
            }
            return isValid;
        }

        private static bool IsDigitsOnly(string str)
        {
            foreach (char c in str)
            {
                if (c < '0' || c > '9')
                    return false;
            }

            return true;
        }
    }
}
