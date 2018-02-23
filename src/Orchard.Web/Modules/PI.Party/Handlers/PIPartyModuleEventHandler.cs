using Orchard;
using Orchard.ContentManagement;
using Orchard.Environment;
using Orchard.Environment.Extensions.Models;
using PI.Party.Helpers.Constant;
using PI.Party.Models;

namespace PI.Party.Handlers
{
    public class PIPartyModuleEventHandler : IFeatureEventHandler
    {
        private readonly IOrchardServices _orchardServices;
        public PIPartyModuleEventHandler(IOrchardServices orchardServices)
        {
            _orchardServices = orchardServices;
        }

        public void Installing(Feature feature) { }
        public void Installed(Feature feature) { }
        public void Enabling(Feature feature) { }
        public void Enabled(Feature feature)
        {
            if (feature.Descriptor.Name.Equals("PI.Party"))
            {
                _orchardServices.WorkContext.CurrentSite.As<PIPartySettingsPart>().MetadataString = SiteConstant.PartyMetaDataString;
            }
        }
        public void Disabling(Feature feature) { }
        public void Disabled(Feature feature) { }
        public void Uninstalling(Feature feature) { }
        public void Uninstalled(Feature feature) { }
    }
}