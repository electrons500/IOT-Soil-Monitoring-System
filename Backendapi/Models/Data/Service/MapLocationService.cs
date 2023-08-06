using Backendapi.Models.Data.ApiModel;
using Backendapi.Models.Data.IOTSMSDBContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backendapi.Models.Data.Service
{
    public class MapLocationService
    {
        private IOTSMSDBContext.IOTSMSDBContext _Context;
        public MapLocationService(IOTSMSDBContext.IOTSMSDBContext context)
        {
            _Context = context;
        }
        //add new map coordinates
        public bool AddorEditFarmMapLocation(DeviceMapLocationModel model) 
        {
            Arduino arduino = _Context.Arduino.Where(x => x.SerialNumber == model.SerialNumber).FirstOrDefault();
            Farm farm = _Context.Farm.Where(x => x.SerialNumber == model.SerialNumber).Include(x => x.Region)
                                                                                      .FirstOrDefault();
            if(arduino == null || farm == null)
            {
                return false;
            }

            //Now check if the device already has it map location on the DB
            int locationCount = _Context.FarmMapLocation.Where(x => x.ArduinoId == arduino.ArduinoId).Count();
            if(locationCount != 0 )
            {
                //update location
                FarmMapLocation mapLocation = _Context.FarmMapLocation.Where(x => x.ArduinoId == arduino.ArduinoId).FirstOrDefault();
                
                mapLocation.SerialNumber = model.SerialNumber;
                mapLocation.ArduinoId = arduino.ArduinoId;
                mapLocation.FarmId = farm.FarmId;
                mapLocation.Title = farm.FarmName;
                mapLocation.Descriptions = $"This farm is located at {farm.Location},{farm.Region.RegionName}";
                mapLocation.Latitude = model.Latitude;
                mapLocation.Longitude = model.Longitude;

                _Context.FarmMapLocation.Update(mapLocation);
                _Context.SaveChanges();

                return true;

            }
            else
            {
                //insert new location
                FarmMapLocation mapLocation = new FarmMapLocation()
                {
                    MaplocationId = Guid.NewGuid().ToString(),
                    ArduinoId = arduino.ArduinoId,
                    SerialNumber = arduino.SerialNumber,
                    FarmId = farm.FarmId,
                    Title = farm.FarmName,
                    Descriptions = $"This farm is located at {farm.Location},{farm.Region.RegionName}",
                    Latitude = model.Latitude,
                    Longitude = model.Longitude
                };

                _Context.FarmMapLocation.Add(mapLocation);
                _Context.SaveChanges();

                return true;

            }

          

        }


        //Get list of farms registered by officer and their map coordinates

        public List<MapLocationApiModel> GetListOfRegisteredFarmAndTheirMapLocation(string OfficerId)
        {
            var LinqQuery = (from maplocation in _Context.FarmMapLocation
                             join arduino in _Context.Arduino on maplocation.SerialNumber equals arduino.SerialNumber
                             join farm in _Context.Farm on maplocation.FarmId equals farm.FarmId
                             join user in _Context.Users.Where(x => x.Id == OfficerId) on arduino.UserId equals user.Id
                             select new
                             {
                                 maplocation.MaplocationId,
                                 maplocation.Title,
                                 maplocation.Descriptions,
                                 maplocation.Latitude,
                                 maplocation.Longitude,
                                 maplocation.FarmId,
                                 maplocation.ArduinoId,
                                 maplocation.SerialNumber,
                                 farm.FarmName,
                                 farm.Location,

                             }
                             ).ToList();

            List<MapLocationApiModel> model = LinqQuery.Select(x => new MapLocationApiModel
            {
                MaplocationId = x.MaplocationId,
                Title = x.Title,
                Descriptions = x.Descriptions,
                Latitude = x.Latitude,
                Longitude = x.Longitude,
                FarmId = x.FarmId,
                FarmName = x.FarmName,
                FarmLocation = x.Location,
                ArduinoId = x.ArduinoId,
                SerialNumber = x.SerialNumber
                
            }).ToList();

            return model;

        }


        //Get farm map details 
        public MapLocationApiModel GetFarmMapLocationDetails(string mapId)
        {
            FarmMapLocation farmMapLocation = _Context.FarmMapLocation.Where(x => x.MaplocationId == mapId)
                                                                      .Include(x => x.Farm)
                                                                      .FirstOrDefault();

            MapLocationApiModel model = new MapLocationApiModel()
            {
                MaplocationId = farmMapLocation.MaplocationId,
                Title = farmMapLocation.Title,
                Descriptions = farmMapLocation.Descriptions,
                Latitude = farmMapLocation.Latitude,
                Longitude = farmMapLocation.Longitude,
                FarmId  = farmMapLocation.FarmId,
                FarmName = farmMapLocation.Farm.FarmName,
                FarmLocation = farmMapLocation.Farm.Location,
                ArduinoId = farmMapLocation.ArduinoId,
                SerialNumber = farmMapLocation.SerialNumber
            };

            return model;
        }


        //Edit Farm map location
        public bool EditMapLocation(MapLocationApiModel model)
        {
            FarmMapLocation farmMapLocation = _Context.FarmMapLocation.Where(x => x.MaplocationId == model.MaplocationId)
                                                                      .FirstOrDefault();
            farmMapLocation.Title = model.Title;
            farmMapLocation.Descriptions = model.Descriptions;
            farmMapLocation.Latitude = model.Latitude;
            farmMapLocation.Longitude = model.Longitude;
            _Context.FarmMapLocation.Update(farmMapLocation);
            _Context.SaveChanges();

            return true;

        }

    }
}
