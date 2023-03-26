using AutoMapper;
using Note.EntityLayer.Concrete;
using Note.Web.Areas.Manager.ViewModels;
using Note.Web.ViewModels;

namespace Note.Web.Mapping
{
    public class MapProfile : Profile
    {
        public MapProfile()
        {
            CreateMap<AppNote, AppNoteListVM>().ReverseMap();
            CreateMap<SignInVM, AppUser>().ReverseMap();
            CreateMap<AppRole, CreateRoleVM>().ReverseMap();
            CreateMap<AppNote, DetailNoteVM>().ReverseMap();
            CreateMap<AppUser, MemberComponentAppUserVM>().ReverseMap();
            CreateMap<Message, MessageVM>().ReverseMap();
            CreateMap<MemberAppUserVM, AppUser>().ReverseMap();
            CreateMap<AppUser, AdminUserListVM>().ReverseMap();

        }
    }
}
