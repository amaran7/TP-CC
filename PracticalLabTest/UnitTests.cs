using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Drawing;
using NSubstitute;
using PracticalLab;
using PracticalLab.BLL;
using PracticalLab.UI;
using PracticalLab.DLL;
using System.IO;
using System;
using System.Reflection;
using System.Drawing.Imaging;

namespace PracticalLabTest
{
    [TestClass]
    public class UnitTests
    {
        static MainForm mainForm = new MainForm();
        Program program = new Program(mainForm);

        // Image de départ
        Bitmap imageInitiale = Resource.BunnyLandscape;
        Bitmap imageBmp = Resource.landscapeNature;
        Bitmap imageJPG = Resource.HortonLandscape;
        Bitmap imageAvecLaplacian5x5 = Resource.BunnyLandscapeWithLaplacian5x5;
        
        IEdgeDetection ied = Substitute.For<IEdgeDetection>();
        IDLLManagerBehaviour idllmb = Substitute.For<IDLLManagerBehaviour>();
        IUIManipulation iuim = Substitute.For<IUIManipulation>();

        
        /**-------------------------
        *
        * LOAD FUNCTIONNALITY TESTS
        *
        --------------------------**/

        [TestMethod]
        public void testLoadNullImage()
        {

            Bitmap nullImg = null;
            idllmb.loadFromDisk("fakeFilename").Returns<Bitmap>(nullImg);
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
            idllmb.When(x => x.loadFromDisk("fakename")).Do(x => { throw new Exception(); });
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

        [TestMethod]
        public void TestShowResult()
        {
            ied.When(x => x.startDetection(null)).Do(x => { });
            ied.getImage().Returns<Bitmap>(imageAvecLaplacian5x5);

            program.applyDetection(null);

        }

        [TestMethod]
        public void TestShowResultRetException()
        {
            ied.When(x => x.startDetection(null)).Do(x => { });
            ied.When(x => x.getImage()).Do(x => { throw new Exception(); });

            program.applyDetection(null);

        }

        /**-------------------------
        *
        * SAVE FUNCTIONNALITY TESTS
        *
        --------------------------**/
        [TestMethod]
        public void TestSaveBMP()
        {
            String tempfilename = "C:\\Users\\Michel\\Documents\\GitHub\\tp-cc\\TP-CC\\PracticalLabTest\\Resources\\test.bmp";
            ImageFormat ifa = new ImageFormat(new Guid());
            ifa = ImageFormat.Png;


            program.save(imageBmp, tempfilename, ifa);


        }
        
        [TestMethod]
        public void TestSaveImgRetException()
        {
            
            idllmb.When(x => x.saveToFile(Arg.Any<Bitmap>(), Arg.Any<String>(), Arg.Any<ImageFormat>())).Do(x => { throw new Exception(); });
            program.setImage(imageInitiale);
            program.save(imageAvecLaplacian5x5, "fakeFilename", null);

        }




    }
}
