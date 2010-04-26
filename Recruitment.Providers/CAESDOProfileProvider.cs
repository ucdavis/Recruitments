using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Web.Profile;
using System.Configuration.Provider;
using System.Data.SqlClient;
using CAESDO.Recruitment.ProvidersUtil;

namespace CAESDO.Recruitment.Providers
{

    /// <summary>
    /// Summary description for CAESDOProfileProvider
    /// </summary>
    public class CAESDOProfileProvider : ProfileProvider
    {
        // private vars
        private string _applicationName;
        private string _description;
        private string _name;
        private string _connectionString;
        private string _connectionStringKey;

        private DataOps _dops;

        //properties
        public override string Name
        {
            get
            {
                return _name;
            }
        }
        public override string Description
        {
            get
            {
                return _description;
            }
        }
        public override string ApplicationName
        {
            get
            {
                return _applicationName;
            }
            set
            {
                if (value.Length > 256)
                    throw new ProviderException(SR.GetString(SR.Provider_application_name_too_long));

                _applicationName = value;
            }
        }

        #region Methods

        public override void Initialize(string name, System.Collections.Specialized.NameValueCollection config)
        {
            if (config == null)
            {
                throw new ArgumentNullException("config");
            }
            if (string.IsNullOrEmpty(name))
            {
                name = "CAESDO Profile Provider";
            }
            if (string.IsNullOrEmpty(config["description"]))
            {
                config.Remove("description");
                config.Add("description", "CAESDO Profile Provider");
            }

            base.Initialize(name, config);

            // Initialize default values
            _applicationName = "DefaultApp";
            _connectionStringKey = @"ASPProvider";
            _description = "DefaultApp";
            _name = name;

            // Now go through the properties and initialize custom values
            foreach (string key in config.Keys)
            {
                switch (key.ToLower())
                {
                    case "applicationname":
                        ApplicationName = config[key];
                        break;
                    case "connectionstring":
                        _connectionStringKey = config[key];
                        break;
                    case "description":
                        _description = config[key];
                        break;
                }
            }

            //Setup the dataops -- We probably want to change the 'connection string' to a connection string key 
            //pointing to a web.config connection strings section
            //_connectionString = System.Web.Configuration.WebConfigurationManager.ConnectionStrings[_connectionStringKey].ToString();
            _dops = new DataOps();
            _dops.ConnectionString = _connectionStringKey;
        }

        public override SettingsPropertyValueCollection GetPropertyValues(SettingsContext context, SettingsPropertyCollection properties)
        {
            SettingsPropertyValueCollection svc = new SettingsPropertyValueCollection();

            //If there are no properties in this collection, return an empty collection
            if (properties.Count < 1)
                return svc;

            //grab the username of the caller
            string username = (string)context["UserName"];

            foreach (SettingsProperty prop in properties)
            {
                //For provider specific serialization, we use a string for primitive types, and xml for everything else
                if (prop.SerializeAs == SettingsSerializeAs.ProviderSpecific)
                {
                    if (prop.PropertyType.IsPrimitive || prop.PropertyType == typeof(string))
                        prop.SerializeAs = SettingsSerializeAs.String;
                    else
                        prop.SerializeAs = SettingsSerializeAs.Xml;
                }

                //add to our collection
                svc.Add(new SettingsPropertyValue(prop));
            }

            if (!String.IsNullOrEmpty(username))
            {
                GetPropertyValuesFromDatabase(username, svc);
            }

            return svc;
        }

        private void GetPropertyValuesFromDatabase(string username, SettingsPropertyValueCollection properties)
        {
            HttpContext context = HttpContext.Current;

            string[] names = null;
            string values = null;
            byte[] buf = null;
            DataSet ds = null;

            //Make sure the user is authenticated (can't get profile info for anonymous users
            if (!context.Request.IsAuthenticated)
                throw new ProviderException(SR.GetString(SR.Provider_this_user_not_found));

            _dops.ResetDops();
            _dops.Sproc = "usp_GetProfileProperties";

            _dops.SetParameter("@ApplicationName", ApplicationName, "IN");
            _dops.SetParameter("@UserName", username, "IN");
            _dops.SetParameter("@CurrentTime", DateTime.Now, "IN");

            try
            {
                ds = _dops.get_dataset();
            }
            catch
            {
                throw;
            }

            if (ds.Tables.Count == 0)   //if there are no properties returned, exit
                return;

            //now we have a dataset with all profile properties from the database
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                //Grab the property associated with this row
                SettingsPropertyValue property = properties[dr["PropertyName"].ToString()];

                if (property == null) //property not found
                    continue;

                //Check the property type out of the database to find out if the current property
                //is stored as a string or a binary blob
                string currentPropertyType = dr["PropertyType"].ToString();

                if ( currentPropertyType == "S" )
                {
                    //String
                    property.SerializedValue = dr["PropertyValueString"];                    
                }

                if ( currentPropertyType == "B" )
                {
                    //Binary blob
                    property.SerializedValue = dr["PropertyValueBinary"];
                }
            }
        }

