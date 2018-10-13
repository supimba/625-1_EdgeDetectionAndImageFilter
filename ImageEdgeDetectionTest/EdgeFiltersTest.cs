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
                var sourceBitmap = new Bitmap("C:\\Users\\dnlro\\Pictures\\Camera Roll\\WIN_20161224_10_18_24_Pro.jpg");

                // Act
                var result = sourceBitmap.CopyToSquareCanvas(600); 
                
                // Assert
                Assert.AreNotSame(result, sourceBitmap);
                Assert.AreEqual(result.Width, picPreviewWidth);
                
            }

        [TestMethod]
        public void CopyToSquareCanvas_ApplyEdgeLaplacian3x3Filter_ReturnsBitmap()
        {
            // Arrange
            var sourceBitmap = new Bitmap("C:\\Users\\dnlro\\Pictures\\Camera Roll\\WIN_20161224_10_18_24_Pro.jpg");

            // Act
            var resultBitmap = EdgeFilters.Laplacian3x3Filter(sourceBitmap, false);

            // Assert
            for (int width = 0; width < sourceBitmap.Width; width++)
            {
                for (int height = 0; height < sourceBitmap.Height; height++)
                {
                    Assert.AreEqual(sourceBitmap.GetPixel(width, height), resultBitmap.GetPixel(width, height));
                }

            }
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
