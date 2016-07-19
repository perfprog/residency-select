using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Text;
using PPI.Core.Web.Models;

namespace PPI.Core.Web.HtmlHelpers
{
    public class PagingBuilder
    {
        public string buildHtmlItem(string url, string display, bool active = false, bool disabled = false)
        {        // every item wrapped in LI tag 
            TagBuilder liTag = new TagBuilder("li");
            if (disabled && !active)
            {
                // add disabled class and use a SPAN tag inside
                liTag.MergeAttribute("class", "disabled");
                var spanTag = new TagBuilder("span");
                spanTag.InnerHtml = display;
                liTag.InnerHtml = spanTag.ToString();
            }
            else
            {
                if (active)
                {
                    liTag.MergeAttribute("class", "active");
                }
                // use inner A tag
                TagBuilder tag = new TagBuilder("a");
                tag.MergeAttribute("href", url);
                tag.InnerHtml = display;
                liTag.InnerHtml = tag.ToString();
            }
            return liTag.ToString();
        }
        public string buildHtmlItemSize(string url, string display, bool active = false, bool disabled = false)
        {        // every item wrapped in LI tag 
            TagBuilder liTag = new TagBuilder("li");
            if (disabled && !active)
            {
                // add disabled class and use a SPAN tag inside
                liTag.MergeAttribute("class", "disabled");
                var spanTag = new TagBuilder("span");
                spanTag.InnerHtml = display;
                liTag.InnerHtml = spanTag.ToString();
            }
            else
            {
                if (active)
                {
                    liTag.MergeAttribute("class", "list-group-item-info");
                    var spanTag = new TagBuilder("span");
                    spanTag.InnerHtml = display;
                    liTag.InnerHtml = spanTag.ToString();
                }
                else
                {
                    // use inner A tag
                    TagBuilder tag = new TagBuilder("a");
                    tag.MergeAttribute("href", url);
                    tag.InnerHtml = display;
                    liTag.InnerHtml = tag.ToString();
                }
                
            }
            return liTag.ToString();
        }
    }
    public static class PagingHelpers
    {
     
        [Log]
        public static MvcHtmlString PageLinks(this HtmlHelper html, PagingInfo pagingInfo, Func<int, string> pageUrl)
        {
            var pagingBuilder = new PagingBuilder();
            if (pagingInfo.LastPage == 0)
            {
                var calcPage = (pagingInfo.TotalRecords -1) / pagingInfo.PageSize + 1;
                if (calcPage <= 0)
                    calcPage = 1;
                pagingInfo.LastPage = calcPage;
            }
            StringBuilder result = new StringBuilder();
            //First Page
            string firstPage = pagingBuilder.buildHtmlItem(pageUrl(pagingInfo.FirstPage), "First", false);
            result.Append(firstPage);
            //previous link
            string prevLink = (pagingInfo.CurrentPage == 1) ? pagingBuilder.buildHtmlItem(pageUrl(pagingInfo.CurrentPage), "Prev", false, true) : pagingBuilder.buildHtmlItem(pageUrl(pagingInfo.CurrentPage - 1), "Prev");
            result.Append(prevLink);
            // only show up to 5 links to the left of the current page
            
            //var start = (pagingInfo.CurrentPage < pagingInfo.NavSize) ? pagingInfo.NavSize - (pagingInfo.NavSize - pagingInfo.CurrentPage) : (pagingInfo.CurrentPage);
            int start;
            int end;

            if (pagingInfo.CurrentPage + 2 > pagingInfo.NavSize)
            {
                start = (pagingInfo.CurrentPage + 2 > pagingInfo.LastPage)
                    ? pagingInfo.CurrentPage - pagingInfo.NavSize + 1 + (pagingInfo.LastPage - pagingInfo.CurrentPage)
                    : pagingInfo.CurrentPage - 2;
                end = (pagingInfo.CurrentPage + 2 > pagingInfo.LastPage)
                    ? pagingInfo.LastPage
                    : pagingInfo.CurrentPage + 2;
            }
            else
            {
                start = 1;
                end = pagingInfo.NavSize;
            }

            for (int i = start; i <= end; i++)
            {
                string pageHtml;
                pageHtml = (i == pagingInfo.CurrentPage)
                    ? pagingBuilder.buildHtmlItem(pageUrl(i), i.ToString(), true, true)
                    : (i > ((pagingInfo.TotalRecords - 1) / pagingInfo.PageSize + 1))
                        ? pagingBuilder.buildHtmlItem(pageUrl(i), i.ToString(), false, true)
                        : pagingBuilder.buildHtmlItem(pageUrl(i), i.ToString());
                result.Append(pageHtml);
            }
                        
            // next link
            string nextLink = (pagingInfo.CurrentPage == end || (pagingInfo.CurrentPage + 1) * pagingInfo.PageSize >= pagingInfo.TotalRecords)
               ? pagingBuilder.buildHtmlItem(pageUrl(pagingInfo.CurrentPage + 1), "Next", false, true)
               : (pagingInfo.CurrentPage == pagingInfo.LastPage)
                   ? pagingBuilder.buildHtmlItem(pageUrl(pagingInfo.CurrentPage + 1), "Next", false, true)
                   : pagingBuilder.buildHtmlItem(pageUrl(pagingInfo.CurrentPage + 1), "Next");
            
            result.Append(nextLink);
            // LastPage
            string lastPage = pagingBuilder.buildHtmlItem(pageUrl(pagingInfo.LastPage), "Last", false);
            result.Append(lastPage);
            return MvcHtmlString.Create(result.ToString());
        }
        [Log]
        public static MvcHtmlString PageSizeList(this HtmlHelper html, List<string> sizeList, Func<int, string> sizeUrl)
        {
            StringBuilder result = new StringBuilder();
            var CurrentPageSize = PPI.Core.Web.Infrastructure.Utility.GetCookie("pageSize").ToString();
            foreach (var item in sizeList)
            {
                var currentItem = "10000"; // MAX for ALL                    
                var builder = new PagingBuilder();
                var Display = item.ToString();

                if (item != "ALL")
                   currentItem = item;                

                Boolean isCurrent = (CurrentPageSize == currentItem);
                result.Append(builder.buildHtmlItemSize(sizeUrl(int.Parse(currentItem)), Display, isCurrent, isCurrent));                

            }

            return MvcHtmlString.Create(result.ToString());
        }
    
    }
    
}
    
