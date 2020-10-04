using System.Collections.Generic;

namespace ShinhanAPI
{
    public class IndiAccount
    {
        public string USER_ID { get; set; }
        public string USER_PW { get; set; }
        public string USER_CERT_PW { get; set; }
        public List<Account> ACCOUNTS { get; set; }

        #region GET

        public string GetUserID(string Key)
        {
            try
            {
                return Tool.Encryption.AES256Decrypt(USER_ID, Key);
            }
            catch { return null; }
        }

        public string GetUserPassword(string Key)
        {
            try
            {
                return Tool.Encryption.AES256Decrypt(USER_PW, Key);
            }
            catch { return null; }
        }

        public string GetUserCertPassword(string Key)
        {
            try
            {
                return Tool.Encryption.AES256Decrypt(USER_CERT_PW, Key);
            }
            catch { return null; }
        }

        #endregion GET

        #region SET

        public void SetUserID(string Account, string Key)
        {
            USER_ID = Tool.Encryption.AES256Encrypt(Account, Key);
        }

        public void SetUserPassword(string Password, string Key)
        {
            USER_PW = Tool.Encryption.AES256Encrypt(Password, Key);
        }

        public void SetUserCertPassword(string CertPassword, string Key)
        {
            USER_CERT_PW = Tool.Encryption.AES256Encrypt(CertPassword, Key);
        }

        #endregion SET

        public bool IsAvailability()
        {
            return !(string.IsNullOrWhiteSpace(USER_ID)
                || string.IsNullOrWhiteSpace(USER_PW)
                || string.IsNullOrWhiteSpace(USER_CERT_PW));
        }
    }

    public class Account
    {
        private string ACCOUNT { get; set; }
        private string ACCOUNT_PW { get; set; }

        #region GET

        public string GetAccount(string Key)
        {
            try
            {
                return Tool.Encryption.AES256Decrypt(ACCOUNT, Key);
            }
            catch { return null; }
        }

        public string GetAccountPassword(string Key)
        {
            try
            {
                return Tool.Encryption.AES256Decrypt(ACCOUNT_PW, Key);
            }
            catch { return null; }
        }

        #endregion GET

        #region SET

        public void SetAccount(string Account, string Key)
        {
            ACCOUNT = Tool.Encryption.AES256Encrypt(Account, Key);
        }

        public void SetAccountPassword(string Password, string Key)
        {
            ACCOUNT_PW = Tool.Encryption.AES256Encrypt(Password, Key);
        }

        #endregion SET
    }
}