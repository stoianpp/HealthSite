namespace AptSystems.Services
{
    using AptSystems.Data.Repository;
    using AptSystems.Models;
    using Models.ViewModels;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.Drawing.Imaging;
    using System.IO;
    using System.Linq;
    using System.Web;

    public class ArticleService : IArticleService
    {
        private IRepository<Article> repo;
        private IRepository<Comment> commentsRepo;
        private DbContext db;

        public ArticleService(IRepository<Article> repoArticle, IRepository<Comment> commentsRepo, DbContext newDb)
        {
            this.repo = repoArticle;
            this.commentsRepo = commentsRepo;
            this.db = newDb;
        }

        public Image ByteArrayToImage(byte[] byteArrayIn)
        {
            MemoryStream ms = new MemoryStream(byteArrayIn);
            Image returnImage = Image.FromStream(ms);
            return returnImage;
        }

        public Image ResizeImage(Image imgToResize, Size size)
        {
            return (Image)(new Bitmap(imgToResize, size));
        }

        public Image FixedSize(Image imgToResize, Size destinationSize)
        {
            var originalWidth = imgToResize.Width;
            var originalHeight = imgToResize.Height;

            //how many units are there to make the original length
            var hRatio = (float)originalHeight / destinationSize.Height;
            var wRatio = (float)originalWidth / destinationSize.Width;

            //get the shorter side
            var ratio = Math.Min(hRatio, wRatio);

            var hScale = Convert.ToInt32(destinationSize.Height * ratio);
            var wScale = Convert.ToInt32(destinationSize.Width * ratio);

            //start cropping from the center
            var startX = (originalWidth - wScale) / 2;
            var startY = (originalHeight - hScale) / 2;

            //crop the image from the specified location and size
            var sourceRectangle = new Rectangle(startX, startY, wScale, hScale);

            //the future size of the image
            var bitmap = new Bitmap(destinationSize.Width, destinationSize.Height);

            //fill-in the whole bitmap
            var destinationRectangle = new Rectangle(0, 0, bitmap.Width, bitmap.Height);

            //generate the new image
            using (var g = Graphics.FromImage(bitmap))
            {
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                g.DrawImage(imgToResize, destinationRectangle, sourceRectangle, GraphicsUnit.Pixel);
            }

            return bitmap;

        }

        public byte[] ImageToByteArray(Image imageIn)
        {
            MemoryStream ms = new MemoryStream();
            imageIn.Save(ms, System.Drawing.Imaging.ImageFormat.Gif);
            return ms.ToArray();
        }

        public void AddComment(Comment newComment)
        {
            this.commentsRepo.Add(newComment);
            this.commentsRepo.SaveChanges();
        }

        public Article AddArticle(Article article, HttpPostedFileBase pathOnServer)
        {
            article.Id = Guid.NewGuid();
            Image byteFile = Image.FromStream(pathOnServer.InputStream, true, true);
            article.Image = ImageToByteArray(byteFile);
            article.Tumbnail = ImageToByteArray(FixedSize(byteFile, new Size(70,70)));
            article.CreatedAt = DateTime.Now;
            repo.Add(article);
            db.SaveChanges();

            return article;
        }

        public ArticleViewModel GetArticle(string id)
        {
            var article = this.repo.GetByGuidId(Guid.Parse(id));
            var viewModel = new ArticleViewModel
            {
                Id = article.Id,
                Title = article.Title,
                Content = article.Content,
                CreatedAt = article.CreatedAt,
                Comments = article.Comments,
                Image395_396 = article.Image,
                Thumbnail70_70 = article.Tumbnail
            };
            return viewModel;
        }

        public List<List<ArticleViewModel>> GetInitialData()
        {
            var result = new List<List<ArticleViewModel>>();

            var allArticles = repo.All();
            result.Add(GetHealthArticles(allArticles));
            result.Add(GetNutritionArticles(allArticles));
            result.Add(GetFitnessArticles(allArticles));
            result.Add(GetMedicineArticles(allArticles));
            result.Add(GetFamilyArticles(allArticles));
            result.Add(GetPromoArticles(allArticles));
            result.Add(GetLastArticles(allArticles));
            result.Add(GetCommentedArticles(allArticles));
            return result;
        }

        public List<ArticleViewModel> GetHealthArticles(IQueryable<Article> allArticles)
        {
            var health5 = allArticles.Where(z => z.Category == CategoryTypes.Health)
                                    .OrderByDescending(x => x.CreatedAt)
                                    .Take(4).ToList();

            var health5List = health5.Select(y => new ArticleViewModel
            {
                Id = y.Id,
                Title = y.Title,
                Content = y.Content,
                CreatedAt = y.CreatedAt,
                Comments = y.Comments,
                Image395_396 = y.Image,
                Thumbnail70_70 = y.Tumbnail,
            }).ToList();

            foreach (var item in health5List)
            {
                Image baseImage = ByteArrayToImage(item.Image395_396);
                item.Image310_150 = ImageToByteArray(FixedSize(baseImage, new Size(310, 150)));
            }

            return health5List;
        }

        public List<ArticleViewModel> GetNutritionArticles(IQueryable<Article> allArticles)
        {
            var nutrition2 = allArticles.Where(z => z.Category == CategoryTypes.Nutrition)
                                    .OrderByDescending(x => x.CreatedAt)
                                    .Take(2).ToList();

            var nutrition2List = nutrition2.Select(y => new ArticleViewModel
            {
                Id = y.Id,
                Title = y.Title,
                Content = y.Content.Substring(0,100)+"...",
                CreatedAt = y.CreatedAt,
                Comments = y.Comments,
                Image395_396 = y.Image,
                Thumbnail70_70 = y.Tumbnail,
            }).ToList();

            foreach (var item in nutrition2List)
            {
                Image baseImage = ByteArrayToImage(item.Image395_396);
                item.Image310_150 = ImageToByteArray(ResizeImage(baseImage, new Size(310, 150)));
            }

            return nutrition2List;
        }

        public List<ArticleViewModel> GetFitnessArticles(IQueryable<Article> allArticles)
        {
            var fitness6 = allArticles.Where(z => z.Category == CategoryTypes.Fitness)
                                    .OrderByDescending(x => x.CreatedAt)
                                    .Take(6).ToList();

            var fitness6List = fitness6.Select(y => new ArticleViewModel
            {
                Id = y.Id,
                Title = y.Title,
                Content = y.Content,
                CreatedAt = y.CreatedAt,
                Comments = y.Comments,
                Image395_396 = y.Image,
                Thumbnail70_70 = y.Tumbnail,
            }).ToList();

            foreach (var item in fitness6List)
            {
                Image baseImage = ByteArrayToImage(item.Image395_396);
                item.Image310_150 = ImageToByteArray(FixedSize(baseImage, new Size(295, 150)));
            }

            return fitness6List;
        }

        public List<ArticleViewModel> GetMedicineArticles(IQueryable<Article> allArticles)
        {
            var medicine6 = allArticles.Where(z => z.Category == CategoryTypes.Medicine)
                                    .OrderByDescending(x => x.CreatedAt)
                                    .Take(6).ToList();

            var medicine6List = medicine6.Select(y => new ArticleViewModel
            {
                Id = y.Id,
                Title = y.Title,
                Content = y.Content,
                CreatedAt = y.CreatedAt,
                Comments = y.Comments,
                Image395_396 = y.Image,
                Thumbnail70_70 = y.Tumbnail,
            }).ToList();

            foreach (var item in medicine6List)
            {
                Image baseImage = ByteArrayToImage(item.Image395_396);
                item.Image310_150 = ImageToByteArray(FixedSize(baseImage, new Size(295, 150)));
            }

            return medicine6List;
        }

        public List<ArticleViewModel> GetFamilyArticles(IQueryable<Article> allArticles)
        {
            var family6 = allArticles.Where(z => z.Category == CategoryTypes.Family)
                                    .OrderByDescending(x => x.CreatedAt)
                                    .Take(6).ToList();

            var family6List = family6.Select(y => new ArticleViewModel
            {
                Id = y.Id,
                Title = y.Title,
                Content = y.Content,
                CreatedAt = y.CreatedAt,
                Comments = y.Comments,
                Image395_396 = y.Image,
                Thumbnail70_70 = y.Tumbnail,
            }).ToList();

            return family6List;
        }

        public List<ArticleViewModel> GetPromoArticles(IQueryable<Article> allArticles)
        {
            var promo4 = allArticles.Where(z => z.Category == CategoryTypes.Slider)
                                    .OrderByDescending(x => x.CreatedAt)
                                    .Take(4).ToList();

            var promo4List = promo4.Select(y => new ArticleViewModel
            {
                Id = y.Id,
                Title = y.Title,
                Content = y.Content,
                CreatedAt = y.CreatedAt,
                Comments = y.Comments,
                Image668_328 = y.Image,
                Thumbnail70_70 = y.Tumbnail,
            }).ToList();

            return promo4List;
        }

        public List<ArticleViewModel> GetLastArticles(IQueryable<Article> allArticles)
        {
            var promo4 = allArticles.Where(z => z.Category == CategoryTypes.Slider)
                                    .OrderByDescending(x => x.CreatedAt)
                                    .Take(10).ToList();

            var last10List = promo4.Select(y => new ArticleViewModel
            {
                Id = y.Id,
                Title = y.Title,
                CreatedAt = y.CreatedAt,
                Comments = y.Comments,
                Image395_396 = y.Image,
                Thumbnail70_70 = y.Tumbnail,
            }).ToList();

            foreach (var item in last10List)
            {
                if (item != null)
                {
                    Image baseImage = ByteArrayToImage(item.Image395_396);
                    item.Image150_70 = ImageToByteArray(FixedSize(baseImage, new Size(150, 70)));
                }
            }

            return last10List;
        }

        public List<ArticleViewModel> GetCommentedArticles(IQueryable<Article> allArticles)
        {
            var commented10 = allArticles.OrderByDescending(x => x.Comments.Count)
                                    .Take(10).ToList();

            var commented10List = commented10.Select(y => new ArticleViewModel
            {
                Id = y.Id,
                Title = y.Title.Length > 50? y.Title.Substring(0, 50)+"...": y.Title, 
                Content = y.Content,
                CreatedAt = y.CreatedAt,
                Comments = y.Comments,
                Image395_396 = y.Image,
                Thumbnail70_70 = y.Tumbnail,
            }).ToList();

            return commented10List;
        }
    }
}
