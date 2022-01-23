using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Persistence.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
        public async Task<IActionResult> PostFile(IFormFile file, int FileTypeId)
        {
            var fileDirection = await _service.SaveFile(file);
            switch (FileTypeId)
            {
                case 1:
                    await _service.AddAllUser(fileDirection);
                    break;

                case 2:
                    await _service.AddAllOrder(fileDirection);
                    break;
            }
            return Ok();
        }

    }

}
