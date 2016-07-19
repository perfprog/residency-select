using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PPI.Core.Web.Models
{
    public class CheckBoxModel
    {
        public string Value { get; set; }
        public string Name { get; set; }
        public bool Selected { get; set; }
        public bool Disabled { get; set; }
    }
}