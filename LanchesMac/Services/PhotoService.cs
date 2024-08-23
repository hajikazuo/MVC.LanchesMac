using LanchesMac.Context;
using LanchesMac.Services.Interfaces;
using System.Drawing.Imaging;
using System.Drawing;

namespace LanchesMac.Services
{
    public class PhotoService : IPhotoService
    {
        private readonly AppDbContext _context;

        public PhotoService(AppDbContext context)
        {
            _context = context;
        }

        public  async Task<string> ProcessarFotoAsync(string caminhoPasta, IFormFile anexo)
        {
            if (ValidaImagem(anexo))
            {
                if (anexo != null && anexo.Length > 0)
                {
                    var nome = Guid.NewGuid().ToString() + Path.GetExtension(anexo.FileName);
                    var filePath = Path.Combine(caminhoPasta, nome);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await anexo.CopyToAsync(stream);
                    }

                    CriarThumbnail(filePath, caminhoPasta, nome);

                    return nome;
                }
            }
            return null;
        }

        private bool ValidaImagem(IFormFile anexo)
        {
            switch (anexo.ContentType)
            {
                case "image/jpeg":
                    return true;
                case "image/bmp":
                    return true;
                case "image/gif":
                    return true;
                case "image/png":
                    return true;
                default:
                    return false;
            }
        }

        private void CriarThumbnail(string caminhoArquivoOriginal,string caminhoArquivoThumb, string nome)
        {
            var pathImgOriginal = caminhoArquivoOriginal;

            using (Image imagemOriginal = Image.FromFile(pathImgOriginal))
            {
                int larguraThumbnail = 180;
                int alturaThumbnail = 155;

                using (Image thumbnail = new Bitmap(larguraThumbnail, alturaThumbnail))
                {
                    using (Graphics grafico = Graphics.FromImage(thumbnail))
                    {
                        grafico.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
                        grafico.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                        grafico.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                        var retanguloImagem = new Rectangle(0, 0, larguraThumbnail, alturaThumbnail);
                        grafico.DrawImage(imagemOriginal, retanguloImagem);

                        string caminhoThumbnail = Path.Combine(caminhoArquivoThumb, "tb", nome);
                        thumbnail.Save(caminhoThumbnail, ImageFormat.Jpeg);
                    }
                }
            }
        }
    }
}
