namespace AptSystems.Services
{
    using Models;
    using Models.ViewModels;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Linq;
    using System.Web;
    public interface IArticleService
    {
        byte[] ImageToByteArray(Image imageIn);

        Image ByteArrayToImage(byte[] byteArrayIn);

        Image ResizeImage(Image imgToResize, Size size);

        Article AddArticle(Article article, HttpPostedFileBase pathOnServer);

        List<List<ArticleViewModel>> GetInitialData();

        ArticleViewModel GetArticle(string id);

        void AddComment(Comment newComment);

        List<ArticleViewModel> GetSingleCategory();
    }
}