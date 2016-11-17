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
        // Programme de base (BLL)
        static MainForm mainForm = new MainForm();
        Program program = new Program(mainForm);

        // Image de départ
        Bitmap imageInitiale = Resource.BunnyLandscape;
        Bitmap imageBmp = Resource.landscapeNature;
        Bitmap imageJPG = Resource.HortonLandscape;
        Bitmap imageAvecLaplacian5x5 = Resource.BunnyLandscapeWithLaplacian5x5;
        
        // Substitution des interfaces
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
            idllmb.setProgram(program);
            program.initDLLManager(idllmb);
            idllmb.loadFromDisk(Arg.Any<String>()).Returns<Bitmap>(nullImg);
            program.load("fakeFilename");
            Assert.AreEqual(nullImg, null);

        }


        [TestMethod]
        public void testLoadEmptyFilename()
        {
            string str = "";
            try
            {
                program.load(str);
                Assert.IsTrue(true);
            }
            catch (Exception e)
            {
                Assert.Fail("Program failure with empty filename image to load");
            }
        }

        [TestMethod]
        public void testLoadFakeFilename()
        {
            string str = "fakefilename";
            try
            {
                program.load(str);
                Assert.IsTrue(true);
            }catch(Exception e)
            {
                Assert.Fail("Program failure with fake filename image to load");
            }
            


        }

        [TestMethod]
        public void testLoadValidImage()
        {
            // chemin permettant d'accéder à l'image se trouvant dans le dossier de ressources
            string str = "..\\..\\Resources\\BunnyLandscape.png";

            program.load(str);

            Bitmap temp = program.getImage();

            // test permettant de savoir si les 2 images sont identiques
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
            idllmb.setProgram(program);
            program.initDLLManager(idllmb);
            idllmb.When(x => x.loadFromDisk(Arg.Any<String>())).Do(x => { throw new Exception(); });
            
            try
            {
                program.load("fakename");
                Assert.IsTrue(true);
            }
            catch(Exception e)
            {
                Assert.Fail("Program fail after load image");
            }

        }


        /**-------------------------
        *
        * FILTERS FUNCTIONNALITY TESTS
        *
        --------------------------**/

        [TestMethod]
        public void TestLaplacian5x5ImageNull()
        {
            program.setImage(imageInitiale);
            program.applyDetection(null);
            Bitmap imageAvecEdgeDetection = program.getImage();

            // test si les 2 images sont identiques pixel par pixel
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
        public void TestLaplacian5x5CompareImagesWithParameter()
        {
            program.applyDetection(imageInitiale);
            Bitmap imageAvecEdgeDetection = program.getImage();

            // test si les 2 images sont identiques pixel par pixel
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

            // test si les 2 images sont identiques pixel par pixel
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
            ied.When(x => x.startDetection(Arg.Any<Bitmap>())).Do(x => { throw new Exception(); });
            program.initEdgeDetection(ied);
            program.setImage(imageInitiale);
            
            try
            {
                program.applyDetection(null);
            }
            catch (Exception)
            {
                Assert.Fail("Test Failed by Buisiness layer");
            }


        }


        /**-------------------------
        *
        * SHOW RESULT FUNCTIONNALITY TESTS
        *
        --------------------------**/

        [TestMethod]
        public void TestShowResult()
        {
            ied.When(x => x.startDetection(Arg.Any<Bitmap>())).Do(x => { });
            ied.getImage().Returns<Bitmap>(imageAvecLaplacian5x5);

            program.applyDetection(null);

        }

        [TestMethod]
        public void TestShowResultRetException()
        {
            program.setMainForm(iuim);
            iuim.When(x => x.display(Arg.Any<Bitmap>(), Arg.Any<String>())).Do(x => { throw new Exception(); });
            try
            {
                program.applyResult(imageInitiale, "fakestate");
                Assert.IsTrue(true);
            }catch(Exception e)
            {
                Assert.Fail("Buisness does not process error after display try");
            }
            

        }

        /**-------------------------
        *
        * SAVE FUNCTIONNALITY TESTS
        *
        --------------------------**/
        [TestMethod]
        public void TestSaveBMP()
        {
            // chemin d'accès permettant de sauvegarder une image nommée test.bmp dans le dossier de ressources
            String tempfilename = "..\\..\\Resources\\test.bmp";
            ImageFormat ifa = new ImageFormat(new Guid());
            ifa = ImageFormat.Png;


            program.save(imageBmp, tempfilename, ifa);


        }
        
        [TestMethod]
        public void TestSaveImgRetException()
        {
            program.initDLLManager(idllmb);
            idllmb.When(x => x.saveToFile(Arg.Any<Bitmap>(), Arg.Any<String>(), Arg.Any<ImageFormat>())).Do(x => { throw new Exception(); });

            program.setImage(imageInitiale);
            try {
                program.save(imageAvecLaplacian5x5, "fakeFilename", null);
                Assert.IsTrue(true);
            }catch(Exception e)
            {
                Assert.Fail("Buisness does not process error from DLL saving exception");
            }
        }




    }
}
