using System;
using System.Drawing;
using ImageEdgeDetection;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ImageEdgeDetectionTest
{
    [TestClass]
    public class ImageFiltersTest
    {
        /* Filter tested : black & white in ImageFilters class.
         * A small custom bitmap is tested with corresponding method and arbitrary values for RGB parameters (120, 90, 150).
         * Expected result for new color.R, color.G and color.B is 120 with those values.
         * */
        [TestMethod]
        public void BlackWhiteTest()
        {
            // color variable used for comparison test
            Color color;
            // Custom image used for test
            Bitmap TestImg = new Bitmap(100, 100);
            // Method result for comparison
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
