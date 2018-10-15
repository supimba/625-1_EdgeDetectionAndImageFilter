using System;
using ImageEdgeDetection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security.Cryptography;

namespace ImageEdgeDetectionTest
{
    [TestClass]
    public class EdgeFiltersTest
    {

        [TestMethod]
        public void CopyToSquareCanvas_TransformImageToSquare_ReturnsSquareCanvasBitmap()
        {
            // Arrange
            var picPreviewWidth = 600;
            var ratio = 1.0f;
            var sourceBitmap = new Bitmap(Properties.Resources.unitTestPropImage1);
            // Act
            var result = sourceBitmap.CopyToSquareCanvas(600); 
            // Assert
            Assert.AreNotEqual(result, sourceBitmap);
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void CopyToSquareCanvas_PropertiesAreNullOrEmpty_ReturnException()
        {
            // Arrange
            Bitmap sourceBitmap = null;
            // Act
            var result = EdgeFilters.CopyToSquareCanvas(sourceBitmap, 0); 
        }

        [TestMethod]
        public void Laplacian3x3Matrix_LaplacianIsCorrect_ReturnsMatrixDouble()
        {
            // Arrange
            var expectedLaplacianMatrix = new double[,]
            { { -1, -1, -1,  },
                { -1,  8, -1,  },
                { -1, -1, -1,  }, };
            // Act
            var result = Matrix.Laplacian3x3; 

            CollectionAssert.AreEqual(expectedLaplacianMatrix, result);
        }

        [TestMethod]
        public void Laplacian3x3Filter_CompareBitmapSize_ReturnsBitmapSizeAfterTransformation()
        {
            // Arrange
            var sourceBitmap = new Bitmap(Properties.Resources.unitTestPropImage1);
            // Act
            var resultBitmap = EdgeFilters.Laplacian3x3Filter(sourceBitmap);
            // Assert
            Assert.AreEqual(sourceBitmap.Size, resultBitmap.Size);
        }

        [TestMethod]
        public void LaplacianOfGaussianFilter_CompareBitmapSize_ReturnsBitmapSizeAfterTransformation()
        {
            // Arrange
            var sourceBitmap = new Bitmap(Properties.Resources.unitTestPropImage1);
            // Act
            var resultBitmap = EdgeFilters.LaplacianOfGaussianFilter(sourceBitmap);
            // Assert
            Assert.AreEqual(sourceBitmap.Size, resultBitmap.Size);
        }

        [TestMethod]
        public void Laplacian3x3Filter_CompareImageWithExistingResultFromOtherSoftware_ReturnsBitmapFiltered()
        {
            // Arrange
            var sourceBitmap = new Bitmap(Properties.Resources.square);
            var existingResult = new Bitmap(Properties.Resources.square_laplacian);
            var resultBitmap = EdgeFilters.LaplacianOfGaussianFilter(sourceBitmap);
            // Act
            var result = CompareImageWithPixel(existingResult, resultBitmap); 
            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void ConvolutionFilter_ImageIsNull_ReturnsException()
        {
            // Arrange
            Bitmap sourceBitmap = null;
            // Act
            var result = EdgeFilters.ConvolutionFilter(sourceBitmap, Matrix.Laplacian3x3, new double[1,0] , 0, 0); 
        }

        /* @author : Alicia
         * Filter tested : Sobel3x3Filter in EdgeFilters class without Grayscale.
         * Byte per byte image comparison: we have a reference image to compare with filtered image */
        [TestMethod]
        public void Sobel3x3FilterTest()
        {
            // Custom image used for test
            Bitmap TestImg = Properties.Resources.cherry;
            // Method result for comparison
            Bitmap Result;
            // Reference image for comparison
            Bitmap Reference = Properties.Resources.cherry_sobel;

            Result = EdgeFilters.Sobel3x3Filter(TestImg, false);

            Assert.IsTrue(CompareBitmap(Result, Reference));
        }

        /* @author : Alicia
         * Byte per byte bitmap comparison.
         * If images are the same, return true. If not, return false.*/
        public bool CompareBitmap(Bitmap bmp1, Bitmap bmp2)
        {
            if (bmp1 == null || bmp2 == null)
                return false;
            if (object.Equals(bmp1, bmp2))
                return true;
            if (!bmp1.Size.Equals(bmp2.Size) || !bmp1.PixelFormat.Equals(bmp2.PixelFormat))
                return false;

            int bytes = bmp1.Width * bmp1.Height * (Image.GetPixelFormatSize(bmp1.PixelFormat) / 8);

            bool result = true;
            byte[] b1bytes = new byte[bytes];
            byte[] b2bytes = new byte[bytes];

            BitmapData bitmapData1 = bmp1.LockBits(new Rectangle(0, 0, bmp1.Width - 1, bmp1.Height - 1), ImageLockMode.ReadOnly, bmp1.PixelFormat);
            BitmapData bitmapData2 = bmp2.LockBits(new Rectangle(0, 0, bmp2.Width - 1, bmp2.Height - 1), ImageLockMode.ReadOnly, bmp2.PixelFormat);

            Marshal.Copy(bitmapData1.Scan0, b1bytes, 0, bytes);
            Marshal.Copy(bitmapData2.Scan0, b2bytes, 0, bytes);

            for (int n = 0; n <= bytes - 1; n++)
            {
                if (b1bytes[n] != b2bytes[n])
                {
                    result = false;
                    break;
                }
            }

            bmp1.UnlockBits(bitmapData1);
            bmp2.UnlockBits(bitmapData2);

            return result;
        }

        public byte[] GetBytes(Bitmap bitmap)
        {
            var bytes = new byte[bitmap.Height * bitmap.Width * 3];
            BitmapData bitmapData = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height),
                ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);

            Marshal.Copy(bitmapData.Scan0, bytes, 0, bytes.Length);
            bitmap.UnlockBits(bitmapData);
            return bytes;
        }

        public bool CompareImageWithPixel(Bitmap existingResult, Bitmap resultBitmap)
        {
            bool result = true;
            string firstPixel;
            string secondPixel;

            if (existingResult.Width == resultBitmap.Width
                && existingResult.Height == resultBitmap.Height)
            {
                for (int i = 0; i < existingResult.Width; i++)
                {
                    for (int j = 0; j < existingResult.Height; j++)
                    {
                        firstPixel = resultBitmap.GetPixel(i, j).ToString();
                        secondPixel = existingResult.GetPixel(i, j).ToString();
                        if (firstPixel != secondPixel)
                        {
                            result = false;
                            break;
                        }
                    }
                }
            }
            return result; 
        }

    }
}
