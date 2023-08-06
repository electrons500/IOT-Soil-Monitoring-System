using Backendapi.Models.Data.ApiModel;
using Backendapi.Models.Data.IOTSMSDBContext;
using Microsoft.AspNetCore.Identity;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;


namespace Backendapi.Models.Data.Service
{
    public class SettingsService
    {
        private IOTSMSDBContext.IOTSMSDBContext _Context;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private IConfiguration _Configuration;
        public SettingsService(IOTSMSDBContext.IOTSMSDBContext context, RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager, IConfiguration configuration)
        {
            _Context = context;
            _roleManager = roleManager;
            _userManager = userManager;
            _Configuration = configuration;
        }

        //Number of Officers

        public string GetNumberOfExtensionOfficers()
        {
            var officerCount = GetOfficers().Count();
            return officerCount.ToString();
        }

        ///Number of devices
        
        public int GetNumberOfRegisteredDevices()
        {
            return _Context.Arduino.Count(); ;
        }

        public int GetNumberOfFarmers()
        {
           return _Context.Farmer.Count();
        }

        public int GetNumberOfFarms()
        {
            return _Context.Farm.Count();
        }

        public List<UserRoleApiModel> GetOfficers() 
        {
            string connection = _Configuration.GetConnectionString("Conn");
            SqlConnection con = new SqlConnection(connection);
            List<UserRoleApiModel> model = new List<UserRoleApiModel>();
            string sql = " SELECT  UserRoles.UserId,Users.FullName,Users.PhoneNumber, UserRoles.RoleId, Role.Name FROM Role INNER JOIN UserRoles ON Role.Id = UserRoles.RoleId INNER JOIN Users ON UserRoles.UserId = Users.Id where Role.Name ='Agric Extension Officer'  ";
            using (SqlCommand cmd = new SqlCommand(sql, con))
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        UserRoleApiModel userRole = new UserRoleApiModel()
                        {
                            RoleId = dr["RoleId"].ToString(),
                            RoleName = dr["Name"].ToString(),
                            UserId = dr["UserId"].ToString(),
                            FullName = dr["FullName"].ToString(),
                            Contact = dr["PhoneNumber"].ToString()

                        };


                        model.Add(userRole);
                    }


                    con.Close();

                }

            }

            return model;
        }

    }
}
