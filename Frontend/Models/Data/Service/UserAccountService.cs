using Frontend.Models.Data.ViewModel;
using Frontend.Models.Service;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Frontend.Models.Data.Service
{
    public class UserAccountService
    {
        private IOTSMSDBContext.IOTSMSDBContext _db;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ILogger<ApplicationUser> _logger;

        private IConfiguration _configuration;
        private readonly IHttpClientFactory _httpClientFactory;
        private RegionService _RegionService;
        private GenderService _GenderService;
        public UserAccountService(IOTSMSDBContext.IOTSMSDBContext db, UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager, SignInManager<ApplicationUser> signInManager,
            ILogger<ApplicationUser> logger,
            RegionService regionService,
             GenderService genderService, IConfiguration configuration
, IHttpClientFactory httpClientFactory)
        {
            _db = db;
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
            _logger = logger;
            _RegionService = regionService;
            _GenderService = genderService;
            _configuration = configuration;
            _httpClientFactory = httpClientFactory;
        }


        public AccountRegistrationViewModel CreateUsers()
        {
            AccountRegistrationViewModel model = new AccountRegistrationViewModel()
            {
                RegionList = new SelectList(_RegionService.GetRegions(), "RegionId", "RegionName"),
                GenderList = new SelectList(_GenderService.GetGenders(), "GenderId", "GenderName")
            };

            return model;
        }

        public bool AdminAccountRegistration(AccountRegistrationViewModel model)
        {
            string middlename = model.MiddleName;
         
            if (string.IsNullOrEmpty(model.MiddleName))
            {
                middlename = "";
            }


            UserAccountRegistrationModel account = new UserAccountRegistrationModel()
            {
                Id = "2",
                FirstName = model.FirstName,
                MiddleName = middlename,
                LastName = model.LastName,
                BirthDate = model.BirthDate,
                GenderId = model.GenderId,
                HomeTown = model.HomeTown,
                RegionId = model.RegionId,
                Residence = model.Residence,
                Address = model.Address,
                ProfilePic = File.ReadAllBytes(GetDefaultImagePath()),
                ContactNumber = model.ContactNumber,
                EmailAdress = model.EmailAdress,
                Password = model.Password,
                RegistrationDate = DateTime.Now

            };

            var httpClient = _httpClientFactory.CreateClient("backendapi");

            //Convert data into json by serializing it with NewtonSoft json package
            string SendingData = JsonConvert.SerializeObject(account);
            StringContent content = new StringContent(SendingData, Encoding.UTF8, "application/json");
            HttpResponseMessage responseMessage = httpClient.PostAsync(httpClient.BaseAddress + "/Account/AdminAccountRegistration", content).Result;
            if (responseMessage.IsSuccessStatusCode)
            {
                return true;
            }
            return false;
        }

        public bool AgricExtOfficerAccountRegistration(AccountRegistrationViewModel model)
        {
            string middlename = model.MiddleName;
           
            if (string.IsNullOrEmpty(model.MiddleName))
            {
                middlename = "";
            }

            UserAccountRegistrationModel account = new UserAccountRegistrationModel()
            {
                Id = "2",
                FirstName = model.FirstName,
                MiddleName = middlename,
                LastName = model.LastName,
                BirthDate = model.BirthDate,
                GenderId = model.GenderId,
                HomeTown = model.HomeTown,
                RegionId = model.RegionId,
                Residence = model.Residence,
                Address = model.Address,
                ProfilePic = File.ReadAllBytes(GetDefaultImagePath()),
                ContactNumber = model.ContactNumber,
                EmailAdress = model.EmailAdress,
                Password = model.Password,
                RegistrationDate = DateTime.Now

            };

            var httpClient = _httpClientFactory.CreateClient("backendapi");

            //Convert data into json by serializing it with NewtonSoft json package
            string SendingData = JsonConvert.SerializeObject(account);
            StringContent content = new StringContent(SendingData, Encoding.UTF8, "application/json");
            HttpResponseMessage responseMessage = httpClient.PostAsync(httpClient.BaseAddress + "/Account/AgricOfficerAccountRegistration", content).Result;
            if (responseMessage.IsSuccessStatusCode)
            {
                return true;
            }
            return false;
        }



        public async Task<SignInResult> UserLoginAsync(LoginViewModel model)
        {
            var userName = model.EmailAdress;

            if (IsValidEmail(model.EmailAdress))
            {
                MailAddress mailAddress = new MailAddress(model.EmailAdress);
                userName = mailAddress.User;

            }


            var result = await _signInManager.PasswordSignInAsync(userName, model.Password, model.RememberMe, lockoutOnFailure: false);

            return result;

        }

        public bool IsValidEmail(string email)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(email))
                    return false;

                try
                {
                    // Normalize the domain
                    email = Regex.Replace(email, @"(@)(.+)$", DomainMapper,
                                          RegexOptions.None, TimeSpan.FromMilliseconds(200));

                    // Examines the domain part of the email and normalizes it.
                    string DomainMapper(Match match)
                    {
                        // Use IdnMapping class to convert Unicode domain names.
                        var idn = new IdnMapping();

                        // Pull out and process domain name (throws ArgumentException on invalid)
                        string domainName = idn.GetAscii(match.Groups[2].Value);

                        return match.Groups[1].Value + domainName;
                    }
                }
                catch (RegexMatchTimeoutException)
                {
                    return false;
                }
                catch (ArgumentException)
                {
                    return false;
                }

                try
                {
                    return Regex.IsMatch(email,
                        @"^[^@\s]+@[^@\s]+\.[^@\s]+$",
                        RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
                }
                catch (RegexMatchTimeoutException)
                {
                    return false;
                }





            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task LogOutAsync()
        {
            await _signInManager.SignOutAsync();
        }



        public List<UserRoleViewModel> GetUserRoles(string rolename)
        {
            var httpClient = _httpClientFactory.CreateClient("backendapi");

            List<UserRoleViewModel> model = new List<UserRoleViewModel>();

            HttpResponseMessage responseMessage = httpClient.GetAsync(httpClient.BaseAddress + "/Account/GetUsersByRole/" + rolename).Result;
            if (responseMessage.IsSuccessStatusCode)
            {
                string ApiData = responseMessage.Content.ReadAsStringAsync().Result;
                //Deserialize the json data into the view Model List by using Newtonsoft json package
                model = JsonConvert.DeserializeObject<List<UserRoleViewModel>>(ApiData);

            }
            return model;
        }

        //Get user details
        public UsersViewModel GetUserDetails(string userId)
        {
            var httpClient = _httpClientFactory.CreateClient("backendapi");
            UsersViewModel model = new UsersViewModel();

            HttpResponseMessage responseMessage = httpClient.GetAsync(httpClient.BaseAddress + "/Account/GetUsersDetails/" + userId).Result;
            if (responseMessage.IsSuccessStatusCode)
            {
                string ApiData = responseMessage.Content.ReadAsStringAsync().Result;
                //Deserialize the json data into the view Model List by using Newtonsoft json package
                model = JsonConvert.DeserializeObject<UsersViewModel>(ApiData);

            }

            return model;
        }

        public AccountRegistrationViewModel LoadUserAccountDetailsAsync(ApplicationUser user)
        {
            var Id = user.Id;
            var FirstName = user.FirstName;
            var LastName = user.LastName;
            var MiddleName = user.MiddleName;
            var BirthDate = user.BirthDate;
            var GenderId = user.GenderId;
            var HomeTown = user.HomeTown;
            var RegionId = user.RegionId;
            var Residence = user.Residence;
            var Address = user.Address;
            var EmailAdress = user.Email;
            var ContactNumber = user.PhoneNumber;
            var ProfilePic = user.ProfilePic;


            AccountRegistrationViewModel model = new AccountRegistrationViewModel()
            {
                Id = Id,
                FirstName = FirstName,
                LastName = LastName,
                MiddleName = MiddleName,
                BirthDate = BirthDate,
                GenderId = GenderId,
                GenderList = new SelectList(_GenderService.GetGenders(), "GenderId", "GenderName"),
                HomeTown = HomeTown,
                RegionId = RegionId,
                RegionList = new SelectList(_RegionService.GetRegions(), "RegionId", "RegionName"),
                Residence = Residence,
                Address = Address,
                ContactNumber = ContactNumber,
                ProfilePic = ProfilePic


            };

            return model;
        }

        public bool UpdateUserAccountAsync(AccountRegistrationViewModel model)
        {
            string middleName = " ";

            if (model.ImageFormFile != null) //user inserted a picture  
            {
                using var stream = new MemoryStream();
                model.ImageFormFile.CopyTo(stream);
                model.ProfilePic = stream.ToArray(); 

            }
            
            if(model.MiddleName != null)
            {
                middleName = model.MiddleName;
            }

            UserDetailsSecondModel usersdata = new UserDetailsSecondModel()
            {
                Id = model.Id,
                FirstName = model.FirstName,
                MiddleName = middleName,
                LastName = model.LastName,
                BirthDate = (DateTime)model.BirthDate,
                GenderId = model.GenderId,
                HomeTown = model.HomeTown,
                RegionId = model.RegionId,
                Residence = model.Residence,
                Address = model.Address,
                ContactNumber = model.ContactNumber, 
                UserPhoto = model.ProfilePic

            };

            var httpClient = _httpClientFactory.CreateClient("backendapi");

            //Convert data into json by serializing it with NewtonSoft json package
            string SendingData = JsonConvert.SerializeObject(usersdata);
            StringContent content = new StringContent(SendingData, Encoding.UTF8, "application/json");
            HttpResponseMessage responseMessage = httpClient.PutAsync(httpClient.BaseAddress + "/Account/UpdateUserAccount/" + model.Id, content).Result;
            if (responseMessage.IsSuccessStatusCode)
            {
                return true;
            }
            return false;


        }

        private string GetDefaultImagePath()
        {
            var PhotofilePath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\images\UserImage.jpg");

            return PhotofilePath;
        }


    }
}
