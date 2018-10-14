using System;
using System.Collections.Generic;
using System.Text;

namespace vtb.Core.Utils.Results
{
    public class PagedResult<TRecord> where TRecord : class
    {
        public int CurrentPage { get; set; }
        public int PerPage { get; set; }

        public int AllRecords { get; set; }

        public ICollection<TRecord> Records { get; set; }
    }
}
