using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VinMart.Models.DTO;

namespace VinMart.Models.ViewModel
{
    public class AccountListAddressVM
    {
        List<Address> listAddress;
        List<CartDetailProductDTO> cartDetailProducts;

        public AccountListAddressVM(List<CartDetailProductDTO> cartDetailProducts, List<Address> listAddress)
        {
            this.CartDetailProducts = cartDetailProducts;
            this.ListAddress = listAddress;
        }

        public List<Address> ListAddress { get => listAddress; set => listAddress = value; }
        public List<CartDetailProductDTO> CartDetailProducts { get => cartDetailProducts; set => cartDetailProducts = value; }
    }
}