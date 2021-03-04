using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KLove.Models
{
    public class VerseResponses
    {
        public List<VerseResponse> Verses { get; set; }
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
        public bool HasMorePages { get; set; }
        public object Id { get; set; }
        public object Url { get; set; }
    }
}
