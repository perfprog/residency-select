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
    
    public partial class ScheduledEmail
    {
        public ScheduledEmail()
        {
            this.ScheduledEmailPerson = new HashSet<ScheduledEmailPerson>();
        }
    
        public int Id { get; set; }
        public System.DateTime ScheduleDate { get; set; }
        public Nullable<System.DateTime> StartDate { get; set; }
        public Nullable<System.DateTime> CompletedDate { get; set; }
        public Nullable<int> EmailId { get; set; }
    
        public virtual Email Email { get; set; }
        public virtual ICollection<ScheduledEmailPerson> ScheduledEmailPerson { get; set; }
    }
}
