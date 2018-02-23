using System;

namespace PI.Party.Models
{
    [Serializable()]
    public class Address
    {
        private string _Address1;
        public string Address1
        {
            get { return _Address1; }
            set { _Address1 = value; }
        }

        private string _Address2;
        public string Address2
        {
            get { return _Address2; }
            set { _Address2 = value; }
        }

        private string _City;
        public string City
        {
            get { return _City; }
            set { _City = value; }
        }

        private string _State;
        public string State
        {
            get { return _State; }
            set { _State = value; }
        }

        private string _ZipCode;
        public string ZipCode
        {
            get { return _ZipCode; }
            set { _ZipCode = value; }
        }

        private string _Country;
        public string Country
        {
            get { return _Country; }
            set { _Country = value; }
        }
    }

    [Serializable()]
    public class Phone
    {
        private string _CountryCode;
        public string CountryCode
        {
            get { return _CountryCode; }
            set { _CountryCode = value; }
        }

        private string _AreaCode;
        public string AreaCode
        {
            get { return _AreaCode; }
            set { _AreaCode = value; }
        }

        private string _Number;
        public string Number
        {
            get { return _Number; }
            set { _Number = value; }
        }
    }

    [Serializable]
    public class WebAppsUserProfile
    {
        public string FirstName;
        public string LastName;
        public Address HomeAddress;
        public Phone HomePhone;
        public Phone WorkPhone;
        public Phone CellPhone;
        public string Email;
    }
}