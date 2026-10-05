using System;
using System.Collections.Generic;

namespace _26_HoangBaThong_Assignment01_BackEnd.DAL.Entities;

public partial class Tag
{
    [System.ComponentModel.DataAnnotations.Key]
    public int TagId { get; set; }

    public string? TagName { get; set; }

    public string? Note { get; set; }

    public virtual ICollection<NewsArticle> NewsArticles { get; set; } = new List<NewsArticle>();
}



