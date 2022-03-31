using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VinMart.Models
{
    public class PurchaseFullDetail
    {
		private int _id;

		private int _idDetail;

		private System.Nullable<int> _idUser;

        private System.Nullable<int> _idAddress;

        private string _address;

        private System.Nullable<System.DateTime> _createAt;

		private System.Nullable<int> _idProduct;

		private string _displayName;

		private double _price;

		private int _count;

        private string _province;

        private string _district;

        private string _commune;

        private string _apartmentNumber;


        public int Id { get => _id; set => _id = value; }
        public int IdDetail { get => _idDetail; set => _idDetail = value; }
        public int? IdUser { get => _idUser; set => _idUser = value; }
        public DateTime? CreateAt { get => _createAt; set => _createAt = value; }
        public int? IdProduct { get => _idProduct; set => _idProduct = value; }
        public string DisplayName { get => _displayName; set => _displayName = value; }
        public double Price { get => _price; set => _price = value; }
        public int Count { get => _count; set => _count = value; }
        public int? IdAddress { get => _idAddress; set => _idAddress = value; }
        public string Address { get => _address; set => _address = value; }
        public string Province { get => _province; set => _province = value; }
        public string District { get => _district; set => _district = value; }
        public string Commune { get => _commune; set => _commune = value; }
        public string ApartmentNumber { get => _apartmentNumber; set => _apartmentNumber = value; }
    }
}