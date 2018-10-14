using System;
using ImageEdgeDetection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Drawing;

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
            Assert.AreEqual(sourceBitmap.Width, resultBitmap.Width);
        }

        [TestMethod]
        public void LaplacianOfGaussianFilter_CompareBitmapSize_ReturnsBitmapSizeAfterTransformation()
        {
            // Arrange
            var sourceBitmap = new Bitmap(Properties.Resources.unitTestPropImage1);
            // Act
            var resultBitmap = EdgeFilters.LaplacianOfGaussianFilter(sourceBitmap);
            // Assert
            Assert.AreEqual(sourceBitmap.Width, resultBitmap.Width);
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


        /*Filter tested : Laplacian 3x3 in EdgeFilters class*/
        [TestMethod]
        public void Laplacian3x3FilterTest()
        {
            // Custom image used for test
            Bitmap TestImg = new Bitmap(100, 100);
            // Method result for comparison
            Bitmap Result;

            Result = EdgeFilters.Laplacian3x3Filter(TestImg, false);

            // TODO : apply method manually or predict result in order to compare with method's result
        }

    }
}
