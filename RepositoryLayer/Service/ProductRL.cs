using Microsoft.Extensions.Configuration;
using ModelLayer.ProductRegisisterModel;
using ModelLayer.UserModel;
using RepositoryLayer.Context;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Service
{
    public class ProductRL : IProductRL
    {
        private readonly FundooContext fundooContext;

        public ProductRL(FundooContext fundooContext)
        {
            this.fundooContext = fundooContext;

        }
        public Product ProductRegistration1(ProductRegistration productRegistration)
        {          
                try
                {
                    Product product=new Product();
                    product.brand = productRegistration.brand;
                    product.pname = productRegistration.pname;
                    fundooContext.Products.Add(product);
                    int result = fundooContext.SaveChanges();
                    if (result != 0)
                    {
                        return product;
                    }
                    else
                    {
                        return null;
                    }

                }
                catch (Exception ex)
                {

                    throw ex;
                }
            }
        public object GetAllProducts()
        {
            try
            {
                var products = fundooContext.Products.ToList();
                if (products!=null)
                {
                    return products;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                return ex;
            }
        }
    }
}