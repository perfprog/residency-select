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
    
    public partial class ResxValue
    {
        public int Id { get; set; }
        public int ResxId { get; set; }
        public int CultureId { get; set; }
        public string Value { get; set; }
    
        public virtual Culture Culture { get; set; }
        public virtual Resx Resx { get; set; }
    }
}
