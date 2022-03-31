using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VinMart.Models
{
    public class AcountFullDetail
    {
        private int _id;

        private string _displayName;

        private string _email;

        private System.Nullable<System.DateTime> _birthDay;

        private string _phoneNumber;

        private string _gender;

        private string _province;

        private string _district;

        private string _Commune;

        private string _apartmentNumber;

        private string _detail;

        public int Id { get => _id; set => _id = value; }
        public string DisplayName { get => _displayName; set => _displayName = value; }
        public string Email { get => _email; set => _email = value; }
        public DateTime? BirthDay { get => _birthDay; set => _birthDay = value; }
        public string PhoneNumber { get => _phoneNumber; set => _phoneNumber = value; }
        public string Gender { get => _gender; set => _gender = value; }
        public string Province { get => _province; set => _province = value; }
        public string District { get => _district; set => _district = value; }
        public string Commune { get => _Commune; set => _Commune = value; }
        public string ApartmentNumber { get => _apartmentNumber; set => _apartmentNumber = value; }
        public string Detail { get => _detail; set => _detail = value; }
    }
}