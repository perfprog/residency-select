//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace PPI.Core.Domain.Entities
{
    using System;
    using System.Collections.Generic;
    
    public partial class vDashboard
    {
        public int Id { get; set; }
        public int PersonId { get; set; }
        public int EventId { get; set; }
        public string HPIDate { get; set; }
        public string HDSDate { get; set; }
        public string MVPIDate { get; set; }
        public Nullable<System.DateTime> LastUpdated { get; set; }
    
        public virtual Person Person { get; set; }
    }
}