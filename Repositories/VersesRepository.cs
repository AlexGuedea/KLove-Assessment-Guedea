using KLove.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;
using KLove.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.WebUtilities;

namespace KLove.Repositories
{
    public class VersesRepository : IVersesRepository
    {
        private VersesContext _dbContext;

        public VersesRepository(VersesContext versesContext)
        {
            _dbContext = versesContext;
        }
                
        public VerseResponses GetVersesTest()
        {
            // just something to play with.....
            VerseResponses f = new VerseResponses();
            {
                f.Verses = new List<VerseResponse> {
                new VerseResponse
                {
                    VerseText = "...Let everything you say be good and helpful, so that your words will be an encouragement to those who hear them.",
                    ImageLink = "https://www.klove.com:443/-/media/assets/cloud/20201006--ephesians-4-29.jpg",
                    VerseDate = Convert.ToDateTime("2020-10-06T00:00:00Z"),
                    VideoLink = "",
                    ReferenceLink = "http://nlt.to/Ephesians%204",
                    VerseNumbers = "29",
                    Chapter = "4",
                    Book = "Ephesians",
                    ReferenceText = "Ephesians 4:29b",
                    BibleReferenceLink = "http://nlt.to",
                    FacebookShareUrl = "u=http://www.klove.com/ministry/verse-of-the-day?d=10/06/2020",
                    TwitterShareUrl = System.Net.WebUtility.UrlEncode("text=K-LOVE's Verse of the Day. Ephesians 4:29b NLT&url=http://www.klove.com/ministry/verse-of-the-day?d=10/06/2020"),
                    PinterestShareUrl =System.Net.WebUtility.UrlEncode("media=https://www.klove.com:443/-/media/assets/cloud/20201006--ephesians-4-29.jpg&description=K-LOVE's Verse of the Day. ...Let everything you say be good and helpful, so that your words will be an encouragement to those who hear them. Ephesians 4:29b NLT&url=http://www.klove.com/ministry/verse-of-the-day?d=10/06/2020"),
                    IsValid = false,
                    Id = "{14BEF93C-509B-48C8-8CE2-EAF3DDB5A0ED}",
                    Url = System.Net.WebUtility.UrlEncode("http://www.klove.com/ministry/verse-of-the-day?d=10/06/2020")
                }
                };

            };

            return f;
        }
        public async Task<List<FavoriteVerses>> GetFavoriteVerses()
        {
            return await _dbContext.GetFavoriteVerses.ToListAsync();
        }

        public async Task<VerseResponses> GetVerses(VersesData versesData)
        {
            VerseResponses versesList = new VerseResponses();
            try
            {
                using (var httpClient = new HttpClient())
                {
                    httpClient.DefaultRequestHeaders.Clear();
                    httpClient.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", "d10161af8cf44f0c8267d571c682fda4");
                    using (var KLoveResponse = await httpClient.GetAsync("https://emfservicesstage-api.azure-api.net/bible/v1/getversesbydate?siteId=1&startdate=" + versesData.VerseDate.ToString("MM/dd/yyyy") + "&PageSize=" + versesData.NumberOfVerses.ToString()))
                    {
                        string KLoveApiResponse = await KLoveResponse.Content.ReadAsStringAsync();
                        versesList = JsonConvert.DeserializeObject<VerseResponses>(KLoveApiResponse);
                    }
                }
            }
            catch (Exception ex)
            {
                // please - more error handling
                System.Diagnostics.Debug.Print(ex.Message);
               
            }
            return versesList;

        }



        public async Task<FavoriteVerses> ToggleFavorites(int Id)
        {
            
            FavoriteVerses verse = _dbContext.GetFavoriteVerses.FirstOrDefault(c => c.VerseId == Id);
            if (verse != null)
            {
                _dbContext.GetFavoriteVerses.Remove(verse);
                _dbContext.SaveChanges();
            }
            return verse;
        }

        public async Task<bool> AddToFavorites(VerseResponse verseResponse)
        {
            bool rtn = true;
            try
            {
                // if the verse already exists, then do not add it again.  Some users like to click multiple times.
                FavoriteVerses verseFind =  _dbContext.GetFavoriteVerses.FirstOrDefault(c => c.VerseText == verseResponse.VerseText);
                if (verseFind == null)
                {
                    FavoriteVerses verse = new FavoriteVerses();
                    verse.VerseText = verseResponse.VerseText;
                    verse.ImageLink = verseResponse.ImageLink ?? "";
                    verse.VerseDate = verseResponse.VerseDate;
                    verse.VideoLink = verseResponse.VideoLink ?? "";
                    verse.ReferenceLink = verseResponse.ReferenceLink ?? "";
                    verse.VerseNumbers = verseResponse.VerseNumbers ?? "";
                    verse.Chapter = verseResponse.Chapter ?? "";
                    verse.Book = verseResponse.Book ?? "";
                    verse.ReferenceText = verseResponse.ReferenceText ?? "";
                    verse.BibleReferenceLink = verseResponse.BibleReferenceLink ?? "";
                    verse.FacebookShareUrl = verseResponse.FacebookShareUrl ?? "";
                    verse.TwitterShareUrl = verseResponse.TwitterShareUrl ?? "";
                    verse.PinterestShareUrl = verseResponse.PinterestShareUrl ?? "";
                    verse.IsValid = verseResponse.IsValid;
                    verse.Id = verseResponse.Id ?? "";
                    verse.Url = verseResponse.Url ?? "";

                    _dbContext.GetFavoriteVerses.Add(verse);
                    _dbContext.SaveChanges();
                }
            }
            catch 
            {
                // please - more error handling
                rtn = false;
            }
            return rtn;
        }
    }
}
