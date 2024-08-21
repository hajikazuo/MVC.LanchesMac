using LanchesMac.Models;

namespace LanchesMac.Services.Interfaces
{
    public interface IReportService
    {
        Task<List<Pedido>> FindByDateAsync(DateTime? minDate, DateTime? maxDate);
    }
}
