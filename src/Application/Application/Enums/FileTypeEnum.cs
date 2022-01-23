using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Application.Enums
{
    public enum ApiResultStatusCode
    {
        [Display(Name = "کاربران")]
        Users = 1,

        [Display(Name = "سفارشات")]
        Orders = 1,
    }
}
