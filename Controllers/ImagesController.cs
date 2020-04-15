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
            String imageList = "";
            PptSlide ppt = new PptSlide();
            foreach(var data in Request.Headers){
                if(data.Key == "text")
                {
                    ppt.text = data.Value;
                }else if(data.Key == "title")
                {
                    ppt.title = data.Value;
                }else if(data.Key == "imageList")
                {
                    imageList = data.Value;
                    
                }
            }
            foreach(String image in imageList.Split('`'))
            {
                ppt.imageList.Add(image);
            }
           // ppt.MakePpt();
        }
        
    }
}
