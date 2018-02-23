using PI.Party.Helpers.Enums;
using PI.Party.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Serialization;
using PS = PI.Party.Services.PIPartyEF;


namespace PI.Party.Services
{
    public class PIUserService : IPIUserService
    {
        private readonly PS.IPIParty _partyServices;

        public PIUserService(PS.IPIParty partyServices)
        {
            _partyServices = partyServices;
        }

        public PS.Party GetUser(string email)
        {
            return _partyServices.PartyContext.FindPartyByMasterIdentity(email);
        }

        public PS.Party_Person CreateUser(string email)
        {
            PS.Party_Person person = new PS.Party_Person();
            person.PartyId = Guid.NewGuid();
            person.DateCreated = DateTime.Now;

            PS.EmailAddress emailAddr = new PS.EmailAddress();
            emailAddr.PartyId = person.PartyId;
            emailAddr.Email = email;
            emailAddr.PurposeTypeID = Convert.ToInt16(PurposeTypeEnum.PrimaryEmailAddress);

            PS.Account_Master acctM = new PS.Account_Master();
            acctM.PartyId = person.PartyId;
            acctM.AccountName = email;

            person.EmailAddresses.Add(emailAddr);
            person.Accounts.Add(acctM);
            _partyServices.PartyContext.Parties.Add(person);
            _partyServices.PartyContext.SaveChanges();
            return person;
        }

        public PS.Party_Person AddUserProfile(PS.Party_Person person, string aspnetProfile)
        {
            XmlSerializer webAppsUserProfile = new XmlSerializer(typeof(WebAppsUserProfile));
            XmlTextReader userProfileXml = new XmlTextReader(new StringReader(aspnetProfile));
            WebAppsUserProfile userProfile = (WebAppsUserProfile)webAppsUserProfile.Deserialize(userProfileXml);

            person.Lastname = userProfile.LastName;
            person.Firstname = userProfile.FirstName;

            if (userProfile.HomeAddress != null)
            {
                PS.Address addr = new PS.Address();
                addr.PurposeTypeID = Convert.ToInt16(PurposeTypeEnum.PermanentAddress);
                addr.PartyId = person.PartyId;

                addr.Address1 = userProfile.HomeAddress.Address1;
                addr.Address2 = userProfile.HomeAddress.Address2;
                addr.City = userProfile.HomeAddress.City == null ? string.Empty : (userProfile.HomeAddress.City.Length > 50 ? userProfile.HomeAddress.City.Substring(0, 50) : userProfile.HomeAddress.City);
                addr.State = userProfile.HomeAddress.State == null ? string.Empty : (userProfile.HomeAddress.State.Length > 50 ? userProfile.HomeAddress.State.Substring(0, 50) : userProfile.HomeAddress.State);
                addr.Zip = userProfile.HomeAddress.ZipCode == null ? string.Empty : (userProfile.HomeAddress.ZipCode.Length > 50 ? userProfile.HomeAddress.ZipCode.Substring(0, 50) : userProfile.HomeAddress.ZipCode);
                addr.Country = userProfile.HomeAddress.Country == null ? string.Empty : (userProfile.HomeAddress.Country.Length > 50 ? userProfile.HomeAddress.Country.Substring(0, 50) : userProfile.HomeAddress.Country);
                person.Addresses.Add(addr);
            }

            if (userProfile.HomePhone != null && !string.IsNullOrEmpty(userProfile.HomePhone.Number))
            {

                PS.Phone phoneNo = new PS.Phone();
                phoneNo.PartyId = person.PartyId;
                phoneNo.AreaCode = userProfile.HomePhone.AreaCode;
                phoneNo.CountryCode = userProfile.HomePhone.CountryCode;
                var phoneNumberParts = userProfile.HomePhone.Number.Split('-');
                if (phoneNumberParts.Length == 2)
                {
                    phoneNo.PhoneExchange = phoneNumberParts[0].Trim();
                    phoneNo.PhoneLine = phoneNumberParts[1].Trim();
                }
                else
                {
                    phoneNo.PhoneLine = userProfile.HomePhone.Number;
                }
                phoneNo.PurposeTypeID = Convert.ToInt16(PurposeTypeEnum.HomePhone);
                person.Phones.Add(phoneNo);
            }

            if (userProfile.CellPhone != null && !string.IsNullOrEmpty(userProfile.CellPhone.Number))
            {
                PS.Phone phoneNo = new PS.Phone();
                phoneNo.PartyId = person.PartyId;
                phoneNo.AreaCode = userProfile.CellPhone.AreaCode;
                phoneNo.CountryCode = userProfile.CellPhone.CountryCode;
                var phoneNumberParts = userProfile.CellPhone.Number.Split('-');
                if (phoneNumberParts.Length == 2)
                {
                    phoneNo.PhoneExchange = phoneNumberParts[0].Trim();
                    phoneNo.PhoneLine = phoneNumberParts[1].Trim();
                }
                else
                {
                    phoneNo.PhoneLine = userProfile.CellPhone.Number;
                }

                phoneNo.PurposeTypeID = Convert.ToInt16(PurposeTypeEnum.USCellPhone);
                person.Phones.Add(phoneNo);
            }

            if (userProfile.WorkPhone != null && !string.IsNullOrEmpty(userProfile.WorkPhone.Number))
            {
                PS.Phone phoneNo = new PS.Phone();
                phoneNo.PartyId = person.PartyId;
                phoneNo.AreaCode = userProfile.WorkPhone.AreaCode;
                phoneNo.CountryCode = userProfile.WorkPhone.CountryCode;
                var phoneNumberParts = userProfile.WorkPhone.Number.Split('-');
                if (phoneNumberParts.Length == 2)
                {
                    phoneNo.PhoneExchange = phoneNumberParts[0].Trim();
                    phoneNo.PhoneLine = phoneNumberParts[1].Trim();
                }
                else
                {
                    phoneNo.PhoneLine = userProfile.WorkPhone.Number;
                }

                phoneNo.PurposeTypeID = Convert.ToInt16(PurposeTypeEnum.WorkPhone);
                person.Phones.Add(phoneNo);
            }
            _partyServices.PartyContext.Entry(person).State = System.Data.Entity.EntityState.Modified;
            _partyServices.PartyContext.SaveChanges();
            return person;
        }

