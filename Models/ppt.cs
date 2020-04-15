using System;
using System.Net;
using System.Web;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Office;
using Microsoft.Office.Core;
using Microsoft.Office.Interop.PowerPoint;

namespace seh3.Models
{

  public class Images
  {
      public string ImageURI { get; set; }
      public bool Selected { get; set; }

      public Images(string URI)
      {
        ImageURI = URI;
        Selected = false;
      }

    public static string GetImages(string Words)
    {
      string url = "https://www.google.com/search?q=" + Words + "&tbm=isch";
      string data = "";

      var request = (HttpWebRequest)WebRequest.Create(url);
      var response = (HttpWebResponse)request.GetResponse();

      using (Stream dataStream = response.GetResponseStream())
      {
          if(dataStream == null)
            return "";
          using (var sr = new StreamReader(dataStream))
          {
            data = sr.ReadToEnd();
          }
      }
      return data;

    }
    public static List<Images> GetImageList(string html)
    {

      List<Images> images = new List<Images>();
        int i = html.IndexOf("heirloom", StringComparison.Ordinal);
        i = html.IndexOf("<img", i, StringComparison.Ordinal);
       Console.WriteLine(html);
       int loops = 0;
      while( i >= 0 && loops < 20)
      {
        i = html.IndexOf("src=\"", i, StringComparison.Ordinal);
        i+=5;
        int j = html.IndexOf("\"",i,StringComparison.Ordinal);
        string uri = html.Substring(i, j - i);
        Console.WriteLine(uri);
        images.Add(new Images(uri));
        i = html.IndexOf("<img", i, StringComparison.Ordinal);
        loops++;

      }
      return images;
    }
  }
  public class PptSlide
  {
    public string title { get; set; }
    public string text { get; set; }
    public List<String> imageList { get; set; }

    public PptSlide(){
      imageList = new List<String>();
      title = "";
      text = "";
    }

    public static List<Images> getImages(String html)
    {

      List<Images> images = new List<Images>();
        int i = html.IndexOf("imageList", StringComparison.Ordinal);
        i = html.IndexOf("imageURI", i, StringComparison.Ordinal);
       Console.WriteLine(html);
       int loops = 0;
      while( i >= 0 && loops < 20)
      {
        i = html.IndexOf("=", i, StringComparison.Ordinal);
        i+=5;
        int j = html.IndexOf("&",i,StringComparison.Ordinal);
        string uri = html.Substring(i, j - i);
        Console.WriteLine(uri);
        images.Add(new Images(uri));
        i = html.IndexOf("imageURI", i, StringComparison.Ordinal);
        loops++;

      }
      return images;
    }

    public void MakePpt(){

// //Application pptApplication = new Application();
// Console.WriteLine("1");
// Microsoft.Office.Interop.PowerPoint.Slides slides;
// Microsoft.Office.Interop.PowerPoint._Slide slide;
// Microsoft.Office.Interop.PowerPoint.TextRange objText;
// Presentation pptPresentation = new Microsoft.Office.Interop.PowerPoint.Application().Presentations.Add(MsoTriState.msoTrue);

// Microsoft.Office.Interop.PowerPoint.CustomLayout customLayout = pptPresentation.SlideMaster.CustomLayouts[Microsoft.Office.Interop.PowerPoint.PpSlideLayout.ppLayoutText];

// slides = pptPresentation.Slides;
// slide = slides.AddSlide(1, customLayout);

// objText = slide.Shapes[1].TextFrame.TextRange;
// objText.Text = title;
// objText.Font.Name = "Arial";
// objText.Font.Size = 32;

// objText = slide.Shapes[2].TextFrame.TextRange;
// objText.Text = text;

// Microsoft.Office.Interop.PowerPoint.Shape shape = slide.Shapes[2];
// foreach(string image in imageList)
// {
//     slide.Shapes.AddPicture(imageList[0],Microsoft.Office.Core.MsoTriState.msoFalse,Microsoft.Office.Core.MsoTriState.msoTrue,shape.Left, shape.Top, shape.Width, shape.Height);
// }



// pptPresentation.SaveAs(@"c:\temp\test.pptx", Microsoft.Office.Interop.PowerPoint.PpSaveAsFileType.ppSaveAsDefault, MsoTriState.msoTrue);   
    }
  }
}