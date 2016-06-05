namespace AptSystems.Models.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;

    public class ArticleViewModel
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public CategoryTypes Category { get; set; }

        public SubCategories SubCategory { get; set; }

        public DateTime CreatedAt { get; set; }

        public Byte[] Image668_328 { get; set; }

        public Byte[] Image395_396 { get; set; }

        public Byte[] Image310_300 { get; set; }

        public Byte[] Image310_150 { get; set; }

        public Byte[] Image262_218 { get; set; }

        public Byte[] Image150_70 { get; set; }

        public Byte[] Thumbnail70_70 { get; set; }

        public ICollection<Comment> Comments { get; set; }
    }
}
