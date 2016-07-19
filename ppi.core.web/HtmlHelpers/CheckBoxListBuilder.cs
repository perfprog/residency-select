using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Text;
using PPI.Core.Web.Models;

namespace PPI.Core.Web.HtmlHelpers
{
    public class CheckBoxListBuilder
    {
        public string BuildHtmlItem(bool selected, string postModelName,int id,string value,string name)
        {
            TagBuilder tagBuilder = new TagBuilder("div");
            tagBuilder.MergeAttribute("class", "checkbox-nice");
            TagBuilder tagInput = new TagBuilder("input");
            tagInput.MergeAttribute("name", postModelName);
            tagInput.MergeAttribute("id", postModelName + id.ToString());
            tagInput.MergeAttribute("value", value);
            tagInput.MergeAttribute("type", "checkbox");
            if (selected)
                tagInput.MergeAttribute("checked",string.Empty);
            TagBuilder tagLabel = new TagBuilder("label");
            tagLabel.MergeAttribute("for", postModelName + id.ToString());
            tagLabel.SetInnerText(name);
            tagInput.InnerHtml = tagLabel.ToString();
            tagBuilder.InnerHtml = tagInput.ToString();
            return tagBuilder.ToString();
        }
    }
    public static class CheckBoxHelpers
    {
        public static MvcHtmlString CheckBoxListFor(this HtmlHelper html, string name, List<CheckBoxModel> checkBoxItems)
        {
            var CheckBoxListBuilder = new CheckBoxListBuilder();
            StringBuilder result = new StringBuilder();
            if (checkBoxItems != null)
            { 
                for (var i = 0; i < checkBoxItems.Count(); i++)
                {
                
                    var checkbox = CheckBoxListBuilder.BuildHtmlItem(checkBoxItems.ElementAt(i).Selected,name,i, checkBoxItems.ElementAt(i).Value,checkBoxItems.ElementAt(i).Name);
                    result.Append(checkbox);
                
                }
            }
            return MvcHtmlString.Create(result.ToString());
        }
    }
}