using Orchard.Localization;
using Orchard.Security;
using Orchard.UI.Navigation;

namespace PI.Party
{
    public class AdminMenu : INavigationProvider
    {
        public Localizer T { get; set; }
        public string MenuName { get { return "admin"; } }

        public void GetNavigation(NavigationBuilder builder)
        {
            builder.AddImageSet("users")
                .Add(T("PI.Party Admin UI"), "11",
                    menu => menu.Action("AdminSettings", "Admin", new { area = "PI.Party" })
                        .Add(T("SqlScripts"), "1.0", item => item.Action("AdminSettings", "Admin", new { area = "PI.Party" })
                            .LocalNav().Permission(StandardPermissions.SiteOwner)));
        }
    }
}