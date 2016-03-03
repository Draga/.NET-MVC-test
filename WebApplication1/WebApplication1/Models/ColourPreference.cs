using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class ColourPreference
    {
        public int ColourId { get; set; }

        public string ColourName { get; set; }

        public bool Favourite { get; set; }
    }
}