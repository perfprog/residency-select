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
    
    public partial class Program
    {
        public Program()
        {
            this.HoganUserInfo = new HashSet<HoganUserInfo>();
            this.OrderForm = new HashSet<OrderForm>();
            this.PracticeCategoryScale = new HashSet<PracticeCategoryScale>();
            this.PracticeScale = new HashSet<PracticeScale>();
            this.ProgramPracticeReports = new HashSet<ProgramPracticeReports>();
            this.ProgramSites = new HashSet<ProgramSite>();
            this.OESSetup = new HashSet<OESSetup>();
        }
    
        public int Id { get; set; }
        public string ProgramName { get; set; }
    
        public virtual ICollection<HoganUserInfo> HoganUserInfo { get; set; }
        public virtual ICollection<OrderForm> OrderForm { get; set; }
        public virtual ICollection<PracticeCategoryScale> PracticeCategoryScale { get; set; }
        public virtual ICollection<PracticeScale> PracticeScale { get; set; }
        public virtual ICollection<ProgramPracticeReports> ProgramPracticeReports { get; set; }
        public virtual ICollection<ProgramSite> ProgramSites { get; set; }
        public virtual ICollection<OESSetup> OESSetup { get; set; }
    }
}
