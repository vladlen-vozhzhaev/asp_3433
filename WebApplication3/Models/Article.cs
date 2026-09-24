using System.ComponentModel.DataAnnotations;

namespace WebApplication3.Models;

public class Article
{
    public int Id { get; set; }
    [StringLength(100, MinimumLength = 3,  ErrorMessage = "Title must be between 3 and 100 characters")]
    public string Title { get; set; } = "";
    [MinLength(10, ErrorMessage = "Текст должен быть не короче 10 символов")]
    public string Content { get; set; } = "";
    [StringLength(50, MinimumLength = 3,  ErrorMessage = "Author must be between 3 and 50 characters")]
    public string Author { get; set; } = "";
    public DateTime CratedAt { get; set; }
}