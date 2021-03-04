using KLove.Data;
using KLove.Models;
using KLove.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace KLove.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly VersesContext _dbContext;

        public HomeController(ILogger<HomeController> logger, VersesContext versesContext)
        {
            _logger = logger;  // unused for now, but just watch me fill the event viewer with "stuff"
            _dbContext = versesContext;
        }

        //[HttpGet]
        //public ViewResult Index() => View();
        ////{
        ////    return View();
        ////}

        /// <summary>
        /// Call the KLove test API
        /// </summary>
        /// <param name="versesData"></param>
        /// startdate
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(200, Type = typeof(VerseResponses))]
        [ProducesResponseType(400, Type = typeof(void))]
        public async Task<IActionResult> GetVersesData(VersesData versesData)
        {
            VerseResponses verses = new VerseResponses();
            try
            {
                if (!ModelState.IsValid)
                    return View();
                

                VersesRepository versesRepository = new VersesRepository(_dbContext);
                verses = await versesRepository.GetVerses(versesData);
                for (int x = 0; x < verses.Verses.Count; x++)
                {
                    verses.Verses[x].TwitterShareUrl = System.Web.HttpUtility.UrlEncode(verses.Verses[x].TwitterShareUrl);
                    verses.Verses[x].PinterestShareUrl = System.Web.HttpUtility.UrlEncode(verses.Verses[x].PinterestShareUrl);
                    verses.Verses[x].Url = System.Web.HttpUtility.UrlEncode(verses.Verses[x].Url);
                }
            }
            catch
            {
                return View("Error");
            }
             return View(verses);
           
        }

        /// <summary>
        /// When the user selects a verse to save as favorites, allow the data to be saved.  The user may want to browse the data through some other UI.
        /// </summary>
        /// <param name="versetext"></param>
        /// <param name="imagelink"></param>
        /// <param name="versedate"></param>
        /// <param name="videolink"></param>
        /// <param name="referencelink"></param>
        /// <param name="versenumbers"></param>
        /// <param name="chapter"></param>
        /// <param name="book"></param>
        /// <param name="biblereferencelink"></param>
        /// <param name="facebookshareurl"></param>
        /// <param name="twittershareurl"></param>
        /// <param name="pinterestshareurl"></param>
        /// <param name="isvalid"></param>
        /// <param name="id"></param>
        /// <param name="url"></param>
        /// <returns></returns>
        public async Task<bool> AddToFavorites(string versetext, string imagelink, DateTime versedate, string videolink, string referencelink, 
                            string versenumbers, string chapter, string book, string biblereferencelink, string facebookshareurl, string twittershareurl,
                            string pinterestshareurl, bool isvalid, string id, string url)
        {


        VerseResponse verseResponse = new VerseResponse { VerseText = versetext, ImageLink = imagelink, VerseDate = versedate, VideoLink = videolink, ReferenceLink = referencelink, 
                            VerseNumbers = versenumbers, Chapter = chapter, Book = book, BibleReferenceLink = biblereferencelink, FacebookShareUrl = facebookshareurl, TwitterShareUrl = twittershareurl,
                            PinterestShareUrl = pinterestshareurl, IsValid = isvalid, Id = id, Url = url};
            VersesRepository versesRepository = new VersesRepository(_dbContext);
            await versesRepository.AddToFavorites(verseResponse);
            return true;
        }


        //[HttpPost]
        //public async Task<IActionResult> ToggleFavorite(int VerseId)
        //{
            

        //    VersesRepository versesRepository = new VersesRepository(_dbContext);
        //    await versesRepository.ToggleFavorites(VerseId);
        //    //return View();
        //    return RedirectToAction("Favorites");
        //}

        /// <summary>
        /// 
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Favorites()
        {
            VersesRepository versesRepository = new VersesRepository(_dbContext);
            FavoriteVersesViewModel favoriteVersesView = new FavoriteVersesViewModel();
            favoriteVersesView.favoriteVerses = await versesRepository.GetFavoriteVerses();
            return View(favoriteVersesView);

        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
