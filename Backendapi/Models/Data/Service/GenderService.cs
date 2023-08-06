using Backendapi.Models.Data.ApiModel;
using Backendapi.Models.Data.IOTSMSDBContext;
using System.Collections.Generic;
using System.Linq;

namespace Backendapi.Models.Data.Service
{
    public class GenderService
    {
        private IOTSMSDBContext.IOTSMSDBContext _Context;
        public GenderService(IOTSMSDBContext.IOTSMSDBContext context)
        {
            _Context = context;
        }

        public List<GenderApiModel> GetGenders()
        {
            List<Gender> genders = _Context.Gender.ToList();
            List<GenderApiModel> model = genders.Select(x => new GenderApiModel
            {
                GenderId = x.GenderId,
                GenderName = x.GenderName
            }).ToList();

            return model;
        }
    }
}
