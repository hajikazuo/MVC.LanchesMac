using LanchesMac.Models;
using LanchesMac.Settings;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace LanchesMac.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class AdminImagensController : Controller
    {
        private readonly string _myConfig;
        private readonly IWebHostEnvironment _hostingEnvironment;

        public AdminImagensController(IOptions<ImagesSettings> myConfig, IWebHostEnvironment hostingEnvironment)
        {
            _myConfig = myConfig.Value.PastaImagens;
            _hostingEnvironment = hostingEnvironment;
        }

        public IActionResult Index()
        {
            FileManagerModel model = new FileManagerModel();

            var userImagesPath = Path.Combine(_hostingEnvironment.WebRootPath, _myConfig);
            DirectoryInfo dir = new DirectoryInfo(userImagesPath);
            FileInfo[] files = dir.GetFiles();

            model.PathImages = _myConfig;

            if (files.Length == 0)
            {
                ViewData["Erro"] = "Nenhum arquivo encontrado";
            }

            model.Files = files;
            return View(model);
        }

        public IActionResult Deletefile(string fname)
        {
            string pastaImagens = Path.Combine(_hostingEnvironment.WebRootPath, _myConfig, fname);

            string pastaThumb = Path.Combine(_hostingEnvironment.WebRootPath, _myConfig, "tb" , fname);

            if (System.IO.File.Exists(pastaImagens))
            {
                System.IO.File.Delete(pastaImagens);
                ViewData["Deletado"] = $"Arquivo {fname} deletado com sucesso.";

                if (System.IO.File.Exists(pastaThumb))
                {
                    System.IO.File.Delete(pastaThumb);
                    ViewData["Deletado"] = $"Arquivo {fname} deletado com sucesso.";
                }
            }

            return RedirectToAction(nameof(Index));
        }

    }
}
