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
           

            //Get the MAC address of the first operational network interface
            string firstMacAddress = NetworkInterface
                .GetAllNetworkInterfaces()
                .Where(nic => nic.OperationalStatus == OperationalStatus.Up && nic.NetworkInterfaceType != NetworkInterfaceType.Loopback)
                .Select(nic => nic.GetPhysicalAddress().ToString())
                .FirstOrDefault();

            if (string.IsNullOrEmpty(firstMacAddress))
            {
                // Handle the case where no MAC address is found
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResponse
                {
                    Status = "error",
                    Message = "No MAC address found.",
                    MessageAr = ""
                });
            }

            // Combine the directory and file paths using Path.Combine
            string textFilePath = Path.Combine(_env.ContentRootPath, "UploadedAttachments", "MAC.abc");

            // Check if the file exists
            if (!System.IO.File.Exists(textFilePath))
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResponse
                {
                    Status = "error",
                    Message = "MAC file not found.",
                    MessageAr = ""
                });
            }

            // Read the lines from the file
            string[] lines = System.IO.File.ReadAllLines(textFilePath);

            // Check if the firstMacAddress is present in the file
            if (!lines.Contains(firstMacAddress))
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResponse
                {
                    Status = "error",
                    Message = "MAC address mismatch.",
                    MessageAr = ""
                });
            }

            return Ok();
        }
    }
}
