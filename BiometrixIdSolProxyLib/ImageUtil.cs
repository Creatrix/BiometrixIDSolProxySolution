// Decompiled with JetBrains decompiler
// Type: BiometrixIDSolProxyLib.ImageUtil
// Assembly: BiometrixIdSolProxyLib, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B0DD19B3-7EB3-4F71-A183-2D7565C618ED
// Assembly location: D:\Recovery\BiometrixIDSolProxy\Bin\BiometrixIdSolProxyLib.dll

using AForge.Imaging.Filters;
using NLog;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace BiometrixIDSolProxyLib
{
  public class ImageUtil
  {
    private static Logger log = LogManager.GetLogger("ImageUtil");

    public static Bitmap Resample(Bitmap source, int dpiX, int dpiY)
    {
      ImageUtil.log.Info(string.Concat(new object[4]
      {
        (object) "Old Width: ",
        (object) source.Width,
        (object) " Old Height: ",
        (object) source.Height
      }));
      int width1 = source.Width;
      int height1 = source.Height;
      double num = 1.0;
      if (source.Width > 800)
        num = 800.0 / (double) source.Width;
      else if (source.Height > 800)
        num = 800.0 / (double) source.Height;
      int width2 = (int) Math.Round((double) source.Width * num);
      int height2 = (int) Math.Round((double) source.Height * num);
      ImageUtil.log.Info(string.Concat(new object[4]
      {
        (object) "New Width: ",
        (object) width2,
        (object) " New Height: ",
        (object) height2
      }));
      Bitmap bitmap = new Bitmap(width2, height2);
      bitmap.SetResolution((float) dpiX, (float) dpiY);
      Graphics graphics = Graphics.FromImage((Image) bitmap);
      graphics.Clear(Color.White);
      graphics.DrawImage((Image) source, new Rectangle(0, 0, width2, height2), new Rectangle(0, 0, source.Width, source.Height), GraphicsUnit.Pixel);
      return bitmap;
    }

    public static Bitmap ConvertToGrayscale(Bitmap source)
    {
      return Grayscale.CommonAlgorithms.Y.Apply(source);
    }

    public static byte[] GetAFISReadyBMP(Bitmap source)
    {
      Bitmap bitmap = ImageUtil.ConvertToGrayscale(ImageUtil.Resample(source, 500, 500));
      MemoryStream memoryStream = new MemoryStream();
      bitmap.Save((Stream) memoryStream, ImageFormat.Bmp);
      return memoryStream.ToArray();
    }

    public static byte[] GetAFISReadyBMP(byte[] sourceData)
    {
      Bitmap bitmap = ImageUtil.ConvertToGrayscale(ImageUtil.Resample(new Bitmap((Stream) new MemoryStream(sourceData)), 500, 500));
      MemoryStream memoryStream = new MemoryStream();
      bitmap.Save((Stream) memoryStream, ImageFormat.Bmp);
      return memoryStream.ToArray();
    }
  }
}
