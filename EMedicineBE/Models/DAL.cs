


using System.Data.SqlClient;
using System.Data;
using EMedicineBE.Modelsget;

namespace EMedicineBE.Models
{
    public class DAL
    {
        public Response Register(Users users,SqlConnection connection)
        { 
            Response response = new Response();
            SqlCommand cmd = new SqlCommand("sp_register",connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@FirstName", users.FirstName);
            cmd.Parameters.AddWithValue("@LastName", users.LastName);
            cmd.Parameters.AddWithValue("@Password", users.Password);
            cmd.Parameters.AddWithValue("@Email", users.Email);
            cmd.Parameters.AddWithValue("@Fund",0);
            cmd.Parameters.AddWithValue("@Type", "Users");
            cmd.Parameters.AddWithValue("@Status","pending");
            connection.Open();
            int i= cmd.ExecuteNonQuery();
            connection.Close();
            if (i > 0)
            {
                response.StatusCode = 200;
                response.StatusMessage = "Üser registered successfully";
            }
            else 
            {
                response.StatusCode = 100;
                response.StatusMessage = "Üser registration failed";
            }
            return response;
        }

        public Response Login(Users users, SqlConnection connection)
        { 
            SqlDataAdapter da= new SqlDataAdapter("sp_login",connection);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.AddWithValue("@Email", users.Email);
            da.SelectCommand.Parameters.AddWithValue("@Password", users.Password);
            DataTable dt= new DataTable();
            da.Fill(dt);
            Response response=new Response();
            Users user = new Users();
            if (dt.Rows.Count > 0)
            {
                user.ID = Convert.ToInt32(dt.Rows[0]["ID"]);
                user.FirstName = Convert.ToString(dt.Rows[0]["FirstName"]);
                user.LastName = Convert.ToString(dt.Rows[0]["LastName"]);
                user.Email= Convert.ToString(dt.Rows[0]["Email"]);
                user.Type = Convert.ToString(dt.Rows[0]["Type"]);
                response.StatusCode = 200;
                response.StatusMessage = "User is valid";
                response.user = user;
            }
            else
            {
                response.StatusCode = 100;
                response.StatusMessage = "User is invalid";
                response.user = null;
            }
            return response;
        }

        public Response ViewUser(Users users, SqlConnection connection) 
        { 
            SqlDataAdapter da= new SqlDataAdapter("p_viewUser",connection);
            da.SelectCommand.CommandType= CommandType.StoredProcedure;
            da.SelectCommand.Parameters.AddWithValue("@ID", users.ID);
            DataTable dt=new DataTable();
            da.Fill(dt);
            Response response=new Response();
            Users user = new Users();
            if (dt.Rows.Count > 0)
            {
                user.ID = Convert.ToInt32(dt.Rows[0]["ID"]);
                user.FirstName = Convert.ToString(dt.Rows[0]["FirstName"]);
                user.LastName = Convert.ToString(dt.Rows[0]["LastName"]);
                user.Email = Convert.ToString(dt.Rows[0]["Email"]);
                user.Type = Convert.ToString(dt.Rows[0]["Type"]);
                user.Fund = Convert.ToDecimal(dt.Rows[0]["Fund"]);
                user.CreatedOn = Convert.ToDateTime(dt.Rows[0]["CreatedOn"]);
                user.Password = Convert.ToString(dt.Rows[0]["Password"]);
                response.StatusCode = 200;
                response.StatusMessage = "User is exists.";
            }
            else
            {
                response.StatusCode = 100;
                response.StatusMessage = "User does not exist.";
                response.user=user;
            }
            return response;
        }
        public Response UpdateProfile(Users users,SqlConnection connection) 
        { 
            Response response = new Response();

            SqlCommand cmd = new SqlCommand("sp_UpdateProfile", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@FirstName",users.FirstName);
            cmd.Parameters.AddWithValue("@LastName", users.LastName);
            cmd.Parameters.AddWithValue("@Password", users.Password);
            cmd.Parameters.AddWithValue("@Email", users.Email);
            connection.Open();
            int i=cmd.ExecuteNonQuery();
            connection.Close();
            if (i > 0)
            { 
                response.StatusCode = 200;
                response.StatusMessage = "Record updated successfully";
            }
            else 
            {
                response.StatusCode = 100;
                response.StatusMessage = "Some error occured. Try after sometime";
            }


            return response;

        }
        public Response AddToCart(Cart cart, SqlConnection connection) 
        { 
            Response response = new Response();
            SqlCommand cmd = new SqlCommand("sp_AddToCart",connection);
            cmd.CommandType= CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserID",cart.UserID);
            cmd.Parameters.AddWithValue("@MedicineID", cart.MedicineID);
            cmd.Parameters.AddWithValue("@UnitPrice", cart.UnitPrice);
            cmd.Parameters.AddWithValue("@Discount", cart.Discount);
            cmd.Parameters.AddWithValue("@Quantity", cart.Quantity);
            cmd.Parameters.AddWithValue("@TotalPrice", cart.TotalPrice);
            connection.Open();
            int i=cmd.ExecuteNonQuery();
            connection.Close();
            if (i > 0)
            {
                response.StatusCode = 200;
                response.StatusMessage = "Item added successfully";
            }
            else
            { 
                response.StatusCode = 100;
                response.StatusMessage = "Item could not be added";
            }
            return response;

        }
        public Response PlaceOrder(Users users, SqlConnection connection)
        {
            Response response = new Response();
            SqlCommand cmd = new SqlCommand("sp_PlaceOrder", connection);
            cmd.CommandType= CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ID", users.ID);
            connection.Open();
            int i=cmd.ExecuteNonQuery();
            connection.Close(); 
            if (i > 0)
            {
                response.StatusCode = 200;
                response.StatusMessage = "Order has been placed successfully";
            }
            else
            { 
                response.StatusCode = 100;
                response.StatusMessage = "Order could not be placed";
            }
            return response;  
        }
        public Response OrderList(Users users, SqlConnection connection)
        { 
            Response response = new Response();
            List<Orders> listOrder = new List<Orders>();
            SqlDataAdapter da=new SqlDataAdapter("sp_OrderList",connection);
            da.SelectCommand.CommandType= CommandType.StoredProcedure;
            da.SelectCommand.Parameters.AddWithValue("@Type", users.Type);
            da.SelectCommand.Parameters.AddWithValue("@ID", users.ID);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    Orders order = new Orders();
                    order.ID = Convert.ToInt32(dt.Rows[i]["ID"]);
                    order.OrderNo = Convert.ToString(dt.Rows[i]["OrderNo"]);
                    order.OrderTotal = Convert.ToDecimal(dt.Rows[i]["OrderTotal"]);
                    order.OrderStatus = Convert.ToString(dt.Rows[i]["OrderStatus"]);
                    listOrder.Add(order);
                }
                if (listOrder.Count > 0)
                {
                    response.StatusCode = 200;
                    response.StatusMessage = "Order details fetched";
                    response.listOrders = listOrder;
                }
                else 
                {
                    response.StatusCode = 100;
                    response.StatusMessage = "Order details are not available";
                    response.listOrders = null;
                }
            }
            else
            {
                response.StatusCode = 100;
                response.StatusMessage = "Order details are not available";
                response.listOrders = null;
            }
            return response;

        }
        public Response AddUpdateMedicine(Medicine medicine, SqlConnection connection)

