using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace UI.ViewModels
{
    public class CommentViewModel
    {
        [Required(ErrorMessage = "لطفا برای فیلد {0} مقدار وارد کنید")]
        [MaxLength(300,ErrorMessage ="حداکثر مقدار فیلد {0} 300 کارکتر میباشد")]
        [MinLength(1, ErrorMessage = "حداقل مقدار فیلد {0} 1 کارکتر میباشد")]
        [DisplayName("ایمیل")]
        public string Email { get; set; }
        [Required(ErrorMessage = "لطفا برای فیلد {0} مقدار وارد کنید")]
        [MaxLength(300, ErrorMessage = "حداکثر مقدار فیلد {0} 300 کارکتر میباشد")]
        [MinLength(1, ErrorMessage = "حداقل مقدار فیلد {0} 1 کارکتر میباشد")]
        [DisplayName("نام")]
        public string name { get; set; }
        [Required(ErrorMessage = "لطفا برای فیلد {0} مقدار وارد کنید")]
        [MaxLength(300, ErrorMessage = "حداکثر مقدار فیلد {0} 300 کارکتر میباشد")]
        [MinLength(1, ErrorMessage = "حداقل مقدار فیلد {0} 1 کارکتر میباشد")]
        [DisplayName("متن نظر")]
        public string Text { get; set; }
    }
}
