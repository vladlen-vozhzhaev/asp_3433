using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using WebApplication3.Models;

namespace WebApplication3.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
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
    
    public IActionResult Articles() => View(_articles);

    public IActionResult Article(int id) {
        var article = _articles.FirstOrDefault(article => article.Id == id);
        if (article == null)
        {
            return NotFound();
        }
        return View(article);
    }

    public IActionResult Create() {
        return View();
    }

    [HttpPost]
    public IActionResult Create(Article article) {
        if (!ModelState.IsValid)
        {
            return View(article);
        }
        article.Id = _articles.Any() ? _articles.Max(p => p.Id) + 1 : 1;
        article.CratedAt = DateTime.Now;
        _articles.Add(article);
        return RedirectToAction(nameof(Articles));
    }

    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}