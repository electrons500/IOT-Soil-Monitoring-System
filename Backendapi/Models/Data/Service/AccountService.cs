using Backendapi.Models.Data;
using Backendapi.Models.Data.ApiModel;
using Backendapi.Models.Data.IOTSMSDBContext;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;

namespace Backendapi.Models.Data.Service
{
    public class AccountService
    {
        private IOTSMSDBContext.IOTSMSDBContext _db;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ILogger<ApplicationUser> _logger;
        private IConfiguration _Configuration;
        private GenderService _GenderService;
        private RegionService _RegionService;
        public AccountService(IOTSMSDBContext.IOTSMSDBContext db, UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager,
            SignInManager<ApplicationUser> signInManager,
            ILogger<ApplicationUser> logger
             , IConfiguration configuration
           , GenderService genderService, RegionService regionService)
        {
            _db = db;
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
            _logger = logger;
            _Configuration = configuration;
            _GenderService = genderService;
            _RegionService = regionService;
        }

        public void InsertRegionsIntoDB()
        {
            try
            {
                //Incase there is no value in Region Table in DB then
                //create the values and store in the DB
                var region = _db.Region.Count();
                if (region == 0)
                {
                    List<Region> regions = new List<Region>()
                {

                        new Region() { RegionName = "Ahafo region" },
                        new Region() { RegionName = "Ashanti region" },
                        new Region() { RegionName = "Bono East region" },
                        new Region() { RegionName = "Bono region" },
                        new Region() { RegionName = "Central region" },
                        new Region() { RegionName = "Eastern region" },
                        new Region() { RegionName = "Greater Accra region" },
                        new Region() { RegionName = "Northern region" },
                        new Region() { RegionName = "North East region" },
                        new Region() { RegionName = "Oti Region" },
                        new Region() { RegionName = "Savannah region" },
                        new Region() { RegionName = "Upper East region" },
                        new Region() { RegionName = "Upper West region" },
                        new Region() { RegionName = "Volta region" },
                        new Region() { RegionName = "Western region" },
                        new Region() { RegionName = "Western North Region" }


                };

                    foreach (var items in regions)
                    {
                        Region regionsInGhana = new Region()
                        {
                            RegionName = items.RegionName
                        };
                        _db.Region.Add(regionsInGhana);
                        _db.SaveChanges();
                    }

                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void InsertGenderValuesIntoDB()
        {
            //Incase there is no value in Gender Table in DB then
            //create the values and store in the DB
            try
            {
                var genda = _db.Gender.Count();
                if (genda == 0)
                {
                    
                    Gender femaleGender = new Gender()
                    {
                        GenderName = "Female"
                    };
                    _db.Gender.Add(femaleGender);
                    _db.SaveChanges();
                    Gender maleGender = new Gender()
                    {
                        GenderName = "Male"
                    };
                    _db.Gender.Add(maleGender);
                    _db.SaveChanges();

                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        //Create Soil category
        public void InsertSoilCategoryIntoDB() 
        {
            try
            {
                //Incase there is no value in Region Table in DB then
                //create the values and store in the DB
                var soilcatgory = _db.SoilCategory.Count();
                if (soilcatgory == 0)
                {
                    List<SoilCategory> soilCategories = new List<SoilCategory>()
                {

                        new SoilCategory() { SoilName = "Sandy Soil" },
                        new SoilCategory() { SoilName = "Clay Soil" },
                        new SoilCategory() { SoilName = "Loamy Soil" },
                        


                };

                    foreach (var items in soilCategories)
                    {
                        SoilCategory soils = new SoilCategory()
                        {
                            SoilName = items.SoilName
                        };
                        _db.SoilCategory.Add(soils);
                        _db.SaveChanges();
                    }

                }
            }
            catch (Exception)
            {

                throw;
            }
        }


        //Create user roles if they donot in the DB
        public async Task GenerateRolesAsync()
        {

            var AdminRoleExist = await _roleManager.FindByNameAsync("Administrator");
            var AgricExtensionOfficerRoleExist = await _roleManager.FindByNameAsync("Agric Extension Officer");
            var NonAgricExtensionOfficerRoleExist = await _roleManager.FindByNameAsync("Non-Agric Entension Officer");

            if (AdminRoleExist == null)
            {
                var CreateAdmin = _roleManager.CreateAsync(new IdentityRole("Administrator")).Result;
            }
            if (AgricExtensionOfficerRoleExist == null)
            {
                var CreateAgricExtensionOfficer = _roleManager.CreateAsync(new IdentityRole("Agric Extension Officer")).Result;
            }
            if (NonAgricExtensionOfficerRoleExist == null)
            {
                var CreateNonAgricExtensionOfficer = _roleManager.CreateAsync(new IdentityRole("Non-Agric Extension Officer")).Result;
            }


        }

        public async Task<IdentityResult> RegisterUserAsync(AccountRegisterApiModel model, int id)
        {
           //Get username from email address
            MailAddress address = new MailAddress(model.EmailAdress);
            var userName = address.User;
            DateTime lockoutendDate = Convert.ToDateTime("1/1/0001");
            var newUser = new ApplicationUser
            {
                LastName = model.LastName,
                MiddleName = model.MiddleName,
                FirstName = model.FirstName,
                FullName = $"{model.FirstName} {model.MiddleName} {model.LastName}",
                BirthDate = model.BirthDate,
                GenderId = model.GenderId,
                HomeTown = model.HomeTown,
                RegionId = model.RegionId,
                Residence = model.Residence,
                Address = model.Address,
                PhoneNumber = model.ContactNumber,
                Email = model.EmailAdress,
                ProfilePic = model.ProfilePic,
                LockoutEnd = lockoutendDate,
                LockoutEnabled = false,
                UserName = userName,
                RegistrationDate = Convert.ToDateTime(DateTime.Now.ToShortDateString())

            };

            var result = await _userManager.CreateAsync(newUser, model.Password);

            if (result.Succeeded)
            {
                //After creating new account,get user id
                string UserId = newUser.Id;
                
                if (id == 1)
                {
                    
                    await _userManager.AddToRoleAsync(newUser, "Agric Extension Officer");
                }
                else if (id == 2)
                {
                    
                    await _userManager.AddToRoleAsync(newUser, "Non-Agric Extension Officer");
                }
                else
                {
                    await _userManager.AddToRoleAsync(newUser, "Administrator");
                }

            }

            return result;


        }

        public async Task<SignInResult> UserloginAsync(LoginApiModel model)
        {
            var userName = model.EmailAdress;
            MailAddress mailAddress = new MailAddress(model.EmailAdress);

            userName = mailAddress.User;

            bool RememberMe = true;
            var result = await _signInManager.PasswordSignInAsync(userName, model.Password, RememberMe, lockoutOnFailure: false);

            return result;

        }


        public async Task LogOutAsync()
        {
           await _signInManager.SignOutAsync();

            
        }


        //update user profile using default picture
        public void UpdateUserProfile(string userId)
        {
           
            var users = _db.Users.Where(x => x.Id == userId).FirstOrDefault();

            DateTime lockoutendDate = Convert.ToDateTime("1/1/0001");
            users.LockoutEnd = lockoutendDate;
            users.RegistrationDate = Convert.ToDateTime(DateTime.Now.ToShortDateString());
            users.LockoutEnabled = false;

            _db.Users.Update(users);
            _db.SaveChanges();
             
        }


       

        //Get user who are administrator
        public List<UserRoleApiModel> GetUserRoles(string rolename)
        {
            string connection = _Configuration.GetConnectionString("Conn");
            SqlConnection con = new SqlConnection(connection);
            List<UserRoleApiModel> model = new List<UserRoleApiModel>();
            string sql = " SELECT  UserRoles.UserId,Users.FullName,Users.PhoneNumber, UserRoles.RoleId, Role.Name FROM Role INNER JOIN UserRoles ON Role.Id = UserRoles.RoleId INNER JOIN Users ON UserRoles.UserId = Users.Id where Role.Name ='"+ rolename +"'";
            using SqlCommand cmd = new SqlCommand(sql, con);
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

            return model;
        }


        public UsersApiModel GetUsersDetails(string userId)
        {
            ApplicationUser users = _db.Users.Where(x => x.Id == userId)
                                             .Include(x => x.Gender)
                                             .Include(x => x.Region)
                                             .FirstOrDefault();
            UsersApiModel model = new UsersApiModel()
            {
                UserId = users.Id,
                FirstName = users.FirstName,
                MiddleName = users.MiddleName,
                LastName = users.LastName,
                BirthDate = users.BirthDate,
                GenderId = users.GenderId,
                GenderName = users.Gender.GenderName,
                RegionId = users.RegionId,
                RegionName = users.Region.RegionName,
                HomeTown = users.HomeTown,
                Residence = users.Residence,
                Address = users.Address,
                PhoneNumber = users.PhoneNumber,
                Email = users.Email,
                ProfilePic = users.ProfilePic,
                RegistrationDate = users.RegistrationDate
                
            };

            return model;

        }
        
        public UserAccountDetailsApiModel LoadUserAccountDetailsAsync(ApplicationUser user)
        {
          
            var FirstName = user.FirstName;
            var LastName = user.LastName;
            var MiddleName = user.MiddleName;
            var BirthDate = user.BirthDate;
            var GenderId = user.GenderId;
            var HomeTown = user.HomeTown;
            var RegionId = user.RegionId;
            var Residence = user.Residence;
            var Address = user.Address;
           
            var ContactNumber = user.PhoneNumber;
            var ProfilePic = user.ProfilePic;


            UserAccountDetailsApiModel model = new UserAccountDetailsApiModel()
            {
             
                FirstName = FirstName,
                LastName = LastName,
                MiddleName = MiddleName,
                BirthDate = BirthDate,
                GenderId = GenderId,               
                HomeTown = HomeTown,
                RegionId = RegionId,
                Residence = Residence,
                Address = Address,
                ContactNumber = ContactNumber,
                UserPhoto = ProfilePic


            };

            return model;
        }

        public bool UpdateUserAccountAsync(UserAccountDetailsApiModel model, string userId)
        {
            byte[] imageByte = model.UserPhoto;
            ApplicationUser user = _db.Users.Where(x => x.Id == userId).FirstOrDefault();
            if (imageByte.Length > 0) //content has a picture
            {          
                user.FirstName = model.FirstName;
                user.MiddleName = model.MiddleName;
                user.LastName = model.LastName;
                user.FullName = $"{model.FirstName} {model.MiddleName} {model.LastName}";
                user.BirthDate = (DateTime)model.BirthDate;
                user.GenderId = model.GenderId;
                user.HomeTown = model.HomeTown;
                user.RegionId = model.RegionId;
                user.Residence = model.Residence;
                user.Address = model.Address;
                user.PhoneNumber = model.ContactNumber;
                user.ProfilePic = model.UserPhoto;

                _db.Users.Update(user);
                _db.SaveChanges();

                return true;
            }
            else
            {

                user.FirstName = model.FirstName;
                user.MiddleName = model.MiddleName;
                user.LastName = model.LastName;
                user.FullName = $"{model.FirstName} {model.MiddleName} {model.LastName}";
                user.BirthDate = (DateTime)model.BirthDate;
                user.GenderId = model.GenderId;
                user.HomeTown = model.HomeTown;
                user.RegionId = model.RegionId;
                user.Residence = model.Residence;
                user.Address = model.Address;
                user.PhoneNumber = model.ContactNumber;

                _db.Users.Update(user);
                _db.SaveChanges();

                return true;
            }



         

        }


    }
}
