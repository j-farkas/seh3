using System;
using System.Net;
using System.Web;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Office;
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
    public List<Images> imageList { get; set; }

    public PptSlide(){
      imageList = new List<Images>();
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

    // public void MakePpt(){
    //   // IPresentation pptxDoc = Presentation.Create();
    //   //
    //   // ISlide slide = pptxDoc.Slides.Add(SlideLayoutType.Blank);
    //   //
    //   // IShape shape = slide.AddTextBox(10,10,500,100);
    //   //
    //   // shape.TextBody.AddParagraph(title);
    //   //
    //   //
    //   // pptxDoc.Save("Sample.pptx");
    //   //
    //   // pptxDoc.Close();
    //   for(int i = 0; i < imageList.Count(); i++)
    //   {
    //     Application pptApplication = new Application();
    //     Presentation pptpresentation = pptApplication.Presentations.Add(Microsoft.Office.Core.MsoTriState.msoTrue);
    //     Microsoft.Office.Interop.PowerPoint.Slides slides;
    //     Microsoft.Office.Interop.PowerPoint._Slide slide;
    //     Microsoft.Office.Interop.PowerPoint.TextRange objText;

    //     Microsoft.Office.Interop.PowerPoint.CustomLayout custLayout = pptpresentation.SlideMaster.CustomLayouts[Microsoft.Office.Interop.PowerPoint.ppSlideLayout.ppLayoutText];
    //     slides=pptpresentation.Slides;
    //     slide=slides.AddSlide(i+1,custLayout);
    //     objText=slide.Shapes[1].TextFrame.TextRange;
    //     objText.Text = "Title";

    //     Microsoft.Office.Interop.PowerPoint.Shape shape = slide.Shapes[2];
    //     slide.Shapes.AddPicture(imageList[i], Microsoft.Office.Core.MsoTristate.msoFalse,Microsoft.Office.Core.MsoTriState.msoTrue,shape.Left,shape.Top, shape.Width,shape.Height);
    //   }
    //   pptpresentation.SaveAs("newslide.pptx", Microsoft.Office.Interop.PowerPoint.ppSaveAsFileType.ppSaveAsDefault,Microsoft.Office.Core.MsoTriState.msoTrue);
    // }
  }
}