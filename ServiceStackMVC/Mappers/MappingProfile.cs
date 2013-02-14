using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using ServiceStackMVC.Models;
using ServiceStackMVC.Models.DTOs;
using ServiceStackMVC.Models.ViewModels;

namespace ServiceStackMVC.Mappers
{
    public class MappingProfile : Profile
    {
        public override string ProfileName
        {
            get { return "MappingProfile"; }
        }

        protected override void Configure()
        {
            Mapper.CreateMap<OrganizationDto, Organization>();
            Mapper.CreateMap<Organization, OrganizationDto>();

            Mapper.CreateMap<UserDto, User>();
            Mapper.CreateMap<User, UserDto>();

            //Mapper.CreateMap<EventDto, Event>();
            //Mapper.CreateMap<Event, EventDto>();

            Mapper.CreateMap<OrganizationDto, OrganizationModel>();
            Mapper.CreateMap<OrganizationModel, OrganizationDto>();

            Mapper.CreateMap<OrganizationDto, OrganizationDetailModel>();
            Mapper.CreateMap<OrganizationDetailModel, OrganizationDto>();

            Mapper.CreateMap<UserDto, UserModel>();
            Mapper.CreateMap<UserModel, UserDto>();

            Mapper.CreateMap<UserDto, UserDetailModel>();
            Mapper.CreateMap<UserDetailModel, UserDto>();

            //Mapper.CreateMap<EventDto, EventModel>();
            //Mapper.CreateMap<EventModel, EventDto>();

            //Mapper.CreateMap<EventDto, EventDetailModel>();
            //Mapper.CreateMap<EventDetailModel, EventDto>();
        }
    }
}