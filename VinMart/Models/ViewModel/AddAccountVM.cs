using VinMart.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VinMart.Models.DTO;

namespace VinMart.Models.ViewModel
{
    public class AddAccountVM
    {
        private AcountFullDetail acountFullDetail;
        List<CartDetailProductDTO> cartDetailProducts;

        public AddAccountVM(AcountFullDetail acountFullDetail, List<CartDetailProductDTO> cartDetailProducts)
        {
            this.acountFullDetail = acountFullDetail;
            this.cartDetailProducts = cartDetailProducts;
        }

        public AcountFullDetail AcountFullDetail { get => acountFullDetail; set => acountFullDetail = value; }
        public List<CartDetailProductDTO> CartDetailProducts { get => cartDetailProducts; set => cartDetailProducts = value; }
    }
}