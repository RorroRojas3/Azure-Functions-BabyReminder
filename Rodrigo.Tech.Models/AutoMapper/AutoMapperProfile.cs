using AutoMapper;
using Rodrigo.Tech.Models.Request;
using Rodrigo.Tech.Models.Response;
using Rodrigo.Tech.Repository.Tables;

namespace Rodrigo.Tech.Models.AutoMapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            #region Requests
            CreateMap<EmailRequest, Email>();
            #endregion

            #region Responses
            CreateMap<Email, EmailResponse>();
            CreateMap<EmailBody, EmailBodyResponse>();
            CreateMap<Language, LanguageResponse>();
            #endregion
        }
    }
}
