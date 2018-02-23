using System.Collections.Generic;
using System.Web.Mvc;
using System.Web.Routing;
using Orchard.Mvc.Routes;

namespace PI.Party
{
    public class Routes : IRouteProvider
    {

        public void GetRoutes(ICollection<RouteDescriptor> routes)
        {
            foreach (var routeDescriptor in GetRoutes())
                routes.Add(routeDescriptor);
        }

        public IEnumerable<RouteDescriptor> GetRoutes()
        {
            return new[] {
                             new RouteDescriptor {
                                                     Priority = 5,
                                                     Route = new Route(
                                                         "PI/PartyUsers",
                                                         new RouteValueDictionary {
                                                                                      {"area", "PI.Party"},
                                                                                      {"controller", "Home"},
                                                                                      {"action", "index"}
                                                                                  },
                                                         null,
                                                         new RouteValueDictionary {
                                                                                      {"area", "PI.Party"}
                                                                                  },
                                                         new MvcRouteHandler())
                                                 },
                             new RouteDescriptor {
                                                     Priority = 5,
                                                     Route = new Route(
                                                         "GetPartyUsers",
                                                         new RouteValueDictionary {
                                                                                      {"area", "PI.Party"},
                                                                                      {"controller", "Home"},
                                                                                      {"action", "GetPartyUsers"}
                                                                                  },
                                                         null,
                                                         new RouteValueDictionary {
                                                                                      {"area", "PI.Party"}
                                                                                  },
                                                         new MvcRouteHandler())
                                                 },
                             new RouteDescriptor {
                                                     Priority = 5,
                                                     Route = new Route(
                                                         "UpdateAccountNumber",
                                                         new RouteValueDictionary {
                                                                                      {"area", "PI.Party"},
                                                                                      {"controller", "Home"},
                                                                                      {"action", "UpdateAccountNumber"}
                                                                                  },
                                                         null,
                                                         new RouteValueDictionary {
                                                                                      {"area", "PI.Party"}
                                                                                  },
                                                         new MvcRouteHandler())
                                                 }
                            };
        }
    }
}
