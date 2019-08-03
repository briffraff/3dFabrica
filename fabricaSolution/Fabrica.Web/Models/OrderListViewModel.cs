namespace Fabrica.Web.Models
{
    using AutoMapper;
    using Fabrica.Services.Models;

    public class OrderListViewModel
    {
        public string Id { get; set; }

        public string Prop { get; set; }

        public string MarvelousProp { get; set; }

        public string OrderedOn { get; set; }

        public string Price { get; set; }

        public string Points { get; set; }

        public string PropOwner { get; set; }

        public string MarvelousOwner { get; set; }

        public void ConfigureMapping(Profile mapper)
        {
            mapper.CreateMap<OrderServiceModel, OrderListViewModel>()
                .ForMember(dest => dest.Prop, opt => opt.MapFrom(src => src.Prop.Name))
                .ForMember(dest => dest.MarvelousProp, opt => opt.MapFrom(src => src.MarvelousProp.Name))
                .ForMember(dest => dest.OrderedOn,opt => opt.MapFrom(src => src.OrderedOn.ToString("hh:mm dd/MM/yyyy")))
                .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Prop.Price))
                .ForMember(dest => dest.Points, opt => opt.MapFrom(src => src.MarvelousProp.Points))
                .ForMember(dest => dest.PropOwner, opt => opt.MapFrom(src => src.Client.UserName))
                .ForMember(dest => dest.MarvelousOwner, opt => opt.MapFrom(src => src.Client.UserName));
        }
    }
}
