using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PI.Party.Models
{
    public class EdgePartyUsers
    {
        public Guid PartyId;
        public string FirstName;
        public string LastName;
        public int AccountNumber;
        public string Email;
        public string Username;
        public Nullable<DateTime> LoginCreatedDate;
        public bool IsAuthenticated;
    }
}