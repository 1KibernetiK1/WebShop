using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Text;
using System.Text.Encodings.Web;
using WebShopAsp2022.Models;

namespace WebShopAsp2022.Helpers
{
    public static class HelperPagination
    {
        public static HtmlString PaginationBootstrap(
            this IHtmlHelper helper,
            ProductsPageModel model)
        {
            var tagUl = new TagBuilder("ul");
            tagUl.AddCssClass("pagination");

            string partRoute = model.CategoryName == null 
                ? "Products"
                : model.CategoryName;

            for (int i = 0; i < model.PagesQuantity; i++)
            {
                var tagLi = new TagBuilder("li"); // <li></li>
                tagLi.AddCssClass("page-item");

                if (model.ActivePage == i + 1)
                    tagLi.AddCssClass("active");

                var tagA = new TagBuilder("a"); // <a> </a>
                tagA.AddCssClass("page-link");

                tagA.MergeAttribute("href", 
                    $"/Product/ListView/" +
                    $"?categoryName={model.CategoryName}" +
                    $"&page={i+1}");

                tagA.InnerHtml.SetContent((i + 1).ToString());

                tagLi.InnerHtml.AppendHtml(tagA);

                tagUl.InnerHtml.AppendHtml(tagLi);
            }
            var writer = new System.IO.StringWriter();
            tagUl.WriteTo(writer, HtmlEncoder.Default);
            return new HtmlString(writer.ToString());
        }

        public static HtmlString Pagination(
            this IHtmlHelper helper, 
            ProductsPageModel model)
        {
            var tagDiv = new TagBuilder("div");
            for (int i = 0; i < model.PagesQuantity; i++)
            {
                var tagA = new TagBuilder("a"); // <a> </a>
                tagA.MergeAttribute("href", $"/Products/Page{i + 1}");
                tagA.InnerHtml.SetContent((i+1).ToString());
                tagDiv.InnerHtml.AppendHtml(tagA);
            }
            var writer = new System.IO.StringWriter();
            tagDiv.WriteTo(writer, HtmlEncoder.Default);
            return new HtmlString(writer.ToString());
        }
    }
}
