namespace Shared.Models;

public class Forum
{
    public int Id { get; set; }
    public User Owner { get; }
    public string Title { get; set; }
   
    public string ArticleContent { get; set; }
    
    public Forum(User owner, string title, string articleContent)
    {
        Owner = owner;
        Title = title;
        ArticleContent = articleContent;
    }
}