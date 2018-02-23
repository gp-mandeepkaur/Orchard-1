using System;
using System.Linq;
using PI.Party.Services;

namespace PI.Party.Services.PIPartyEF
{
    partial class PIPartyEF
    {

        #region "CStor"
        public PIPartyEF(string connString) : base(connString) {}
        #endregion

        #region "Public Methods"
        public Party FindPartyById(Guid id)
        {
            var party = Parties.Where(p => p.PartyId == id).SingleOrDefault();

            return party;
        }
        public Party FindPartyByMasterIdentity(string username)
        {
            var ma = this.Set<Account_Master>().Where(p => p.AccountName == username)
                .SingleOrDefault();

            if (ma != null)
                return ma.Party;
            else
                return null;
        }
        public Account_Master FindMasterIdentityByPartyId(Guid partyId)
        {
            var ma = this.Set<Account_Master>().Where(p => p.PartyId == partyId)
                .SingleOrDefault();

            return ma;
        }
        
        #endregion

    }
}