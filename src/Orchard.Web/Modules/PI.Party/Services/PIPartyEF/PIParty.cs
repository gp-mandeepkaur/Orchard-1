using Orchard;
using System;
using System.Data.Entity.Core.EntityClient;
using System.Data.SqlClient;
using Orchard.ContentManagement;
using PI.Party.Models;
using PI.Party.Helpers.Constant;

namespace PI.Party.Services.PIPartyEF
{
    public class PIParty : IPIParty
    {

        #region "Properties"
        private readonly IOrchardServices _orchardServices;
        private PIPartyEF _partyContext;
        public PIPartyEF PartyContext
        {
            get
            {
                if (_partyContext == null)
                    _partyContext = new PIPartyEF(GenerateConnectionString());
                return _partyContext;
            }
        }
        #endregion

        #region "CStor"
        public PIParty(IOrchardServices orchardServices)
        {
            _orchardServices = orchardServices;
        }
        #endregion

        #region "Implemented Methods"
        public void SaveChanges()
        {
            PartyContext.SaveChanges();
        }
        #endregion

        #region "Private Methods"
        private string GenerateConnectionString()
        {
            SqlConnectionStringBuilder sqlBuilder = new SqlConnectionStringBuilder();
            // Set the Metadata location.
            var piPartySettings = _orchardServices.WorkContext.CurrentSite.As<PIPartySettingsPart>();


            // Set the properties for the data source.

            if (piPartySettings.DataSource == null)
                return "";

            string metadata = piPartySettings.MetadataString;
            sqlBuilder.DataSource = piPartySettings.DataSource;
            sqlBuilder.InitialCatalog = piPartySettings.Catalog;
            sqlBuilder.UserID = piPartySettings.Username;
            sqlBuilder.Password = piPartySettings.Password;
            sqlBuilder.IntegratedSecurity = false;

            // Build the SqlConnection connection string.
            string providerString = sqlBuilder.ToString();

            // Initialize the EntityConnectionStringBuilder.
            EntityConnectionStringBuilder entityBuilder = new EntityConnectionStringBuilder();

            //Set the provider name.
            entityBuilder.Provider = "System.Data.SqlClient";

            // Set the provider-specific connection string.
            entityBuilder.ProviderConnectionString = providerString;

            if (!String.IsNullOrEmpty(metadata))
                entityBuilder.Metadata = metadata;
            else
                entityBuilder.Metadata = SiteConstant.PartyMetaDataString;

            return entityBuilder.ToString();
        }
        #endregion
    }
}