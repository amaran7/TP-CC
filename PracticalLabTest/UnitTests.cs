using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Drawing;
using NSubstitute;
using PracticalLab;
using PracticalLab.BLL;
using PracticalLab.UI;
using System.IO;
using System;

namespace PracticalLabTest
{
    [TestClass]
    public class UnitTests
    {
        static MainForm mainForm = new MainForm();
        Program program = new Program(mainForm);

        // Image de départ
        Bitmap imageInitiale = Resource.BunnyLandscape;
        Bitmap imageAvecLaplacian5x5 = Resource.BunnyLandscapeWithLaplacian5x5;

        ILoadBehaviour ilb = Substitute.For<ILoadBehaviour>();

        
        [TestMethod]
        public void TestLaplacian5x5ImageNull()
        {
            Bitmap imageNull = null;

            // Assert.IsNull(PracticalLab.BLL.IProgramBehaviour.(imageNull));
        }

        [TestMethod]
        public void TestLaplacian5x5CompareImages()
        {
            MainForm mainForm = new MainForm();
            Program program = new Program(mainForm);
            program.startDetection(imageInitiale);
            Bitmap imageAvecEdgeDetection = program.getImage();

            for (int i = 0; i < imageAvecEdgeDetection.Width; i++)
            {
                for (int j = 0; j < imageAvecEdgeDetection.Height; j++)
                {
                    Color couleurPixelSouhaite = imageAvecLaplacian5x5.GetPixel(i, j);
                    Color couleurPixelTest = imageAvecEdgeDetection.GetPixel(i, j);

                    Assert.AreEqual(couleurPixelSouhaite, couleurPixelTest);
                }

            }

        }

        [TestMethod]
        public void testLoadNullImage()
        {

            Bitmap nullimg = null;
            ilb.loadImage("").Returns<Bitmap>(nullimg);
            program.load("fakeFilename");

        }

        [TestMethod]
        public void testLoadValidImage()
        {
            Bitmap imageTest = Resource.BunnyLandscape.;
            String fname = Path.GetFileName(Resource.BunnyLandscape.ToString());
            ilb.loadImage("").Returns<Bitmap>(nullimg);
            program.load("jhkljh");

        }


    }
}
