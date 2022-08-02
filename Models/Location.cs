using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace LocationApi.Models
{
    public partial class Location
    {
        public int Id { get; set; }

        [DefaultValue("")]
        public string? Locationname { get; set; }

        [DefaultValue(-1)]
        public double? X { get; set; }
        
        [DefaultValue(-1)]
        public double? Y { get; set; }
    }
}
