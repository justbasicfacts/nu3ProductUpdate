using LiteDB;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using nu3ProductUpdate.Classes;
using nu3ProductUpdate.Data.Classes;
using nu3ProductUpdate.Data.Enums;
using nu3ProductUpdate.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;

namespace nu3ProductUpdate.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize()]
    public class FilesController : ControllerBase
    {
        private readonly IFilesService _filesService;
        private readonly IInventoryService _inventoryService;
        private readonly ILogger<FilesController> _logger;
        private readonly IProductsService _productsService;

        public FilesController(ILogger<FilesController> logger, IFilesService filesService, IProductsService productsService, IInventoryService inventoryService)
        {
            _filesService = filesService;
            _productsService = productsService;
            _inventoryService = inventoryService;
            _inventoryService.Subscribe(_filesService);
            _productsService.Subscribe(_filesService);
            _logger = logger;
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            var isDeleted = _filesService.Delete(id);
            if (isDeleted)
            {
                return Ok(new { id, isDeleted });
            }
            else
            {
                return NotFound(new { id, isDeleted });
            }
        }

        [HttpGet("{id}")]
        public IActionResult Download(string id)
        {
            var fileInfo = _filesService.GetFileInfoById(id);
            if (fileInfo == null)
                return NotFound();

            LiteFileStream<string> fileStream = _filesService.GetFileStreamById(id);

            return File(fileStream, fileInfo.MimeType, fileInfo.Filename);
        }

        [HttpGet]
        public IEnumerable<LiteFileInfo<string>> Get()
        {
            return _filesService.FindAll();
        }

        [AllowedFileExtension(new string[] { "xml", "csv" })]
        [HttpPut]
        public IActionResult Upload([FromForm] IFormFile productData)
        {
            FileUploadResult result;
            try
            {
                result = UploadFile(productData);
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex, "There was an error saving file");
                return Problem("There was an error saving file");
            }

            return Ok(result);
        }

        private FileUploadResult UploadFile(IFormFile file)
        {
            FileUploadResult fileUploadResult = new FileUploadResult { IsSuccessful = false };
            try
            {
                string extension = Path.GetExtension(file.FileName);

                using (var stream = file.OpenReadStream())
                {
                    var fileId = Guid.NewGuid().ToString();
                    var fileName = $"{fileId}{extension}";

                    var fileInfo = _filesService.Add(fileId, fileName, stream);

                    fileUploadResult.FileInfo = fileInfo;
                    fileUploadResult.FileType = fileInfo.MimeType == AllowedMimeTypes.Text.Xml ? FileType.Product : FileType.Inventory;
                }

                fileUploadResult.IsSuccessful = true;
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex, "There was an error while saving the file");
            }

            return fileUploadResult;
        }
    }
}