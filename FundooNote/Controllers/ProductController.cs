using BusinessLayer.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ModelLayer.ProductRegisisterModel;

namespace FundooNote.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {

        private readonly IProductBL iproductBL;

        public ProductController(IProductBL iproductBL)
        {
            this.iproductBL = iproductBL;
        }
        [HttpPost]
        [Route("GetAllProducts")]
        public IActionResult GetAllUser()
        {
            try
            {
                var result = iproductBL.GetAllProducts();
                if (result != null)
                {
                    return Ok(new { success = true, message = "Data Feached", data = result });
                }
                else
                {
                    return BadRequest(new { success = false, message = "Try again" });
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        [HttpPost]
        [Route("Register products")]
        public IActionResult ProductRegistration1(ProductRegistration productRegistration)
        {
            try
            {
                var result = iproductBL.ProductRegistration1( productRegistration);
                if (result != null)
                {
                    return Ok(new { success = true, message = "DataRegister", data = result });
                }
                else
                {
                    return BadRequest(new { success = false, message = "Try again" });
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }

    }
}
