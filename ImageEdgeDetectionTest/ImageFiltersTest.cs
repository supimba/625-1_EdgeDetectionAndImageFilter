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
            Color color;

            Bitmap TestImg = new Bitmap(100, 100);
            Bitmap Result;

            for (int y = 0; y < TestImg.Height; y++)
                for (int x = 0; x < TestImg.Width; x++)
                {
                    TestImg.SetPixel(x, y, Color.FromArgb(120, 90, 150));
                }

            Result = ImageFilters.BlackWhite(TestImg);

            for (int y = 0; y < Result.Height; y++)
                for (int x = 0; x < Result.Width; x++)
                {
                    color = Result.GetPixel(x, y);
                    Assert.AreEqual((int)color.R, 120);
                    Assert.AreEqual((int)color.G, 120);
                    Assert.AreEqual((int)color.B, 120);
                }
        }
    }
}
