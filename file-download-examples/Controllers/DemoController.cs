using System;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Mvc;

namespace file_download_examples.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class DemoController : ControllerBase
	{
        [Route("static-file")]
        [HttpGet]
        public IActionResult GetStaticFile()
        {
            Response.Headers.Add("Content-Disposition", "attachment;filename=demo.pdf");
            var physicalLocation = "/Users/tvaidyan/Projects/file-download-examples/file-download-examples/wwwroot/demo.pdf";
            return new PhysicalFileResult(physicalLocation, "application/pdf");
        }

        [Route("virtual-file")]
        [HttpGet]
        public IActionResult GetVirtualile()
        {
            Response.Headers.Add("Content-Disposition", "attachment;filename=demo.pdf");
            var path = "demo.pdf";
            return new VirtualFileResult(path, "application/pdf");
        }

        [Route("on-the-fly")]
        [HttpGet]
        public IActionResult GetFileOnTheFly()
        {
            Response.Headers.Add("Content-Disposition", "attachment;filename=demo.txt");
            byte[] fileContentBytes = default!;
            using (var ms = new MemoryStream())
            {
                using TextWriter textWriter = new StreamWriter(ms);
                textWriter.WriteLine("Hello world!");
                textWriter.Flush();

                ms.Position = 0;
                fileContentBytes = ms.ToArray();
            }

            return new FileContentResult(fileContentBytes, "text/plain");
        }

        [Route("stream")]
        [HttpGet]
        public IActionResult GetStream()
        {
            Response.Headers.Add("Content-Disposition", "attachment;filename=demo.txt");
            var ms = new MemoryStream();
            TextWriter textWriter = new StreamWriter(ms);
            textWriter.WriteLine("Hello world!");
            textWriter.Flush();
            ms.Position = 0;
            return new FileStreamResult(ms, "text/plain");
        }

        [Route("file-with-helper")]
        [HttpGet]
        public IActionResult GetFileWithHelper()
        {

            var ms = new MemoryStream();
            TextWriter textWriter = new StreamWriter(ms);
            textWriter.WriteLine("Hello world!");
            textWriter.Flush();
            ms.Position = 0;
            return File(ms, "text/plain", "hello.txt");
        }
    }
}

