using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Drawing;
using NSubstitute;
using PracticalLab;
using PracticalLab.BLL;
using PracticalLab.UI;
using System.IO;
using System;
using System.Reflection;

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
        IEdgeDetection ied = Substitute.For<IEdgeDetection>();

        
        /**-------------------------
        *
        * LOAD FUNCTIONNALITY TESTS
        *
        --------------------------**/

        [TestMethod]
        public void testLoadNullImage()
        {

            Bitmap nullimg = null;
            ilb.loadImage("").Returns<Bitmap>(nullimg);
            program.load("fakeFilename");

        }


        [TestMethod]
        public void testLoadEmptyFilename()
        {
            string str = "";
            program.load(str);
        }


        [TestMethod]
        public void testLoadValidImage()
        {

            string str = "C:\\Users\\Michel\\Documents\\GitHub\\tp-cc\\TP-CC\\PracticalLabTest\\Resources\\BunnyLandscape.png";

            program.load(str);

            Bitmap temp = program.getImage();

            for(int x = 0; x < temp.Height; x++)
            {
                for (int y = 0; y < temp.Width; y++) {
                    Assert.AreEqual(imageInitiale.GetPixel(y, x), temp.GetPixel(y, x));
                }
            }

        }

        [TestMethod]
        public void testLoadRetException()
        {
            ilb.When(x => x.loadImage("")).Do(x => { throw new Exception(); });
            program.load("fakename");

        }


        /**-------------------------
        *
        * FILTERS FUNCTIONNALITY TESTS
        *
        --------------------------**/

        [TestMethod]
        public void TestLaplacian5x5ImageNull()
        {
        
            program.applyDetection(null);
            Bitmap imageAvecEdgeDetection = program.getImage();
        }

        [TestMethod]
        public void TestLaplacian5x5CompareImagesWithParameter()
        {
            program.applyDetection(imageInitiale);
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
        public void TestLaplacian5x5CompareImagesWithNullParameter()
        {
            program.setImage(imageInitiale);
            program.applyDetection(null);
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
        public void TestLaplacian5x5RetException()
        {
            ied.When(x => x.startDetection(null)).Do(x => { throw new Exception(); });
            program.applyDetection(null);
        }


        /**-------------------------
        *
        * SHOW RESULT FUNCTIONNALITY TESTS
        *
        --------------------------**/



    }
}
