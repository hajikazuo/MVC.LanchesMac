namespace LanchesMac.Services.Interfaces
{
    public interface IPhotoService
    {
        Task<string> ProcessarFotoAsync(string caminhoPasta, IFormFile anexo);
    }
}
