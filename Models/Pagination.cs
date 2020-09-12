using System;
using System.Collections.Generic;

namespace BlogCore.Models
{
    public class Pagination<T>
    {
        public List<T> Items { get; set; }

        public int TotalRecords { get; set; }

        public int PageSize { get; set; } = 10;

        public int TotalPages { get; set; }

        public int CurrentPage { get; set; } = 1;
    }
}
