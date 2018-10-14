using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using vtb.Core.Utils.Validators;

namespace vtb.Core.Utils.Requests
{
    public class PagedRequest
    {
        public int Page { get; set; } = 0;

        [Required]
        [GreaterOrEqualTo(1)]
        public int PerPage { get; set; } = 10;

        public string Order { get; set; }
    }
}
