using Backendapi.Models.Data.ApiModel;
using Backendapi.Models.Data.IOTSMSDBContext;
using System.Collections.Generic;
using System.Linq;

namespace Backendapi.Models.Data.Service
{
    public class SoilCategoryService
    {
        private IOTSMSDBContext.IOTSMSDBContext _Context;
        public SoilCategoryService(IOTSMSDBContext.IOTSMSDBContext context)
        {
            _Context = context;
        }

        //Get List of Soil
        
        public List<SoilCategoryApiModel> GetSoilCategories()
        {
            List<SoilCategory> soilCategories = _Context.SoilCategory.ToList();
            List<SoilCategoryApiModel> model = soilCategories.Select(x => new SoilCategoryApiModel
            {
                SoilCategoryId = x.SoilCategoryId,
                SoilName = x.SoilName
            }).ToList();

            return model;
        }


        //Get soil details

        public SoilCategoryApiModel GetSoilCategoryDetails(int id)
        {
            SoilCategory soilCategory = _Context.SoilCategory.Where(x => x.SoilCategoryId == id).FirstOrDefault();
            SoilCategoryApiModel model = new SoilCategoryApiModel()
            {
                SoilCategoryId = soilCategory.SoilCategoryId,
                SoilName = soilCategory.SoilName
            };

            return model;
        }

        //Add new soil
        public bool AddNewSoil(SoilCategoryApiModel model)
        {
            SoilCategory soilCategory = new SoilCategory()
            {
                SoilName = model.SoilName
            };

            _Context.SoilCategory.Add(soilCategory);
            _Context.SaveChanges();

            return true;

        }


        public bool updateSoilCategory(int id,SoilCategoryApiModel model)
        {
            SoilCategory soilCategory = _Context.SoilCategory.Where(x => x.SoilCategoryId == id).FirstOrDefault();

            soilCategory.SoilName = model.SoilName;

            _Context.SoilCategory.Update(soilCategory);
            _Context.SaveChanges();

            return true;
        }

        public bool DeleteSoilCategory(int id)
        {
            SoilCategory soilCategory = _Context.SoilCategory.Where(x => x.SoilCategoryId == id).FirstOrDefault();

            _Context.SoilCategory.Remove(soilCategory);

            _Context.SaveChanges();

            return true;
        }


    }
}
