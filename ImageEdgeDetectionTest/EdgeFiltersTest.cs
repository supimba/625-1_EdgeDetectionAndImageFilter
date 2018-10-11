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
                var sourceBitmap = new Bitmap("C:\\Users\\dnlro\\Pictures\\Camera Roll\\WIN_20161224_10_18_24_Pro.jpg");

                // Act
                var result = EdgeFilters.CopyToSquareCanvas(sourceBitmap, 600); 
                
                // Assert
                Assert.AreNotSame(result, sourceBitmap);


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
