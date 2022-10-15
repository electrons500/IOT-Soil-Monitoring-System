using Backendapi.Models.Data.ApiModel;
using Backendapi.Models.Data.IOTSMSDBContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Backendapi.Models.Data.Service
{
    public class SoilDataService
    {
        private IOTSMSDBContext.IOTSMSDBContext _Context;
        private ArduinoService _ArduinoService;
        private SMSService _SMSService;
        public SoilDataService(IOTSMSDBContext.IOTSMSDBContext context, ArduinoService arduinoService,
            SMSService sMSService)
        {
            _Context = context;
            _ArduinoService = arduinoService;
            _SMSService = sMSService;
        }

        //Get a list of all the data sent by all arduino devices 
        public List<SoilDataApiModel> GetSoilDatasFromAllDevices()
        {
            List<SoilData> soilData = _Context.SoilData.Include(x => x.Arduino).ToList();
            List<SoilDataApiModel> model = soilData.Select(x => new SoilDataApiModel
            {
                SoilDataId = x.SoilDataId,
                SoilMoisture = x.SoilMoisture,
                Temperature = x.Temperature,
                Humidity = x.Humidity,
                SoilTemperature = x.SoilTemperature,
                Nitrogen = x.Nitrogen,
                Potassium = x.Potassium,
                Phosphorus = x.Phosphorus,
                Date = x.Date,
                Time = x.Time,
                SerialNumber = x.SerialNumber,
                ArduinoId = x.ArduinoId

            }).ToList();

            return model;
        }


        //Get list of soil data sent by arduino device base on their serial numbers or their device id

        public List<SoilDataApiModel> GetSoilDatasbyDeviceId(string farmId)
        {
            
            Farm farm = _Context.Farm.Where(x => x.FarmId == farmId).FirstOrDefault();

            List<SoilData> soilData = _Context.SoilData.Where(x => x.ArduinoId == farm.ArduinoId).ToList();
            List<SoilDataApiModel> model = soilData.Select(x => new SoilDataApiModel
            {
               SoilDataId = x.SoilDataId,
                SoilMoisture = x.SoilMoisture,
                Temperature = x.Temperature,
                Humidity = x.Humidity,
                SoilTemperature = x.SoilTemperature,
                Nitrogen = x.Nitrogen,
                Potassium = x.Potassium,
                Phosphorus = x.Phosphorus,
                Date = x.Date,
                Time = x.Time,
                SerialNumber = x.SerialNumber,
                ArduinoId = x.ArduinoId

            }).ToList(); 

            return model;
        }


        public SoilDataApiModel GetSoilDataDetails(int soilId)
        {
            SoilData soilData = _Context.SoilData.Where(x => x.SoilDataId == soilId).FirstOrDefault();
            SoilDataApiModel model = new SoilDataApiModel()
            {
                SoilDataId = soilData.SoilDataId,
                SoilMoisture = soilData.SoilMoisture,
                Temperature = soilData.Temperature,
                Humidity = soilData.Humidity, 
                SoilTemperature = soilData.SoilTemperature,
                Nitrogen = soilData.Nitrogen,
                Potassium = soilData.Potassium,
                Phosphorus = soilData.Phosphorus,
                Date = soilData.Date,
                Time = soilData.Time,
                SerialNumber = soilData.SerialNumber,
                ArduinoId = soilData.ArduinoId
            };

            return model;
        }

        //Add new soil data
        public bool AddSoilData(SoilDataFromDeviceModel model)
        {
            //get arduino id of the sending device using it serial number
            Arduino arduino = _Context.Arduino.Where(x => x.SerialNumber == model.SerialNumber).FirstOrDefault();

            ////Before inserting data, device need to check if it is not activated
            if (arduino.IsActivated == false)
            {
                //send email or SMS to officer
                bool IsMessageSent = SendSMSAlert(arduino.UserId, arduino.SerialNumber);
                if (IsMessageSent)
                {
                    return true;
                }

                return false;
            }
            else
            {

                //Save new soil data into the db
                SoilData soilDatum = new SoilData()
                {
                    SoilMoisture = model.SoilMoisture,
                    Temperature = model.Temperature,
                    Humidity = model.Humidity,
                    SoilTemperature = model.SoilTemperature,
                    Potassium = model.Potassium,
                    Nitrogen = model.Nitrogen,
                    Phosphorus = model.Phosphorus,
                    SerialNumber = model.SerialNumber,
                    ArduinoId = arduino.ArduinoId,
                    Date = DateTime.Now.ToShortDateString(),
                    Time = DateTime.Now.ToShortTimeString()

                };

                _Context.SoilData.Add(soilDatum);
                _Context.SaveChanges();


                //Update last date and time device was powered on
                arduino.LastPowerOnDate = DateTime.Now.ToShortDateString();
                arduino.LastPowerOnTime = DateTime.Now.ToShortTimeString();
                _Context.Arduino.Update(arduino);
                _Context.SaveChanges();


                return true;
            }

        }


        //Delete soil data by soil data id
        public bool DeleteSoilData(int soilDataId)
        {
            SoilData soilDatum = _Context.SoilData.Where(x => x.SoilDataId == soilDataId).FirstOrDefault();
            _Context.SoilData.Remove(soilDatum);
            _Context.SaveChanges();

            return true;
        }


        private bool SendSMSAlert(string userId,string serialNo) 
        {
            ApplicationUser user = _Context.Users.Where(x => x.Id == userId).FirstOrDefault();
            string officerNumber = user.PhoneNumber;
            string message = "The device with the serial number " + serialNo + " want to send data but is not activated.";
            bool msgResult = _SMSService.SendSMS(message, officerNumber); //send message to officer
            if (msgResult)
            {
                return true;
            }

            return false;
        }


    }
}
