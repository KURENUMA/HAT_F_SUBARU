using Microsoft.AspNetCore.Mvc;
using HAT_F_api.Services;
using HAT_F_api.CustomModels;
using Azure.Storage.Blobs.Models;

namespace HAT_F_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlobController : ControllerBase
    {
        private readonly NLog.ILogger _logger;

        private readonly BlobService _blobService;

        /// <summary>コンストラクタ</summary>
        /// <param name="logger">NLog.ILogger</param>
        /// <param name="blobService"></param>
        public BlobController(NLog.ILogger logger, BlobService blobService)
        {
            _logger = logger;
            _blobService = blobService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResponse<List<BlobFileInfo>>>> ListFiles([FromRoute] string id)
        {
            var result = (await _blobService.ListFiles(id))
                .Select(b =>
                {
                    string createdOn = b.Metadata.ContainsKey("CreatedOn")
                      ? b.Metadata["CreatedOn"]
                      : b.Properties.CreatedOn.ToString();
                    string lastModified = b.Metadata.ContainsKey("LastModified")
                      ? b.Metadata["LastModified"]
                      : b.Properties.LastModified.ToString();

                    return new BlobFileInfo
                    {
                        Name = b.Name,
                        ContentLength = b.Properties.ContentLength.HasValue ? (long)b.Properties.ContentLength : 0L,
                        CreatedOn = createdOn,
                        LastModified = lastModified,
                    };
                })
                .ToList()
              ;
            return new ApiOkResponse<List<BlobFileInfo>>(result);
        }

        [HttpGet("{id}/{name}")]
        public async Task<IActionResult> DownloadFile([FromRoute] string id, [FromRoute] string name)
        {
            var stream = await _blobService.DownloadFile(id, name);
            return File(stream, "application/octet-stream", name);
        }

        [HttpPost("{id}/{name}")]
        [RequestSizeLimit(52428800)] // 50 MB
        public async Task<IActionResult> UploadFile([FromRoute] string id, [FromRoute] string name, IFormFile formFile)
        {
            var resp = await _blobService.UploadFile(formFile.OpenReadStream(), id, name);
            return Ok(resp);
        }
        [HttpPut("{id}/{name}")]
        public async Task<IActionResult> SetMetaData([FromRoute] string id, [FromRoute] string name, [FromBody] IDictionary<string, string> metaData)
        {
            var resp = await _blobService.SetMetaData(id, name, metaData);
            return Ok(resp);
        }

        [HttpDelete("{id}/{name}")]
        public async Task<IActionResult> DeleteFile([FromRoute] string id, [FromRoute] string name)
        {
            var resp = await _blobService.DeleteFile(id, name);
            return Ok(resp);
        }
    }
}