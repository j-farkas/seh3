using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Text.Json;
using System.Text.Json.Serialization;
using seh3.Models;

namespace seh3.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ImagesController : ControllerBase
    {

        private readonly ILogger<ImagesController> _logger;

        public ImagesController(ILogger<ImagesController> logger)
        {
            _logger = logger;
        }

        [HttpGet("{id}")]
        public IEnumerable<Images> GetImageItem(string id)
        {
         string rawData = Images.GetImages(id);
          // return rawData;
          
         return Images.GetImageList(rawData);
        }
        [HttpPost]
        public void Index()
        {
            Console.WriteLine(Request.Headers.Count);

            foreach(var data in Request.Headers){
                Console.WriteLine(data);
            }
            // var options = new JsonSerializerOptions
            // {
            //     AllowTrailingCommas = true
            // };
            //  PptSlide ppt = JsonSerializer.Deserialize<PptSlide>(json);
            //  Console.WriteLine(ppt.title);
        }
        
    }
}
