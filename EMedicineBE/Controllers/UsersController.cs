using EMedicineBE.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;

namespace EMedicineBE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public UsersController(IConfiguration configuration)
        {
         _configuration= configuration;
        }

        [HttpPost]
        [Route("registration")]
        public Response Register(Users users)
        { 
            Response response = new Response();
            DAL dal = new DAL();
            SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("EMedCS").ToString());
            response = dal.Register(users, connection);
            return response;
        }
        [HttpPost]
        [Route("login")]
        public Response Login(Users users)
        {
            DAL dal = new DAL();
            SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("EMedCS").ToString());
            Response response = new Response();
            response = dal.Login(users, connection);
            return response;
        }

        [HttpPost]
        [Route("viewUser")]
        public Response ViewUser(Users users)
        {
            DAL dal = new DAL();
            SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("EMedCS").ToString());
            Response response = dal.ViewUser(users, connection);
            return response;
        }

        [HttpPost]
        [Route("UpdateProfile")]
        public Response UpdateProfile(Users users)
        { 
            DAL dal = new DAL();
            SqlConnection connection= new SqlConnection(_configuration.GetConnectionString("EMedCS").ToString());
            Response response = dal.UpdateProfile(users,connection);
            return response;
        }
    }            
}
