using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using ecommerce_template.Models;
using System.Data.SqlClient;


namespace ecommerce_template.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public ProductsController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        [HttpPost]
        [Route("addtocart")]
        public Response addtocart(Cart cart)
        {
            DAL dAL = new DAL();
            SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("EcommerceCS").ToString());
            Response response = dAL.Addtocart(cart, connection);
            return response;
        }

        [HttpPost]
        [Route("PlaceOrder")]
        public Response PlaceOrder(Users users) 
        {
            DAL dAL = new DAL();
            SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("EcommerceCS").ToString());
            Response response = dAL.PlaceOrder(users, connection);
            return response;
        }

        [HttpPost]
        [Route("OrderList")]
        public Response OrderList(Users users) 
        {
            DAL dAL = new DAL();
            SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("EcommerceCS").ToString());
            Response response = dAL.OrderList(users, connection);
            return response;
        }

        [HttpPost]
        [Route("CategoryList")]
        public Response CategoryList(Products products)
        {
            DAL dAL = new DAL();
            SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("EcommerceCS").ToString());
            Response response = dAL.CategoryList(products, connection);
            return response;
        }

        [HttpPost]
        [Route("AddToCategory")]
        public Response AddToCategory(Categories categories) 
        {
            DAL dAL = new DAL();
            SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("EcommerceCS").ToString());
            Response response = dAL.AddToCategory(categories, connection);
            return response;
        }
    }
}
