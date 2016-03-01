//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Person
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Person()
        {
            this.Colours = new HashSet<Colour>();
        }
    
        public int PersonId { get; set; }

        [DisplayName("Name")]
        public string FirstName { get; set; }

        [DisplayName("Surname")]
        public string LastName { get; set; }

        [DisplayName("Authorised")]
        public bool IsAuthorised { get; set; }

        [DisplayName("Valid")]
        public bool IsValid { get; set; }

        [DisplayName("Enabled")]
        public bool IsEnabled { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Colour> Colours { get; set; }
    }
}
