using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UI.ViewModels
{
    public class NewProductsViewModel
    {
        public int Id { get; set; }
        public float Price { get; set; }
        public string BaseImage { get; set; }
        public string Name { get; set; }
        public string Slug { get; set; }
    }

}
