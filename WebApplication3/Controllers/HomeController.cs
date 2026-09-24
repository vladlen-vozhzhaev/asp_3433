using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using WebApplication3.Data;
using WebApplication3.Models;

namespace WebApplication3.Controllers;

public class HomeController : Controller
{
    private readonly AppDbContext _db;
    private readonly ILogger<HomeController> _logger;

    public HomeController(AppDbContext db, ILogger<HomeController> logger)
    {
        _logger = logger;
        _db = db;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    public IActionResult About() =>  View();
    

    public IActionResult Contact() => View();


    private static readonly List<Article> _articles = new()
    {
        new Article
        {
            Id = 1,
            Title = "lorem ipsum",
            Content =
                "Lorem ipsum dolor sit amet, consectetur adipisicing elit. Aperiam atque doloribus labore modi molestiae natus nostrum sapiente. Aspernatur consectetur delectus quibusdam ratione tempore? Consectetur cum in odit omnis similique. Aliquid aperiam at cumque cupiditate distinctio doloremque doloribus ea eligendi est facilis hic illum impedit iusto magni mollitia nisi quaerat quas, qui, quisquam reiciendis repellat repellendus rerum tempore voluptas voluptatum? A delectus facilis fuga id labore nulla repellat temporibus tenetur velit veritatis! Aliquid asperiores deleniti ea eligendi ex laudantium libero neque praesentium quo rem repellendus, repudiandae tempora, tenetur. Aperiam atque cumque facere hic ipsa nam numquam, perspiciatis porro quisquam quod sapiente.",
            Author = "Admin",
            CratedAt = new DateTime(2026, 09, 23)
        },
        new Article
        {
            Id = 2,
            Title = "lorem ipsum 2",
            Content =
                "Aperiam atque doloribus labore modi molestiae natus nostrum sapiente. Aspernatur consectetur delectus quibusdam ratione tempore? Consectetur cum in odit omnis similique. Aliquid aperiam at cumque cupiditate distinctio doloremque doloribus ea eligendi est facilis hic illum impedit iusto magni mollitia nisi quaerat quas, qui, quisquam reiciendis repellat repellendus rerum tempore voluptas voluptatum? A delectus facilis fuga id labore nulla repellat temporibus tenetur velit veritatis! Aliquid asperiores deleniti ea eligendi ex laudantium libero neque praesentium quo rem repellendus, repudiandae tempora, tenetur. Aperiam atque cumque facere hic ipsa nam numquam, perspiciatis porro quisquam quod sapiente.",
            Author = "Admin",
            CratedAt = new DateTime(2026, 09, 24)
        }
    };

    public IActionResult Articles()
    {
        var article = _db.Articles.OrderByDescending(a => a.CratedAt).ToList();
        return View(article);
    }

    public IActionResult Article(int id) {
        var article = _db.Articles.FirstOrDefault(a => a.Id == id);
        if (article == null)
        {
            return NotFound();
        }
        return View(article);
    }

    public IActionResult Create() {
        return View();
    }

    public IActionResult Edit(int id)
    {
        var article = _db.Articles.FirstOrDefault(a => a.Id == id);
        if (article == null) return NotFound();
        return View(article);
    }

    /*public IActionResult Delete(int id)
    {
        var article = _db.Articles.FirstOrDefault(a => a.Id == id);
        if (article == null) return NotFound();
        return View(article);
    }*/

    [HttpPost, ActionName("Delete")]
    public IActionResult DeleteConfirmed(int id)
    {
        var article = _db.Articles.FirstOrDefault(a => a.Id == id);
        if (article == null) return NotFound();
        _db.Articles.Remove(article);
        _db.SaveChanges();
        return RedirectToAction(nameof(Articles));
    }

    [HttpPost]
    public IActionResult Edit(Article article)
    {
        if (!ModelState.IsValid)
        {
            return View(article);
        }
        var existingArticle = _db.Articles.FirstOrDefault(a => a.Id == article.Id);
        if (existingArticle == null) return NotFound();
        
        existingArticle.Author = article.Author;
        existingArticle.Title = article.Title;
        existingArticle.Content = article.Content;
        _db.SaveChanges();
        return RedirectToAction("Article", new { id = article.Id });
    }
    
    [HttpPost]
    public IActionResult Create(Article article) {
        if (!ModelState.IsValid)
        {
            return View(article);
        }
        article.CratedAt = DateTime.Now;
        _db.Articles.Add(article);
        _db.SaveChanges(); // Выполнить сохранение
        return RedirectToAction(nameof(Articles));
    }

    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}