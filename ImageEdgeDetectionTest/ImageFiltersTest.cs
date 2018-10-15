using System;
using System.Drawing;
using ImageEdgeDetection;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ImageEdgeDetectionTest
{
    [TestClass]
    public class ImageFiltersTest
    {
        /* @author : Alicia
         * Filter tested : black & white in ImageFilters class.
         * A small custom bitmap is tested with corresponding method and arbitrary values for RGB parameters (120, 90, 150).
         * Expected result for new color.R, color.G and color.B is 120 with those values.
         * */
        [TestMethod]
        public void BlackWhiteTest()
        {
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
            Assert.IsTrue(IsPixelColorEqual(Result));            
        }

        /* @author : Alicia
         * Method used to compare RGB parameters values*/
        public bool IsPixelColorEqual(Bitmap Result)
        {
            // result of test, set to false
            bool IsEqual = false;
            // color variable used for comparison test
            Color color;

            for (int y = 0; y < Result.Height; y++)
                for (int x = 0; x < Result.Width; x++)
                {
                    color = Result.GetPixel(x, y);
                    if (color.R == 120 && color.G == 120 && color.B == 120)
                        IsEqual = true;
                    else
                        IsEqual = false;
                }

            return IsEqual;
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ApplyFilter_InvalidArguments_ReturnsArgumentException()
        {
            // Arrange
            var sourceBitmap = Properties.Resources.unitTestPropImage1;
            // Act
            var resultBitmap = ImageFilters.ApplyFilter(sourceBitmap, -1, -1, -1,-1); 
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void RainbowFilter_ArgumentIsNull_ReturnsNullReferenceException()
        {
            // Arrange
            Bitmap sourceBitmap = new Bitmap(100, 100);
            sourceBitmap = null; 
            // Act
            var resultBitmap = ImageFilters.RainbowFilter(sourceBitmap);
        }
    }
}
