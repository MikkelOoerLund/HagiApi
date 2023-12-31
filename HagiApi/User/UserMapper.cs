﻿using HagiDomain;
using AutoMapper;

namespace HagiApi
{
    public class UserMapper : Profile
    {
        public UserMapper()
        {
            CreateMap<AuthenticationDTO, User>();
        }
    }
}
