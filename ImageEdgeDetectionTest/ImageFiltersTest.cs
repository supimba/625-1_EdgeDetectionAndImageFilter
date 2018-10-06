using System;
using System.Drawing;
using ImageEdgeDetection;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ImageEdgeDetectionTest
{
    [TestClass]
    public class ImageFiltersTest
    {
        /*Filter tested : black & white from ImageFilters class*/
        [TestMethod]
        public void BlackWhiteTest()
        {
            int width = 100;
            int height = 100;
            int redValue = 120;
            int red;
            int greenValue = 90;
            int green;
            int blueValue = 150;
            int blue;
            int rgb = (int)((redValue + greenValue + blueValue) / 3);
            Color color;

            Bitmap TestImg = new Bitmap(width, height);
            Bitmap Result;

            for (int y = 0; y < TestImg.Height; y++)
                for (int x = 0; x < TestImg.Width; x++)
                {
                    TestImg.SetPixel(x, y, Color.FromArgb(redValue, greenValue, blueValue));
                }

            Result = ImageFilters.BlackWhite(TestImg);

            for (int y = 0; y < Result.Height; y++)
                for (int x = 0; x < Result.Width; x++)
                {
                    color = Result.GetPixel(x, y);
                    red = (int) color.R;
                    green = (int) color.G;
                    blue = (int) color.B;
                    Assert.AreEqual(red, rgb);
                    Assert.AreEqual(green, rgb);
                    Assert.AreEqual(blue, rgb);
                }
        }
    }
}
