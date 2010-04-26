using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Configuration.Provider;
using System.Security.Cryptography;
using System.Text;
using System.Web.Configuration;
using System.Collections;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using System.Collections.Specialized;
using System.Globalization;
using CAESDO.Recruitment.ProvidersUtil;

namespace CAESDO.Recruitment.Providers
{
    /// <summary>
    /// Summary description for FRACMembershipProvider
    /// </summary>
    public class FRACMembershipProvider : MembershipProvider
    {
        private const int SALT_SIZE_IN_BYTES = 16;
        private const int PASSWORD_SIZE = 14;

        //Private variables
        private string _ApplicationName;
        private string _ConnectionString;
        private string _Description;
        private string _Name;
        private bool _EnablePasswordRetrieval;
        private bool _EnablePasswordReset;
        private bool _RequiresQuestionAndAnswer;
        private bool _RequiresUniqueEmail;
        private string _HashAlgorithmType;
        private int _MaxInvalidPasswordAttempts;
        private int _PasswordAttemptWindow;
        private int _MinRequiredPasswordLength;
        private int _MinRequiredNonalphanumericCharacters;
        private string _PasswordStrengthRegularExpression;
        private DateTime _ApplicationIDCacheDate;
        private MembershipPasswordFormat _PasswordFormat;
        private DataOps _dops;

#region Properties
        //Public Properties
        public override string ApplicationName
        {
            get
            {
                return _ApplicationName;
            }
            set
            {
                _ApplicationName = value;
            }
        }

        public override string Description
        {
            get
            {
                return _Description;
            }
        }

        public string ConnectionString
        {
            get
            {
                return _ConnectionString;
            }
        }

        public override string Name
        {
            get { return _Name; }
        }

        public override bool EnablePasswordReset
        {
            get { return _EnablePasswordReset; }
        }

        public override bool EnablePasswordRetrieval
        {
            get { return _EnablePasswordRetrieval; }
        }

        public override int MaxInvalidPasswordAttempts
        {
            get { return _MaxInvalidPasswordAttempts; }
        }

        public override int MinRequiredNonAlphanumericCharacters
        {
            get { return _MinRequiredNonalphanumericCharacters; }
        }

        public override int MinRequiredPasswordLength
        {
            get { return _MinRequiredPasswordLength; }
        }

        public override int PasswordAttemptWindow
        {
            get { return _PasswordAttemptWindow; }
        }

        public override MembershipPasswordFormat PasswordFormat
        {
            get { return _PasswordFormat; }
        }

        public override string PasswordStrengthRegularExpression
        {
            get { return _PasswordStrengthRegularExpression; }
        }

        public override bool RequiresQuestionAndAnswer
        {
            get { return _RequiresQuestionAndAnswer; }
        }

        public override bool RequiresUniqueEmail
        {
            get { return _RequiresUniqueEmail; }
        }

#endregion

#region Methods
        
