namespace AptSystems.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    public class Article
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Въведете заглавие по-дълго от 3 символа")]
        [Display(Name = "Заглавие")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Въведете коментар от 3 до 100 символа")]
        public string Title { get; set; }

        [Display(Name = "Текст")]
        [Required(ErrorMessage = "Текст по-дълъг от 3 символа")]
        [StringLength(10000, MinimumLength = 3, ErrorMessage = "Въведете текст от 3 до 10000 символа")]
        public string Content { get; set; }

        [Required(ErrorMessage = "Изберете категория")]
        [Display(Name = "Категория")]
        public CategoryTypes Category { get; set; }

        [Required(ErrorMessage = "Изберете под-категория")]
        [Display(Name = "Под-категория")]
        public SubCategories SubCategory { get; set; }

        public DateTime CreatedAt { get; set; }
                
        public byte[] Image { get; set; }

        public byte[] Tumbnail { get; set; }

        private ICollection<Comment> comments;

        public Article()
        {
            this.comments = new HashSet<Comment>();
        }

        public virtual ICollection<Comment> Comments
        {
            get { return this.comments; }
            set { this.comments = value; }
        }

    }
}