        public override void SetPropertyValues(SettingsContext context, SettingsPropertyValueCollection properties)
        {
            string username = (string)context["UserName"];
            bool isAuthenticated = (bool)context["IsAuthenticated"];

            //Make sure we valid parameters
            if (username == null || username.Length < 1 || properties.Count < 1)
                return;

            foreach (SettingsPropertyValue property in properties)
            {
                string sVal = string.Empty;
                string PropertyType = string.Empty;

                //only save if the property is dirty or using its default value
                if (property.IsDirty || property.UsingDefaultValue)
                {
                    if (property.Property.SerializeAs == SettingsSerializeAs.Binary)
                    {
                        //Serialize the property value as binary
                        PropertyType = "B";

                        //NOT IMPLEMENTED
                    }
                    else
                    {
                        //Any serialization other than binary
                        object propVal = property.PropertyValue;

                        if (property.Deserialized && property.PropertyValue == null)
                        {
                            sVal = string.Empty;
                        }
                        else
                        {
                            if (!(property.SerializedValue is string))
                            {
                                if (property.SerializedValue == null)
                                    sVal = string.Empty;
                                else
                                    sVal = Convert.ToBase64String((byte[])property.SerializedValue);

                            }
                            else
                            {
                                sVal = (string)property.SerializedValue;
                            }
                        }
                        //if (property.Deserialized)
                        //{
                        //    sVal = Convert.ToBase64String((byte[])property.SerializedValue);
                        //}
                        //else
                        //{
                        //    sVal = Convert.ToBase64String((byte[])propVal);
                        //}

                        PropertyType = "S";
                    }

                    //Now call dataops and save the current property
                    _dops.ResetDops();
                    _dops.Sproc = "usp_SetProfileProperties";

                    _dops.SetParameter("@ApplicationName", ApplicationName, "IN");
                    _dops.SetParameter("@UserName", username, "IN");
                    _dops.SetParameter("@PropertyName", property.Name, "IN");
                    _dops.SetParameter("@PropertyValueString", sVal, "IN");
                    _dops.SetParameter("@PropertyValueBinary", string.Empty, "IN");
                    _dops.SetParameter("@PropertyType", PropertyType, "IN");
                    _dops.SetParameter("@CurrentTime", DateTime.Now, "IN");

                    _dops.Execute_Sql();
                }
            }

        }

        #endregion

        #region Non-Implemented Methods

        public override int DeleteInactiveProfiles(ProfileAuthenticationOption authenticationOption, DateTime userInactiveSinceDate)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override int DeleteProfiles(string[] usernames)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override int DeleteProfiles(ProfileInfoCollection profiles)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        public override ProfileInfoCollection FindInactiveProfilesByUserName(ProfileAuthenticationOption authenticationOption, string usernameToMatch, DateTime userInactiveSinceDate, int pageIndex, int pageSize, out int totalRecords)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override ProfileInfoCollection FindProfilesByUserName(ProfileAuthenticationOption authenticationOption, string usernameToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override ProfileInfoCollection GetAllInactiveProfiles(ProfileAuthenticationOption authenticationOption, DateTime userInactiveSinceDate, int pageIndex, int pageSize, out int totalRecords)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override ProfileInfoCollection GetAllProfiles(ProfileAuthenticationOption authenticationOption, int pageIndex, int pageSize, out int totalRecords)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override int GetNumberOfInactiveProfiles(ProfileAuthenticationOption authenticationOption, DateTime userInactiveSinceDate)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override string ToString()
        {
            return base.ToString();
        }

        #endregion
    }

}