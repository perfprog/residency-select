using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
using PPI.Core.Domain.Abstract;
using PPI.Core.Domain.Entities;

namespace PPI.Core.Web.Models
{
    public class SiteViewModel
    {

        public int Id { get; set; }
        public string SiteName { get; set; }
        public string FriendlyName { get; set; }
        public string BrandingColor { get; set; }
        public byte[] BrandingLogo { get; set; }
        public string BrandingLogoMimeType { get; set; }
        public byte[] BrandingBackground { get; set; }
        public string BrandingBackgroundMimeType { get; set; }
        public int OrganizationId { get; set; }
        public string OrganizationName { get; set; }
    }
}