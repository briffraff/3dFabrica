﻿namespace Fabrica.Services.Models
{
    using System.ComponentModel.DataAnnotations;
    using Microsoft.EntityFrameworkCore.Metadata.Internal;
    using Fabrica.Models;
    using Fabrica.Models.Enums;
    using Infrastructure.Mapping;
    using System.Collections.Generic;

    public class PropServiceModel : IMapWith<Prop>
    {
        [Key]
        public string Id { get; set; }

        public string Name { get; set; }

        public PropType propType { get; set; }

        public double Price { get; set; }

        public string ImageUrl { get; set; }

        public string Hashtags { get; set; }

        public string Description { get; set; }

        public bool IsDeleted { get; set; }

        public string PropCreatorId { get; set; }
        public FabricaUser PropCreator { get; set; }

        public ICollection<PropOrder> Orders { get; set; }
    }
}
