using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BookmarksManager.Models
{
public class Bookmark
{
    public int Id { get; set; }
    [Required]
    [DisplayName("Titre")]
    [Remote("TitleAvailable", "Bookmarks", HttpMethod = "POST", ErrorMessage = "Ce titre est déjà utilisé.")]
    public string Title { get; set; }

    [Required]
    [DisplayName("Lien vers le site")]
    [Url(ErrorMessage = "Hyperlien invalide")]
    public string Url { get; set; }
}
}