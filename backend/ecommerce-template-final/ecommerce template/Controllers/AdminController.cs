using ecommerce_template.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;

namespace ecommerce_template.Controllers
{
    public class AdminController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public AdminController(IConfiguration configuration)
        {
            _configuration = configuration;
        }


        [HttpPost]
        [Route("AddUpdateProducts")]
        public Response AddUpdateProducts (Products products)
        {
            DAL dal = new DAL();
            SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("EcommerceCS").ToString());
            Response response = dal.AddUpdateProducts(products,connection);
            return response;
        }

        [HttpGet]
        [Route("UserList")]
        public Response userlist()
        {
           DAL dal = new DAL();
            SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("EcommerceCS").ToString());
            Response response = dal.UserList(connection);
            return response;
        }

        [HttpPost]
        [Route("AddUpdateCategories")]
        public Response AddUpdateCategories (Categories categories)
        {
            DAL dAL = new DAL();
            SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("EcommerceCS").ToString());
            Response response=dAL.AddUpdateCategories(categories,connection);
            return response;
        }
    }
}
