using Geradores.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace UtilidadesDev.Controllers
{
    public class InicioController : Controller
    {
        private readonly ILogger<InicioController> _logger;
        private readonly IMemoryCache _cache;


        public InicioController(ILogger<InicioController> logger, IMemoryCache cache)
        {
            _logger = logger;
            _cache = cache;
        }

        public IActionResult Index()
        {
            return View();
        }

        [Route("/Privacidade")]
        public IActionResult Privacidade()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


        [Route("/sitemap.xml")]
        public IActionResult SitemapXml()
        {
            string baseUrl = $"{Request.Scheme}://{Request.Host}{Request.PathBase}";
            string baseUrl2 = "http://www.utilidadesdev.com.br";
            string contentType = "application/xml";

            string cacheKey = "sitemap.xml";

            var cd = new System.Net.Mime.ContentDisposition
            {
                FileName = cacheKey,
                Inline = true,
            };
            Response.Headers.Append("Content-Disposition", cd.ToString());

            var bytes = _cache.Get<byte[]>(cacheKey);
            if (bytes != null)
                return File(bytes, contentType);

            var sb = new StringBuilder();
            sb.AppendLine($"<?xml version=\"1.0\" encoding=\"UTF-8\"?>");
            sb.AppendLine($"<urlset xmlns=\"http://www.sitemaps.org/schemas/sitemap/0.9\">");

            sb.AppendLine($"    <url>");

            sb.AppendLine($"        <loc>{baseUrl}/</loc>");
            sb.AppendLine($"        <lastmod>2021-11-09</lastmod>");
            //sb.AppendLine($"        <changefreq>weekly</changefreq>");
            sb.AppendLine($"        <priority>0.8</priority>");

            sb.AppendLine($"    </url>");

            sb.AppendLine($"    <url>");

            sb.AppendLine($"        <loc>{baseUrl}/QRCode</loc>");
            sb.AppendLine($"        <lastmod>2021-11-09</lastmod>");
            //sb.AppendLine($"        <changefreq>weekly</changefreq>");
            sb.AppendLine($"        <priority>0.8</priority>");

            sb.AppendLine($"    </url>");

            sb.AppendLine($"    <url>");

            sb.AppendLine($"        <loc>{baseUrl}/CPF</loc>");
            sb.AppendLine($"        <lastmod>2021-11-09</lastmod>");
            //sb.AppendLine($"        <changefreq>weekly</changefreq>");
            sb.AppendLine($"        <priority>0.8</priority>");

            sb.AppendLine($"    </url>");
            //----------------------------------Url 2---------------------------------------

            sb.AppendLine($"    <url>");

            sb.AppendLine($"        <loc>{baseUrl2}/</loc>");
            sb.AppendLine($"        <lastmod>2021-11-09</lastmod>");
            //sb.AppendLine($"        <changefreq>weekly</changefreq>");
            sb.AppendLine($"        <priority>0.8</priority>");

            sb.AppendLine($"    </url>");

            sb.AppendLine($"    <url>");

            sb.AppendLine($"        <loc>{baseUrl2}/QRCode</loc>");
            sb.AppendLine($"        <lastmod>2021-11-09</lastmod>");
            //sb.AppendLine($"        <changefreq>weekly</changefreq>");
            sb.AppendLine($"        <priority>0.8</priority>");

            sb.AppendLine($"    </url>");

            sb.AppendLine($"    <url>");

            sb.AppendLine($"        <loc>{baseUrl2}/CPF</loc>");
            sb.AppendLine($"        <lastmod>2021-11-09</lastmod>");
            //sb.AppendLine($"        <changefreq>weekly</changefreq>");
            sb.AppendLine($"        <priority>0.8</priority>");

            sb.AppendLine($"    </url>");
            sb.AppendLine($"</urlset>");

            bytes = Encoding.UTF8.GetBytes(sb.ToString());

            //_cache.Set(cacheKey, bytes, TimeSpan.FromHours(24));
            return File(bytes, contentType);
        }

        [Route("/sitemap.txt")]
        public IActionResult SitemapTxt()
        {
            string baseUrl = $"{Request.Scheme}://{Request.Host}{Request.PathBase}";
            string baseUrl2 = "http://www.utilidadesdev.com.br";
            string contentType = "application/txt";

            string cacheKey = "sitemap.txt";

            var cd = new System.Net.Mime.ContentDisposition
            {
                FileName = cacheKey,
                Inline = true,
            };
            Response.Headers.Append("Content-Disposition", cd.ToString());

            var bytes = _cache.Get<byte[]>(cacheKey);
            if (bytes != null)
                return File(bytes, contentType);

            var sb = new StringBuilder();
            sb.AppendLine($"{baseUrl}/");
            sb.AppendLine($"{baseUrl}/QRCode");
            sb.AppendLine($"{baseUrl}/CPF");
            sb.AppendLine($"{baseUrl}/GerarLoremipsum");

            sb.AppendLine($"{baseUrl2}/");
            sb.AppendLine($"{baseUrl2}/QRCode");
            sb.AppendLine($"{baseUrl2}/CPF");
            sb.AppendLine($"{baseUrl2}/GerarLoremipsum");

            bytes = Encoding.UTF8.GetBytes(sb.ToString());

            return File(bytes, contentType);
        }

    }
}
