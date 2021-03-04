using KLove.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KLove.Repositories
{
    public interface IVersesRepository
    {
        // Get verses from the KLove API
        Task<VerseResponses> GetVerses(VersesData versesData);

        // Get the favorite verses from the database
        Task<List<FavoriteVerses>> GetFavoriteVerses();

        // Remove the verse from the favorites list
        Task<FavoriteVerses> ToggleFavorites(int Id);

        // Add to the favorites (db)
        Task<bool> AddToFavorites(VerseResponse favoriteVerses);
    }
}
