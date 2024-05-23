using ModelLayer.ProductRegisisterModel;
using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Interface
{
    public interface IProductRL
    {
      
        public object GetAllProducts();
        public Product ProductRegistration1(ProductRegistration productRegistration);
    }
}
