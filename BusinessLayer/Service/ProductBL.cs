using BusinessLayer.Interface;
using ModelLayer.ProductRegisisterModel;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Service
{
    public class ProductBL : IProductBL
    {
        private readonly IProductRL iproduct;

        public ProductBL(IProductRL iproduct) 
        {
        this.iproduct=iproduct;
        }
        public object GetAllProducts()
        {
            return iproduct.GetAllProducts();
        }

        public Product ProductRegistration1(ProductRegistration productRegistration)
        {
          return iproduct.ProductRegistration1(productRegistration);  
        }
    }
}
