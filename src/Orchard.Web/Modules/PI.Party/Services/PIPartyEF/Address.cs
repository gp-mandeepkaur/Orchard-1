//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace PI.Party.Services.PIPartyEF
{
    using System;
    using System.Collections.Generic;
    
    public partial class Address
    {
        public int ID { get; set; }
        public Nullable<System.Guid> PartyId { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string Zip { get; set; }
        public int PurposeTypeID { get; set; }
    
        public virtual Party Party { get; set; }
        public virtual AddressPurpose AddressPurpose { get; set; }
    }
}
