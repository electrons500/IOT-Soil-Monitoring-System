using Backendapi.Models.Data.ApiModel;
using Backendapi.Models.Data.IOTSMSDBContext;
using System.Collections.Generic;
using System.Linq;

namespace Backendapi.Models.Data.Service
{
    public class RegionService
    {
        private IOTSMSDBContext.IOTSMSDBContext _Context;
        public RegionService(IOTSMSDBContext.IOTSMSDBContext context)
        {
            _Context = context;
        }


        public List<RegionApiModel> GetRegions()
        {
            List<Region> regions = _Context.Region.ToList();
            List<RegionApiModel> model = regions.Select(x => new RegionApiModel
            {
                RegionId = x.RegionId,
                RegionName = x.RegionName,
            }).ToList();

            return model;
        }

        public RegionApiModel GetRegionsDetails(int regionId)
        {
            Region region = _Context.Region.Where(x => x.RegionId == regionId).FirstOrDefault();
            RegionApiModel model = new RegionApiModel()
            {
                RegionId = region.RegionId,
                RegionName = region.RegionName
            };

            return model;
        }

        public bool AddNewRegion(RegionApiModel model)
        {
            Region region = new Region
            {
                RegionName = model.RegionName
            };

            _Context.Region.Add(region);
            _Context.SaveChanges();

            return true;

        }

        public bool EditRegion(RegionApiModel model)
        {
            Region region = _Context.Region.Where(x => x.RegionId == model.RegionId).FirstOrDefault();
            region.RegionName = model.RegionName;

            _Context.Region.Update(region);
            _Context.SaveChanges();

            return true;
        }


        public bool DeleteRegion(int regionId)
        {
            Region region = _Context.Region.Where(x => x.RegionId == regionId).FirstOrDefault();
            _Context.Region.Remove(region);
            _Context.SaveChanges();

            return true;
        }

    }
}
