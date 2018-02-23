using Orchard.ContentManagement;
using Orchard.ContentManagement.Handlers;
using Orchard.Localization;
using PI.Party.Models;
using System.Linq;

namespace PI.Party.Handlers
{
    public class PIPartySettingsHandler : ContentHandler
    {
        public PIPartySettingsHandler()
        {
            T = NullLocalizer.Instance;
            Filters.Add(new ActivatingFilter<PIPartySettingsPart>("Site"));
            Filters.Add(new TemplateFilterForPart<PIPartySettingsPart>("PIParty", "PIPartySettings", "PIParty"));
        }
    
        public Localizer T { get; set; }

        protected override void GetItemMetadata(GetContentItemMetadataContext context)
        {
            if (context.ContentItem.ContentType != "Site")
                return;
            base.GetItemMetadata(context);

            var t = context.Metadata.EditorGroupInfo.Where(p => p.Name.Equals(T("PIParty"))).SingleOrDefault();

            if (t == null)
                context.Metadata.EditorGroupInfo.Add(new GroupInfo(T("PIParty")));

        }
    }
}
