using Orchard;
using PI.Party.Services;
using System.Web.Mvc;
using Orchard.Themes;
using PI.Party.Services.PIPartyEF;
using System;
using Orchard.Security;
using Orchard.Localization;
using PI.Party.Helpers.Constant;

namespace PI.Party.Controllers
{
    [Authorize]
    [Themed]
    public class HomeController : Controller
    {

        #region "Properties"
        private readonly IPIUserService _partyUserService;
        private readonly IOrchardServices _orchardServices;
        private readonly IAuthenticationService _authenticationService;
        public Localizer T { get; set; }
        #endregion

        #region "CStor"
        public HomeController(IPIUserService partyUserService, IOrchardServices orchardServices, IAuthenticationService authenticationService)
        {
            _orchardServices = orchardServices;
            _partyUserService = partyUserService;
            _authenticationService = authenticationService;
            T = NullLocalizer.Instance;
        }

        #endregion

        #region "Action Methods"
        public ActionResult Index()
        {
            if (!_orchardServices.Authorizer.Authorize(Permissions.ViewPartyGrid))
                return View("Unauthorised");
            ViewBag.Authorized = _orchardServices.Authorizer.Authorize(Permissions.MapAccount);
            return View();
        }

        public JsonResult GetPartyUsers()
        {
            var users = _partyUserService.GetPartyUsers();
            return Json(users, JsonRequestBehavior.AllowGet);
        }

        public ActionResult UpdateAccountNumber(string partyid, string accountnumber)
        {
            if (!_orchardServices.Authorizer.Authorize(Permissions.MapAccount))
                return Json(new { success = false, responseText = MessageConstant.NotAuthorizedToMap }, JsonRequestBehavior.AllowGet);
            else
            {
                Guid partyId = Guid.Empty;
                int accnumber;
                if (!int.TryParse(accountnumber, out accnumber) || accnumber <= 0)
                    return Json(new { success = false, responseText = MessageConstant.AccountNumberNotValid }, JsonRequestBehavior.AllowGet);
                else if (!Guid.TryParse(partyid, out partyId))
                    return Json(new { success = false, responseText = MessageConstant.SomeErrorOccured }, JsonRequestBehavior.AllowGet);
                else if (!_partyUserService.UpdateAccountNumber(partyId, accnumber, _authenticationService.GetAuthenticatedUser().UserName))
                    return Json(new { success = false, responseText = MessageConstant.AccountAlreadyExists }, JsonRequestBehavior.AllowGet);
                else
                    return Json(new { success = true, responseText = MessageConstant.AccountSavedSucessfully }, JsonRequestBehavior.AllowGet);
            }
        }

        #endregion
    }
}