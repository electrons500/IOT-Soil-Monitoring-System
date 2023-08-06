using Backendapi.Models.Data.ApiModel;
using Backendapi.Models.Data.IOTSMSDBContext;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Backendapi.Models.Data.Service
{
    public class FarmerService
    {
        private IOTSMSDBContext.IOTSMSDBContext _Context;
        public FarmerService(IOTSMSDBContext.IOTSMSDBContext context)
        {
            _Context = context;
        }

        //  Get list of farmers
        public List<FarmerApiModel> GetFarmers()
        {
            List<Farmer> farmers = _Context.Farmer.Include(x => x.FarmerImage)
                                                   .Include(x => x.ApplicationUser)
                                                   .Include(x => x.Gender)
                                                   .Include(x => x.Region)
                                                   .ToList();

            List<FarmerApiModel> model = farmers.Select(x => new FarmerApiModel
            {
                FarmerId = x.FarmerId,
                Firstname = x.Firstname,
                MiddleName = x.MiddleName,
                LastName = x.LastName,
                FullName = $"{x.Firstname} {x.MiddleName} {x.LastName}",
                Address = x.Address,
                Location = x.Location,
                Contact = x.Contact,
                GenderId = x.GenderId,
                GenderName = x.Gender.GenderName,
                RegionId = x.RegionId,
                RegionName = x.Region.RegionName,
                FarmerImageId = x.FarmerImageId,
                FarmerPhoto = x.FarmerImage.FarmerPhoto,
                UserId = x.UserId,
                Username = x.ApplicationUser.FullName,
                DateCreated = x.DateCreated


            }).ToList();

            return model;


        }
         //  Get list of farmers registered by Officers
        public List<FarmerApiModel> GetFarmersByOfficerRegistration(string userId) 
        {
            List<Farmer> farmers = _Context.Farmer.Where(x => x.UserId == userId).Include(x => x.FarmerImage)
                                                                                 .Include(x => x.ApplicationUser)
                                                                                 .Include(x => x.Gender)
                                                                                 .Include(x => x.Region)
                                                                                 .ToList();

            List<FarmerApiModel> model = farmers.Select(x => new FarmerApiModel
            {
                FarmerId = x.FarmerId,
                Firstname = x.Firstname,
                MiddleName = x.MiddleName,
                LastName = x.LastName,
                FullName = $"{x.Firstname} {x.MiddleName} {x.LastName}",
                Address = x.Address,
                Location = x.Location,
                Contact = x.Contact,
                GenderId = x.GenderId,
                GenderName = x.Gender.GenderName,
                RegionId = x.RegionId,
                RegionName = x.Region.RegionName,
                FarmerImageId = x.FarmerImageId,
                FarmerPhoto = x.FarmerImage.FarmerPhoto,
                UserId = x.UserId,
                Username = x.ApplicationUser.FullName,
                DateCreated = x.DateCreated


            }).ToList();

            return model;


        }


        // Get farm details
        public FarmerApiModel GetFarmerDetails(string id)
        {
            Farmer farmers = _Context.Farmer.Where(x => x.FarmerId == id)
                                            .Include(x => x.FarmerImage)

                                            .Include(x => x.Gender)
                                            .Include(x => x.Region)
                                            .FirstOrDefault();

            FarmerApiModel model = new FarmerApiModel()
            {
                FarmerId = farmers.FarmerId,
                Firstname = farmers.Firstname,
                MiddleName = farmers.MiddleName,
                LastName = farmers.LastName,
                FullName = $"{farmers.Firstname} {farmers.MiddleName} {farmers.LastName}",
                Address = farmers.Address,
                Location = farmers.Location,
                Contact = farmers.Contact,
                GenderId = farmers.GenderId,
                GenderName = farmers.Gender.GenderName,
                RegionId = farmers.RegionId,
                RegionName = farmers.Region.RegionName,

                FarmerImageId = farmers.FarmerImageId,
                FarmerPhoto = farmers.FarmerImage.FarmerPhoto,
                UserId = farmers.UserId,

                DateCreated = farmers.DateCreated
            };

            return model;
        }


        //Add new farmer
        public bool AddFarmer(FarmerApiModel model)
        {
            int imageId;


            //store byte array from front end to DB and get me it image id
            FarmerImage image = new FarmerImage()
            {
                FarmerPhoto = model.FarmerPhoto
            };
            _Context.FarmerImage.Add(image);
            _Context.SaveChanges();
            imageId = image.FarmerImageId;

            Farmer farmer = new Farmer()
            {
                FarmerId = model.FarmerId,
                Firstname = model.Firstname,
                MiddleName = model.MiddleName,
                LastName = model.LastName,
                Address = model.Address,
                Location = model.Location,
                Contact = model.Contact,
                GenderId = model.GenderId,
                RegionId = model.RegionId,
                FarmerImageId = imageId,
                UserId = model.UserId,
                DateCreated = model.DateCreated
            };

            _Context.Farmer.Add(farmer);
            _Context.SaveChanges();
            return true;
        }


       
        //update farmer details

        public bool UpdateFarmerDetails(FarmerApiModel model, string farmerId)
        {
            
            byte[] imageByte;

            Farmer farmer = _Context.Farmer.Where(x => x.FarmerId == farmerId).FirstOrDefault();
            //update farmer picture
            if (model.FarmerPhoto.Length > 0)
            {
                imageByte = model.FarmerPhoto;

                FarmerImage image = _Context.FarmerImage.Where(x => x.FarmerImageId == farmer.FarmerImageId).FirstOrDefault();
                image.FarmerPhoto = imageByte;
                _Context.FarmerImage.Update(image);
                _Context.SaveChanges();

                farmer.Firstname = model.Firstname;
                farmer.MiddleName = model.MiddleName;
                farmer.LastName = model.LastName;
                farmer.Address = model.Address;
                farmer.Location = model.Location;
                farmer.Contact = model.Contact;
                farmer.GenderId = model.GenderId;
                farmer.RegionId = model.RegionId;
                farmer.FarmerImageId = farmer.FarmerImageId;
                farmer.UserId = farmer.UserId;
                farmer.DateCreated = farmer.DateCreated;

                _Context.Farmer.Update(farmer);
                _Context.SaveChanges();

            }
            else
            {
              
                farmer.Firstname = model.Firstname;
                farmer.MiddleName = model.MiddleName;
                farmer.LastName = model.LastName;
                farmer.Address = model.Address;
                farmer.Location = model.Location;
                farmer.Contact = model.Contact;
                farmer.GenderId = model.GenderId;
                farmer.RegionId = model.RegionId;
                farmer.FarmerImageId = farmer.FarmerImageId;
                farmer.UserId = farmer.UserId;
                farmer.DateCreated = farmer.DateCreated;

                _Context.Farmer.Update(farmer);
                _Context.SaveChanges();

            }



            return true;

        }


        //delete farmer details
        // will do it later


        //Get default farmer image path
        private string GetDefaultImagePath()
        {
            var PhotofilePath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\lib\images\photo1.jpg");

            return PhotofilePath;
        }

    }
}