        public override void Initialize(string name, System.Collections.Specialized.NameValueCollection config)
        {
            if (config == null)
            {
                throw new ArgumentNullException("config");
            }
            if (string.IsNullOrEmpty(name))
            {
                name = "FRACMembershipProvider";
            }
            if (string.IsNullOrEmpty(config["description"]))
            {
                config.Remove("description");
                config.Add("description", "FRAC Membership Provider");
            }

            //Initialize the base class
            base.Initialize(name, config);

            // Initialize default values
            _ApplicationName = "DefaultApp";
            _ConnectionString = @"Data Source=ROBERTO;Initial Catalog=Kong;Integrated Security=True";
            _Description = "DefaultApp";
            _MinRequiredPasswordLength = SecUtility.GetIntValue(config, "minRequiredPasswordLength", 7, false, 128);
            _MinRequiredNonalphanumericCharacters = SecUtility.GetIntValue(config, "minRequiredNonalphanumericCharacters", 1, true, 128);
            _EnablePasswordRetrieval = SecUtility.GetBooleanValue(config, "enablePasswordRetrieval", false);
            _EnablePasswordReset = SecUtility.GetBooleanValue(config, "enablePasswordReset", true);
            
            _Name = name;

            // Now go through the properties and initialize custom values
            foreach (string key in config.Keys)
            {
                switch (key.ToLower())
                {
                    case "applicationname":
                        _ApplicationName = config[key];
                        break;
                    case "connectionstring":
                        _ConnectionString = config[key];
                        break;
                    case "description":
                        _Description = config[key];
                        break;
                    case "requiresquestionandanswer":
                        _RequiresQuestionAndAnswer = SecUtility.GetBooleanValue(config, key, true);
                        break;
                    case "requiresuniqueemail":
                        _RequiresUniqueEmail = SecUtility.GetBooleanValue(config, key, true);
                        break;
                }
            }

            string strTemp = string.IsNullOrEmpty(config["passwordFormat"]) ? "Hashed" : config["passwordFormat"];
            
            switch (strTemp)
            {
                case "Clear":
                    _PasswordFormat = MembershipPasswordFormat.Clear;
                    break;
                case "Encrypted":
                    _PasswordFormat = MembershipPasswordFormat.Encrypted;
                    break;
                case "Hashed":
                    _PasswordFormat = MembershipPasswordFormat.Hashed;
                    break;
                default:
                    throw new ProviderException("Bad password format");
            }
            
            _HashAlgorithmType = config["hashAlgorithmType"];
            if (String.IsNullOrEmpty(_HashAlgorithmType))
            {
                _HashAlgorithmType = "SHA1";
            }

            if (_PasswordStrengthRegularExpression != null)
            {
                _PasswordStrengthRegularExpression = _PasswordStrengthRegularExpression.Trim();
                if (_PasswordStrengthRegularExpression.Length != 0)
                {
                    try
                    {
                        Regex regex = new Regex(_PasswordStrengthRegularExpression);
                    }
                    catch (ArgumentException e)
                    {
                        throw new ProviderException(e.Message, e);
                    }
                }
            }
            else
            {
                _PasswordStrengthRegularExpression = string.Empty;
            }

            if (_PasswordFormat == MembershipPasswordFormat.Hashed && _EnablePasswordRetrieval)
                throw new ProviderException("Provider cannot retrieve hashed password");

            //Setup the dataops -- We probably want to change the 'connection string' to a connection string key 
            //pointing to a web.config connection strings section
            _dops = new DataOps();
            _dops.ConnectionString = _ConnectionString;
        }

