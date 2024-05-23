using ModelLayer.ProductRegisisterModel;
using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Interface
{
    public interface IProductBL
    {        
        public Product ProductRegistration1(ProductRegistration productRegistration);

        public object GetAllProducts();
    }
}
