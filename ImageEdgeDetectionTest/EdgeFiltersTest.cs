using ImageEdgeDetection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageEdgeDetectionTest
{
    [TestClass]
    class EdgeFiltersTest
    {
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
