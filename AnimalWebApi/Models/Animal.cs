using System;
using System.Collections.Generic;

namespace AnimalWebApi.Models
{
    public partial class Animal
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Age { get; set; }
        public string Gender { get; set; }
    }
}
