﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fabrica.Infrastructure.Mapping;
using Fabrica.Models;

namespace Fabrica.Services.Models
{
    public class MarvelousPropOrderServiceModel : IMapWith<MarvelousPropOrder>
    {
        public string MarvelousPropId { get; set; }
        public MarvelousProp MarvelousProp { get; set; }

        public string OrderId { get; set; }
        public Order Order { get; set; }
    }
}
