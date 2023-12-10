using Azure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.NetworkInformation;
using UPA.Web.Helpers;

namespace UPA.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ErrorController : ControllerBase
    {
        IWebHostEnvironment _env;

        public ErrorController(IWebHostEnvironment env)
        {

            _env = env;

        }

        [HttpGet]
        [Route("GetMacAddress")]
        public IActionResult GetMacAddress()
        {
            string firstMacAddress = NetworkInterface.GetAllNetworkInterfaces()
                                         .Where(nic => nic.OperationalStatus == OperationalStatus.Up && nic.NetworkInterfaceType != NetworkInterfaceType.Loopback)
                                         .Select(nic => nic.GetPhysicalAddress().ToString()).FirstOrDefault();


            string textFile = Path.Combine(_env.ContentRootPath, "UploadedAttachments") + "\\" + "MAC.abc";
            FileInfo file = new FileInfo(textFile);
            file.Attributes &= ~FileAttributes.Hidden;


            using (StreamReader streamfile = new StreamReader(textFile))
            {
                int counter = 0;
                string ln;
                while ((ln = streamfile.ReadLine()) != null)
                {
                    if (ln != firstMacAddress)
                    {
                        System.IO.File.SetAttributes(textFile, FileAttributes.Hidden);
                        return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResponse { Status = "error", Message = "", MessageAr = "" });
                    }
                    counter++;
                }
                streamfile.Close();
            }
            return Ok();
        }
    }
}
