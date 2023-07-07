 using System.Data.SqlClient;
using System.Linq;
using System.Data;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;

namespace ecommerce_template.Models
{
    public class DAL
    {
        //register
        public Response Register(Users users, SqlConnection connection)
        {
            Response response = new Response();
            SqlCommand cmd = new SqlCommand("sp_register",connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Name", users.Name);
            cmd.Parameters.AddWithValue("@Email", users.Email);
            cmd.Parameters.AddWithValue("@Password", users.Password);
            cmd.Parameters.AddWithValue("@Username", users.Username);
            cmd.Parameters.AddWithValue("@Type", users.Type);
            connection.Open();
            int i = cmd.ExecuteNonQuery();
            connection.Close();
            if (i > 0)
            {
                response.StatusCode = 200;
                response.StatusMessage = "registration successful";
            }
            else
            {
                response.StatusCode = 400;
                response.StatusMessage = "registaration failed";

            }
            return response;
        }
        //login
        public Response Login(Users users, SqlConnection connection)
        {
            SqlDataAdapter da = new SqlDataAdapter("sp login", connection);
            da.SelectCommand.CommandType= CommandType.StoredProcedure;
            da.SelectCommand.Parameters.AddWithValue ("@Email", users.Email);
            da.SelectCommand.Parameters.AddWithValue("@Password", users.Password);
            DataTable dt = new DataTable();
            da.Fill(dt);
            Response response = new Response();
            Users user = new Users();
            if(dt.Rows.Count > 0)
            {
                user.Id = Convert.ToInt32(dt.Rows[0]["ID"]);
                user.Name = Convert.ToString(dt.Rows[0]["Name"]);
                user.Email = Convert.ToString(dt.Rows[0]["Email"]);
                user.Username = Convert.ToString(dt.Rows[0]["Username"]);
                user.Type = Convert.ToString(dt.Rows[0]["Type"]);
                response.StatusCode = 200;
                response.StatusMessage = "Login success";
                response.user= user;
            }
            else
            {
                response.StatusCode = 400;
                response.StatusMessage = "Login failed";
                response.user = null;
            }
            return response;
        }
        
        //view users
        public Response viewuser(Users users, SqlConnection connection)
        {
            SqlDataAdapter da = new SqlDataAdapter("p_viewuser", connection);
            da.SelectCommand.CommandType=CommandType.StoredProcedure;
            da.SelectCommand.Parameters.AddWithValue("@ID", users.Id);
            DataTable dt = new DataTable();
            da.Fill (dt);
            Response response = new Response();
            Users user = new Users();
            if (dt.Rows.Count > 0)
            {
                user.Id = Convert.ToInt32(dt.Rows[0]["ID"]);
                user.Name = Convert.ToString(dt.Rows[0]["Name"]);
                user.Email = Convert.ToString(dt.Rows[0]["Email"]);
                user.Username = Convert.ToString(dt.Rows[0]["Username"]);
                user.Type = Convert.ToString(dt.Rows[0]["Type"]);
                user.Password = Convert.ToString(dt.Rows[0]["Password"]);
                response.StatusCode=200;
                response.StatusMessage = "User exists";
                
            }
            else
            {
                response.StatusCode = 400;
                response.StatusMessage = "User Doesn't exist";
                response.user = user;
            }
            return response;
        }
        
        //update profile
        public Response updateprofile(Users users,SqlConnection connection)
        {
            Response response = new Response();
            SqlCommand cmd = new SqlCommand("sp_updateProfile", connection);
            cmd.CommandType= CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Name", users.Name);
            cmd.Parameters.AddWithValue("@Email", users.Email);
            cmd.Parameters.AddWithValue("@Password", users.Password);
            cmd.Parameters.AddWithValue("@Username", users.Username);
            connection.Open();
            int i = cmd.ExecuteNonQuery();
            connection.Close();
            if (i > 0)
            {
                response.StatusCode = 200;
                response.StatusMessage = "update success";

            }
            else
            {
                response.StatusCode = 400;
                response.StatusMessage = "failed";
            }
            return response;
        }

        //add to cart
        public Response Addtocart(Cart cart,SqlConnection connection) 
        {
            Response response = new Response();
            SqlCommand cmd = new SqlCommand("sp_AddtoCart", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Userid", cart.Userid);
            cmd.Parameters.AddWithValue("@Productid", cart.Productid);
            cmd.Parameters.AddWithValue("@price", cart.price);
            cmd.Parameters.AddWithValue("@Quantity", cart.Quantity);
            cmd.Parameters.AddWithValue("@status", cart.status);
            cmd.Parameters.AddWithValue("@discount", cart.discount);
            cmd.Parameters.AddWithValue("@TotalPrice", cart.TotalPrice);
            connection.Open();
            int i = cmd.ExecuteNonQuery();
            connection.Close();
            if (i > 0)
            {
                response.StatusCode = 200;
                response.StatusMessage = "item add success";
            }
            else
            {
                response.StatusCode = 400;
                response.StatusMessage = "item could not be added";
            }
            return response;
        }

        //add product to category (admin)
        public Response AddToCategory(Categories category, SqlConnection connection)
        {
            Response response = new Response();
            SqlCommand cmd = new SqlCommand("sp_AddtoCategory", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@CategoryId", category.CategoryId);
            cmd.Parameters.AddWithValue("@Name", category.Name);
            cmd.Parameters.AddWithValue("@ProductID", category.ProductID);
            cmd.Parameters.AddWithValue("@type", category.Type);
            cmd.Parameters.AddWithValue("@imageurl", category.ImageURL);
            connection.Open();
            int i = cmd.ExecuteNonQuery();
            connection.Close();
            if (i > 0)
            {
                response.StatusCode = 200;
                response.StatusMessage = "product added to category successfully";
            }
            else
            {
                response.StatusCode = 400;
                response.StatusMessage = "product couldn't be added to category";
            }
            return response;
        }

        //PLACE ORDER
        public Response PlaceOrder(Users users,SqlConnection connection)
        {
            Response response= new Response();
            SqlCommand cmd = new SqlCommand("sp_PlaceOrder", connection);
            cmd.CommandType= CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ID", users.Id);
            connection.Open ();
            int i = cmd.ExecuteNonQuery();
            connection.Close();
            if (i > 0)
            {
                response.StatusCode = 200;
                response.StatusMessage = "order place success";
            }
            else
            {
                response.StatusCode = 400;
                response.StatusMessage = "order place failed";
            }
            return response;
        }

        //order list
        public Response OrderList(Users users,SqlConnection connection)
        {
            Response response= new Response();
            List<Orders> ListOrder = new List<Orders>();
            SqlDataAdapter da = new SqlDataAdapter("sb_OrderList", connection);
            da.SelectCommand.CommandType= CommandType.StoredProcedure;
            da.SelectCommand.Parameters.AddWithValue("@Type", users.Type);
            da.SelectCommand.Parameters.AddWithValue("@ID", users.Id);
            DataTable dt= new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                for(int i=0; i<dt.Rows.Count; i++)
                {
                    Orders order = new Orders();
                    order.OrderId = Convert.ToInt32(dt.Rows[i]["ID"]);
                    order.OrderNo = Convert.ToString(dt.Rows[i]["OrderNo"]);
                    order.OrderTotal = Convert.ToDecimal(dt.Rows[i]["OrderTotal"]);
                    order.OrderStatus = Convert.ToString(dt.Rows[i]["OrderTotal"]);
                    ListOrder.Add(order);
                }
                if(ListOrder.Count > 0)
                {
                    response.StatusCode = 200;
                    response.StatusMessage= "order added";
                    response.ListofOrders = ListOrder; ;
                }
                else
                {
                    response.StatusCode = 400;
                    response.StatusMessage = "order details are not available";
                    response.ListofOrders = null;
                }
            }
            else
            {
                response.StatusCode=400;
                response.StatusMessage = "order details are not available";
                response.ListofOrders = null;
            }
            return response;
        }

        //category list
        public Response CategoryList(Products products,SqlConnection connection)
        {
            Response response = new Response();
            List<Categories> listcategories = new List<Categories>();
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.AddWithValue("@ProductId", products.ProductId);
            da.SelectCommand.Parameters.AddWithValue("@Type", products.type);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    Categories category = new Categories();
                    category.CategoryId = Convert.ToInt32(dt.Rows[i]["ID"]);
                    category.Name = Convert.ToString(dt.Rows[i]["Name"]);
                    category.Type = Convert.ToString(dt.Rows[i]["Type"]);
                    category.Description = Convert.ToString(dt.Rows[i]["Description"]);
                    category.ImageURL = Convert.ToString(dt.Rows[i]["imageurl"]);

                    listcategories.Add(category);
                }
                if (listcategories.Count > 0)
                {
                    response.StatusCode = 200;
                    response.StatusMessage = "category created";
                    response.Listofcategory = listcategories;
                }
                else
                {
                    response.StatusCode = 400;
                    response.StatusMessage = "category couldnt be created";
                    response.Listofcategory = null;
                }

            }
            else
            {
                response.StatusCode=400;
                response.StatusMessage = "order details are not available";
                response.ListofOrders = null;
            }
            return response;
        }

