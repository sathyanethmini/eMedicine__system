using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using EMedicineBE.Models;
using System.Data.SqlClient;

namespace EMedicineBE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MedicineController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public MedicineController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        [HttpPost]
        [Route("AddToCart")]
        public Response AddToCart(Cart cart)
        {
            DAL dal = new DAL();
            SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("EMedCS").ToString());
            Response response = dal.AddToCart(cart, connection);
            return response;
        }
        [HttpPost]
        [Route("PlaceOrder")]
        public Response PlaceOrder(Users users)
        {
            DAL dal = new DAL();
            SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("EMedCS").ToString());
            Response response = dal.PlaceOrder(users, connection);
            return response;
        }
        [HttpPost]
        [Route("OrderList")]
        public Response OrderList(Users users)
        {
            DAL dal = new DAL();
            SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("EMedCS").ToString());
            Response response = dal.OrderList(users, connection);
            return response;
        }

    }
}
