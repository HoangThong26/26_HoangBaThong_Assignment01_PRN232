using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace _26_HoangBaThong_Assignment01_FrontEnd.Models;

public partial class SystemAccount
{
    public short AccountId { get; set; }

    [Required(ErrorMessage = "Account Name is required")]
    public string? AccountName { get; set; }

    [Required(ErrorMessage = "Email is required")]
    [EmailAddress(ErrorMessage = "Invalid Email Address format")]
    public string? AccountEmail { get; set; }

    [Required(ErrorMessage = "Role is required")]
    public int? AccountRole { get; set; }

    [Required(ErrorMessage = "Password is required")]
    public string? AccountPassword { get; set; }

    public virtual ICollection<NewsArticle> NewsArticles { get; set; } = new List<NewsArticle>();
}