        /// <summary>
        /// Creates a user from the given parameters and settings in the web.config (under the membership section)
        /// </summary>
        /// <param name="username">Kerberos LoginID of the user who created the account (or string.empty)</param>
        /// <param name="password">Password -- complexity determined by web.config settings</param>
        /// <param name="email">Email entered by user</param>
        /// <param name="passwordQuestion"></param>
        /// <param name="passwordAnswer"></param>
        /// <param name="isApproved"></param>
        /// <param name="providerUserKey">Not used since username is always unique, we can look up with UserID when necessary</param>
        /// <param name="status"></param>
        /// <returns>A representation of the current user's membership information</returns>
        public override MembershipUser CreateUser(string username, string password, string email, string passwordQuestion, string passwordAnswer, bool isApproved, object providerUserKey, out MembershipCreateStatus status)
        {
            //if the username is SELFCREATED, set it to empty so that we know it was not created on a Kerberos user's behalf
            if (username == "SELFCREATED")
                username = string.Empty;

            //Make sure the password is non-null or empty (excluding white space)
            if (!SecUtility.ValidateParameter(ref password, true, true, false, 0))
            {
                //If the password is invalid, return the correct status
                status = MembershipCreateStatus.InvalidPassword;
                return null;
            }

            //Check that the password meets all requirements laid out in the web.config
            if (password.Length < MinRequiredPasswordLength)
            {
                status = MembershipCreateStatus.InvalidPassword;
                return null;
            }

            int count = 0;

            for (int i = 0; i < password.Length; i++)
            {
                if (!char.IsLetterOrDigit(password, i))
                {
                    count++;
                }
            }

            if (count < MinRequiredNonAlphanumericCharacters)
            {
                status = MembershipCreateStatus.InvalidPassword;
                return null;
            }

            if (PasswordStrengthRegularExpression.Length > 0)
            {
                if (!Regex.IsMatch(password, PasswordStrengthRegularExpression))
                {
                    status = MembershipCreateStatus.InvalidPassword;
                    return null;
                }
            }

            //Validate with email as the username
            ValidatePasswordEventArgs e = new ValidatePasswordEventArgs(email, password, true);
            OnValidatingPassword(e);

            if (e.Cancel)
            {
                status = MembershipCreateStatus.InvalidPassword;
                return null;
            }

            //Generate a salt of length SALT_SIZE_IN_BYTES
            string salt = GenerateSalt();

            //Encodes the password using the method defined in the web.config membership section (clear, hashed, or encrypted)
            //If method = hashed, then the algortihm defined by the HashAlgorithmType key is used
            string encodedPassword = EncodePassword(password, (int)_PasswordFormat, salt);

            //Make sure the password isn't too long (if it is, it will not fit in the database
            if (encodedPassword.Length > 128)
            {
                status = MembershipCreateStatus.InvalidPassword;
                return null;
            }

            //Check the email, question, answer (only the last two if they are required in the web.config)
            //if (!SecUtility.ValidateParameter(ref username, true, true, true, 255))
            //{
            //    status = MembershipCreateStatus.InvalidUserName;
            //    return null;
            //}

            if (!SecUtility.ValidateParameter(ref email,
                                               RequiresUniqueEmail,
                                               RequiresUniqueEmail,
                                               false,
                                               128))
            {
                status = MembershipCreateStatus.InvalidEmail;
                return null;
            }

            if (!SecUtility.ValidateParameter(ref passwordQuestion,
                                               RequiresQuestionAndAnswer,
                                               true,
                                               false,
                                               255))
            {
                status = MembershipCreateStatus.InvalidQuestion;
                return null;
            }

            if (!SecUtility.ValidateParameter(ref passwordAnswer,
                                               RequiresQuestionAndAnswer,
                                               true,
                                               false,
                                               128))
            {
                status = MembershipCreateStatus.InvalidAnswer;
                return null;
            }

            _dops.ResetDops();
            _dops.Sproc = "usp_InsertAccount";

            _dops.SetParameter("@LoginID", username, "IN"); //KerberosID of user that created this account (null if created by applicant)
            _dops.SetParameter("@Email", email, "IN");
            _dops.SetParameter("@Password", encodedPassword, "IN");
            _dops.SetParameter("@PasswordFormat", (int)PasswordFormat, "IN");
            _dops.SetParameter("@PasswordSalt", salt, "IN");
            _dops.SetParameter("@PasswordQuestion", passwordQuestion, "IN");
            _dops.SetParameter("@PasswordAnswer", passwordAnswer, "IN");
            _dops.SetParameter("@CreateStatus", string.Empty, "OUT");
            _dops.SetParameter("RETURN_VALUE", string.Empty, "RETURN");

            try
            {
                _dops.Execute_Sql();
            }
            catch (SqlException)
            {
                status = MembershipCreateStatus.ProviderError;
                return null;
            }

            //If the return value is not 0 (success), inspect the error and return it to the user
            if ((int)_dops.GetOutputVariable("RETURN_VALUE") != 0)
            {
                switch ((string)_dops.GetOutputVariable("@CreateStatus"))
                {
                    case "InvalidLogin":
                        status = MembershipCreateStatus.DuplicateUserName;
                        break;
                    case "InvalidEmail":
                        status = MembershipCreateStatus.DuplicateEmail;
                        break;
                    default:
                        status = MembershipCreateStatus.ProviderError;
                        break;
                }

                return null;
            }
            else
            {
                //No error, so go ahead and return success
                DateTime dt = DateTime.Now;

                status = MembershipCreateStatus.Success;
                return new MembershipUser(this.Name, 
                                            username, 
                                            null, 
                                            email, 
                                            passwordQuestion, 
                                            string.Empty, 
                                            isApproved, 
                                            false, 
                                            dt, 
                                            dt, 
                                            dt, 
                                            dt, 
                                            DateTime.MinValue);
            }
        }

