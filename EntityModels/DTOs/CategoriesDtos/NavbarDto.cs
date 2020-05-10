using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EntityModels.DTOs.CategoriesDtos
{
    public class NavbarDto
    {
        public class NestedProductGroupDto
        {
            public int Id { get; set; }
            public string Text { get; set; }
        }
        public int Id { get; set; }
        [Required]
        public string Text { get; set; }

        public List<NestedProductGroupDto> Groups { get; set; } = new List<NestedProductGroupDto>();
    }
}
