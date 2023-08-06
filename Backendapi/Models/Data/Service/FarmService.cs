using Backendapi.Models.Data.ApiModel;
using Backendapi.Models.Data.IOTSMSDBContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Backendapi.Models.Data.Service
{
    public class FarmService
    {
        private IOTSMSDBContext.IOTSMSDBContext _Context;
        public FarmService(IOTSMSDBContext.IOTSMSDBContext context)
        {
            _Context = context;
        }

        //Get all list of farms
        public List<FarmApiModel> GetFarms()
        {
            List<Farm> farms = _Context.Farm.Include(x => x.Farmer)
                                             .Include(x => x.Region)
                                             .Include(x => x.SoilCategory)
                                             .ToList();
            List<FarmApiModel> model = farms.Select(x => new FarmApiModel
            {
                FarmId = x.FarmId,
                FarmName = x.FarmName,
                Location = x.Location,
                DateCreated = x.DateCreated,
                FarmerId = x.FarmerId,
                FarmerName = $"{x.Farmer.Firstname} {x.Farmer.MiddleName} {x.Farmer.LastName}",
                FarmerContact = x.Farmer.Contact,
                RegionId = x.RegionId,
                RegionName = x.Region.RegionName,
                SoilCategoryId = x.SoilCategoryId,
                SoilCategoryName = x.SoilCategory.SoilName,
                SerialNumber = x.SerialNumber
                

            }).ToList();


            return model;
        }



        //Get all list of farms belonging to a farmer
        public List<FarmApiModel> GetFarmsbyFarmerId(string farmerId) 
        {
            
            List<Farm> farms = _Context.Farm.Where(x => x.FarmerId == farmerId).Include(x => x.Farmer)
                                                                               .Include(x => x.Region)
                                                                               .Include(x => x.SoilCategory)
                                                                               .ToList();
            List<FarmApiModel> model = farms.Select(x => new FarmApiModel
            {
                FarmId = x.FarmId,
                FarmName = x.FarmName,
                Location = x.Location,
                DateCreated = x.DateCreated,
                FarmerId = x.FarmerId,
                FarmerName = $"{x.Farmer.Firstname} {x.Farmer.MiddleName} {x.Farmer.LastName}",
                FarmerContact = x.Farmer.Contact,
                RegionId = x.RegionId,
                RegionName = x.Region.RegionName,
                SoilCategoryId = x.SoilCategoryId,
                SoilCategoryName = x.SoilCategory.SoilName,
                SerialNumber = x.SerialNumber
                

            }).ToList();


            return model;
        }

        //Get farm details by farm id

        public FarmApiModel GetFarmDetails(string farmId)
        {
            Farm farm = _Context.Farm.Where(x => x.FarmId == farmId)
                                      .Include(x => x.Region)
                                      .Include(x => x.Farmer)
                                      .Include(x => x.SoilCategory)
                                      .FirstOrDefault();

            FarmApiModel model = new FarmApiModel()
            {
                FarmId = farm.FarmId,
                FarmName = farm.FarmName,
                Location = farm.Location,
                DateCreated = farm.DateCreated,
                FarmerId = farm.FarmerId,
                FarmerName = $"{farm.Farmer.Firstname} {farm.Farmer.MiddleName} {farm.Farmer.LastName}",
                FarmerContact = farm.Farmer.Contact,
                RegionId = farm.RegionId,
                RegionName = farm.Region.RegionName,
                SoilCategoryId = farm.SoilCategoryId,
                SoilCategoryName = farm.SoilCategory.SoilName,
                SerialNumber = farm.SerialNumber         
               
            };

            return model;
        }


        //Get farms base on individual farmer id
        public FarmApiModel GetFarmDetailsByFarmerId(string farmerId)
        {
            Farm farm = _Context.Farm.Where(x => x.FarmerId == farmerId)
                                      .Include(x => x.Region)
                                      .Include(x => x.Farmer)
                                      .Include(x => x.SoilCategory)
                                      .FirstOrDefault();

            FarmApiModel model = new FarmApiModel()
            {

                FarmId = farm.FarmId,
                FarmName = farm.FarmName,
                Location = farm.Location,
                DateCreated = farm.DateCreated,
                FarmerId = farm.FarmerId,
                FarmerName = $"{farm.Farmer.Firstname} {farm.Farmer.MiddleName} {farm.Farmer.LastName}",
                FarmerContact = farm.Farmer.Contact,
                RegionId = farm.RegionId,
                RegionName = farm.Region.RegionName,
                SoilCategoryId = farm.SoilCategoryId,
                SoilCategoryName = farm.SoilCategory.SoilName

            };

            return model;
        }

        //List of all farms registered by officer
        public List<FarmApiModel> GetFarmsRegisteredByOfficer(string officerId) 
        {
            var LinqQuery = (from farm in _Context.Farm 
                        join farmer in _Context.Farmer
                        on farm.FarmerId equals farmer.FarmerId
                        join user in _Context.Users.Where(x => x.Id == officerId)
                        on farmer.UserId equals user.Id
                        select new
                        {
                            farm.FarmId,
                            farm.FarmName,
                            farm.Location,
                            farm.DateCreated,
                            farm.FarmerId,
                            farmer.Firstname,
                            farmer.MiddleName,
                            farmer.LastName,
                            farmer.Contact,
                            farm.RegionId,
                            farm.Region.RegionName,
                            farm.SoilCategoryId,
                            farm.SoilCategory.SoilName,
                            farm.SerialNumber,              

                        }).ToList();

         
           
            List<FarmApiModel> model = LinqQuery.Select(x => new FarmApiModel{
              
                FarmId = x.FarmId,
                FarmName = x.FarmName,
                Location = x.Location,
                DateCreated = x.DateCreated,
                FarmerId = x.FarmerId,
                FarmerContact = x.Contact,
                FarmerName = $"{x.Firstname} {x.MiddleName} {x.LastName}",
                RegionId = x.RegionId,
                RegionName = x.RegionName,
                SoilCategoryId = x.SoilCategoryId,
                SoilCategoryName = x.SoilName,
                SerialNumber = x.SerialNumber

            }).ToList();
            return model;
        }

        //Add new farm 

        public bool AddFarm(FarmApiModel model)
        {
            Guid guid = Guid.NewGuid();
            string arduinoId;

           
            //Get arduino id belonging to the farmer
            Arduino arduino = _Context.Arduino.Where(x => x.SerialNumber == model.SerialNumber).FirstOrDefault();
            if (arduino == null)
            {
                return false;
            }
            else
            {
                arduinoId = arduino.ArduinoId;

            }
       

            Farm farm = new Farm()
            {
                FarmId = model.FarmId,
                FarmName = model.FarmName,
                Location = model.Location,
                DateCreated = model.DateCreated,
                FarmerId = model.FarmerId,
                RegionId = model.RegionId,
                SoilCategoryId = model.SoilCategoryId,
                SerialNumber = model.SerialNumber,
                ArduinoId = arduinoId
            };

            _Context.Farm.Add(farm);
            _Context.SaveChanges();

            return true;

        }

        //update farm details

        public bool UpdateFarmDetails(FarmApiModel model,string farmId)
        {
            Farm farm = _Context.Farm.Where(x => x.FarmId == farmId).FirstOrDefault();
            farm.FarmName = model.FarmName;
            farm.Location = model.Location;
            farm.FarmerId = model.FarmerId;
            farm.RegionId = model.RegionId;
            farm.SoilCategoryId = model.SoilCategoryId;
            farm.SerialNumber = model.SerialNumber;

            _Context.Farm.Update(farm);
            _Context.SaveChanges();

            return true; 

        }

        //delete farm details
        public bool DeleteFarmDetails(string farmId)
        {
            Farm farm = _Context.Farm.Where(x => x.FarmId == farmId).FirstOrDefault();
            _Context.Farm.Remove(farm);
            _Context.SaveChanges();

            return true;
        }

    }
}