        /// <summary>
        /// Validates a user by making sure their username and password are correct in the system.  Regardless of the web.config settings,
        /// the password information (salt, format) are taken from the database and then compared
        /// </summary>
        /// <param name="username">Unique username</param>
        /// <param name="password">Password to be compared against database.</param>
        /// <returns>true if user entered valid information</returns>
        public override bool ValidateUser(string username, string password)
        {
            //Validate the username and password being entered
            if (!SecUtility.ValidateParameter(ref username,
                                               true,
                                               true,
                                               false,
                                               255))
            {
                return false;
            }

            if (!SecUtility.ValidateParameter(ref password,
                                               true,
                                               true,
                                               false,
                                               128))
            {
                return false;
            }

            int status;

            return CheckPassword(username, password, out status);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="username">Email Address of the account</param>
        /// <param name="userIsOnline"></param>
        /// <returns></returns>
        public override MembershipUser GetUser(string username, bool userIsOnline)
        {
            SecUtility.CheckParameter(ref username, true, false, true, 255, "username");

            ArrayList fields = new ArrayList();
            fields.Add("Email");
            fields.Add("PasswordQuestion");
            fields.Add("isActive");

            ArrayList results = new ArrayList();

            _dops.ResetDops();
            _dops.Sproc = "usp_getUserInfoByEmail";

            _dops.SetParameter("@Email", username, "IN");

            try
            {
                results = _dops.get_arrayList(fields);
            }
            catch (SqlException ex)
            {
                throw new ProviderException(ex.Message, ex);
            }

            //Check if a user was found
            if (results.Count == 0)
            {
                return null;
            }

            ArrayList resultsFirstRow = (ArrayList)results[0];
            DateTime dt = DateTime.Now;

            return new MembershipUser(this.Name,
                                        (string)resultsFirstRow[0], //Email
                                        (object)resultsFirstRow[0], //Email
                                        (string)resultsFirstRow[0], //Email
                                        (string)resultsFirstRow[1], //PasswordQuestion
                                        string.Empty,
                                        (bool)resultsFirstRow[2],   //isActive
                                        false,
                                        dt,
                                        dt,
                                        dt,
                                        dt,
                                        DateTime.MinValue);
        }

        /// <summary>
        /// Pulls the password out of the database if EnablePasswordRetrieval is set to true (not yet implemented). 
        /// </summary>
        /// <param name="username">Username</param>
        /// <param name="answer">Answer to the users question</param>
        /// <returns>Password as a string</returns>
        /// <exception cref="">NotSupportedException</exception>
        public override string GetPassword(string username, string answer)
        {
            if (!EnablePasswordRetrieval)
            {
                throw new NotSupportedException();
            }

            //SecUtility.CheckParameter(ref username, true, true, true, 256, "username");

            return string.Empty;
            //throw new Exception("The method or operation is not implemented.");
        }

        /// <summary>
        /// Resets a user's password if they have the correct answer to the passwordQuestion
        /// </summary>
        /// <param name="username">Username</param>
        /// <param name="answer">Answer to the users question</param>
        /// <returns>New password as a string</returns>
        public override string ResetPassword(string username, string passwordAnswer)
        {
            if (!EnablePasswordReset)
            {
                throw new NotSupportedException();
            }

            SecUtility.CheckParameter(ref username, true, true, true, 256, "username");

            string salt;
            int passwordFormat;
            int status;
            bool isApproved;
            
            GetPasswordWithFormat(username, out passwordFormat, out status, out salt, out isApproved);

            //Check to see if there were any problems (may expand later to involve more specific errors)
            if (status != 0) //problem occurred
            {
                throw new ProviderException();
            }

            //We will do encoding of passwords later, will require changes to createUser, ValidateUser, and others
            //string encodedPasswordAnswer;

            //if (!string.IsNullOrEmpty(passwordAnswer))
            //    encodedPasswordAnswer = EncodePassword(passwordAnswer.ToLower(), passwordFormat, salt);
            //else
            //    encodedPasswordAnswer = passwordAnswer;

            if (passwordAnswer != null)
            {
                passwordAnswer = passwordAnswer.Trim();
            }
            
            SecUtility.CheckParameter(ref passwordAnswer, RequiresQuestionAndAnswer, RequiresQuestionAndAnswer, false, 128, "passwordAnswer");
            
            string newPassword = GeneratePassword();

            ValidatePasswordEventArgs eventArgs = new ValidatePasswordEventArgs(username, newPassword, false);
            OnValidatingPassword(eventArgs);

            if (eventArgs.Cancel)
            {
                if (eventArgs.FailureInformation != null)
                {
                    throw eventArgs.FailureInformation;
                }
                else
                {
                    throw new ProviderException();
                }
            }

            _dops.ResetDops();
            _dops.Sproc = "usp_ResetPassword";

            _dops.SetParameter("@Email", username, "IN");
            _dops.SetParameter("@NewPassword", EncodePassword(newPassword, (int)passwordFormat, salt), "IN");
            _dops.SetParameter("@PasswordFormat", passwordFormat, "IN");
            _dops.SetParameter("@PasswordSalt", salt, "IN");
            _dops.SetParameter("@PasswordAnswer", passwordAnswer, "IN");
            _dops.SetParameter("RETURN_VALUE", string.Empty, "RETURN");

            _dops.Execute_Sql();

            int success = 1;

            try
            {
                success = (int)_dops.GetOutputVariable("RETURN_VALUE");
            }
            catch (SqlException ex)
            {
                throw new ProviderException(ex.Message, ex);
            }

            //Check to see if there is a problem
            if (success != 0)
            {
                throw new MembershipPasswordException(); //If there is a problem, throw the exception
            }

            return newPassword;
        }

        /// <summary>
        /// Changes a users password after checking that they know their old password
        /// </summary>
        /// <param name="username">Username</param>
        /// <param name="oldPassword">The old password in the system</param>
        /// <param name="newPassword">The desired new password</param>
        /// <returns>True if the change succeeds, otherwise false</returns>
        public override bool ChangePassword(string username, string oldPassword, string newPassword)
        {
            SecUtility.CheckParameter(ref username, true, true, true, 256, "username");
            SecUtility.CheckParameter(ref oldPassword, true, true, false, 128, "oldPassword");
            SecUtility.CheckParameter(ref newPassword, true, true, false, 128, "newPassword");

            int status;

            //Return false if the old password is not correct
            if (!CheckPassword(username, oldPassword, out status))
            {
                return false;
            }

            //Make sure the new password meets password length requirements
            if (newPassword.Length < MinRequiredPasswordLength)
            {
                throw new ArgumentException();
            }

            int count = 0;

            //Check the number of non-alpha numeric chars
            for (int i = 0; i < newPassword.Length; i++)
            {
                if (!char.IsLetterOrDigit(newPassword, i))
                {
                    count++;
                }
            }

            if (count < MinRequiredNonAlphanumericCharacters)
            {
                throw new ArgumentException();
            }

            //If we are using a regular expression, make sure the password passes
            if (PasswordStrengthRegularExpression.Length > 0)
            {
                if (!Regex.IsMatch(newPassword, PasswordStrengthRegularExpression))
                {
                    throw new ArgumentException();
                }
            }

            string salt = GenerateSalt();

            //Encode password using new salt and password format settings
            string pass = EncodePassword(newPassword, (int)_PasswordFormat, salt);

            if (pass.Length > 128)
            {
                throw new ArgumentException();
            }

            //Raise the OnValidatingPassword() function so someone can hook into it if they want
            ValidatePasswordEventArgs e = new ValidatePasswordEventArgs(username, newPassword, false);
            OnValidatingPassword(e);

            if (e.Cancel)
            {
                if (e.FailureInformation != null)
                {
                    throw e.FailureInformation;
                }
                else
                {
                    throw new ArgumentException();
                }
            }

            _dops.ResetDops();
            _dops.Sproc = "usp_ChangePassword";

            _dops.SetParameter("@Email", username, "IN");
            _dops.SetParameter("@NewPassword", pass, "IN");
            _dops.SetParameter("@PasswordFormat", (int)_PasswordFormat, "IN");
            _dops.SetParameter("@PasswordSalt", salt, "IN");
            _dops.SetParameter("RETURN_VALUE", string.Empty, "RETURN");

            try
            {
                _dops.Execute_Sql();
            }
            catch (SqlException ex)
            {
                throw new ProviderException(ex.Message, ex);
            }

            int result = (int)_dops.GetOutputVariable("RETURN_VALUE");

            //Check to see if there is a problem
            if (result != 0)
            {
                throw new MembershipPasswordException(); //If there is a problem, throw the exception
            }

            return true;
        }

    #region Non-Implemented Methods

        public override void UpdateUser(MembershipUser user)
        {
            throw new Exception("The method or operation is not implemented.");
        }
        
        public override string GetUserNameByEmail(string email)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override bool ChangePasswordQuestionAndAnswer(string username, string password, string newPasswordQuestion, string newPasswordAnswer)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        protected override byte[] DecryptPassword(byte[] encodedPassword)
        {
            return base.DecryptPassword(encodedPassword);
        }

        public override bool DeleteUser(string username, bool deleteAllRelatedData)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        protected override byte[] EncryptPassword(byte[] password)
        {
            return base.EncryptPassword(password);
        }

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        public override MembershipUserCollection FindUsersByEmail(string emailToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override MembershipUserCollection FindUsersByName(string usernameToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override MembershipUserCollection GetAllUsers(int pageIndex, int pageSize, out int totalRecords)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override int GetNumberOfUsersOnline()
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override MembershipUser GetUser(object providerUserKey, bool userIsOnline)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        protected override void OnValidatingPassword(ValidatePasswordEventArgs e)
        {
            base.OnValidatingPassword(e);
        }

        public override string ToString()
        {
            return base.ToString();
        }

        public override bool UnlockUser(string userName)
        {
            throw new Exception("The method or operation is not implemented.");
        }

    #endregion

        public virtual string GeneratePassword()
        {
            return Membership.GeneratePassword(
                      MinRequiredPasswordLength < PASSWORD_SIZE ? PASSWORD_SIZE : MinRequiredPasswordLength,
                      MinRequiredNonAlphanumericCharacters);
        }

    #region Private Methods

        /// <summary>
        /// Checks the given password against the stored password for a user
        /// </summary>
        /// <param name="username">Username</param>
        /// <param name="password">Password</param>
        /// <param name="status">Outputs the status of the user's account</param>
        /// <returns>True if the password is correct for the username</returns>
        private bool CheckPassword(string username, string password, out int status)
        {
            string salt;
            bool userIsApproved;
            int passwordFormat;
            string pass = GetPasswordWithFormat(username, out passwordFormat, out status, out salt, out userIsApproved);
            string pass2 = EncodePassword(password, passwordFormat, salt);

            if (status != 0) //get password failed
                return false;

            return (pass == pass2);
        }

        /// <summary>
        /// Pulls out the password information for user identified by 'username', along with the password format, salt,
        /// and user approval information
        /// </summary>
        /// <param name="username">The 'username', aka EmailAddress, of the user</param>
        /// <param name="passwordFormat">Password format as an int</param>
        /// <param name="status">Status</param>
        /// <param name="passwordSalt">Salt of the password as a string</param>
        /// <param name="userIsApproved">true if the user is approved</param>
        /// <returns>Password of the user from the database</returns>
        private string GetPasswordWithFormat(string username, out int passwordFormat, out int status, out string passwordSalt, out bool userIsApproved)
        {
            ArrayList fields = new ArrayList();
            fields.Add("PasswordFormat");
            fields.Add("Password");
            fields.Add("PasswordSalt");
            fields.Add("isActive");

            _dops.ResetDops();
            _dops.Sproc = "usp_getPasswordByUser";

            //Using Email as the username
            _dops.SetParameter("@Email", username, "IN");

            ArrayList results = new ArrayList();

            //default values
            passwordFormat = 0;
            passwordSalt = string.Empty;
            userIsApproved = false;
            status = 1;

            try
            {
                results = _dops.get_arrayList(fields);
            }
            catch (SqlException)
            {
                return string.Empty;
            }

            //If there is no password returned, then the user is not valid
            if (results.Count == 0)
            {
                status = 1; //user not found
                return string.Empty;
            }

            ArrayList resultsFirstRow = (ArrayList)results[0];

            passwordFormat = (int)resultsFirstRow[0];
            passwordSalt = (string)resultsFirstRow[2];
            userIsApproved = (bool)resultsFirstRow[3];
            
            status = 0;

            return (string)resultsFirstRow[1];
        }

        private string GenerateSalt()
        {
            byte[] buf = new byte[SALT_SIZE_IN_BYTES];
            (new RNGCryptoServiceProvider()).GetBytes(buf);
            return Convert.ToBase64String(buf);
        }

        private string EncodePassword(string pass, int passwordFormat, string salt)
        {
            if (passwordFormat == 0) // MembershipPasswordFormat.Clear
                return pass;

            byte[] bIn = Encoding.Unicode.GetBytes(pass);
            byte[] bSalt = Convert.FromBase64String(salt);
            byte[] bAll = new byte[bSalt.Length + bIn.Length];
            byte[] bRet = null;

            Buffer.BlockCopy(bSalt, 0, bAll, 0, bSalt.Length);
            Buffer.BlockCopy(bIn, 0, bAll, bSalt.Length, bIn.Length);
            if (passwordFormat == 1)
            { // MembershipPasswordFormat.Hashed
                HashAlgorithm s = HashAlgorithm.Create(_HashAlgorithmType);

                // If the hash algorithm is null (and came from config), throw a config exception
                if (s == null)
                {
                    throw new ProviderException("Could not create a hash algorithm");
                }
                bRet = s.ComputeHash(bAll);
            }
            else
            {
                bRet = EncryptPassword(bAll);
            }

            return Convert.ToBase64String(bRet);
        }

        private string UnEncodePassword(string pass, int passwordFormat)
        {
            switch (passwordFormat)
            {
                case 0: // MembershipPasswordFormat.Clear:
                    return pass;
                case 1: // MembershipPasswordFormat.Hashed:
                    throw new ProviderException("Provider can not decode hashed password");
                default:
                    byte[] bIn = Convert.FromBase64String(pass);
                    byte[] bRet = DecryptPassword(bIn);
                    if (bRet == null)
                        return null;
                    return Encoding.Unicode.GetString(bRet, SALT_SIZE_IN_BYTES, bRet.Length - SALT_SIZE_IN_BYTES);
            }
        } 
    #endregion

#endregion

    }
}