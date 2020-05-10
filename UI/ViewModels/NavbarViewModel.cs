using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UI.ViewModels
{
    public class NavbarViewModel
    {
        public class NestedProductViewModel
        {
            public int Id { get; set; }
            public string Text { get; set; }
        }
        public int Id { get; set; }
        public string Text { get; set; }

        public List<NestedProductViewModel> Groups { get; set; } = new List<NestedProductViewModel>();
    }
}
