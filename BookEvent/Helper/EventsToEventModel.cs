using AutoMapper;
using BookEvent.Models;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookEvent.Helper
{
    public class EventsToEventModel
    {
        public Event EventsToEventModelMapping(Events e)
        {
            var config = new MapperConfiguration(cfg =>
            {

                cfg.CreateMap<Events,Event>();

            });
            IMapper iMapper = config.CreateMapper();

            var source = e;

            var destination = iMapper.Map<Event>(e);
            return destination;
        }
    }
}
