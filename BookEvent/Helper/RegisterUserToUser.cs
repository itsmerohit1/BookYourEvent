using AutoMapper;
using BookEvent.Models;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookEvent.Helper
{
    public class RegisterUserToUser
    {
        public User RegisterUserModelToUserMapping(RegisterUser e)
        {
            var config = new MapperConfiguration(cfg =>
            {

                cfg.CreateMap<RegisterUser, User>();

            });
            IMapper iMapper = config.CreateMapper();

            var source = e;

            var destination = iMapper.Map<RegisterUser, User>(source);
            return destination;
        }
    }
}