        public List<EdgePartyUsers> GetPartyUsers()
        {
            List<EdgePartyUsers> users = new List<EdgePartyUsers>();
            var registrations = from reg in _partyServices.PartyContext.Registrations
                                group reg by reg.PartyId
                                into regParty
                                select regParty.OrderByDescending(p => p.DateTimeCreated).FirstOrDefault();

            users = (from pp in _partyServices.PartyContext.Parties.OfType<PS.Party_Person>()
                     join em in _partyServices.PartyContext.EmailAddresses
                     on pp.PartyId equals em.PartyId
                     join reg in registrations
                     on pp.PartyId equals reg.PartyId
                     join edgerAcc in _partyServices.PartyContext.Account_SECRM
                     on pp.PartyId equals edgerAcc.PartyId into joinedrr
                     from edger in joinedrr.DefaultIfEmpty()
                     orderby reg.LoginCreatedDate descending
                     select new EdgePartyUsers
                     {
                         PartyId = pp.PartyId,
                         FirstName = pp.Firstname,
                         LastName = pp.Lastname,
                         Email = em.Email,
                         LoginCreatedDate = reg.LoginCreatedDate,
                         IsAuthenticated = reg.LoginCreatedDate == null ? false : true,
                         AccountNumber = edger != null && edger.AccountNumber.HasValue ? edger.AccountNumber.Value : 0
                     }).ToList();
            return users;
        }

        public bool UpdateAccountNumber(Guid partyId, int accountNumber,string username)
        {
            var isPartyExist=_partyServices.PartyContext.Account_SECRM.Any(x => x.PartyId != partyId && x.AccountNumber == accountNumber);
            if (isPartyExist)
            {
                return false;
            }
            var party = _partyServices.PartyContext.Account_SECRM.SingleOrDefault(x => x.PartyId == partyId);
            if (party == null)
            {
                PS.Account_SECRM acc = new PS.Account_SECRM();
                acc.PartyId = partyId;
                acc.AccountNumber = accountNumber;
                acc.CreatedDate = DateTime.UtcNow;
                acc.CreatedBy = username;
                _partyServices.PartyContext.Account_SECRM.Add(acc);
            }
            else
            {
                party.AccountNumber = accountNumber;
                party.ModifiedDate = DateTime.UtcNow;
                party.ModifiedBy = username;
            }
            _partyServices.PartyContext.SaveChanges();
            return true;
        }
    }
}