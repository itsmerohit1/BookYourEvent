using AutoMapper;
using BookEvent.Models;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookEvent.Helper
{

    public class EventModelToEvents
    {
        public Events EventModelToEventsMapping(Event e)
        {
            var config = new MapperConfiguration(cfg =>
            {

                cfg.CreateMap<Event,Events>();

            });
            IMapper iMapper = config.CreateMapper();

            var source = e;

            var destination = iMapper.Map<Events>(e);
            return destination;
        }
    }
}
