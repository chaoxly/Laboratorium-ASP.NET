using Laboratorium5.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Linq;

namespace Laboratorium5.Controllers
{
    [Authorize]
    public class AlbumController : Controller
    {
        private readonly IAlbumService _albumService;
        private readonly IPiosenkaService _piosenkaService;

        public AlbumController(IAlbumService albumService, IPiosenkaService piosenkaService)
        {
            _albumService = albumService;
            _piosenkaService = piosenkaService;
        }

        [AllowAnonymous]
        public IActionResult Index()
        {
            var albumDictionary = _albumService.FindAll().ToDictionary(a => a.Id);
            return View(albumDictionary);
        }
        [Authorize(Roles = "admin")]
        [HttpGet]
        public IActionResult Create()
        {
            return View(new Album());
        }
        [Authorize(Roles = "admin")]
        [HttpPost]
        public IActionResult Create(Album model)
        {
            if (ModelState.IsValid)
            {
                _albumService.Add(model);
                return RedirectToAction("Index");
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult CreateSong(int albumId)
        {
            var albums = _albumService.FindAll(); 
            ViewBag.Albums = new SelectList(albums, "Id", "Nazwa"); 
            ViewBag.AlbumId = albumId;
            return View();
        }


        [HttpPost]
        public IActionResult CreateSong(Piosenka model, int AlbumId)
        {
            if (ModelState.IsValid)
            {
                _piosenkaService.Add(model);
                return RedirectToAction("Index");
            }
            return View(model);
        }
        [HttpGet]
        public IActionResult CreateApi()
        {
            var albums = _albumService.FindAll(); 
            ViewBag.Albums = new SelectList(albums, "Id", "Nazwa");
            return View(new Album());
        }

        [HttpPost]
        public IActionResult CreateApi([FromBody] Album album)
        {
            if (ModelState.IsValid)
            {
                _albumService.Add(album);
                return RedirectToAction(nameof(Index));


            }

            return View("Create", album);
        }
        [AllowAnonymous]
        [HttpGet]
        public IActionResult Details(int id)
        {
            var album = _albumService.FindById(id);
            if (album == null)
            {
                return NotFound();
            }
            return View(album);
        }

        [HttpPost]
        public IActionResult Details(Album model)
        {
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Update(int id)
        {
            var album = _albumService.FindById(id);
            if (album == null)
            {
                return NotFound();
            }
            return View(album);
        }

        [HttpPost]
        public IActionResult Update(Album model)
        {
            if (ModelState.IsValid)
            {
               
                _albumService.Update(model);
                return RedirectToAction("Index");
            }
            return View(model);
        }

        [HttpPost]
        public IActionResult UpdateAlbum(Album model)
        {
            if (ModelState.IsValid)
            {
                _albumService.Update(model);
                return RedirectToAction("Index");
            }
            return View("Update", model);
        }

        [HttpPost]
        public IActionResult UpdatePiosenka(List<Piosenka> piosenki)
        {
            if (ModelState.IsValid)
            {
                foreach (var piosenka in piosenki)
                {
                    _piosenkaService.Update(piosenka);
                }
                return RedirectToAction("Index");
            }
            return View("Update", piosenki);
          
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var album = _albumService.FindById(id);
            if (album == null)
            {
                return NotFound();
            }
            return View(album);
        }

        [HttpPost]
        public IActionResult ConfirmDelete(int id)
        {
            var albumToDelete = _albumService.FindById(id);
            if (albumToDelete != null)
            {
                _albumService.DeleteById(albumToDelete);
                return RedirectToAction("Index");
            }
            return NotFound();
        }

        [AllowAnonymous]
        public IActionResult PagedIndex(int page = 1, int size = 5)
        {
            var pagedResult = _albumService.FindPage(page, size); 
            return View(pagedResult);
        }

        [AllowAnonymous]
        public IActionResult SearchSongs()
        {
            var piosenki = _piosenkaService.FindAll();
            return View(piosenki);
        }

        [HttpPost]
        public IActionResult Polub(int piosenkaId)
        {
            var piosenka = _piosenkaService.FindById(piosenkaId);
            if (piosenka == null)
            {
                return NotFound();
            }

            _albumService.IncrementNotowanie(piosenka.AlbumId);

            return RedirectToAction("SearchSongs");
        }

    }
}