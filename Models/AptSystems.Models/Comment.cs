using System;
using System.ComponentModel.DataAnnotations;

namespace AptSystems.Models
{
    public class Comment
    {
        public int Id { get; set; }

        public string Author { get; set; }

        //public virtual ApplicationUser ApplicationUser { get; set; }

        public Guid ArticleId { get; set; }

        public virtual Article Article { get; set; }

        [Required(ErrorMessage = "Въведете коментар по-дълъг от 5 символа")]
        [Display(Name = "Текст")]
        [StringLength(2000, MinimumLength = 5, ErrorMessage = "Въведете коментар от 5 до 2000 символа")]
        public string Content { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
