using EventProjectSWP.DTOs;
using EventProjectSWP.Models;
using EventProjectSWP.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace EventProjectSWP.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    
    public class AdminController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IAuthentication _authentication;

        public AdminController(IConfiguration configuration, IAuthentication authentication)
        {
            _configuration = configuration;
            _authentication = authentication;
        }
        //lấy danh sách các admin 
        [HttpGet("get-list-admin")]
        public IActionResult Get()
        {
            try
            {
                string query = @"select admin_id , admin_name, admin_phone , admin_email, admin_password, admin_role from dbo.tblAdmin";

                DataTable table = new DataTable();
                string sqlDataSource = _configuration.GetConnectionString("EventAppConn");
                SqlDataReader myReader;
                using (SqlConnection myCon = new SqlConnection(sqlDataSource))
                {
                    myCon.Open();
                    using (SqlCommand myCommand = new SqlCommand(query, myCon))
                    {
                        myReader = myCommand.ExecuteReader();
                        table.Load(myReader);
                        myReader.Close();
                        myCon.Close();

                    }
                }
                if (table.Rows.Count > 0)
                {
                    return Ok(new Response<DataTable>(table));
                }
                return BadRequest(new Response<string>("No Data"));
            }catch (Exception ex)
            {
                return BadRequest(new Response<string>(ex.Message));
            }        
        }

        [HttpPost("Add-Admin")]
        public IActionResult AddAdmin(AddAdmin addAdmin)
        {
            try
            {
                string query = @"insert into tblAdmin values(@admin_name,@admin_phone,@admin_email,@admin_password,@admin_role)";
                string sqlDataSource = _configuration.GetConnectionString("EventAppConn");
                SqlDataReader myReader;
                using (SqlConnection myCon = new SqlConnection(sqlDataSource))
                {
                    myCon.Open();
                    using (SqlCommand myCommand = new SqlCommand(query, myCon))
                    {
                        myCommand.Parameters.AddWithValue("@admin_name", addAdmin.adminName);
                        myCommand.Parameters.AddWithValue("@admin_phone", addAdmin.adminPhone);
                        myCommand.Parameters.AddWithValue("@admin_email", addAdmin.adminEmail);
                        myCommand.Parameters.AddWithValue("@admin_password", addAdmin.adminPassword);
                        myCommand.Parameters.AddWithValue("@admin_role", addAdmin.adminRole);
                        myReader = myCommand.ExecuteReader();
                        myReader.Close();
                        myCon.Close();
                    }
                }
                return Ok("Add Successfully");
            }
            catch (Exception ex)
            {
                return BadRequest(new Response<string>(ex.Message));
            }

        }
        //update thông tin admin
        [HttpPut("update-admin")]
        public IActionResult Put(UpdateAdmin updateAdmin, int adminId)
        {
            try
            {
                string query = @"update dbo.tblAdmin set admin_name =@admin_name,admin_phone=@admin_phone,admin_password=@admin_password where admin_id =@admin_id";
                string sqlDataSource = _configuration.GetConnectionString("EventAppConn");
                SqlDataReader myReader;
                using (SqlConnection myCon = new SqlConnection(sqlDataSource))
                {
                    myCon.Open();
                    using (SqlCommand myCommand = new SqlCommand(query, myCon))
                    {
                        myCommand.Parameters.AddWithValue("@admin_name", updateAdmin.adminName);
                        myCommand.Parameters.AddWithValue("@admin_phone", updateAdmin.adminPhone);
                        myCommand.Parameters.AddWithValue("@admin_password", updateAdmin.adminPassword);
                        myCommand.Parameters.AddWithValue("@admin_id", adminId);
                        myReader = myCommand.ExecuteReader();
                        myReader.Close();
                        myCon.Close();
                    }
                }
                    return Ok("Update Successfully");
            }
            catch(Exception ex)
            {
                return Ok(new Response<DataTable>(ex.Message));
            }
        }
        //tìm admin bằng id của admin
        [HttpGet("get-admin-by-id")]
        public IActionResult GetClubById(string id)
        {
            try
            {
                string query = @"select admin_name, admin_phone , admin_email from dbo.tblAdmin where admin_id = @admin_id";

                DataTable table = new DataTable();
                string sqlDataSource = _configuration.GetConnectionString("EventAppConn");
                SqlDataReader myReader;
                using (SqlConnection myCon = new SqlConnection(sqlDataSource))
                {
                    myCon.Open();
                    using (SqlCommand myCommand = new SqlCommand(query, myCon))
                    {
                        myCommand.Parameters.AddWithValue("@admin_id", id);
                        myReader = myCommand.ExecuteReader();
                        table.Load(myReader);
                        myReader.Close();
                        myCon.Close();

                    }
                }
                if (table.Rows.Count > 0)
                {
                    return Ok(new Response<DataTable>(table));
                }
                return BadRequest(new Response<string>("No Data"));
            }catch (Exception ex)
            {
                return BadRequest(new Response<string>(ex.Message));
            }
            
        }
        //tìm admin bằng tên admin 
        [HttpGet("get-admin-by-name")]
        public async Task<IActionResult> GetClubByName(string name)
        {
            try
            {
                string query = @"select admin_name, admin_phone , admin_email from dbo.tblAdmin where admin_name like concat (@admin_name, '%')";
                DataTable table = new DataTable();
                string sqlDataSource = _configuration.GetConnectionString("EventAppConn");
                SqlDataReader myReader;
                using (SqlConnection myCon = new SqlConnection(sqlDataSource))
                {
                    myCon.Open();
                    using (SqlCommand myCommand = new SqlCommand(query, myCon))
                    {
                        myCommand.Parameters.AddWithValue("@admin_name", name);
                        myReader = myCommand.ExecuteReader();
                        table.Load(myReader);
                        myReader.Close();
                        myCon.Close();

                    }
                }
                if (table.Rows.Count > 0)
                {
                    return Ok(new Response<DataTable>(table));
                }
                return BadRequest(new Response<string>("No Data"));
            }
            catch (Exception ex)
            {
                return BadRequest(new Response<string>(ex.Message));
            }

        }
        //Login cho admin bằng tài khoản mật khẩu
        [HttpPost("login-admin")]
        public async Task<IActionResult> loginAdmin(LoginAdmin loginAdmin)
        {
            string query = @"select * from tblAdmin where admin_email = @admin_email and admin_password=@admin_password";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("EventAppConn");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@admin_email", loginAdmin.adminMail);
                    myCommand.Parameters.AddWithValue("@admin_password", loginAdmin.adminPassword);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();

                }
            }
            if (table.Rows.Count == 0)
            {
                return BadRequest(new Response<string>("invalid-email-or-password"));
            }

            Admin admin = new Admin()
            {
                adminID = Convert.ToInt32(table.Rows[0]["admin_id"]),
                adminEmail = table.Rows[0]["admin_email"].ToString(),
                adminName = table.Rows[0]["admin_name"].ToString(),
                adminRole = table.Rows[0]["admin_role"].ToString(),
            };
            var accessToken = await _authentication.GenerateTokenAdmin(admin);
            return Ok(new Response<string>(accessToken, null));
        }


        [HttpPut("Check attend")]
        public IActionResult CheckAttend(bool status, CheckAttendance checkAttend)
        {
            try
            {
                DataTable table = new DataTable();
                string query = @"update tblEventParticipated set users_status = @users_status where event_id = @event_id and users_id = @users_id";
                string sqlDataSource = _configuration.GetConnectionString("EventAppConn");
                SqlDataReader myReader;
                using (SqlConnection myCon = new SqlConnection(sqlDataSource))
                {
                    myCon.Open();
                    using (SqlCommand myCommand = new SqlCommand(query, myCon))
                    {
                        myCommand.Parameters.AddWithValue("@event_id", checkAttend.eventID);
                        myCommand.Parameters.AddWithValue("@users_id", checkAttend.userID);
                        myCommand.Parameters.AddWithValue("@users_status", status);
                        myReader = myCommand.ExecuteReader();
                        myReader.Close();
                        myCon.Close();

                    }
                }
                    return Ok(new Response<string>("Check Attend Successfully"));
            }
            catch (Exception ex)
            {
                return BadRequest(new Response<string>(ex.Message));
            }           
        }




        /*  [HttpPut("Check attend")]
          public JsonResult CheckAttend(EventParticipated user, bool status)
          {
              string query = @"update tblEventParticipated set users_status = @users_status where event_id = @event_id, users_id = @users_id";
              DataTable table = new DataTable();
              string sqlDataSource = _configuration.GetConnectionString("EventAppConn");
              SqlDataReader myReader;
              using (SqlConnection myCon = new SqlConnection(sqlDataSource))
              {
                  myCon.Open();
                  using (SqlCommand myCommand = new SqlCommand(query, myCon))
                  {

                      myCommand.Parameters.AddWithValue("@users_id", user.UserID);
                      myCommand.Parameters.AddWithValue("@users_status", status);
                      myReader = myCommand.ExecuteReader();
                      myReader.Close();
                      myCon.Close();

                  }
              }
              return new JsonResult("Check attend success");
          }*/


        //Điểm danh người tham gia event bằng event id và user id
        [HttpPut("Check attend")]
            public JsonResult CheckAttend(bool status, CheckAttendance checkAttend)
            //public JsonResult CheckAttend(EventParticipated ev)
        {
            try
            {
                string query = @"select users_status from tblEventParticipated where users_id = @users_id  and event_id= @event_id";

                DataTable table = new DataTable();
                string sqlDataSource = _configuration.GetConnectionString("EventAppConn");
                SqlDataReader myReader;
                using (SqlConnection myCon = new SqlConnection(sqlDataSource))
                {
                    myCon.Open();
                    using (SqlCommand myCommand = new SqlCommand(query, myCon))
                    {
                        myCommand.Parameters.AddWithValue("@users_id", userId);
                        myCommand.Parameters.AddWithValue("@event_id", eventId);
                        myReader = myCommand.ExecuteReader();
                        table.Load(myReader);
                        myReader.Close();
                        myCon.Close();
                    }
                }
                if (table.Rows.Count > 0)
                {
                    return Ok(new Response<DataTable>(table));
                }
                return BadRequest(new Response<string>("No Data"));
            }
            catch (Exception ex)
            {
                return BadRequest(new Response<string>(ex.Message));
            }

        }



    }
}
