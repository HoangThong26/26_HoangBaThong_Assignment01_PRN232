using System;
using System.Collections.Generic;

namespace _26_HoangBaThong_Assignment01_FrontEnd.Models;

public partial class Tag
{
    public int TagId
    {
        get; set;
    }

    public string? TagName
    {
        get; set;
    }

    public string? Note
    {
        get; set;
    }

    public virtual ICollection<NewsArticle> NewsArticles
    {
        get; set;
    } = new List<NewsArticle>();
}

