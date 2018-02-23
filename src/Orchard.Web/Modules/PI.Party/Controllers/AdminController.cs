using PI.Party.Services;
using System;
using System.Web.Mvc;
using PI.Common.Helpers;

namespace PI.Party.Controllers
{
    [ValidateInput(false)]
    public class AdminController : Controller
    {
        private readonly IAdminSettingsService _adminSettingsService;

        public AdminController(
           IAdminSettingsService adminSettingsService)
        {
            _adminSettingsService = adminSettingsService;
        }

        public ActionResult AdminSettings()
        {
            return View();
        }

        [HttpPost, ActionName("AdminSettings")]
        public ActionResult ExecuteSql()
        {
            _adminSettingsService.ExecuteSql();
            return View();
        }
      
        public JsonResult ValidateConnectionSettings(FormCollection form)
        {
            var partyPassword = !String.IsNullOrEmpty(form["PIParty.Password"]) ? form["PIParty.Password"] : StringEncryptionHelper.DecryptString(form["PI.hdnPartyPass"], "PartyPass");
            var partyConnectionStatus = ConnectionStringHelper.TestConnectionString(form["PIParty.DataSource"], form["PIParty.Catalog"], form["PIParty.Username"], partyPassword);
            return Json(partyConnectionStatus, JsonRequestBehavior.AllowGet);
        }

    }
}