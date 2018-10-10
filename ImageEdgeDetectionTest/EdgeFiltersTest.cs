using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ImageEdgeDetection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static ImageEdgeDetection.MainForm;

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
        }
    
}
