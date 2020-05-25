using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UI.ViewComponents
{
    public class UIPagingViewComponent : ViewComponent
    {
        public class Paging
        {
            public int TotalCount { get; protected set; }
            public int TotalPageCount { get; protected set; }
            public int CountInPage { get; protected set; }
            public int CurrentPage { get; protected set; }
            public string Key { get; set; }
            public string OrderSelect { get; set; }
            

        }
        public class Pager<T> : Paging where T : class
        {
            public Pager(List<T> items, int totalCount, int CountInPage, int CurrentPage)
            {
                this.Items = items;
                InitialPaging(totalCount, CountInPage, CurrentPage);
            }
            public List<T> Items { get; set; }

            private void InitialPaging(int totalCount, int CountInPage, int CurrentPage)
            {
                TotalCount = totalCount;
                this.CountInPage = CountInPage;
                this.CurrentPage = CurrentPage;
                TotalPageCount = (int)Math.Ceiling((decimal)TotalCount / CountInPage);
            }
            

        }


        public async Task<IViewComponentResult> InvokeAsync(Paging pager)
        {
            return View(pager);
        }
    }
}
