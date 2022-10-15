using Backendapi.Models.Data.ApiModel;
using Backendapi.Models.Data.IOTSMSDBContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Backendapi.Models.Data.Service
{
    public class ArduinoService
    {
        private IOTSMSDBContext.IOTSMSDBContext _Context;
        public ArduinoService(IOTSMSDBContext.IOTSMSDBContext context)
        {
            _Context = context;
        }

        //Get All Arduino devices information
        public List<ArduinoApiModel> GetArduinos()
        {
            List<Arduino> arduinos = _Context.Arduino.ToList();
            List<ArduinoApiModel> model = arduinos.Select(x => new ArduinoApiModel
            {
                ArduinoId = x.ArduinoId,
                Vid = x.Vid,
                Pid = x.Pid,
                SerialNumber = x.SerialNumber,
                Bn = x.Bn,
                DeploymentDate = x.DeploymentDate,
                IsVerified = x.IsVerified,
                IsActivated = x.IsActivated,
                DateOfActivation = x.DateOfActivation,
                LastPowerOnDate = x.LastPowerOnDate,
                LastPowerOnTime = x.LastPowerOnTime,
                IsActive = x.IsActive,
                IsOnsite = x.IsOnsite,
                UserId = x.UserId,
               
            }).ToList();

            return model;
        }

        //Get All Arduino devices registered by an Agric extension officer
        public List<ArduinoApiModel> GetArduinoDevicesByAgricOfficer(string userId)
        {
            List<Arduino> arduinos = _Context.Arduino.Where(x => x.UserId == userId).ToList();
            List<ArduinoApiModel> model = arduinos.Select(x => new ArduinoApiModel
            {
                ArduinoId = x.ArduinoId,
                Vid = x.Vid,
                Pid = x.Pid,
                SerialNumber = x.SerialNumber,
                Bn = x.Bn,
                DeploymentDate = x.DeploymentDate,
                IsVerified = x.IsVerified,
                IsActivated = x.IsActivated,
                DateOfActivation = x.DateOfActivation,
                LastPowerOnDate = x.LastPowerOnDate,
                LastPowerOnTime = x.LastPowerOnTime,
                IsActive = x.IsActive,
                IsOnsite = x.IsOnsite,
                UserId = x.UserId
              
            }).ToList();

            return model;
        }

        //Get detail for an arduino board
        public ArduinoApiModel GetArduinoDetail(string arduinoId)
        {
            Arduino arduino = _Context.Arduino.Where(x => x.ArduinoId == arduinoId).FirstOrDefault();
            ArduinoApiModel model = new ArduinoApiModel()
            {
               ArduinoId = arduino.ArduinoId,
                Vid = arduino.Vid,
                Pid = arduino.Pid,
                SerialNumber = arduino.SerialNumber,
                Bn = arduino.Bn,
                DeploymentDate = arduino.DeploymentDate,
                IsVerified = arduino.IsVerified,
                IsActivated = arduino.IsActivated,
                DateOfActivation = arduino.DateOfActivation,
                LastPowerOnDate = arduino.LastPowerOnDate,
                LastPowerOnTime = arduino.LastPowerOnTime,
                IsActive = arduino.IsActive,
                IsOnsite = arduino.IsOnsite,
                UserId = arduino.UserId,
               
            };

            return model;
        }

        //Add new Arduino
        public bool AddNewArduino(ArduinoApiModel model)
        {
           //Create a global Id
            Guid GlobalId = Guid.NewGuid();

            Arduino arduino = new Arduino()
            {
                ArduinoId = GlobalId.ToString(),
                Vid = model.Vid,
                Pid = model.Pid,
                SerialNumber = model.SerialNumber,
                Bn = model.Bn,
                DeploymentDate = model.DeploymentDate,
                IsVerified = model.IsVerified,
                IsActivated = model.IsActivated,
                DateOfActivation = model.DateOfActivation,
                LastPowerOnDate = model.LastPowerOnDate,
                LastPowerOnTime = model.LastPowerOnTime,
                IsActive = model.IsActive,
                IsOnsite = model.IsOnsite,
                UserId = model.UserId
               
            };

            _Context.Arduino.Add(arduino);
            _Context.SaveChanges();

            return true;

        }

        //update Arduino info
        public bool UpdateArduino(ArduinoApiModel model)
        {
            Arduino arduino = _Context.Arduino.Where(x => x.ArduinoId == model.ArduinoId).FirstOrDefault();
            arduino.Vid = model.Vid;
            arduino.Pid = model.Pid;
            arduino.SerialNumber = model.SerialNumber;
            arduino.Bn = model.Bn;
            arduino.DeploymentDate = model.DeploymentDate;
            arduino.IsVerified = model.IsVerified;
            arduino.IsActivated = model.IsActivated;
            arduino.DateOfActivation = model.DateOfActivation;
            arduino.LastPowerOnDate = model.LastPowerOnDate;
            arduino.LastPowerOnTime = model.LastPowerOnTime;
            arduino.IsActive = model.IsActive;
            arduino.IsOnsite = model.IsOnsite;
            arduino.UserId = model.UserId;
            

            _Context.Arduino.Update(arduino);
            _Context.SaveChanges();

            return true;

        }

        // update Arduino devices current status start here
        public bool SetArduinoToActive(string arduinoId)
        {
            Arduino arduino = _Context.Arduino.Where(x => x.ArduinoId == arduinoId).FirstOrDefault();
            arduino.IsActive = true;
            arduino.LastPowerOnDate = DateTime.Now.ToShortDateString();
            arduino.LastPowerOnTime = DateTime.Now.ToShortTimeString();

            _Context.Arduino.Update(arduino);
            _Context.SaveChanges();

            return true;

        }
         public bool SetArduinoToInactive(string arduinoId)
        {
            Arduino arduino = _Context.Arduino.Where(x => x.ArduinoId == arduinoId).FirstOrDefault();
            arduino.IsActive = false;
            arduino.LastPowerOnDate = DateTime.Now.ToShortDateString();
            arduino.LastPowerOnTime = DateTime.Now.ToShortTimeString();

            _Context.Arduino.Update(arduino);
            _Context.SaveChanges();

            return true;

        }

        public bool SetArduinoIfOnSite(string arduinoId)
        {
            Arduino arduino = _Context.Arduino.Where(x => x.ArduinoId == arduinoId).FirstOrDefault();
            arduino.IsOnsite = true;
            arduino.IsActive = true;
            arduino.LastPowerOnDate = DateTime.Now.ToShortDateString();
            arduino.LastPowerOnTime = DateTime.Now.ToShortTimeString();

            _Context.Arduino.Update(arduino);
            _Context.SaveChanges();

            return true;

        }

         public bool ChangeArduinoToOffsite(string arduinoId)
        {
            Arduino arduino = _Context.Arduino.Where(x => x.ArduinoId == arduinoId).FirstOrDefault();
            arduino.IsOnsite = false;
            arduino.IsActive = false;
           
            _Context.Arduino.Update(arduino);
            _Context.SaveChanges();

            return true;

        }


        //  Ends here

        //delete arduino information
        public bool DeleteArduino(string arduinoId)
        {
            Arduino arduino = _Context.Arduino.Where(x => x.ArduinoId == arduinoId).FirstOrDefault();
            _Context.Arduino.Remove(arduino);
            _Context.SaveChanges();

            return true;
        }


        public bool VerifyDevice(string serialnumber)
        {
            Arduino arduino = _Context.Arduino.Where(x => x.SerialNumber == serialnumber).FirstOrDefault();
            if (arduino == null)
            {
                return false;
            }
            arduino.IsVerified = true;
            _Context.Arduino.Update(arduino);
            _Context.SaveChanges();

            return true;
        }
          public bool CheckDeviceIsNotVerified(string serialnumber) 
        {
            Arduino arduino = _Context.Arduino.Where(x => x.SerialNumber == serialnumber).FirstOrDefault();
            if(arduino.IsVerified == false)
            {
                return false;
            }

            return true;
        }

        //Activate Arduino device
        //NB: ACTIVATION CAN BE DONE BY THE OFFICER 
        public bool ActivateArduino(string serialNumber,string userId)
        {
            try
            {
               
                Arduino arduino = _Context.Arduino.Where(x => x.SerialNumber == serialNumber).FirstOrDefault();
                if (arduino == null)
                {
                    return false;
                }
                 arduino.IsActivated = true;
                 arduino.UserId = userId;
                 arduino.DateOfActivation = DateTime.Now.ToShortDateString();
                _Context.Arduino.Update(arduino);
                _Context.SaveChanges();

                return true;

            }
            catch (NullReferenceException ex)
            {
                return false;
                
            }
        }
         //NB: ACTIVATION CAN BE DONE BY THE DEVICE AFTER SENDING IT FIRST DATA 
        public bool ActivateArduinoByDeviceOnsite(string arduinoId)
        {
            Arduino arduino = _Context.Arduino.Where(x => x.ArduinoId == arduinoId).FirstOrDefault();
           
            arduino.IsActivated = true;
            arduino.DateOfActivation = DateTime.Now.ToShortDateString();
            arduino.IsActive = true;
            arduino.IsOnsite = true;
            _Context.Arduino.Update(arduino);
            _Context.SaveChanges();
            return true;

        }


        //Get List of Activated devices
        public List<ArduinoApiModel> GetActivatedDevices()
        {
            List<Arduino> arduinos = _Context.Arduino.Where(x => x.IsActivated == true).ToList();
            List<ArduinoApiModel> model = arduinos.Select(x => new ArduinoApiModel
            {
                ArduinoId = x.ArduinoId,
                Vid = x.Vid,
                Pid = x.Pid,
                SerialNumber = x.SerialNumber,
                Bn = x.Bn,
                DeploymentDate = x.DeploymentDate,
                IsVerified = x.IsVerified,
                IsActivated = x.IsActivated,
                DateOfActivation = x.DateOfActivation,
                LastPowerOnDate = x.LastPowerOnDate,
                LastPowerOnTime = x.LastPowerOnTime,
                IsActive = x.IsActive,
                IsOnsite = x.IsOnsite,
                UserId = x.UserId,

            }).ToList();

            return model;
        }

        //Get List of Unactivated devices
        public List<ArduinoApiModel> GetUnActivatedDevices()
        {
            List<Arduino> arduinos = _Context.Arduino.Where(x => x.IsActivated == false).ToList();
            List<ArduinoApiModel> model = arduinos.Select(x => new ArduinoApiModel
            {
                ArduinoId = x.ArduinoId,
                Vid = x.Vid,
                Pid = x.Pid,
                SerialNumber = x.SerialNumber,
                Bn = x.Bn,
                DeploymentDate = x.DeploymentDate,
                IsVerified = x.IsVerified,
                IsActivated = x.IsActivated,
                DateOfActivation = x.DateOfActivation,
                LastPowerOnDate = x.LastPowerOnDate,
                LastPowerOnTime = x.LastPowerOnTime,
                IsActive = x.IsActive,
                IsOnsite = x.IsOnsite,
                UserId = x.UserId,

            }).ToList();

            return model;
        }



        //Get List of Activated devices but not on site
        public List<ArduinoApiModel> GetActivatedDevicesAndNotOnsite()
        {
            List<Arduino> arduinos = _Context.Arduino.Where(x => x.IsActivated == true && x.IsOnsite == false).ToList();
            List<ArduinoApiModel> model = arduinos.Select(x => new ArduinoApiModel
            {
                ArduinoId = x.ArduinoId,
                Vid = x.Vid,
                Pid = x.Pid,
                SerialNumber = x.SerialNumber,
                Bn = x.Bn,
                DeploymentDate = x.DeploymentDate,
                IsVerified = x.IsVerified,
                IsActivated = x.IsActivated,
                DateOfActivation = x.DateOfActivation,
                LastPowerOnDate = x.LastPowerOnDate,
                LastPowerOnTime = x.LastPowerOnTime,
                IsActive = x.IsActive,
                IsOnsite = x.IsOnsite,
                UserId = x.UserId,

            }).ToList();

            return model;
        }
         //Get List of Activated devices but are on site
        public List<ArduinoApiModel> GetActivatedDevicesButAreOnsite() 
        {
            List<Arduino> arduinos = _Context.Arduino.Where(x => x.IsActivated == true && x.IsOnsite == true).ToList();
            List<ArduinoApiModel> model = arduinos.Select(x => new ArduinoApiModel
            {
                ArduinoId = x.ArduinoId,
                Vid = x.Vid,
                Pid = x.Pid,
                SerialNumber = x.SerialNumber,
                Bn = x.Bn,
                DeploymentDate = x.DeploymentDate,
                IsVerified = x.IsVerified,
                IsActivated = x.IsActivated,
                DateOfActivation = x.DateOfActivation,
                LastPowerOnDate = x.LastPowerOnDate,
                LastPowerOnTime = x.LastPowerOnTime,
                IsActive = x.IsActive,
                IsOnsite = x.IsOnsite,
                UserId = x.UserId,

            }).ToList();

            return model;
        }

         //Get List of Activated devices that are on site and are active
        public List<ArduinoApiModel> GetActivatedDevicesThatAreOnsiteAndActive() 
        {
            List<Arduino> arduinos = _Context.Arduino.Where(x => x.IsOnsite == true && x.IsActive == true).ToList();
            List<ArduinoApiModel> model = arduinos.Select(x => new ArduinoApiModel
            {
                ArduinoId = x.ArduinoId,
                Vid = x.Vid,
                Pid = x.Pid,
                SerialNumber = x.SerialNumber,
                Bn = x.Bn,
                DeploymentDate = x.DeploymentDate,
                IsVerified = x.IsVerified,
                IsActivated = x.IsActivated,
                DateOfActivation = x.DateOfActivation,
                LastPowerOnDate = x.LastPowerOnDate,
                LastPowerOnTime = x.LastPowerOnTime,
                IsActive = x.IsActive,
                IsOnsite = x.IsOnsite,
                UserId = x.UserId
               
            }).ToList();

            return model;
        }

        //Get List of Activated devices that are on site and are unactive due to low battery power
        public List<ArduinoApiModel> GetActivatedDevicesThatAreOnsiteAndUnactive() 
        {
            List<Arduino> arduinos = _Context.Arduino
                .Where(x =>  x.IsOnsite == true && x.IsActive == false)
                .ToList();
            List<ArduinoApiModel> model = arduinos.Select(x => new ArduinoApiModel
            {
                ArduinoId = x.ArduinoId,
                Vid = x.Vid,
                Pid = x.Pid,
                SerialNumber = x.SerialNumber,
                Bn = x.Bn,
                DeploymentDate = x.DeploymentDate,
                IsVerified = x.IsVerified,
                IsActivated = x.IsActivated,
                DateOfActivation = x.DateOfActivation,
                LastPowerOnDate = x.LastPowerOnDate,
                LastPowerOnTime = x.LastPowerOnTime,
                IsActive = x.IsActive,
                IsOnsite = x.IsOnsite,
                UserId = x.UserId,

            }).ToList();

            return model;
        }




         //Get List of Activated devices registerd by Agric Extension officer
        public List<ArduinoApiModel> GetActivatedDevicesRegisterdByAgricExtensionOfficer(string officerId)
        {
            List<Arduino> arduinos = _Context.Arduino.Where(x => x.IsActivated == true && x.UserId == officerId)
                                                     .ToList();
            List<ArduinoApiModel> model = arduinos.Select(x => new ArduinoApiModel
            {
                ArduinoId = x.ArduinoId,
                Vid = x.Vid,
                Pid = x.Pid,
                SerialNumber = x.SerialNumber,
                Bn = x.Bn,
                DeploymentDate = x.DeploymentDate,
                IsVerified = x.IsVerified,
                IsActivated = x.IsActivated,
                DateOfActivation = x.DateOfActivation,
                LastPowerOnDate = x.LastPowerOnDate,
                LastPowerOnTime = x.LastPowerOnTime,
                IsActive = x.IsActive,
                IsOnsite = x.IsOnsite,
                UserId = x.UserId,

            }).ToList();

            return model;
        }
         
         //Get List of Unactivated devices registerd by Agric Extension officer
        public List<ArduinoApiModel> GetUnActivatedDevicesRegisterdByAgricExtensionOfficer(string officerId)
        {
            List<Arduino> arduinos = _Context.Arduino.Where(x => x.IsActivated == false && x.UserId == officerId)
                                                     .ToList();
            List<ArduinoApiModel> model = arduinos.Select(x => new ArduinoApiModel
            {
                ArduinoId = x.ArduinoId,
                Vid = x.Vid,
                Pid = x.Pid,
                SerialNumber = x.SerialNumber,
                Bn = x.Bn,
                DeploymentDate = x.DeploymentDate,
                IsVerified = x.IsVerified,
                IsActivated = x.IsActivated,
                DateOfActivation = x.DateOfActivation,
                LastPowerOnDate = x.LastPowerOnDate,
                LastPowerOnTime = x.LastPowerOnTime,
                IsActive = x.IsActive,
                IsOnsite = x.IsOnsite,
                UserId = x.UserId,

            }).ToList();

            return model;
        }







        //Get List of Activated devices  registered by Agric Extension Officer but not on site
        public List<ArduinoApiModel> GetActivatedDevicesRegisterdByAgricExtensionOfficerAndNotOnsite( string userId)
        {
            List<Arduino> arduinos = _Context.Arduino.Where(x => x.UserId == userId && x.IsOnsite == false ).ToList();
            List<ArduinoApiModel> model = arduinos.Select(x => new ArduinoApiModel
            {
                ArduinoId = x.ArduinoId,
                Vid = x.Vid,
                Pid = x.Pid,
                SerialNumber = x.SerialNumber,
                Bn = x.Bn,
                DeploymentDate = x.DeploymentDate,
                IsVerified = x.IsVerified,
                IsActivated = x.IsActivated,
                DateOfActivation = x.DateOfActivation,
                LastPowerOnDate = x.LastPowerOnDate,
                LastPowerOnTime = x.LastPowerOnTime,
                IsActive = x.IsActive,
                IsOnsite = x.IsOnsite,
                UserId = x.UserId,

            }).ToList();

            return model;
        }
        //Get List of Activated devices  registered by Agric Extension Officer but are on site
        public List<ArduinoApiModel> GetActivatedDevicesRegisterdByAgricExtensionOfficerButAreOnsite( string userId)
        {
            List<Arduino> arduinos = _Context.Arduino.Where(x => x.IsOnsite == true && x.UserId ==userId).ToList();
            List<ArduinoApiModel> model = arduinos.Select(x => new ArduinoApiModel
            {
                ArduinoId = x.ArduinoId,
                Vid = x.Vid,
                Pid = x.Pid,
                SerialNumber = x.SerialNumber,
                Bn = x.Bn,
                DeploymentDate = x.DeploymentDate,
                IsVerified = x.IsVerified,
                IsActivated = x.IsActivated,
                DateOfActivation = x.DateOfActivation,
                LastPowerOnDate = x.LastPowerOnDate,
                LastPowerOnTime = x.LastPowerOnTime,
                IsActive = x.IsActive,
                IsOnsite = x.IsOnsite,
                UserId = x.UserId,

            }).ToList();

            return model;
        }

        //Get List of Activated devices registered by Agric Extension Officer that are on site and are active
        public List<ArduinoApiModel> GetActivatedDevicesRegisterdByAgricExtensionOfficerThatAreOnsiteAndActive( string userId)
        {
            List<Arduino> arduinos = _Context.Arduino.Where(x => x.IsOnsite == true && x.IsActive == true && x.UserId == userId).ToList();
            List<ArduinoApiModel> model = arduinos.Select(x => new ArduinoApiModel
            {
                ArduinoId = x.ArduinoId,
                Vid = x.Vid,
                Pid = x.Pid,
                SerialNumber = x.SerialNumber,
                Bn = x.Bn,
                DeploymentDate = x.DeploymentDate,
                IsVerified = x.IsVerified,
                IsActivated = x.IsActivated,
                DateOfActivation = x.DateOfActivation,
                LastPowerOnDate = x.LastPowerOnDate,
                LastPowerOnTime = x.LastPowerOnTime,
                IsActive = x.IsActive,
                IsOnsite = x.IsOnsite,
                UserId = x.UserId,

            }).ToList();

            return model;
        }

        //Get List of Activated devices registered by Agric Extension Officer that are on site and are unactive
        public List<ArduinoApiModel> GetActivatedDevicesRegisterdByAgricExtensionOfficerThatAreOnsiteAndUnactive( string userId)
        {
            List<Arduino> arduinos = _Context.Arduino
                .Where(x => x.IsOnsite == true && x.IsActive == false && x.UserId== userId)
                .ToList();
            List<ArduinoApiModel> model = arduinos.Select(x => new ArduinoApiModel
            {
                ArduinoId = x.ArduinoId,
                Vid = x.Vid,
                Pid = x.Pid,
                SerialNumber = x.SerialNumber,
                Bn = x.Bn,
                DeploymentDate = x.DeploymentDate,
                IsVerified = x.IsVerified,
                IsActivated = x.IsActivated,
                DateOfActivation = x.DateOfActivation,
                LastPowerOnDate = x.LastPowerOnDate,
                LastPowerOnTime = x.LastPowerOnTime,
                IsActive = x.IsActive,
                IsOnsite = x.IsOnsite,
                UserId = x.UserId,

            }).ToList();

            return model;
        }


         public List<RegisteredDevicesModel> GetDevicesRegisteredByOfficer(string userId)
        {
            List<Arduino> arduinos = _Context.Arduino.Where(x => x.UserId == userId)
                                                     .Include(x => x.ApplicationUser)
                                                     .ToList();
            List<RegisteredDevicesModel> model = arduinos.Select(x => new RegisteredDevicesModel
            {
                ArduinoId = x.ArduinoId,
                Vid = x.Vid,
                Pid = x.Pid,
                SerialNumber = x.SerialNumber,
                Bn = x.Bn,
                UserId = x.UserId,
                OfficerName = x.ApplicationUser.FullName

            }).ToList();

            return model;
        }





    }
}
