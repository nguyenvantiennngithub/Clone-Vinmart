using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VinMart.Models.DTO;

namespace VinMart.Models.ViewModel
{
    public class AddAddressVM
    {
        private List<CartDetailProductDTO> cartDetailProducts;
        private Address address;

        public AddAddressVM(List<CartDetailProductDTO> cartDetailProducts, Address address)
        {
            this.CartDetailProducts = cartDetailProducts;
            this.Address = address;
        }

        public List<CartDetailProductDTO> CartDetailProducts { get => cartDetailProducts; set => cartDetailProducts = value; }
        public Address Address { get => address; set => address = value; }
    }
}