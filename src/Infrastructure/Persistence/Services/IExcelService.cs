using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace Persistence.Services
{
    public interface IExcelService
    {
        Task<string> SaveFile(IFormFile file);
        Task CreateDataInDb(int fileTypeId,string fileDirection);
        Task AddAllUser(string fileName);
        Task AddAllOrder(string fileName);
    }
}
