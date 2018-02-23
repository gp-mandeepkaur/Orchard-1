using Orchard.ContentManagement;

namespace PI.Party.Models
{
    public class PIPartySettingsPart : ContentPart
    {
        public string RedirectURL
        {
            get { return this.Retrieve(x => x.RedirectURL); }
            set { this.Store(x => x.RedirectURL, value); }
        }

        public string MetadataString
        {
            get { return this.Retrieve(x => x.MetadataString); }
            set { this.Store(x => x.MetadataString, value); }
        }

        public string DataSource
        {
            get { return this.Retrieve(x => x.DataSource); }
            set { this.Store(x => x.DataSource, value); }
        }


        public string Catalog
        {
            get { return this.Retrieve(x => x.Catalog); }
            set { this.Store(x => x.Catalog, value); }
        }

        public string Username
        {
            get { return this.Retrieve(x => x.Username); }
            set { this.Store(x => x.Username, value); }
        }

        public string Password
        {
            get { return this.Retrieve(x => x.Password); }
            set
            {
                if (value != null)
                {
                    this.Store(x => x.Password, value);
                }
            }
        }
    }

}
