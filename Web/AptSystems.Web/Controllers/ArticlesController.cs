namespace AptSystems.Web.Controllers
{
    using AptSystems.Data.Repository;
    using AptSystems.Models;
    using AptSystems.Models.ViewModels;
    using AptSystems.Services;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.IO;
    using System.Linq;
    using System.Net;
    using System.Net.Mail;
    using System.Web;
    using System.Web.Http.Description;
    using System.Web.Mvc;
    public class ArticlesController : Controller
    {
        private IArticleService service;
        private IRepository<Article> articleRepo;
        private IRepository<Comment> commentRepo;
        private DbContext db;

        public ArticlesController(
            IArticleService newService,
            IRepository<Article> newRepo,
            IRepository<Comment> newCommentRepo,
            DbContext newDb)
        {
            this.service = newService;
            this.articleRepo = newRepo;
            this.commentRepo = newCommentRepo;
            this.db = newDb;
        }


        public ActionResult Index(int commentStatus=0)
        {
            var initialData = new List<List<ArticleViewModel>>();
            initialData = service.GetInitialData();
            ViewBag.Status = commentStatus;
            return View(initialData);
        }

        [Authorize(Roles = "Admin")]
        public void DeleteAll()
        {
            foreach (var item in articleRepo.All())
            {
                articleRepo.Delete(item);
            }
            articleRepo.SaveChanges();
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            ViewBag.Categories = (CategoryTypes[])Enum.GetValues(typeof(CategoryTypes));
            ViewBag.SubCategories = (SubCategories[])Enum.GetValues(typeof(SubCategories));

            return View();
        }

        public ActionResult _SingleArticle(string id)
        {
            var articleViewModel = service.GetArticle(id);
            return PartialView(articleViewModel);
        }

        public ActionResult _SingleCategory(CategoryTypes category)
        {
            var articleViewModel = service.GetSingleCategory(category);
            return PartialView(articleViewModel);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Article article, HttpPostedFileBase pathOnServer)
        {
            if (pathOnServer != null && pathOnServer.ContentLength > 0)
            {
                if (ModelState.IsValid)
                {
                    try
                    {
                        service.AddArticle(article, pathOnServer);
                        ModelState.Clear();
                        ViewBag.Message = "Публикацията заредена успешно";
                    }
                    catch (Exception ex)
                    {
                        ViewBag.Message = "ERROR:" + ex.Message.ToString();
                    }
                }
            }
            else
            {
                ViewBag.Message = "You have not specified a file.";
            }
            return View();

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult AddComment(Comment newComment)
        {
            if (this.ModelState.IsValid)
            {
                try
                {
                    newComment.Author = this.User.Identity.Name;
                    newComment.CreatedAt = DateTime.Now;
                    service.AddComment(newComment);
                    ModelState.Clear();
                    return RedirectToAction("Index", new { commentStatus = 1 });
                }
                catch (Exception ex)
                {
                    return RedirectToAction("Index", new { commentStatus = 2 });
                }
            }
            else return RedirectToAction("Index", new { commentStatus = 2 });
        }

        // GET: api/Articles
        public IQueryable<Article> GetArticles()
        {
            return articleRepo.All();
        }

        

        // GET: api/Articles/5
        [ResponseType(typeof(Article))]
        public Article GetArticle(Guid id)
        {
            Article article = articleRepo.GetByGuidId(id);
            if (article == null)
            {
                throw new ArgumentNullException("Няма такава статия");
            }

            return article;
        }

        // PUT: api/Articles/5
        [Authorize(Roles = "Admin")]
        [ResponseType(typeof(void))]
        public HttpStatusCode PutArticle(Guid id, Article article)
        {
            if (!ModelState.IsValid)
            {
                throw new ArgumentException("Неправилен модел на статия");
            }

            if (id != article.Id)
            {
                throw new ArgumentNullException("Няма такава статия");
            }

            try
            {
                articleRepo.Update(article);
                articleRepo.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ArticleExists(id))
                {
                    throw new ArgumentException("Няма такава статия");
                }
                else
                {
                    throw;
                }
            }

            return HttpStatusCode.OK;
        }

        // POST: api/Articles
        [Authorize(Roles = "Admin")]
        [ResponseType(typeof(Article))]
        public HttpStatusCode PostArticle(Article article)
        {
            if (!ModelState.IsValid)
            {
                return HttpStatusCode.NotFound;
            }

            try
            {
                articleRepo.Add(article);
                articleRepo.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (ArticleExists(article.Id))
                {
                    throw new ArgumentException("Статията вече съществува");
                }
                else
                {
                    throw;
                }
            }

            return HttpStatusCode.OK;
        }

        // DELETE: api/Articles/5
        [Authorize(Roles = "Admin")]
        [ResponseType(typeof(Article))]
        public HttpStatusCode DeleteArticle(Guid id)
        {
            Article article = articleRepo.GetByGuidId(id);
            if (article == null)
            {
                throw new ArgumentException("Няма такава статия");
            }

            articleRepo.Delete(article);
            articleRepo.SaveChanges();

            return HttpStatusCode.OK;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                articleRepo.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ArticleExists(Guid id)
        {
            return articleRepo.All().Count(e => e.Id == id) > 0;
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SendMail(string name, string email, string content)
        {
                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");

                mail.From = new MailAddress("denisspenchev@gmail.com");
                mail.To.Add("stoianpp2013@gmail.com");
                mail.Subject = email + " изпраща от сайта zdravetomi.com";
                mail.Body = content;
                SmtpServer.Port = 587;
                SmtpServer.Credentials = new System.Net.NetworkCredential("denisspenchev", "denissp1");
                SmtpServer.EnableSsl = true;
            try
            {
                SmtpServer.Send(mail);
                return RedirectToAction("Index", new { commentStatus = 3 });
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index", new { commentStatus = 4 });
            }
        }
    }
}