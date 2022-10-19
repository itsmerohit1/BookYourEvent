using AutoMapper;
using BookEvent.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookEvent.Helper
{
    public class LoginUserToUser
    {
        public User LoginUserToUserMapping(LoginUser e)
        {
            var config = new MapperConfiguration(cfg =>
            {

                cfg.CreateMap<LoginUser, User>();

            });
            IMapper iMapper = config.CreateMapper();

            var source = e;

            var destination = iMapper.Map<LoginUser, User>(source);
            return destination;
        }


    }
}