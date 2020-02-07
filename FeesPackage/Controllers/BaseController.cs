using FeesPackage.Data_Access;
using Root.Reports;
using System;
using System.IO;
using System.Net;
using System.Web.Mvc;

namespace FeesPackage.Controllers
{
    public class BaseController : Controller
    {
        protected readonly FeesPackageEntities db = new FeesPackageEntities();

        protected HttpStatusCodeResult HandleException(Exception ex)
        {
            if (ex.InnerException != null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound, ex.InnerException.InnerException.Message.Replace("\r\n", " "));
            }
            return new HttpStatusCodeResult(HttpStatusCode.NotFound, ex.Message);
        }

        static protected void RenderToPDF(ControllerContext ctx, string pathToView)
        {
            Report report = new Report(new PdfFormatter());
            FontDef fd = new FontDef(report, "Courier");
            FontProp fp = new FontPropMM(fd, 11 / 4);
            Page page = new Page(report);

            string html = RenderViewToString(ctx, pathToView, null, true);

            string[] lines = html.Split('\n');
            int lineNo = 0;
            foreach (var line in lines)
            {
                page.AddLT_MM(0, lineNo, new RepString(fp, line));
                lineNo += (int) (4 * 1.5);
            }

#if DEBUG
            RT.ViewPDF(report);
#else
            RT.ResponsePDF(report, System.Web.HttpContext.Current.Response);
            //RT.ResponsePDF(report, page);
#endif
        }

        static protected string RenderViewToString(ControllerContext context, string viewPath, object model = null, bool partial = false)
        {
            // first find the ViewEngine for this view
            ViewEngineResult viewEngineResult;
            if (partial)
                viewEngineResult = ViewEngines.Engines.FindPartialView(context, viewPath);
            else
                viewEngineResult = ViewEngines.Engines.FindView(context, viewPath, null);

            if (viewEngineResult == null)
                throw new FileNotFoundException("View cannot be found.");

            // get the view and attach the model to view data
            var view = viewEngineResult.View;
            context.Controller.ViewData.Model = model;

            string result = null;

            using (var sw = new StringWriter())
            {
                var ctx = new ViewContext(context, view,
                                          context.Controller.ViewData,
                                          context.Controller.TempData,
                                          sw);
                view.Render(ctx, sw);
                result = sw.ToString();
            }

            return result;
        }
    }
}
