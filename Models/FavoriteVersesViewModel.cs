using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KLove.Models
{
    public class FavoriteVersesViewModel
    {
        public IEnumerable<FavoriteVerses> favoriteVerses { get; set; }
        public int VerseId { get; set; }
    }
}