        //userlist

        public Response UserList( SqlConnection connection)
        {
            Response response = new Response();
            List<Users> ListUsers = new List<Users>();
            SqlDataAdapter da = new SqlDataAdapter("sb_UserList", connection);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
          
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    Users user = new Users();
                   user.Id = Convert.ToInt32(dt.Rows[i]["ID"]);
                   user.Name = Convert.ToString(dt.Rows[i]["Name"]); 
                   user.Email = Convert.ToString(dt.Rows[i]["Email"]);
                   user.Password = Convert.ToString(dt.Rows[i]["Password"]);
                   user.Username = Convert.ToString(dt.Rows[i]["Username"]);
                   user.Type = Convert.ToString(dt.Rows[i]["Type"]);

                    ListUsers.Add(user);
                }
                if (ListUsers.Count > 0)
                {
                    response.StatusCode = 200;
                    response.StatusMessage = "user added";
                    response.ListofUsers = ListUsers;
                   
                }
                else
                {
                    response.StatusCode = 400;
                    response.StatusMessage = "user details are not available";
                    response.ListofOrders = null;
                }
            }
            else
            {
                response.StatusCode = 400;
                response.StatusMessage = "user details are not available";
                response.ListofOrders = null;
            }
            return response;
        }


    // add or update products
        public Response AddUpdateProducts (Products products,SqlConnection connection)
        {
            Response response = new Response();
            SqlCommand cmd = new SqlCommand("sp_AddUpdateProducts", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ProductId", products.ProductId);
            cmd.Parameters.AddWithValue("@name", products.Name);
            cmd.Parameters.AddWithValue("@Price", products.Price);
            cmd.Parameters.AddWithValue("@Description", products.Description);
            cmd.Parameters.AddWithValue("@type", products.type);
            cmd.Parameters.AddWithValue("@Quantity", products.Quantity);
            cmd.Parameters.AddWithValue("@imageUrl", products.imageUrl);
            cmd.Parameters.AddWithValue("@discount", products.discount);
            cmd.Parameters.AddWithValue("@status", products.status);

            connection.Open();
            int i = cmd.ExecuteNonQuery();
            connection.Close();

            if (i > 0)
            {
                response.StatusCode = 200;
                response.StatusMessage = "product inserted with success";
            }
            else
            {
                response.StatusCode = 400;
                response.StatusMessage = "product not inserted, mn tani ya 3bdooo ";
            }
            return response;
        }

        public Response AddUpdateCategories(Categories categories,SqlConnection connection)
        {
            Response response = new Response();
            SqlCommand cmd = new SqlCommand("sp_AddUpdateProducts", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@CategoryId", categories.CategoryId);
            cmd.Parameters.AddWithValue("@Name", categories.Name);
            cmd.Parameters.AddWithValue("@Description", categories.Description);
            cmd.Parameters.AddWithValue("@ProductId", categories.ProductID);
            cmd.Parameters.AddWithValue("@type", categories.Type);
            cmd.Parameters.AddWithValue("@ImageURL", categories.ImageURL);
            connection.Open() ;
            int i = cmd.ExecuteNonQuery();
            connection.Close();
            if (i > 0)
            {
                response.StatusCode = 200;
                response.StatusMessage = "category created successfully";
            }
            else
            {
                response.StatusCode = 400;
                response.StatusMessage = "category creation failed";
            }
            return response;


        }
    }
}
