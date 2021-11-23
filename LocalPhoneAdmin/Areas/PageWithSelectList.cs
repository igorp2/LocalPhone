
using LocalPhoneDomain.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace LocalPhoneAdmin.Areas
{
    public abstract class PageWithSelectList<T> : PageModel where T : BaseModel
    {
        public SelectList SelectList { get; set; }

        public void PopulateDropDownList(List<T> items, string dataValueField, string dataTextField)
        {
            SelectList = new SelectList(items, dataValueField, dataTextField);
        }

    }
}