        { 
            Response response = new Response();
            SqlCommand cmd = new SqlCommand("sp_AddUpdateMedicine", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Name", medicine.Name);
            cmd.Parameters.AddWithValue("@Manufacturer", medicine.Manufacturer);
            cmd.Parameters.AddWithValue("@UnitPrice", medicine.UnitPrice);
            cmd.Parameters.AddWithValue("@Discount", medicine.Discount);
            cmd.Parameters.AddWithValue("@Quantity", medicine.Quantity);
            cmd.Parameters.AddWithValue("@ExpDate", medicine.ExpDate);
            cmd.Parameters.AddWithValue("@ImageUrl", medicine.ImageUrl);
            cmd.Parameters.AddWithValue("@Status", medicine.Status);
            cmd.Parameters.AddWithValue("@Type", medicine.Type);
            connection.Open();
            int i = cmd.ExecuteNonQuery();
            connection.Close();
            if (i > 0)
            {
                response.StatusCode = 200;
                response.StatusMessage = "Medicine inserted successfully";
            }
            else 
            {
                response.StatusCode = 100;
                response.StatusMessage = "Medicine did not save. Try again.";
            }
            return response;

        }
        public Response UserList( SqlConnection connection)
        {
            Response response = new Response();
            List<Users> listUsers = new List<Users>();
            SqlDataAdapter da = new SqlDataAdapter("sp_UserList", connection);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    Users user= new Users();
                    user.ID = Convert.ToInt32(dt.Rows[i]["ID"]);
                    user.FirstName = Convert.ToString(dt.Rows[i]["FirstName"]);
                    user.LastName = Convert.ToString(dt.Rows[i]["LastName"]);
                    user.Password = Convert.ToString(dt.Rows[i]["Password"]);
                    user.Email = Convert.ToString(dt.Rows[i]["Email"]);
                    user.Fund = Convert.ToDecimal(dt.Rows[i]["Fund"]);
                    user.Status = Convert.ToInt32(dt.Rows[i]["Status"]);
                    user.CreatedOn = Convert.ToDateTime(dt.Rows[i]["CreatedOn"]);
                    listUsers.Add(user);
                }
                if (listUsers.Count > 0)
                {
                    response.StatusCode = 200;
                    response.StatusMessage = "User details fetched";
                    response.listUsers = listUsers;
                }
                else
                {
                    response.StatusCode = 100;
                    response.StatusMessage = "User details are not available";
                    response.listUsers = null;
                }
            }
            else
            {
                response.StatusCode = 100;
                response.StatusMessage = "User details are not available";
                response.listUsers = null;
            }
            return response;

        }

    }
}
