using System.ComponentModel.DataAnnotations;

namespace Application.Enums
{
    public enum FileTypeEnum
    {
        [Display(Name = "کاربران")]
        Users = 1,

        [Display(Name = "سفارشات")]
        Orders = 1,
    }
}
