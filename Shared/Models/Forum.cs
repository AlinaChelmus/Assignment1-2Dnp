using System.ComponentModel.DataAnnotations;

namespace Shared.Models;

public class Forum
{
    [Range(1, int.MaxValue, ErrorMessage = "Please enter a value bigger then {1}")]
    public int Id { get; set; }
    public User Owner { get; private set; }
    public string Title { get; set; }
   
    public string ArticleContent { get; set; }
    
    public Forum(User owner, string title, string articleContent)
    {
        Owner = owner;
        Title = title;
        ArticleContent = articleContent;
    }
    
    private Forum(){}
}