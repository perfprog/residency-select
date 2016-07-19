using System.Collections.Generic;
using System.Web.WebPages.Html;
using System.Linq;

namespace PPI.Core.Domain.Concrete
{
    using PPI.Core.Domain.Abstract;
    using PPI.Core.Domain.Entities;

    public static class EventTypeExtention
    {
        public static List<SelectListItem> SelectEventTypeList(this IGenericRepository<EventType> events, int cultureId)
        {
            var SelectEventTypeId = new List<SelectListItem>();
            var loopEvents = events.AsQueryable();
            foreach (var item in loopEvents)
            {
                var selectItem = new SelectListItem();                
                selectItem.Text = item.NameResx.ResxValues.FirstOrDefault(m => m.CultureId == cultureId).Value;
                selectItem.Value = item.Id.ToString();
                SelectEventTypeId.Add(selectItem);
            }

            return SelectEventTypeId;
        }
    
    }
}
