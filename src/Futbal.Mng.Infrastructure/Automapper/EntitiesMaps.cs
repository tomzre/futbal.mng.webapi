using System.Collections.Generic;
using AutoMapper;
using Futbal.Mng.Domain.Event;
using Futbal.Mng.Domain.UserManagement;
using Futbal.Mng.Domain.ValueObjects;
using Futbal.Mng.Infrastructure.DTO;

namespace Futbal.Mng.Infrastructure.Automapper
{
    public class EntitiesMaps : Profile
    {
        public EntitiesMaps()
        {
            CreateMap<User, AttendeeDto>()
                .ForMember(x => x.FirstName, w => w.MapFrom(o => o.Username))
                .ForMember(x => x.LastName, w => w.MapFrom(o => o.Email));

            CreateMap<Game, GameDetailsDto>()
                .ForMember(x => x.Address, w => w.MapFrom(o => o.Place))
                .ForMember(x => x.Attendees, w => w.Ignore());

            CreateMap<User, OwnerDto>()
                .ForMember(x => x.FirstName, w => w.MapFrom(o => o.Username))
                .ForMember(x => x.LastName, w => w.MapFrom(o => o.Email));

            CreateMap<Address, PlaceDto>();
        }
    }
}