using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class EditPersonModel
    {
        public int PersonId { get; set; }

        [DisplayName("Authorised")]
        public bool IsAuthorised { get; set; }

        [DisplayName("Enabled")]
        public bool IsEnabled { get; set; }

        public List<Colour> FavouriteColors { get; set; } 
    }
}