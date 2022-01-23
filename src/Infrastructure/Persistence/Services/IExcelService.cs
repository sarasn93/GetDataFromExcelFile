using Domain;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Services
{
    public interface IExcelService
    {
        Task<string> SaveFile(IFormFile file);
        Task AddAllUser(string fileName);
        Task AddAllOrder(string fileName);
    }
}
