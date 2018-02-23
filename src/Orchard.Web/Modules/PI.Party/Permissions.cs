using System.Collections.Generic;
using Orchard.Environment.Extensions.Models;
using Orchard.Security.Permissions;

namespace PI.Party
{
    public class Permissions : IPermissionProvider
    {
        public static readonly Permission ViewPartyGrid = new Permission { Description = "View Party Grid", Name = "ViewPartyGrid" };
        public static readonly Permission MapAccount = new Permission { Description = "Map Account", Name = "Map Account" };

        public virtual Feature Feature { get; set; }

        public IEnumerable<Permission> GetPermissions()
        {
            return new[] {
                ViewPartyGrid,
                MapAccount
            };
        }

        public IEnumerable<PermissionStereotype> GetDefaultStereotypes()
        {
            return new[] {
                new PermissionStereotype {
                    Name = "Administrator",
                    Permissions = new[] { ViewPartyGrid,MapAccount}
                },
            };
        }

    }
}