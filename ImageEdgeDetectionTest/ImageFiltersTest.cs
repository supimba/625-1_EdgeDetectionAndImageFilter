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
        * Filter tested : black & white in ImageFilters class.
        * A small custom bitmap is tested with corresponding method and arbitrary values for RGB parameters (120, 90, 150).
        * Expected result for new color.R, color.G and color.B is 120 with those values.*/
        [TestMethod]
        public void RainbowTest()
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

            Result = ImageFilters.RainbowFilter(TestImg);

            Assert.IsTrue(IsRainbowApplied(Result));
        }

        /* @author : Alicia
         * Method used to compare RGB parameters values*/
        public bool IsPixelColorEqual(Bitmap Result)
        {
            // method result, default value is false
            bool IsEqual = false;
            // color variable used for comparison test
            Color color;

            // checking if color modification is correctly applied
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

        public bool IsRainbowApplied(Bitmap Image)
        {
            // int variable used to apply rainbow filter
            int raz = Image.Width / 4;
            // color variable used for comparison test
            Color color;
            // method result, default value is false
            bool IsApplied = false;

            // checking if color modifications are correctly applied
            for (int i = 0; i < Image.Width; i++)
            {
                for (int x = 0; x < Image.Height; x++)
                {
                    if (i < (raz))
                    {
                        color = Image.GetPixel(i, x);
                        if (color.R == 24 && color.G == 90 && color.B == 150)
                            IsApplied = true;
                        else
                            IsApplied = false;
                    }
                    else if (i < (raz * 2))
                    {
                    color = Image.GetPixel(i, x);
                        if (color.R == 120 && color.G == 18 && color.B == 150)
                            IsApplied = true;
                        else
                            IsApplied = false;                        
                    }
                    else if (i < (raz * 3))
                    {
                        color = Image.GetPixel(i, x);
                        if (color.R == 120 && color.G == 90 && color.B == 30)
                            IsApplied = true;
                        else
                            IsApplied = false;                         
                    }
                    else
                    {
                        color = Image.GetPixel(i, x);
                        if (color.R == 24 && color.G == 90 && color.B == 30)
                            IsApplied = true;
                        else
                            IsApplied = false;   
                    }
                }
            }

            return IsApplied;
        }
        /*
         * @author : daniel
         */
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ApplyFilter_InvalidArguments_ReturnsArgumentException()
        {
            // Arrange
            var sourceBitmap = Properties.Resources.unitTestPropImage1;
            // Act
            var resultBitmap = ImageFilters.ApplyFilter(sourceBitmap, -1, -1, -1,-1); 
        }
        /*
         * @author: daniel
         */
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
