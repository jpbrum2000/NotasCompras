using AutoMapper;
using Domain;
using WebApi.ViewModel;

namespace WebApi.Helpers {
    public class AutoMapperProfiles : Profile {
        public AutoMapperProfiles()
        {
            CreateMap<NotaCompra, NotaCompraViewModel>().ReverseMap();
        }
    }
}