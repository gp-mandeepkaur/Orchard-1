using Orchard;
using PI.Party.Services;

namespace PI.Party.Services.PIPartyEF
{
    public interface IPIParty:IDependency
    {

        #region "Properties"
        PIPartyEF PartyContext { get; }
        #endregion
       
    }
}