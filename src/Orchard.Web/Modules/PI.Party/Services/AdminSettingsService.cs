using Orchard;
using Orchard.ContentManagement;
using Orchard.Localization;
using Orchard.Logging;
using PI.Party.Models;
using System;
using Orchard.UI.Notify;
using System.Linq;
using System.Web;
using System.IO;
using System.Text.RegularExpressions;
using Orchard.Security;
using Orchard.Environment.Extensions;
using PI.Party.Helpers;
using PI.Party.Helpers.Constant;
using PI.Party.Services;
using PI.Party.Services.PIPartyEF;

namespace PI.Party.Services
{
    public class AdminSettingsService : IAdminSettingsService
    {
        private readonly IOrchardServices _orchardServices;
        private readonly IPIParty _partyService;
        private readonly IAuthenticationService _authenticationService;
        private readonly IExtensionManager _extensionManager;
        public Localizer T { get; set; }
        public ILogger Logger { get; set; }
        public AdminSettingsService(
            IOrchardServices orchardServices,
            IAuthenticationService authenticationService,
            IExtensionManager extensionManager,
            IPIParty partyService)
        {
            _orchardServices = orchardServices;
            _authenticationService = authenticationService;
            _extensionManager = extensionManager;
            _partyService = partyService;
            T = NullLocalizer.Instance;
            Logger = NullLogger.Instance;
        }

        public void ExecuteSql()
        {
            var piPartySettings = _orchardServices.WorkContext.CurrentSite.As<PIPartySettingsPart>();
            if (!string.IsNullOrEmpty(piPartySettings.MetadataString)
                && !string.IsNullOrEmpty(piPartySettings.DataSource)
                && !string.IsNullOrEmpty(piPartySettings.Catalog)
                && !string.IsNullOrEmpty(piPartySettings.Username)
                && !string.IsNullOrEmpty(piPartySettings.Password))
            {
                string sourcePath = HttpContext.Current.Server.MapPath("~") + SiteConstant.SqlScriptPath;
                try
                {
                    if (Directory.GetFiles(sourcePath).Count() > 0)
                    {
                        foreach (var file in Directory.GetFiles(sourcePath))
                        {
                            try
                            {
                                string fileText = File.OpenText(file).ReadToEnd();
                                string updatedFileText = Regex.Replace(fileText, @"\bGO\b", "");
                                _partyService.PartyContext.Database.ExecuteSqlCommand(updatedFileText);
                                UpdateSchemaVersion();
                                _orchardServices.Notifier.Information(T(ExecuteSqlConstant.ScriptExecuted));
                            }
                            catch (Exception ex)
                            {
                                _orchardServices.Notifier.Error(T(ExecuteSqlConstant.FailedToExecuteAllScripts));
                                this.Logger.Error(ex, ex.Message);
                            }
                        }
                    }
                    else
                    {
                        _orchardServices.Notifier.Error(T(ExecuteSqlConstant.NoScriptFound));
                    }
                }
                catch (Exception ex)
                {
                    _orchardServices.Notifier.Error(T(ExecuteSqlConstant.FailedToExecuteAllScripts));
                    this.Logger.Error(ex, ex.Message);
                }
            }
            else
            {
                _orchardServices.Notifier.Error(T(ExecuteSqlConstant.UpdateConnectionSettings));
            }

        }

        private void UpdateSchemaVersion()
        {
            //Get logged in user
            var user = _authenticationService.GetAuthenticatedUser();

            //Get the module version from module.txt
            var moduleVersion = _extensionManager.GetExtension("PI.Party");

            var moduleSchemaSettings = _partyService.PartyContext.ModuleSchemaSettings
                                                                         .SingleOrDefault(x => x.ModuleSchemaVersion == moduleVersion.Version);

            if (moduleSchemaSettings == null)
            {
                //ModuleSettings obj
                ModuleSchemaSetting settingsObj = new ModuleSchemaSetting();
                settingsObj.UpdatedBy = user.UserName;
                settingsObj.DateSchemaUpdated = DateTime.Now;
                settingsObj.ModuleSchemaVersion = moduleVersion.Version;

                _partyService.PartyContext.ModuleSchemaSettings.Add(settingsObj);
                _partyService.PartyContext.SaveChanges();
            }

            else
            {
                moduleSchemaSettings.DateSchemaUpdated = DateTime.Now;
                _partyService.PartyContext.Entry(moduleSchemaSettings).State = System.Data.Entity.EntityState.Modified;
                _partyService.PartyContext.SaveChanges();
            }
        }
    }
}