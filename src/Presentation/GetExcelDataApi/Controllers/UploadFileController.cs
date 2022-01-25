using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Persistence.Services;
using System;
using System.Threading.Tasks;

namespace GetExcelDataApi.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class UploadFileController : ControllerBase
    {
        private readonly IExcelService _service;
        public UploadFileController(IExcelService service)
        {
            _service = service ?? throw new ArgumentNullException(nameof(service));
        }
        [HttpPost]
        [Route("upload")]
        public async Task<IActionResult> PostFile(IFormFile file, int fileTypeId=0)
        {
            var fileDirection = await _service.SaveFile(file);
            await _service.CreateDataInDb(fileTypeId, fileDirection);
            return Ok();
        }

    }

}
